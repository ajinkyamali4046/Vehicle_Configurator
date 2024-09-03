using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace v_conf_dn.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [Required]
        [Column("address_line1")]
        public string ?AddressLine1 { get; set; }

        [Column("address_line2")]
        public string ?AddressLine2 { get; set; }

        [Required]
        public string ?City { get; set; }

        [Required]
        [Column("company_name")]
        public string? CompanyName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        [Column("gst_number")]
        public string ?GstNumber { get; set; }

        [Required]
        public string ?Password { get; set; }

        [Required]
        [Column("pin_code")]
        public string ?PinCode { get; set; }

        [Required]
        public string ?State { get; set; }

        [Required]
        public string ?Telephone { get; set; }

        [Required]
        public string ?Username { get; set; }

        public User() { }

        public User( string addressLine1, string addressLine2, string city, string companyName, string email,
                    string gstNumber, string password, string pinCode, string state, string telephone, string username)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            CompanyName = companyName;
            Email = email;
            GstNumber = gstNumber;
            Password = password;
            PinCode = pinCode;
            State = state;
            Telephone = telephone;
            Username = username;
        }

        public override string ToString()
        {
            return $"User [UserId={UserId}, AddressLine1={AddressLine1}, AddressLine2={AddressLine2}, City={City}, " +
                   $"CompanyName={CompanyName}, Email={Email}, GstNumber={GstNumber}, Password={Password}, " +
                   $"PinCode={PinCode}, State={State}, Telephone={Telephone}, Username={Username}]";
        }
    }
}
