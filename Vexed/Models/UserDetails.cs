using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Vexed.Models
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string LastName { get; set; }

        [DisplayName("Preffered Last Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string PreferredLastName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Address { get; set; }
        public string? AdditionalInformation { get; set; }
    }
}
