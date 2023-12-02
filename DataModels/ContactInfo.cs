using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class ContactInfo
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Contact { get; set; }

        public ContactInfo() 
        {
            Type = string.Empty;
            Contact = string.Empty;
        }
    }
}
