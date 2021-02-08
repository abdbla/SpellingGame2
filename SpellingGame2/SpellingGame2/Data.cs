using System;
using System.Collections.Generic;
using System.Text;

namespace SpellingGame2
{
    public class Data
    {
        public Dictionary<SpellRecipeID, SpellRecipe> recipes;
        public Dictionary<ObjectID, Object> objects;
        public Dictionary<CommissionID, Commission> commissions;

        public Data() {
            recipes = SpellRecipeXmlHandler.SpellRecipesDeserialize();
            objects = ObjectXmlHandler.ObjectsDeserialize();
            commissions = CommissionXmlHandler.CommissionsDeserialize();
        }
    }
}
