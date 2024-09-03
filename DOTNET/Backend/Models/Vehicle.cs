using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using v_conf_dn.Models;

namespace v_conf_dn.Models
{
    public enum CompType
    {
        S, C, I, E
    }

    public enum IsConfigurable
    {
        Y, N
    }

    [Table("vehicles")]
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [Required]
        [Column]
        [JsonPropertyName("comp_type")]
        public CompType? CompType { get; set; }

        [Required]
        [Column]
        [JsonPropertyName("is_configurable")]
        public IsConfigurable? IsConfigurable { get; set; }

        [Required]
        [ForeignKey("Model")]
        public long ?ModelId { get; set; }

        public virtual Model Mod { get; set; }

        [Required]
        [ForeignKey("Component")]
        public long ?ComponentId { get; set; }

        [JsonPropertyName("comp_name")]
        public virtual Component Component { get; set; }

        public Vehicle() { }

        public Vehicle(long id, CompType compType, IsConfigurable isConfigurable, Model model, Component component)
        {
            Id = id;
            CompType = compType;
            IsConfigurable = isConfigurable;
            Mod = model;
            Component = component;
        }

        public override string ToString()
        {
            return $"Vehicle [Id={Id}, CompType={CompType}, IsConfigurable={IsConfigurable}, Model={Mod}, Component={Component}]";
        }
    }
}
