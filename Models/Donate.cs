using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDonation.Models
{
    public class Donate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RequestID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string PurposeOfDonation { get; set; }

        [Required]
        public int NoOfServing { get; set; }

        [ForeignKey("UserMaster")]
        public string UserId { get; set; }

        public string Status { get; set; }

        public UserMaster UserMaster { get; set; }
    }
}
