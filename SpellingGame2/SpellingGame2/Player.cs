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
        public int money = 0;
        public int actions = 4;
        public int stamina = 100;

        public Player() {
            essentia = new Dictionary<Aspect, int>();
            lore = new Dictionary<Lore, int>();
            objects = new List<ObjectID>();
            knownRituals = new List<SpellRecipeID>();
        }

        public void RestoreStats(object sender, DayEndEventArgs e) {
            stamina = 100;
            actions = 4;
        }

        public void GenerateCommissions() {
            commissions.Clear();
            for (int i = 0; i < rng.Next(1, 4); i++) {
                commissions.Add((CommissionID)rng.Next(0, CommissionExtensions.Count()));
            }
        }

        public void GenerateCommissions(object sender, DayEndEventArgs e) {
            commissions.Clear();
            for (int i = 0; i < rng.Next(1, 4); i++) {
                commissions.Add((CommissionID)rng.Next(0, CommissionExtensions.Count()));
            }
        }
    }
}
