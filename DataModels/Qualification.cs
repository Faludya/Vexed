using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Qualification
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime DateObtained { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsVerified { get; set; }

        public string? AttachmentUrl { get; set; }
    }
}
