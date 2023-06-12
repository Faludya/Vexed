namespace DataModels.ViewModels
{
    public class DashboardViewModel
    {
        public List<ToDo> ToDoList { get; set; }
        public List<CardsVM> LastCards { get; set; }
        public List<ProjectTeam> ProjectTeams { get; set; }
        public Salary Salary { get; set; }
    }
}
