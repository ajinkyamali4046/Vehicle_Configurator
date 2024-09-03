using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace v_conf_dn.Models
{
    public class AlternateComponent
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public double ?DeltaPrice { get; set; }

        [ForeignKey("Model")]
        public long ?ModId { get; set; }
        public virtual Model Model { get; set; }

        [ForeignKey("Component")]
        public long? CompId { get; set; }
        public virtual Component Component { get; set; }

        [ForeignKey("AlternateComponent")]
        public long ?AltCompId { get; set; }
        public virtual Component AltComp { get; set; }
    }
}
