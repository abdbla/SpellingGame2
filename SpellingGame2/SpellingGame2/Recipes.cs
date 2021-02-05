using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SpellingGame2
{
    public static class RecipeXmlHandler
    {
        static public Dictionary<RecipeID, Recipe> RecipesDeserialize() {
            XmlSerializer serializer = new XmlSerializer(typeof(Recipe));
            Dictionary<RecipeID, Recipe> recipes = new Dictionary<RecipeID, Recipe>();
            foreach (var item in Directory.GetFiles(@"..\..\..\..\recipes\")) {
                using (FileStream input = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read)) {
                    Recipe tmp = (Recipe)serializer.Deserialize(input);
                    recipes.Add(tmp.id, tmp);
                }
            }
            return recipes;
        }
        static public void RecipesSerialize(Dictionary<RecipeID,Recipe> recipes) {
            XmlSerializer serializer = new XmlSerializer(typeof(Recipe));
            foreach (var item in recipes) {
                StringBuilder path = new StringBuilder(@"..\..\..\..\recipes\");
                path.Append(item.Key.ToString());
                path.Append(".xml");
                using (FileStream output = new FileStream(path.ToString(), FileMode.OpenOrCreate, FileAccess.Write)) {
                    serializer.Serialize(output, item.Value);
                }
            }
        }
    }

    [Serializable]
    public struct Recipe
    {
        public List<Aspect> aspects;
        public List<Practice> practices;
        public RecipeID id;

        public Recipe(List<Aspect> _aspects, List<Practice> _practices, RecipeID _id) {
            aspects = _aspects;
            practices = _practices;
            id = _id;
        }

        public override bool Equals(object obj) {
            if (obj.GetType() == typeof(Recipe)) {
                return ((Recipe)obj).id == this.id;
            }
            return false;
        }
    }

    [Serializable]
    public enum RecipeID
    {
        SeeTheWinds,
        TestRecipe
    }

    [Serializable]
    public enum Practice
    {
        Compelling,
        Knowing,
        Unveiling,
        Ruling,
        Shielding,
        Veiling,
        Fraying,
        Perfecting,
        Weaving,
        Patterning,
        Unraveling,
        Making,
        Unmaking
    }

    [Serializable]
    public enum Aspect
    {
        Aer,
        Aqua,
        Ignis,
        Terra,
        Ordo,
        Perditio,
    }
}
