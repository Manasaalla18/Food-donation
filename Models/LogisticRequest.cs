using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDonation.Models
{
    public class LogisticRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string LogisticId { get; set; }

        [Required]

        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Locality { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public String PhoneNumber { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
