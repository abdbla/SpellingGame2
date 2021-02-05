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
            Recipe recipe = new Recipe(new List<Aspect>() { Aspect.Aer, Aspect.Ordo }, new List<Practice>() { Practice.Knowing }, RecipeID.TestRecipe);
            Dictionary<RecipeID, Recipe> recipes = new Dictionary<RecipeID, Recipe>();
            recipes.Add(RecipeID.TestRecipe, recipe);
            RecipeXmlHandler.RecipesSerialize(recipes);

            //test
            var deserializedRecipes = RecipeXmlHandler.RecipesDeserialize();

            //assert
            Assert.AreEqual(deserializedRecipes[RecipeID.TestRecipe], recipe);
        }
    }
}