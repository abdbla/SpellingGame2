using NUnit.Framework;
using SpellingGame2;
using System.Collections.Generic;

namespace SpellingUnitTest
{
    public class SpellRecipeTests
    {
        [Test]
        public void SerializationTest() {
            //setup
            SpellRecipe spellRecipe = new SpellRecipe(new List<(Aspect, int)>() { (Aspect.Aer, 3), (Aspect.Ordo, 1) }, new List<(Practice, Lore)>() { (Practice.Knowing, Lore.LorePrime) }, SpellRecipeID.TestRecipe);
            Dictionary<SpellRecipeID, SpellRecipe> SpellRecipes = new Dictionary<SpellRecipeID, SpellRecipe>();
            SpellRecipes.Add(SpellRecipeID.TestRecipe, spellRecipe);
            SpellRecipeXmlHandler.SpellRecipesSerialize(SpellRecipes);

            //test
            var deserializedRecipes = SpellRecipeXmlHandler.SpellRecipesDeserialize();

            //assert
            Assert.AreEqual(deserializedRecipes[SpellRecipeID.TestRecipe], spellRecipe);
        }

        [Test]
        public void ExtensionTest() {
            //setup
            Aspect aspect = Aspect.Mors;
            Lore lore = Lore.LoreDeath;

            //test
            string sLore = lore.LoreToString();
            Rarity rAspect = aspect.AspectRarity();

            //assert
            Assert.AreEqual(rAspect, Rarity.Complex);
            Assert.AreEqual(sLore, "The Principles of the World Below");
        }
    }
}