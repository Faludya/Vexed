namespace DataModels.ViewModels
{
    public class EmploymentViewModel
    {
        public UserEmployment UserEmployment { get; set; }
        public List<UserNameVM> UserNamesVM { get; set; }
        public List<UserNameVM> SuperiorNamesVM { get; set; }
        public string SelectedUserId { get; set; }
        public string SelectedSuperiorId { get; set; }
        public List<string> AllRoles { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
