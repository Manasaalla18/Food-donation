using System.ComponentModel.DataAnnotations;

namespace FoodDonation.Models
{
    public class UserMaster
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Key]
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int roleId { get; set; }

        public ICollection<Donate> Donations { get; set; }
    }
}
