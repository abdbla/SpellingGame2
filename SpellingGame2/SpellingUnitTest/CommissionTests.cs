using NUnit.Framework;
using SpellingGame2;
using System;
using System.Collections.Generic;
using System.Text;
using Object = SpellingGame2.Object;

namespace SpellingUnitTest
{
    public class CommissionTests
    {
        [Test]
        public void SerializationTest() {
            //setup
            Commission commission = new Commission(SpellRecipeID.TestRecipe, new List<(Aspect, int)>() { (Aspect.Aer, 5) }, null, CommissionType.Essentia, "", CommissionID.TestCommission);
            Dictionary<CommissionID, Commission> commissionects = new Dictionary<CommissionID, Commission>();
            commissionects.Add(CommissionID.TestCommission, commission);
            CommissionXmlHandler.CommissionsSerialize(commissionects);

            //test
            var deserializedCommissions = CommissionXmlHandler.CommissionsDeserialize();

            //assert
            Assert.AreEqual(deserializedCommissions[CommissionID.TestCommission], commission);
        }
    }
}
