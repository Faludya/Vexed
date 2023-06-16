using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.ViewModels
{
    public class ProjectInfoVM
    {

        public Project Project { get; set; }
        public List<ProjectTeam> Team { get; set; }

    }
}
