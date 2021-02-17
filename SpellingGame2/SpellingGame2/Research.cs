using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace SpellingGame2
{
    static class Research
    {
        static Dictionary<ObjectID, Action<IUserInterface, Player>> GetResearch() {
            Dictionary<ObjectID, Action<IUserInterface, Player>> research = new Dictionary<ObjectID, Action<IUserInterface, Player>>();
            research.Add(ObjectID.UnceasingTop, delegate (IUserInterface ui, Player p)
            {

            });

            return research;
        }
    }
}
