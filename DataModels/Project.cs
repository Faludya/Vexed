using DataModels.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    public class Project
    {
        public Project()
        {
            TechnologiesList = new List<string>();
            UsefulLinksList = new List<string>();
            ProjectManagersList = new List<UserNameVM>();
            Name = string.Empty;
            Code = string.Empty;
            Description = string.Empty;
            Status = string.Empty;
            ProjectManager = string.Empty;
            ImageLink = string.Empty;
            Technologies = string.Empty;
            UsefulLinks = string.Empty;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public Guid ProjectManagerId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string ProjectManager { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string ImageLink { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Technologies { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string UsefulLinks { get; set; }


        [NotMapped]
        [BindNever]
        public List<string> TechnologiesList { get; set; }

        [NotMapped]
        [BindNever]
        public List<string> UsefulLinksList { get; set; }

        [NotMapped]
        [BindNever]
        public List<UserNameVM> ProjectManagersList { get; set; }

    }
}
