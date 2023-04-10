using System.ComponentModel.DataAnnotations;

namespace Vexed.Models
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
    }
}
