using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Models;
using src;

namespace Tests
{
    [TestClass]
    public class Test_RulesEngine
    {
        private RulesEngine _rulesEngine;

        [TestInitialize]
        public void Setup()
        {
            _rulesEngine = new RulesEngine();
        }

        [TestMethod]
        public void TestCoupleWithChildren()
        {
            // Arrange
            var input = new InputData
            {
                _id = "123",
                NumberOfChildren = 2,
                FamilyComposition = "couple",
                FamilyUnitInPayForDecember = true
            };

            // Act
            var result = _rulesEngine.Process(input);

            // Assert
            Assert.IsTrue(result.IsEligible);
            Assert.AreEqual(120, result.BaseAmount);
            Assert.AreEqual(40, result.ChildrenAmount);
            Assert.AreEqual(160, result.SupplementAmount);
        }

        [TestMethod]
        public void TestSingleNoChildren()
        {
            // Arrange
            var input = new InputData
            {
                _id = "124",
                NumberOfChildren = 0,
                FamilyComposition = "single",
                FamilyUnitInPayForDecember = true
            };

            // Act
            var result = _rulesEngine.Process(input);

            // Assert
            Assert.IsTrue(result.IsEligible);
            Assert.AreEqual(60, result.BaseAmount);
            Assert.AreEqual(0, result.ChildrenAmount);
            Assert.AreEqual(60, result.SupplementAmount);
        }

        [TestMethod]
        public void TestNotEligible(){
            // Arrange
            var input = new InputData
            {
                _id = "125",
                NumberOfChildren = 1,
                FamilyComposition = "couple",
                FamilyUnitInPayForDecember = false
            };

            // Act
            var result = _rulesEngine.Process(input);

            // Assert
            Assert.IsFalse(result.IsEligible);
            Assert.AreEqual(0, result.BaseAmount);
            Assert.AreEqual(0, result.ChildrenAmount);
            Assert.AreEqual(0, result.SupplementAmount);
        }
        [TestMethod]
        public void randomTest(){
            var input = new InputData{
                _id = "234",
                NumberOfChildren = 1,
                FamilyComposition = "couple",
                FamilyUnitInPayForDecember = true
            };
            var result = _rulesEngine.Process(input);

            Assert.IsTrue(result.IsEligible);
            Assert.AreEqual(120, result.BaseAmount);
            Assert.AreEqual(20, result.ChildrenAmount);
            Assert.AreEqual(140, result.SupplementAmount);

        }
    }
}