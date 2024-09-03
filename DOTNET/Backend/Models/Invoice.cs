using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace v_conf_dn.Models
{
    [Table("invoices")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("user_id")]
        public long? UserId { get; set; }

        [Column("model_id")]
        public long? ModelId { get; set; }

        [Column("ordered_qty")]
        public int ?OrderedQty { get; set; }

        // Replace int[] with List<int> to handle collections
        [NotMapped]
        public List<long> AltCompId { get; set; }

        [Column("model_price")]
        public int ?ModelPrice { get; set; }

        [Column("total_price")]
        public int ?TotalPrice { get; set; }
    }
}
