using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using SpellingGame2;
using Object = SpellingGame2.Object;

namespace SpellingUnitTest
{
    public class ObjectTests
    {
        [Test]
        public void SerializationTest() {
            //setup
            Object obj = new Object(new List<(Aspect, int)>() { (Aspect.Aer, 3), (Aspect.Ordo, 1) }, ObjectID.TestObject, "test");
            Dictionary<ObjectID, Object> objects = new Dictionary<ObjectID, Object>();
            objects.Add(ObjectID.TestObject, obj);
            ObjectXmlHandler.ObjectsSerialize(objects);

            //test
            var deserializedObjects = ObjectXmlHandler.ObjectsDeserialize();

            //assert
            Assert.AreEqual(deserializedObjects[ObjectID.TestObject], obj);
        }
    }
}
