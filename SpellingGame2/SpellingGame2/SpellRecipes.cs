using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SpellingGame2
{
    public static class SpellRecipeXmlHandler
    {
        static public Dictionary<SpellRecipeID, SpellRecipe> SpellRecipesDeserialize() {
            XmlSerializer serializer = new XmlSerializer(typeof(SpellRecipe));
            Dictionary<SpellRecipeID, SpellRecipe> SpellRecipes = new Dictionary<SpellRecipeID, SpellRecipe>();
            foreach (var item in Directory.GetFiles(@"..\..\..\..\recipes\")) {
                using (FileStream input = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read)) {
                    SpellRecipe tmp = (SpellRecipe)serializer.Deserialize(input);
                    SpellRecipes.Add(tmp.id, tmp);
                }
            }
            return SpellRecipes;
        }
        static public void SpellRecipesSerialize(Dictionary<SpellRecipeID, SpellRecipe> SpellRecipes) {
            XmlSerializer serializer = new XmlSerializer(typeof(SpellRecipe));
            foreach (var item in SpellRecipes) {
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
    public struct SpellRecipe
    {
        public List<Aspect> aspects;
        public List<Practice> practices;
        public SpellRecipeID id;

        public SpellRecipe(List<Aspect> _aspects, List<Practice> _practices, SpellRecipeID _id) {
            aspects = _aspects;
            practices = _practices;
            id = _id;
        }

        public override bool Equals(object obj) {
            if (obj.GetType() == typeof(SpellRecipe)) {
                return ((SpellRecipe)obj).id == this.id;
            }
            return false;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return base.ToString();
        }
    }

    [Serializable]
    public enum SpellRecipeID
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
