using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using static SpellingGame2.Engine;

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
        public List<(Aspect, int)> aspects;
        public List<(Practice, Lore)> practices;
        public string desc;
        public SpellRecipeID id;

        public SpellRecipe(List<(Aspect, int)> _aspects, List<(Practice, Lore)> _practices, SpellRecipeID _id) {
            aspects = _aspects;
            practices = _practices;
            id = _id;
            desc = "default";
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
        MinorLuck,
        ReadTheFlesh,
        SupernalEyes,
        NaturalHealing,
        TestRecipe
    }

    public static class SpellRecipeIDExtensions {
        public static string ToName(this SpellRecipeID id) {
            switch (id) {
                case SpellRecipeID.MinorLuck:
                    return "Minor Blessing of Luck";
                case SpellRecipeID.ReadTheFlesh:
                    return "Read the Flesh";
                case SpellRecipeID.SupernalEyes:
                    return "Supernal Eyes";
                case SpellRecipeID.NaturalHealing:
                    return "Natural Healing";
                default:
                    return "Default Recipe";
            }
        }

        public static SpellRecipeID ToID(this string str) {
            switch (str) {
                case "Minor Blessing of Luck":
                    return SpellRecipeID.MinorLuck;
                case "Read the Flesh":
                    return SpellRecipeID.ReadTheFlesh;
                case "Supernal Eyes":
                    return SpellRecipeID.SupernalEyes;
                case "Natural Healing":
                    return SpellRecipeID.NaturalHealing;
                default:
                    throw new Exception("Not a valid string to convert");
            }

        }
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
        Fulmen,
        Vanus,
        Venenum,
        Gelum,
        Potentia,
        Acidum,
        Vitae,
        Metallum,
        Mors,
        Anima,
        Bestia,
        Praecantatio,
        Herba,
        Alienus,
        Permutatio,
        Vis,
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

    public enum Rarity
    {
        Primal,
        Elemental,
        Complex,
        Intricate,
        Manifold
    }

    public static class Extensions
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

        public static Rarity AspectRarity(this Aspect aspect) {
            switch (aspect) {
                default:
                case Aspect.Aer:
                case Aspect.Aqua:
                case Aspect.Ignis:
                case Aspect.Terra:
                case Aspect.Ordo:
                case Aspect.Perditio:
                    return Rarity.Primal;
                case Aspect.Fulmen:
                case Aspect.Vanus:
                case Aspect.Venenum:
                case Aspect.Gelum:
                case Aspect.Potentia:
                case Aspect.Acidum:
                case Aspect.Vitae:
                case Aspect.Metallum:
                    return Rarity.Elemental;
                case Aspect.Mors:
                case Aspect.Anima:
                case Aspect.Bestia:
                case Aspect.Praecantatio:
                case Aspect.Alienus:
                case Aspect.Permutatio:
                case Aspect.Herba:
                case Aspect.Vis:
                    return Rarity.Complex;
            }
        }
    }
    public static class Effects
    {
        public static Dictionary<SpellRecipeID, Action<IUserInterface, Player, Engine>> GetEffects() {
            Dictionary<SpellRecipeID, Action<IUserInterface, Player, Engine>> effects = new Dictionary<SpellRecipeID, Action<IUserInterface, Player, Engine>>();

            effects.Add(SpellRecipeID.MinorLuck, delegate (IUserInterface ui, Player p, Engine e) {
                ui.WriteIntoDescription("You feel the strings that tie you to the world bend, and stretch towards the sky. Somehow, things are better.", 2);

                p.statuses.Add(StatusID.MinorLuck);
                EventHandler<DayEndEventArgs> action = null;
                action = delegate (object sender, DayEndEventArgs args) {
                    p.statuses.Remove(StatusID.MinorLuck);
                    e.dayEnd -= action;
                };
                e.dayEnd += action;
            });

            effects.Add(SpellRecipeID.NaturalHealing, delegate (IUserInterface ui, Player p, Engine e) {
                ui.WriteIntoDescription("Your skin starts to crawl, almost as if it has a life of its own.", 2);

                p.statuses.Add(StatusID.NaturalHealing);
                EventHandler<DayEndEventArgs> action = null;
                EventHandler healer = delegate (object sender, EventArgs args) {
                    p.stamina += 10;
                };
                action = delegate (object sender, DayEndEventArgs args) {
                    p.statuses.Remove(StatusID.NaturalHealing);
                    e.dayEnd -= action;
                    p.useAction -= healer;
                };
                e.dayEnd += action;
                p.useAction += healer;
            });

            effects.Add(SpellRecipeID.ReadTheFlesh, delegate (IUserInterface ui, Player p, Engine e) {
                ui.WriteIntoDescription("Your mind expands, and suddenly your body is like an open book. Everything makes sense.", 2);

                p.statuses.Add(StatusID.ReadTheFlesh);
                EventHandler<DayEndEventArgs> action = null;
                action = delegate (object sender, DayEndEventArgs args) {
                    p.statuses.Remove(StatusID.ReadTheFlesh);
                    e.dayEnd -= action;
                };
                e.dayEnd += action;
            });

            effects.Add(SpellRecipeID.SupernalEyes, delegate (IUserInterface ui, Player p, Engine e) {
                ui.WriteIntoDescription("The layers of the world peel away before you, and for now you can truly ", 0);
                ui.WriteIntoDescription("see.", ConsoleColor.Cyan, ConsoleColor.Black, 2);

                p.statuses.Add(StatusID.SupernalEyes);
                EventHandler<DayEndEventArgs> action = null;
                action = delegate (object sender, DayEndEventArgs args) {
                    p.statuses.Remove(StatusID.SupernalEyes);
                    e.dayEnd -= action;
                };
                e.dayEnd += action;
            });

            return effects;
        }
    }
}
