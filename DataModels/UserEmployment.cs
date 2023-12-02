using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class UserEmployment
    {
        public UserEmployment() 
        {
            CompanyName = string.Empty;
            Department = string.Empty;
            Function = string.Empty;
            Location = string.Empty;
            SuperiorName = string.Empty;
        }

        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SuperiorId { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Function { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public float HourlyPay { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string SuperiorName { get; set; }

    }
}
