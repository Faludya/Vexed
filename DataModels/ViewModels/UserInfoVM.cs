namespace DataModels.ViewModels
{
    public class UserInfoVM
    {
        public UserDetails Details { get; set; }
        public UserEmployment Employment { get; set; }
        public string Email { get; set; }

        public UserInfoVM() 
        {
            Details = new UserDetails();
            Employment = new UserEmployment();
            Email = string.Empty;
        }
    }
}
