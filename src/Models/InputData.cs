using System;
using System.Text.json.Serialization;

namespace src.Models
{
    public class InputData
    {
        [JsonPropertyName("id")]
        public string _id {get; set;}

        [JsonPropertyName("numberofChildren")]
        public int NumberofChildren {get; set;} // min = 0 max =?

        [JsonPropertyName("familyComposition")]
        public string FamilyComposition {get; set;} // single or couple

        [JsonPropertyName("familyUnitInPayForDecember")]
        public bool FamilyUnitInPayForDecember { get; set; }
    }
}

