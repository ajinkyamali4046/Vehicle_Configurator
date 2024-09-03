using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace v_conf_dn.Models
{
    [Table("segments")]
    public class Segment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [Column("segName", TypeName = "nvarchar(255)")]
        
        [Required]
        [JsonPropertyName("name")]
        public string? SegName { get; set; }

        // Parameterless constructor
        public Segment() { }

        // Constructor with parameters
        public Segment(string segName)
        {
            SegName = segName;
        }

        public override string ToString()
        {
            return $"Segment [id={Id}, name={SegName}]";
        }
    }
}
