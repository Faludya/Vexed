using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vexed.Models
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        public string Type { get; set; }

        [DisplayName("Start Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime EndDate { get; set; }

        [DefaultValue(8)]

        [Required(ErrorMessage = "{0} is required")]
        public float Quantity { get; set; }

        public string Status { get; set; } = "Awaiting approval";
        public string? Comments { get; set; }
    }
}
