using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SpellingGame2
{
    public enum ExpeditionID
    {
        Garden,
        Store,
        AbandonedMine,
        TestExpedition,
    }
    static class Expeditions
    {
        static public Dictionary<ExpeditionID, Func<IUserInterface, Player, List<ObjectID>>> GetExpeditions() {
            Dictionary<ExpeditionID, Func<IUserInterface, Player, List<ObjectID>>> expeditions = new Dictionary<ExpeditionID, Func<IUserInterface, Player, List<ObjectID>>>();
            expeditions.Add(ExpeditionID.Garden,
                delegate (IUserInterface ui, Player p) {
                    if (p.actions < 2) { ui.WriteIntoDescription("It's too late in the day to tend to the garden.", 2); return new List<ObjectID>() { }; }
                    p.actions -= 2;
                    ui.WriteIntoDescription("You tend to your simple garden, harvesting whatever plants have bloomed since last time, replanting the ones that have and watering the rest.", 2);

                    List<ObjectID> rewards = new List<ObjectID>();
                    for (int i = 0; i < Engine.rng.Next(0, 4); i++) {
                        rewards.Add(ObjectID.Basil);
                        ui.WriteIntoDescription("You have gained Basil!", ConsoleColor.Green, ConsoleColor.Black, 1);
                    }
                    if (Engine.rng.NextDouble() < 0.7d) {
                        rewards.Add(ObjectID.Datura);
                        ui.WriteIntoDescription("You have gained Datura!", ConsoleColor.Green, ConsoleColor.Black, 1);
                    }
                    if (Engine.rng.NextDouble() < 0.7d) {
                        rewards.Add(ObjectID.Marijuana);
                        ui.WriteIntoDescription("You have gained Marijuana!", ConsoleColor.Green, ConsoleColor.Black, 1);
                    }
                    return rewards;
                });

            expeditions.Add(ExpeditionID.Store,
                delegate (IUserInterface ui, Player p) {
                    if (p.actions < 1) { ui.WriteIntoDescription("You're too tired to go to the store, especially not at this time of day.", 2); }
                    if (p.money < 10) { ui.WriteIntoDescription("You're practically broke. You wouldn't be able to get anything useful at the store, anyway.", 2); }

                    p.actions--;
                    p.money -= 10;

                    List<ObjectID> rewards = new List<ObjectID>();
                    ui.WriteIntoDescription("Heading to the store you pick up any useful items you can spot.", 0);
                    if (Engine.rng.Next(0, 3) == 2) {
                        ui.WriteIntoDescription(" Several shelves of distilled water, which is always useful for some basic distilling, ironically enough.", ConsoleColor.Green, ConsoleColor.Black, 0);
                        rewards.Add(ObjectID.DistilledWater);
                    } else {
                        ui.WriteIntoDescription(" Some distilled water, which is always useful for some basic distilling, ironically enough.", ConsoleColor.Green, ConsoleColor.Black, 0);
                    }
                    rewards.Add(ObjectID.DistilledWater);
                    ui.WriteIntoDescription(" Continuing on to the less obvious ingredients...", 0);
                    if (Engine.rng.NextDouble() < 0.7) {
                        ui.WriteIntoDescription(" You find some bottles of high-concentration vinegar. You take them,", 0);
                        rewards.Add(ObjectID.Vinegar);
                    } else {
                        ui.WriteIntoDescription(" You sadly find the shelves deserted of anything useful. Maybe next time.", 0);
                    }
                    if (Engine.rng.NextDouble() < 0.3) {
                        ui.WriteIntoDescription(" Finally, at the counter, you spot ", 0);
                        ui.WriteIntoDescription("A pair of double-D batteries, not often stocked here. Unusually potent in rituals.", ConsoleColor.Green, ConsoleColor.Black, 0);
                        rewards.Add(ObjectID.Batteries);
                    }
                    return rewards;
                });

            return expeditions;
        }
    }
}
