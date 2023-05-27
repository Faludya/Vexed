using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        
        // ------------ Employee details -----------
        [Required(ErrorMessage = "{0} is required")]
        public string FullName{ get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Function { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Company { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime GeneratedDate { get; set; }

        // ------------ Worked time details ---------
        [Required(ErrorMessage = "{0} is required")]
        public float TotalWorkedHours { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int TotalWorkedDays { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public float GrossSalary{ get; set; }

        // ------------- Tax details ---------------
        [Required(ErrorMessage = "{0} is required")]
        public float SocialInsuranceTax { get; set; } = 25;

        [Required(ErrorMessage = "{0} is required")]
        public float SocialInsuranceValue { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public float HealthInsuranceTax { get; set; } = 10;

        [Required(ErrorMessage = "{0} is required")]
        public float HealthInsuranceValue { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public float PersonalIncomeTax { get; set; } = 10;

        [Required(ErrorMessage = "{0} is required")]
        public float PersonalIncomeValue { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public float WorkInsuranceTax { get; set; } = 0.15f;

        [Required(ErrorMessage = "{0} is required")]
        public float WorkInsuranceValue { get; set; }

        // ------------- Pay details ---------------
        [Required(ErrorMessage = "{0} is required")]
        public float NetSalary { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int MealTicketValue { get; set; } = 20;

        [Required(ErrorMessage = "{0} is required")]
        public int MealTicketTotal { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Status { get; set; }
    }
}
