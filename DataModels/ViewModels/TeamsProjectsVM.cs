using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.ViewModels
{
    public class TeamsProjectsVM
    {
        public ProjectTeam ProjectTeam { get; set; }
        public List<UserNameVM> UserNames { get; set; }
        public List<Project> Projects { get; set; }

        public TeamsProjectsVM() 
        {
            Projects = new List<Project>();
            ProjectTeam = new ProjectTeam();
            UserNames = new List<UserNameVM>();
        }
    }
}
