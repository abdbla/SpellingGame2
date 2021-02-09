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

    static public Dictionary<ExpeditionID, Func<IUserInterface, Player, List<Object>>> GetExpeditions() {
        Dictionary<ExpeditionID, Func<IUserInterface, Player, List<Object>>> expeditions = new Dictionary<ExpeditionID, Func<IUserInterface, Player, List<Object>>>();
        expeditions.Add(ExpeditionID.Garden,
            delegate (IUserInterface ui, Player p) {
                if (p.actions < 1) { ui.WriteIntoDescription("You're too tired to go to the store today.", true); return new List<Object>() { }; }
                if (p.money < 10) { ui.WriteIntoDescription("You, sadly, do not have enough money to buy anything of use", true) return new List<Object>() { }; }
                p.actions--;
                p.money -= 10;

                List<Object> rewards = new List<Object>();
            });
    }
}
