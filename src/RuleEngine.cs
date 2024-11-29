using System;
using src.Models;

namespace src
{
    public class RulesEngine
    {
        public OutputData Process(InputData inputdata)  
        {
            var output = new OutputData
            {
                _id = inputdata._id,
                IsEligible = inputdata.FamilyUnitInPayForDecember,
                BaseAmount = 0,
                ChildrenAmount = 0,
                SupplementAmount = 0
            };

            if (!inputdata.FamilyUnitInPayForDecember)
            {
                output.IsEligible = false;
                return output;
            }

            output.BaseAmount = BaseAmount_Calculate(inputdata.FamilyComposition);
            output.ChildrenAmount = ChildrenAmount_Calculate(inputdata.NumberOfChildren);  
            output.SupplementAmount = output.BaseAmount + output.ChildrenAmount;

            return output;
        }

        private float BaseAmount_Calculate(string familyComposition){
            if (familyComposition == "single"){
                return 60;
            } 
            else if (familyComposition == "couple"){   
                return 120; 
            } 
            else{   
                return 0; 
            }
        }

        private float ChildrenAmount_Calculate(int childrenAmount){
            if (childrenAmount == 0){
                return 0;
            }
            else{
                return 20 * childrenAmount;
            }
        }
    }
}