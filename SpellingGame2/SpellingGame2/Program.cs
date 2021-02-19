using System;
using System.Collections.Generic;
using System.Globalization;

namespace SpellingGame2
{
    class Program
    {
        static IUserInterface userInterface = new UI();
        static Engine engine = new Engine();
        static Player player = new Player();
        static Data data = new Data();
        static void Main(string[] args) { //start of game setup
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            engine.dayEnd += player.RestoreStats;
            player.knownRituals.AddRange(new List<SpellRecipeID>() { SpellRecipeID.MinorLuck, SpellRecipeID.NaturalHealing, SpellRecipeID.ReadTheFlesh });
            player.money += 200;
            player.statuses.Add(StatusID.ArcaneMind);
            userInterface.OptionSelected += delegate (object sender, InterfaceEventArgs e) { };
            engine.dayEnd += player.GenerateCommissions;
            player.GenerateCommissions();
            player.essentia.Add(Aspect.Praecantatio, 15);
            player.objects.Add(ObjectID.Basil);
            player.objects.Add(ObjectID.Basil);
            player.objects.Add(ObjectID.Basil);
            player.objects.Add(ObjectID.Lead);
            player.objects.Add(ObjectID.Silver);
            player.objects.Add(ObjectID.Vinegar);
            ShopMenu(userInterface);
        }

