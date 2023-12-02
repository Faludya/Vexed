namespace DataModels.ViewModels
{
    public class UsersDetailsViewModel
    {
        public UserDetails UserDetails { get; set; }
        public List<UserNameVM> UserNamesVM { get; set; }
        public string SelectedUserId { get; set; }

        public UsersDetailsViewModel() 
        {
            UserDetails = new UserDetails();
            UserNamesVM = new List<UserNameVM>();
            SelectedUserId = string.Empty;
        }
    }
}
