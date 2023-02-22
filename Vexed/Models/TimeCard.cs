using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vexed.Models
{
    public class TimeCard
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SuperiorId { get; set; }

        [DisplayName("Project Code")]
        [DefaultValue("Regular")]

        [Required(ErrorMessage = "{0} is required")]
        public string ProjectCode { get; set; }

        [DisplayName("Task Details")]

        [Required(ErrorMessage = "{0} is required")]
        public string TaskDetails { get; set; }

        [DefaultValue("Work from office")]
        [Required(ErrorMessage = "{0} is required")]
        public string Location { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime EndDate { get; set; }

        [DefaultValue(8)]

        [Required(ErrorMessage = "{0} is required")]
        public float Quantity { get; set; }


        public string? Status { get; set; }
        public string? Comments { get; set; }
    }
}
