﻿using System;
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
        public List<(Practice, Lore)> practices;
        public SpellRecipeID id;

        public SpellRecipe(List<Aspect> _aspects, List<(Practice, Lore)> _practices, SpellRecipeID _id) {
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
        Compelling = 1,
        Knowing = 1,
        Unveiling = 1,
        Ruling = 2,
        Shielding = 2,
        Veiling = 2,
        Fraying = 3,
        Perfecting = 3,
        Weaving = 3,
        Patterning = 4,
        Unraveling = 4,
        Making = 5,
        Unmaking = 5
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

    [Serializable]
    public enum Lore
    {
        LoreDeath,
        LoreFate,
        LoreForces,
        LoreLife,
        LoreMatter,
        LoreMind,
        LorePrime,
        LoreSpace,
        LoreTime,
    }

    public static class LoreExtension
    {
        public static string LoreToString(this Lore lore) {
            switch (lore) {
                case Lore.LoreDeath:
                    return "The Principles of the World Below";
                case Lore.LoreFate:
                    return "The Principles of the Loom in Heaven";
                case Lore.LoreForces:
                    return "The Principles of the Sun";
                case Lore.LoreLife:
                    return "The Principles of the Earth That Was";
                case Lore.LoreMatter:
                    return "The Principles of the Earth That Is";
                case Lore.LoreMind:
                    return "The Principles of the World Inside";
                case Lore.LorePrime:
                    return "The Prime Principalities";
                case Lore.LoreSpace:
                    return "The Principles of the Weave in Exile";
                case Lore.LoreTime:
                    return "The Principles of the Moon";
                default:
                    return "An Error Has Occurred";
            }
        }
    }
}