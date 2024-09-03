using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace v_conf_dn.Models
{
    [Table("components")]
    public class Component
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("comp_name")]
        public string? CompName { get; set; }
    }
}
