using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class DashboardViewModel
    {
        public List<ToDo> ToDoList { get; set; }
        public List<CardsVM> LastCards { get; set; }
        public Salary Salary { get; set; }
    }
}
