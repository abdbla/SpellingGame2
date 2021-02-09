using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SpellingGame2
{
    public class ObjectXmlHandler
    {
        static public Dictionary<ObjectID, Object> ObjectsDeserialize() {
            XmlSerializer serializer = new XmlSerializer(typeof(Object));
            Dictionary<ObjectID, Object> objects = new Dictionary<ObjectID, Object>();
            foreach (var item in Directory.GetFiles(@"..\..\..\..\objects\")) {
                using (FileStream input = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read)) {
                    Object tmp = (Object)serializer.Deserialize(input);
                    objects.Add(tmp.id, tmp);
                }
            }
            return objects;
        }
        static public void ObjectsSerialize(Dictionary<ObjectID, Object> Objects) {
            XmlSerializer serializer = new XmlSerializer(typeof(Object));
            foreach (var item in Objects) {
                StringBuilder path = new StringBuilder(@"..\..\..\..\objects\");
                path.Append(item.Key.ToString());
                path.Append(".xml");
                using (FileStream output = new FileStream(path.ToString(), FileMode.OpenOrCreate, FileAccess.Write)) {
                    serializer.Serialize(output, item.Value);
                }
            }
        }
    }

    public struct Object
    {
        public List<(Aspect, int)> containedEssentia;
        public string desc;
        public ObjectID id;

        public Object(List<(Aspect, int)> _containedEssentia, ObjectID _id, string _desc) {
            id = _id;
            desc = _desc;
            containedEssentia = _containedEssentia;
        }

        public override bool Equals(object obj) {
            if (obj.GetType() == typeof(Object)) {
                return ((Object)obj).id == this.id;
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

    public enum ObjectID
    {
        TestObject,
        Lead,
        Tin,
        Silver,
        Datura,
        Marijuana,
        Basil,
        Vinegar,
        DistilledWater,
        Batteries,
    }
}
