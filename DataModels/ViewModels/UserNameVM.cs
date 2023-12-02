namespace DataModels.ViewModels
{
    public class UserNameVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public UserNameVM()
        {
            UserId = string.Empty;
            UserName = string.Empty;
        }
    }
}
