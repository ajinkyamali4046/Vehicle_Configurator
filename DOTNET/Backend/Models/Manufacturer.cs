using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace v_conf_dn.Models
{
    [Table("manufacturers")]
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [Column("manu_name", TypeName = "nvarchar(255)")]
        [Required]
        [JsonPropertyName("name")]
        public string? ManuName { get; set; }

        [ForeignKey("SegId")]
        [JsonIgnore]
        public Segment ?Segment { get; set; }
    }
}