        static void ShopMenu(IUserInterface userInterface) {
            while (true) {
                userInterface.ClearDescription();
                List<string> options = new List<string>() { "Expeditions", "Research", "Commissions", "Distillery", "Rituals", "Bedroom", "You", "Exit" };
                List<string> stats = new List<string>() { $"Actions: {player.actions}/4", $"Stamina: {player.stamina}/100", $"{player.money:C}" };
                userInterface.SetOptions("towerMenu", options);
                userInterface.ChangeTitle("The Bookstore Backrooms");
                userInterface.SetStatus(stats);
                userInterface.WriteIntoDescription("You are in the backrooms of the bookstore, the bustle of customers audible from outside.", 2);
                switch (userInterface.GetInput().ToLower()) {
                    case "expeditions":
                        ExpeditionMenu();
                        break;
                    case "research":
                        ResearchMenu();
                        break;
                    case "commissions":
                        CommissionMenu();
                        break;
                    case "distillery":
                        DistillationMenu();
                        break;
                    case "rituals":
                        RitualMenu();
                        break;
                    case "bedroom":
                        BedroomMenu();
                        break;
                    case "you":
                        PlayerMenu();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void ExpeditionMenu() {
            userInterface.ChangeTitle("The Planning Room");
            List<string> options = new List<string>();
            foreach (var item in Expeditions.GetExpeditions()) {
                options.Add(item.Key.ToString());
            }
            options.Add("Leave");
            userInterface.SetOptions("expeditions", options);
            userInterface.SetStatus(new List<string>() { });
            userInterface.WriteIntoDescription("The planning room greets you, notes detailing locations where you believe you could obtain magically significant materials.", 2);
            while (true) {
                try {
                    foreach (var item in Expeditions.GetExpeditions()[Enum.Parse<ExpeditionID>(userInterface.GetInput())](userInterface, player)) {
                        player.objects.Add(item);
                    }
                } catch {
                    return;
                }
            }
        }

        static void ResearchMenu() {

        }

        static void CommissionMenu() {
            userInterface.ChangeTitle("The Bookstore Counter");
            userInterface.SetOptions("commission", new List<string>() { "Accept", "Decline", "Go Back" });
            List<string> statuses = new List<string>();
            foreach (var item in player.essentia) {
                statuses.Add(item.Key + ": " + item.Value);
            }
            userInterface.SetStatus(statuses);
            while (true) {
                if (player.commissions.Count == 0) {
                    userInterface.WriteIntoDescription("There are no more commissions, today.", 2);
                    userInterface.SetOptions("commission", new List<string>() { "Go Back" });
                    switch (userInterface.GetInput().ToLower()) {
                        case "go back":
                            return;
                    }
                } else {
                    userInterface.WriteIntoDescription("A man walks up to the counter.", 1);
                    userInterface.WriteIntoDescription(data.commissions[player.commissions[0]].desc, ConsoleColor.Blue, ConsoleColor.Black, 2);
                    switch (userInterface.GetInput().ToLower()) {
                        case "accept":
                            bool fulfilledRequirements = true;
                            switch (data.commissions[player.commissions[0]].type) {
                                case CommissionType.Essentia:
                                    foreach (var item in data.commissions[player.commissions[0]].requiredEssentia) {
                                        if (!player.essentia.TryGetValue(item.Item1, out _) || player.essentia[item.Item1] < item.Item2) {
                                            fulfilledRequirements = false;
                                        }
                                    }
                                    if (fulfilledRequirements) {
                                        userInterface.WriteIntoDescription("Ah, thank you.", 2);
                                        foreach (var item in data.commissions[player.commissions[0]].requiredEssentia) {
                                            player.essentia[item.Item1] -= item.Item2;
                                            userInterface.WriteIntoDescription("You have lost " + item.Item2 + " " + item.Item1, ConsoleColor.Red, ConsoleColor.Black, 1);
                                        }
                                        userInterface.WriteIntoDescription($"You have gained {data.commissions[player.commissions[0]].moneyReward:C}", ConsoleColor.Green, ConsoleColor.Black, 1);
                                        foreach (var item in data.commissions[player.commissions[0]].objectsReward) {
                                            player.objects.Add(item);
                                            userInterface.WriteIntoDescription($"You have gained {item}", ConsoleColor.Green, ConsoleColor.Black, 1);
                                        }
                                        player.commissions.RemoveAt(0);
                                    } else {
                                        userInterface.WriteIntoDescription("You don't have the Essentia to fulfill this request.", 2);
                                    }
                                    break;
                                case CommissionType.Ritual:
                                    foreach (var item in data.recipes[data.commissions[player.commissions[0]].requiredRitual].aspects) {
                                        if (!player.essentia.TryGetValue(item.Item1, out _) || player.essentia[item.Item1] < item.Item2) {
                                            fulfilledRequirements = false;
                                        }
                                    }
                                    if (!player.knownRituals.Contains(data.commissions[player.commissions[0]].requiredRitual)) fulfilledRequirements = false;
                                    if (fulfilledRequirements) {
                                        userInterface.WriteIntoDescription("Ah, thank you.", 2);
                                        foreach (var item in data.recipes[data.commissions[player.commissions[0]].requiredRitual].aspects) {
                                            player.essentia[item.Item1] -= item.Item2;
                                            userInterface.WriteIntoDescription("You have lost " + item.Item2 + " " + item.Item1, ConsoleColor.Red, ConsoleColor.Black, 1);
                                        }
                                        userInterface.WriteIntoDescription($"You have gained {data.commissions[player.commissions[0]].moneyReward:C}", ConsoleColor.Green, ConsoleColor.Black, 1);
                                        foreach (var item in data.commissions[player.commissions[0]].objectsReward) {
                                            player.objects.Add(item);
                                            userInterface.WriteIntoDescription($"You have gained {item}", ConsoleColor.Green, ConsoleColor.Black, 1);
                                        }
                                        player.commissions.RemoveAt(0);
                                    } else {
                                        if (!player.knownRituals.Contains(data.commissions[player.commissions[0]].requiredRitual)) {
                                            userInterface.WriteIntoDescription("You don't know any ritual that fulfills this request.", 2);
                                        } else {
                                            userInterface.WriteIntoDescription("You don't have the Essentia to fulfill this request.", 2);
                                        }
                                    }
                                    break;
                                case CommissionType.Treatise:
                                    break;
                            }
                            break;
                        case "decline":
                            userInterface.WriteIntoDescription("Ah, I see. Well, in that case, I wish you good luck.", ConsoleColor.Blue, ConsoleColor.Black, 2);
                            player.commissions.RemoveAt(0);
                            break;
                        case "go back":
                            return;
                    }
                }
            }
        }

        static void DistillationMenu() {
            userInterface.WriteIntoDescription("You enter the basement room, a large alembic distillery connected to the surrounding pipework, complicated machinery leading to magically-reinforced jars. Essentia containers.", 2);
            userInterface.ChangeTitle("The Alembic Distillery");
            foreach (var item in player.essentia) {
                userInterface.WriteIntoDescription(item.Key + ": " + item.Value, 1);
            }
            while (true) {
                List<string> options = new List<string>();
                List<string> statuses = new List<string>();
                foreach (var item in player.objects) {
                    options.Add("Distill " + item.ToString());
                    statuses.Add(item.ToString() + ": " + data.objects[item].desc);
                }
                options.Add("Cancel");
                if (options.Count == 1) statuses.Add("You have no sacraments to distill.");
                userInterface.SetOptions("distilling", options);
                userInterface.SetStatus(statuses);
                string choice = userInterface.GetInput().ToLower();
                if (choice == "cancel") return;
                userInterface.WriteIntoDescription(choice.Split(' ')[1].ToObject() + " was distilled into...", 2);
                foreach (var item in data.objects[choice.Split(' ')[1].ToObject()].containedEssentia) {
                    if (player.essentia.TryGetValue(item.Item1, out _)) { player.essentia[item.Item1] += item.Item2; } 
                    else { player.essentia.Add(item.Item1, item.Item2); }
                    userInterface.WriteIntoDescription(item.Item2 + " " + item.Item1, ConsoleColor.Green, ConsoleColor.Black, 1);
                }
                userInterface.WriteIntoDescription("", 1);
                player.objects.Remove(choice.Split(' ')[1].ToObject());
            }
        }

        static void RitualMenu() {
            userInterface.ChangeTitle("The Adytum");
            List<string> statuses = new List<string>();
            foreach (var item in player.knownRituals) {
                string temp = item.ToName() + ": " + data.recipes[item].desc;
                statuses.Add(temp);
            }
            userInterface.SetStatus(statuses);
            List<string> options = new List<string>();
            foreach (var item in player.knownRituals) {
                options.Add(item.ToName());
            }
            userInterface.SetOptions("rituals", options);
        }

        static void BedroomMenu() {
            userInterface.ChangeTitle("The Bedroom Chamber");
            userInterface.SetOptions("bedroom", new List<string>() { "Go to Sleep", "Go back" });
            userInterface.WriteIntoDescription("The bedroom is an unassuming room. Small, a single bed in the corner of the room, scant light shining from the window above. A small nightlamp sits upon the wall adjacent to the bed, currently off.", 2);
            while(true) {
                switch (userInterface.GetInput().ToLower()) {
                    case "go to sleep":
                        engine.dayEnd(null, null);
                        return;
                    case "go back":
                        return;
                }
            }
        }

        static void PlayerMenu() {
            userInterface.ChangeTitle("The Mirror on the Wall");
            userInterface.SetOptions("you", new List<string>() { "Go back" });
            userInterface.WriteIntoDescription("It's you.", 2);
            userInterface.WriteIntoDescription("Effects currently affecting you:", 1);
            foreach (var item in player.statuses)
            {
                userInterface.WriteIntoDescription($"[{item.StatusName()}]: {item.StatusDesc()}", 1);
            }
            userInterface.WriteIntoDescription("", 1);
            userInterface.GetInput();
        }
    }
}
