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
            userInterface.OptionSelected += delegate (object sender, InterfaceEventArgs e) { }; // prevents crashes lol
            TowerMenu(userInterface);
        }

        static void TowerMenu(IUserInterface userInterface) {
            userInterface.ClearDescription();
            List<string> options = new List<string>() { "Expeditions", "Research", "Commissions", "Distillery", "Rituals", "You" };
            List<string> stats = new List<string>() { $"Actions: {player.actions}/4", $"Stamina: {player.stamina}/100", $"£{player.money}.00" };
            userInterface.SetOptions("towerMenu", options);
            userInterface.SetStatus(stats);
            userInterface.WriteIntoDescription("You are in your tower.", 2);
            while (true) {
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
                    case "you":
                        PlayerMenu();
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

        }

        static void RitualMenu() {

        }

        static void PlayerMenu() {

        }
    }
}
