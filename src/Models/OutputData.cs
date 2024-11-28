using System;
using System.Text.json.Serialization;

namespace src.Models
{
    public class InputData
    {
        [JsonPropertyName("id")]
        public string _id {get; set;}// ID from Input

        [JsonPropertyName("isEligible")]
        public bool IsEligible {get; set;}// Eligibility, equal to "familyUnitInPayForDecember"

        [JsonPropertyName("baseAmount")]
        public float baseAmount {get; set;}// Base amount calculated from family composition

        [JsonPropertyName("childrenAmount")]
        public float ChildrenAmount {get; set;}// Additional amount for children

         [JsonPropertyName("supplementAmount")]
        public float SupplementAmount {get; set;}// Total amount
    }
}