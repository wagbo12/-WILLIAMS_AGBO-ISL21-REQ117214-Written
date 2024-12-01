/*======================================================================
| RulesEngine class
|
| Name: RulesEngine.cs
|
| Written by: Williams Agbo - 2024-12-01
|
| Purpose: Processes eligibility and benefit calculations based on input data.
|
| Usage: Invoked to determine eligibility and calculate benefits.
|
| Description of properties: None
|
|----
*/

using System; 
using src.Models; 

namespace src 
{
    // The RulesEngine class contains logic to process eligibility and calculate amounts
    public class RulesEngine
    {
        // Main method to process input data and generate output data
        public OutputData Process(InputData inputdata)
        {
            // Initialize output data with default values and map input ID
            var output = new OutputData
            {
                _id = inputdata._id, // Copy the ID from input data
                IsEligible = inputdata.FamilyUnitInPayForDecember, // Eligibility is initially set based on the 'FamilyUnitInPayForDecember' field
                BaseAmount = 0, // Initialize BaseAmount to 0
                ChildrenAmount = 0, // Initialize ChildrenAmount to 0
                SupplementAmount = 0 // Initialize SupplementAmount to 0
            };

            // If the family unit is not eligible for payment in December, set eligibility to false and return
            if (!inputdata.FamilyUnitInPayForDecember)
            {
                output.IsEligible = false; // Explicitly mark the family as ineligible
                return output; // Exit early with the output containing default values
            }

            // Calculate the base amount based on the family composition
            output.BaseAmount = BaseAmount_Calculate(inputdata.FamilyComposition);

            // Calculate the additional amount for children
            output.ChildrenAmount = ChildrenAmount_Calculate(inputdata.NumberOfChildren);

            // Compute the total supplement amount as the sum of base and children amounts
            output.SupplementAmount = output.BaseAmount + output.ChildrenAmount;

            // Return the fully calculated output data
            return output;
        }

        // Private helper method to calculate the base amount based on family composition
        private float BaseAmount_Calculate(string familyComposition)
        {
            // Check if the family composition is "single"
            if (familyComposition == "single")
            {
                return 60; // Return base amount for a single person
            }
            // Check if the family composition is "couple"
            else if (familyComposition == "couple")
            {
                return 120; // Return base amount for a couple
            }
            // Handle invalid or unrecognized family compositions
            else
            {
                return 0; // Return 0 for invalid family compositions
            }
        }

        // Private helper method to calculate the additional amount for children
        private float ChildrenAmount_Calculate(int childrenAmount)
        {
            // If there are no children, return 0
            if (childrenAmount == 0)
            {
                return 0; // No additional amount for 0 children
            }
            // Otherwise, calculate the amount as 20 per child
            else
            {
                return 20 * childrenAmount; // Multiply number of children by 20
            }
        }
    }
}
