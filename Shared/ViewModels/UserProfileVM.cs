using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vexed.Models;

namespace Shared.ViewModels
{
    public class UserProfileVM
    {
        public UserDetails Details { get; set; }
        public UserEmployment Employment { get; set; }
        public string Email { get; set; }
    }
}
