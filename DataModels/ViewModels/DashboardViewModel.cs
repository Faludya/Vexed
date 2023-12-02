namespace DataModels.ViewModels
{
    public class DashboardViewModel
    {
        public List<ToDo> ToDoList { get; set; }
        public List<CardsVM> LastCards { get; set; }
        public List<ProjectTeam> ProjectTeams { get; set; }
        public Salary Salary { get; set; }

        public DashboardViewModel()
        {
            ToDoList = new List<ToDo>();
            LastCards = new List<CardsVM>();
            ProjectTeams = new List<ProjectTeam>();
            Salary = new Salary();
        }
    }
}
