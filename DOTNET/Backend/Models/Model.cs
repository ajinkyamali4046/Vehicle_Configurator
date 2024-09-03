using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace v_conf_dn.Models
{
    [Table("models")]
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [ForeignKey("SegId")]
        public Segment? Segment { get; set; }

        [ForeignKey("ManuId")]
        [JsonPropertyName("manu_name")]
        public Manufacturer ?Manufacturer { get; set; }

        [Column("mod_name", TypeName = "nvarchar(255)")]
        [JsonPropertyName("mod_name")]
        [Required]
        public string? ModName { get; set; }

        [Column("price", TypeName = "int")]
        [Required]
        [JsonIgnore]
        public int ?Price { get; set; }

        [Column("image_path", TypeName = "nvarchar(255)")]
        [Required]
        [JsonIgnore]
        public string ?ImagePath { get; set; }

        [Column("min_qty", TypeName = "int")]
        [Required]
        [JsonIgnore]
        public int ?MinQty { get; set; }

        [Column("safety_rating", TypeName = "int")]
        [DefaultValue(5)]
        [JsonIgnore]
        public int ?SafetyRating { get; set; }

        // Parameterless constructor
        public Model() { }

        // Constructor with parameters
        public Model(long id, Segment segment, Manufacturer manufacturer, string modName, int price, string imagePath,
                     int minQty, int safetyRating)
        {
            Id = id;
            Segment = segment;
            Manufacturer = manufacturer;
            ModName = modName;
            Price = price;
            ImagePath = imagePath;
            MinQty = minQty;
            SafetyRating = safetyRating;
        }
    }
}
