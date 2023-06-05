using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class EmergencyContact
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

        [Required(ErrorMessage = "{0} is required")]
        public string Relationship { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Address { get; set; }

        [DisplayName("Additional Information")]
        public string? AdditionalInformation { get; set; }
    }
}
