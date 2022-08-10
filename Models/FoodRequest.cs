using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDonation.Models
{
    public class FoodRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string FoodRequestId { get; set; }

        [Required]
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public string Date { get; set; }

        [Required]
        public string Locality { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public string Occasion { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
