using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class ProjectTeam
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string UserProjectRole { get; set; }
        public string ImageLink { get; set; }
    }
}
