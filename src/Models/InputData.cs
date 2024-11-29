using System.Text.Json.Serialization;

namespace src.Models
{
    public class InputData
    {
        [JsonPropertyName("id")]
        public string _id { get; set; } = string.Empty; 

        [JsonPropertyName("numberOfChildren")]
        public int NumberOfChildren { get; set; }

        [JsonPropertyName("familyComposition")]
        public string FamilyComposition { get; set; } = string.Empty; 

        [JsonPropertyName("familyUnitInPayForDecember")]
        public bool FamilyUnitInPayForDecember { get; set; }
    }
}
