using System;
using System.Collections.Generic;
using System.Text;
using static SpellingGame2.Engine;

namespace SpellingGame2
{
    public class Player
    {
        public Dictionary<Aspect, int> essentia;
        public Dictionary<Lore, int> lore;
        public List<ObjectID> objects;
        public List<SpellRecipeID> knownRituals;
        public List<CommissionID> commissions;
        public List<StatusID> statuses;
        public Dictionary<ObjectID, bool> researchedObjects;
        public EventHandler useAction;
        public int money = 0;
        public int actions = 4;
        public int stamina = 100;

        public Player() {
            essentia = new Dictionary<Aspect, int>();
            lore = new Dictionary<Lore, int>();
            objects = new List<ObjectID>();
            researchedObjects = new Dictionary<ObjectID, bool>();
            statuses = new List<StatusID>();
            foreach (var item in Enum.GetValues(ObjectID.TestObject.GetType())) {
                researchedObjects.Add((ObjectID)item, false);
            }
            knownRituals = new List<SpellRecipeID>();
            commissions = new List<CommissionID>();
            useAction += delegate (object sender, EventArgs args) { actions--; };
        }

        public void RestoreStats(object sender, DayEndEventArgs e) {
            stamina = 100;
            actions = 4;
        }

        public void GenerateCommissions() {
            commissions.Clear();
            for (int i = 0; i < rng.Next(1, 4); i++) {
                commissions.Add((CommissionID)rng.Next(0, CommissionExtensions.Count() - 1));
            }
        }

        public void GenerateCommissions(object sender, DayEndEventArgs e) {
            GenerateCommissions();
        }
    }

    public enum StatusID
    {
        ArcaneMind,
        SupernalEyes,
        MinorLuck,
        ReadTheFlesh,
        NaturalHealing,
    }

    public static class StatusExtensions
    {
        public static string StatusDesc(this StatusID id)
        {
            switch (id)
            {
                case StatusID.ArcaneMind:
                    return "Your mind is like glass, a prism, thoughts as light, trapped within.";
                case StatusID.SupernalEyes:
                    return "You see the essentia that lies behind all things.";
                case StatusID.MinorLuck:
                    return "You feel a little lucky.";
                case StatusID.ReadTheFlesh:
                    return "Living things unveil their conditions to you, like words on paper.";
                case StatusID.NaturalHealing:
                    return "Your flesh squirms beneath your skin, hyperactive.";
                default:
                    return "An error has occurred.";
            }
        }

        public static string StatusName(this StatusID id)
        {
            switch (id)
            {
                case StatusID.ArcaneMind:
                    return "Arcane Mind";
                case StatusID.SupernalEyes:
                    return "Supernal Eyes";
                case StatusID.MinorLuck:
                    return "Minor Luck";
                case StatusID.ReadTheFlesh:
                    return "Read the Flesh";
                default:
                    return "An error has occurred.";
            }
        }
    }
}
