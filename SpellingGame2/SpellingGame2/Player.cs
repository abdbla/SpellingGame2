using System;
using System.Collections.Generic;
using System.Text;
using static SpellingGame2.Engine;

namespace SpellingGame2
{
    class Player
    {
        Dictionary<Aspect, int> essentia;
        Dictionary<Lore, int> lore;
        List<ObjectID> objects;
        List<SpellRecipeID> knownRituals;
        int gilt = 0;
        int actions = 4;
        int stamina = 100;

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
    }
}
