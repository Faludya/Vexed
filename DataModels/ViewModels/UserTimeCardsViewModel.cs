namespace DataModels.ViewModels
{
    public class UserTimeCardsViewModel
    {
        public TimeCard TimeCard { get; set; }
        public UserDetails UserDetails { get; set; }

        public UserTimeCardsViewModel() 
        {
            UserDetails = new UserDetails();
            TimeCard = new TimeCard();
        }
    }
}
