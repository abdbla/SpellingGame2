using NUnit.Framework;
using SpellingGame2;
using System.Collections.Generic;

namespace SpellingUnitTest
{
    public class Tests
    {
        [Test]
        public void SerializationTest() {
            //setup
            SpellRecipe spellRecipe = new SpellRecipe(new List<Aspect>() { Aspect.Aer, Aspect.Ordo }, new List<(Practice, Lore)>() { (Practice.Knowing, Lore.LorePrime) }, SpellRecipeID.TestRecipe);
            Dictionary<SpellRecipeID, SpellRecipe> SpellRecipes = new Dictionary<SpellRecipeID, SpellRecipe>();
            SpellRecipes.Add(SpellRecipeID.TestRecipe, spellRecipe);
            SpellRecipeXmlHandler.SpellRecipesSerialize(SpellRecipes);

            //test
            var deserializedRecipes = SpellRecipeXmlHandler.SpellRecipesDeserialize();

            //assert
            Assert.AreEqual(deserializedRecipes[SpellRecipeID.TestRecipe], spellRecipe);
        }
    }
}