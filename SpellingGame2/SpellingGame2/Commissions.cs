using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SpellingGame2
{
    public class CommissionXmlHandler
    {
        static public Dictionary<CommissionID, Commission> CommissionsDeserialize() {
            XmlSerializer serializer = new XmlSerializer(typeof(Commission));
            Dictionary<CommissionID, Commission> commissions = new Dictionary<CommissionID, Commission>();
            foreach (var item in Directory.GetFiles(@"..\..\..\..\commissions\")) {
                using (FileStream input = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read)) {
                    Commission tmp = (Commission)serializer.Deserialize(input);
                    commissions.Add(tmp.id, tmp);
                }
            }
            return commissions;
        }
        static public void CommissionsSerialize(Dictionary<CommissionID, Commission> commissions) {
            XmlSerializer serializer = new XmlSerializer(typeof(Commission));
            foreach (var item in commissions) {
                StringBuilder path = new StringBuilder(@"..\..\..\..\commissions\");
                path.Append(item.Key.ToString());
                path.Append(".xml");
                using (FileStream output = new FileStream(path.ToString(), FileMode.OpenOrCreate, FileAccess.Write)) {
                    serializer.Serialize(output, item.Value);
                }
            }
        }
    }

    public enum CommissionID
    {
        Ritual1,
        Ritual2,
        Ritual3,
        Essentia1,
        Essentia2,
        Essentia3,
        TestCommission,
    }

    public struct Commission
    {
        public SpellRecipeID requiredRitual;
        public List<(Aspect, int)> requiredEssentia;
        public List<(Lore, int)> requiredLore;
        public CommissionType type;
        public CommissionID id;
        public int moneyReward;
        public List<ObjectID> objectsReward;

        public string desc;

        public Commission(SpellRecipeID _requiredRitual, List<(Aspect, int)> _requiredEssentia, List<(Lore, int)> _requiredLore, CommissionType _type, string _desc, CommissionID _id, int _money, List<ObjectID> _objects) {
            requiredRitual = _requiredRitual;
            requiredEssentia = _requiredEssentia;
            requiredLore = _requiredLore;
            type = _type;
            desc = _desc;
            id = _id;
            moneyReward = _money;
            objectsReward = _objects;
        }
        public override bool Equals(object obj) {
            if (obj.GetType() == typeof(Commission)) {
                return ((Commission)obj).id == this.id;
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

    public enum CommissionType
    {
        Ritual,
        Treatise,
        Essentia
    }

    static public class CommissionExtensions
    {
        public static int Count() {
            return 7;
        }
    }
}
