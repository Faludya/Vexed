using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.ViewModels
{
    public class UserProfileVM
    {
        public UserDetails Details { get; set; }
        public UserEmployment Employment { get; set; }
        public string Email { get; set; }

        public List<ProjectTeam> ProjectTeams { get; set; } // Projects are included with Eager Loading
        public List<Qualification> Qualifications { get; set; }

        public List<ContactInfo> ContactInfos { get; set; }
        public List<EmergencyContact> EmergencyContacts { get; set; }
        public List<UserNameVM> UserNameVMs { get; set; }
        public Guid SelectedUserId { get; set; }


        public UserProfileVM()
        {
            Details = new UserDetails();
            Employment = new UserEmployment();
            ProjectTeams = new List<ProjectTeam>();
            Qualifications = new List<Qualification>();
            ContactInfos = new List<ContactInfo>();
            EmergencyContacts = new List<EmergencyContact>();
            UserNameVMs = new List<UserNameVM>();
        }
    }
}
