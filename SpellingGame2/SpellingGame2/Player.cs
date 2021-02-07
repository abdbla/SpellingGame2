using System;
using System.Collections.Generic;
using System.Text;

namespace SpellingGame2
{
    class Player
    {
        Dictionary<Aspect, int> essentia;
        Dictionary<Lore, int> lore;
        List<ObjectID> objects;
        List<SpellRecipeID> knownRituals;
        int gilt;
        int stamina;
    }
}
