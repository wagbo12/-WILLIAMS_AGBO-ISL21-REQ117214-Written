using System.Text.Json.Serialization;

namespace src.Models
{
    public class OutputData
    {
        [JsonPropertyName("id")]
        public string _id { get; set; } = string.Empty;

        [JsonPropertyName("isEligible")]
        public bool IsEligible { get; set; }

        [JsonPropertyName("baseAmount")]
        public float BaseAmount { get; set; }

        [JsonPropertyName("childrenAmount")]
        public float ChildrenAmount { get; set; }

        [JsonPropertyName("supplementAmount")]
        public float SupplementAmount { get; set; }
    }
}
