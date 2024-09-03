using System;
using System.Text.Json.Serialization;
namespace v_conf_dn.Dto;
public class VehicleDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("comp_type")]
    public string CompType { get; set; }

    [JsonPropertyName("is_configurable")]
    public string IsConfigurable { get; set; }

    [JsonPropertyName("model_id")]
    public long? ModelId { get; set; }

    [JsonPropertyName("comp_name")]
    public string CompName { get; set; }

    [JsonPropertyName("mod_id")]
    public long? ModId { get; set; }
}
