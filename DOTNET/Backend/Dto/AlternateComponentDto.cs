using System.Text.Json.Serialization;

namespace v_conf_dn.Dto
{
    public class AlternateComponentDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("delta_price")]
        public double? DeltaPrice { get; set; }
        public long? ModId { get; set; }
        [JsonPropertyName("comp_id")]
        public long? CompId { get; set; }

        [JsonPropertyName("alt_comp_id")]
        public long? AltCompId { get; set; }
        [JsonPropertyName("comp_name")]
        public string ComponentName { get; set; }

        public string AlternateComponentName { get; set; }
    }


}
