using System.ComponentModel.DataAnnotations;

namespace FoodDonation.Models
{
    public class LogisticModel
    {
        [Key]
        [Required]
        public string LogUserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string vechicalNumber { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public string location { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
