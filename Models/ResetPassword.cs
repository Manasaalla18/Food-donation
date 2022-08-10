using System.ComponentModel.DataAnnotations;
namespace FoodDonation.Models
{
    public class ResetPassword
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Confirm Password not Matched")]
        public string ConfirmPassword { get; set; }
    }
}
