using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace SpellingGame2
{
    static class Research
    {
        static Dictionary<ObjectID, Action<IUserInterface, Player, Engine>> GetResearch() {
            Dictionary<ObjectID, Action<IUserInterface, Player, Engine>> research = new Dictionary<ObjectID, Action<IUserInterface, Player, Engine>>();
            research.Add(ObjectID.UnceasingTop, delegate (IUserInterface ui, Player p, Engine e)
            {

            });

            return research;
        }
    }
}
