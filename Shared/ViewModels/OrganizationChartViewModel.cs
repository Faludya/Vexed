using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class OrganizationChartViewModel
    {
        public UserProfileVM Superior { get; set; }
        public List<UserProfileVM> TeamMembers { get; set; }
    }
}
