/*======================================================================
| OutputData class
|
| Name: OutputData.cs
|
| Written by: Williams Agbo - 2024-12-01
|
| Purpose: Defines the structure for output data sent to the MQTT topic.
|
| Usage: Used to serialize output data after processing.
|
| Description of properties:
| - id: Unique identifier for the output data.
| - isEligible: Indicates if the family is eligible for the benefit.
| - baseAmount: Base amount calculated for the benefit.
| - childrenAmount: Additional amount per child.
| - supplementAmount: Total supplement amount.
|
|----
*/
using System.Text.Json.Serialization;

//JSon Schema gotten from Instructions
namespace src.Models
{
    public class OutputData
    {
        [JsonPropertyName("id")] // Maps the "id" field from JSON to this property
        public string _id { get; set; } = string.Empty; // Unique identifier for the output, defaults to an empty string

        [JsonPropertyName("isEligible")] // Maps the "isEligible" field from JSON to this property
        public bool IsEligible { get; set; } // Indicates whether the family is eligible for the supplement

        [JsonPropertyName("baseAmount")] // Maps the "baseAmount" field from JSON to this property
        public float BaseAmount { get; set; } // Base supplement amount based on family composition

        [JsonPropertyName("childrenAmount")] // Maps the "childrenAmount" field from JSON to this property
        public float ChildrenAmount { get; set; } // Additional supplement amount based on the number of children

        [JsonPropertyName("supplementAmount")] // Maps the "supplementAmount" field from JSON to this property
        public float SupplementAmount { get; set; } // Total supplement amount (base + children amounts)
    }
}

