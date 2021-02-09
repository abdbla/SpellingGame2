using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SpellingGame2
{
    public class ExpeditionXmlHandler
    {
        static public Dictionary<ExpeditionID, Expedition> ExpeditionsDeserialize() {
            XmlSerializer serializer = new XmlSerializer(typeof(Expedition));
            Dictionary<ExpeditionID, Expedition> expeditions = new Dictionary<ExpeditionID, Expedition>();
            foreach (var item in Directory.GetFiles(@"..\..\..\..\expeditions\")) {
                using (FileStream input = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read)) {
                    Expedition tmp = (Expedition)serializer.Deserialize(input);
                    expeditions.Add(tmp.id, tmp);
                }
            }
            return expeditions;
        }
        static public void ExpeditionsSerialize(Dictionary<ExpeditionID, Expedition> expeditions) {
            XmlSerializer serializer = new XmlSerializer(typeof(Expedition));
            foreach (var item in expeditions) {
                StringBuilder path = new StringBuilder(@"..\..\..\..\expeditions\");
                path.Append(item.Key.ToString());
                path.Append(".xml");
                using (FileStream output = new FileStream(path.ToString(), FileMode.OpenOrCreate, FileAccess.Write)) {
                    serializer.Serialize(output, item.Value);
                }
            }
        }
    }

    public struct ExpeditionRequirements
    {
        public int actions;
        public int money;
        public int stamina;
        public List<ObjectID> objects;
        public List<SpellRecipeID> rituals;
    }

    public class Expedition
    {
        public ExpeditionID id;
        public string description;
        public string failureDesc;
        public string successDesc;
        public ExpeditionRequirements requirements;
        public ExpeditionRequirements costs;
        public ExpeditionRequirements failureCosts;
        public Func<Expedition, List<ObjectID>> rewards;

        public Expedition() {
            id = ExpeditionID.TestExpedition;
            description = "Default text. Report this as an error.";
            failureDesc = "Default text. Report this as an error.";
            successDesc = "Default text. Report this as an error.";
            requirements = new ExpeditionRequirements();
            costs = new ExpeditionRequirements();
            failureCosts = new ExpeditionRequirements();
            rewards = new List<ObjectID>;
        }

        public override bool Equals(object obj) {
            if (obj.GetType() == typeof(Expedition)) {
                return ((Expedition)obj).id == this.id;
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

    public enum ExpeditionID
    {
        Garden,
        Store,
        AbandonedMine,
        TestExpedition,
    }
}
