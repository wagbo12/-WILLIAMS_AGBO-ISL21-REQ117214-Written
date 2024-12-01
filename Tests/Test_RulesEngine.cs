/*======================================================================
| Test_RulesEngine class
|
| Name: Test_RulesEngine.cs
|
| Written by: Williams Agbo - 2024-12-01
|
| Purpose: Contains unit tests for the RulesEngine class.
|
| Usage: Run tests to ensure the RulesEngine functions as expected.
|
| Description of properties: None
|
|----
*/
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using src.Models; 
using src; 

namespace Tests
{
    [TestClass] // Mark this class as a test class for MSTest
    public class Test_RulesEngine
    {
        private RulesEngine _rulesEngine; // Declare an instance of RulesEngine to test

        [TestInitialize] // This method runs before each test to set up necessary resources
        public void Setup()
        {
            _rulesEngine = new RulesEngine(); // Instantiate a new RulesEngine object
        }

        [TestMethod] // Test scenario: Couple with two children, eligible for payment
        public void TestCoupleWithChildren()
        {
            // Arrange: Prepare input data for a couple with two children, eligible in December
            var input = new InputData
            {
                _id = "123",
                NumberOfChildren = 2,
                FamilyComposition = "couple",
                FamilyUnitInPayForDecember = true
            };

            // Act: Process the input data through the RulesEngine
            var result = _rulesEngine.Process(input);

            // Assert: Validate the calculated eligibility and amounts
            Assert.IsTrue(result.IsEligible); // Ensure the family is eligible
            Assert.AreEqual(120, result.BaseAmount); // Base amount for a couple
            Assert.AreEqual(40, result.ChildrenAmount); // 20 per child for 2 children
            Assert.AreEqual(160, result.SupplementAmount); // Total supplement = Base + Children
        }

        [TestMethod] // Test scenario: Single person with no children, eligible for payment
        public void TestSingleNoChildren()
        {
            // Arrange: Prepare input data for a single person with no children, eligible in December
            var input = new InputData
            {
                _id = "124",
                NumberOfChildren = 0,
                FamilyComposition = "single",
                FamilyUnitInPayForDecember = true
            };

            // Act: Process the input data through the RulesEngine
            var result = _rulesEngine.Process(input);

            // Assert: Validate the calculated eligibility and amounts
            Assert.IsTrue(result.IsEligible); // Ensure the person is eligible
            Assert.AreEqual(60, result.BaseAmount); // Base amount for a single person
            Assert.AreEqual(0, result.ChildrenAmount); // No children = 0 children amount
            Assert.AreEqual(60, result.SupplementAmount); // Total supplement = Base
        }

        [TestMethod] // Test scenario: Ineligible couple with one child
        public void TestNotEligible()
        {
            // Arrange: Prepare input data for a couple with one child, not eligible in December
            var input = new InputData
            {
                _id = "125",
                NumberOfChildren = 1,
                FamilyComposition = "couple",
                FamilyUnitInPayForDecember = false // Not in pay for December
            };

            // Act: Process the input data through the RulesEngine
            var result = _rulesEngine.Process(input);

            // Assert: Validate the calculated eligibility and amounts
            Assert.IsFalse(result.IsEligible); // Ensure the family is not eligible
            Assert.AreEqual(0, result.BaseAmount); // No base amount for ineligible families
            Assert.AreEqual(0, result.ChildrenAmount); // No children amount for ineligible families
            Assert.AreEqual(0, result.SupplementAmount); // Total supplement = 0
        }

        [TestMethod] // Test scenario: Edge case with a single parent and one child
        public void TestSingleParentWithOneChild()
        {
            // Arrange: Prepare input data for a single parent with one child, eligible in December
            var input = new InputData
            {
                _id = "126",
                NumberOfChildren = 1,
                FamilyComposition = "single",
                FamilyUnitInPayForDecember = true
            };

            // Act: Process the input data through the RulesEngine
            var result = _rulesEngine.Process(input);

            // Assert: Validate the calculated eligibility and amounts
            Assert.IsTrue(result.IsEligible); // Ensure the family is eligible
            Assert.AreEqual(60, result.BaseAmount); // Base amount for a single parent
            Assert.AreEqual(20, result.ChildrenAmount); // 20 per child for 1 child
            Assert.AreEqual(80, result.SupplementAmount); // Total supplement = Base + Children
        }

        [TestMethod] // Test scenario: Invalid family composition
        public void TestInvalidFamilyComposition()
        {
            // Arrange: Prepare input data with an unrecognized family composition
            var input = new InputData
            {
                _id = "127",
                NumberOfChildren = 2,
                FamilyComposition = "unknown",
                FamilyUnitInPayForDecember = true
            };

            // Act: Process the input data through the RulesEngine
            var result = _rulesEngine.Process(input);

            // Assert: Validate the calculated eligibility and amounts
            Assert.IsTrue(result.IsEligible); // Ensure the family is eligible
            Assert.AreEqual(0, result.BaseAmount); // Invalid composition = 0 base amount
            Assert.AreEqual(40, result.ChildrenAmount); // 20 per child for 2 children
            Assert.AreEqual(40, result.SupplementAmount); // Total supplement = Base + Children
        }

        [TestMethod] // Test scenario: Couple with zero children
        public void TestCoupleNoChildren()
        {
            // Arrange: Prepare input data for a couple with no children, eligible in December
            var input = new InputData
            {
                _id = "128",
                NumberOfChildren = 0,
                FamilyComposition = "couple",
                FamilyUnitInPayForDecember = true
            };

            // Act: Process the input data through the RulesEngine
            var result = _rulesEngine.Process(input);

            // Assert: Validate the calculated eligibility and amounts
            Assert.IsTrue(result.IsEligible); // Ensure the family is eligible
            Assert.AreEqual(120, result.BaseAmount); // Base amount for a couple
            Assert.AreEqual(0, result.ChildrenAmount); // No children = 0 children amount
            Assert.AreEqual(120, result.SupplementAmount); // Total supplement = Base
        }
    }
}
