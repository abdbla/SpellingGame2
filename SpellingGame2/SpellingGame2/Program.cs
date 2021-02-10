using System;
using System.Collections.Generic;

namespace SpellingGame2
{
    class Program
    {
        static IUserInterface userInterface = new UI();
        static Engine engine = new Engine();
        static Player player = new Player();
        static Data data = new Data();
        static void Main(string[] args) { //start of game setup
            engine.dayEnd += player.RestoreStats;
            player.knownRituals.AddRange(new List<SpellRecipeID>() { SpellRecipeID.MinorLuck, SpellRecipeID.NaturalHealing, SpellRecipeID.ReadTheFlesh });
            player.money += 200;
            userInterface.OptionSelected += delegate (object sender, InterfaceEventArgs e) { };
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

        }

        static void ResearchMenu() {

        }

        static void CommissionMenu() {

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

        }

        static void BedroomMenu() {

        }

        static void PlayerMenu() {

        }
    }
}
