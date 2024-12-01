/*======================================================================
| InputData class
|
| Name: InputData.cs
|
| Written by: Williams Agbo - 2024-12-01
|
| Purpose: Defines the structure for input data received from the MQTT topic.
|
| Usage: Used to deserialize JSON input data.
|
| Description of properties:
| - id: Unique identifier for the input data.
| - numberOfChildren: Number of children in the family.
| - familyComposition: Composition of the family (e.g., couple, single).
| - familyUnitInPayForDecember: Indicates if the family unit is in pay for December.
|
|----
*/

using System.Text.Json.Serialization; 

//JSon Schema gotten from Instructions
namespace src.Models 
{
   
    public class InputData
    {
        [JsonPropertyName("id")] // Maps the "id" field from JSON to this property
        public string _id { get; set; } = string.Empty; // Unique identifier for the input; defaults to an empty string

        [JsonPropertyName("numberOfChildren")] // Maps the "numberOfChildren" field from JSON to this property
        public int NumberOfChildren { get; set; } // Number of children in the family, defaults to 0

        [JsonPropertyName("familyComposition")] // Maps the "familyComposition" field from JSON to this property
        public string FamilyComposition { get; set; } = string.Empty; // Family composition (e.g., "single", "couple"), defaults to an empty string

        [JsonPropertyName("familyUnitInPayForDecember")] // Maps the "familyUnitInPayForDecember" field from JSON to this property
        public bool FamilyUnitInPayForDecember { get; set; } // Indicates if the family unit is eligible for December pay
    }
}
