namespace DataModels.ViewModels
{
    public class OrganizationChartViewModel
    {
        public UserInfoVM Superior { get; set; }
        public List<UserInfoVM> TeamMembers { get; set; }

        public OrganizationChartViewModel() 
        { 
            Superior = new UserInfoVM();
            TeamMembers = new List<UserInfoVM>();
        }
    }
}
