using System.ComponentModel.DataAnnotations;
namespace FoodDonation.Models
{
    public class FeedBack
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Q1 { get; set; }
        [Required]
        public int Q2 { get; set; }
        [Required]
        public int Q3 { get; set; }

    }
}
