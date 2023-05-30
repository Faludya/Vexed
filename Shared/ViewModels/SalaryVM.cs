using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vexed.Models.ViewModels;

namespace Shared.ViewModels
{
    public class SalaryVM
    {
        public List<UserNameVM> UserNameVMs { get; set; }
        public Guid SelectedUserId { get; set; }
        public Salary Salary { get; set; }
    }
}
