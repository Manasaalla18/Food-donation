using System.ComponentModel.DataAnnotations;
namespace FoodDonation.Models
{
    public class AdminMsg
    {
        [Key]
        public string RequestId { get; set; }

        [Required]
        public string DonarUserId { get; set; }

        [Required]
        public string LogisticUserId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
