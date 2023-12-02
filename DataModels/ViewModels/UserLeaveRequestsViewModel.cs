namespace DataModels.ViewModels
{
    public class UserLeaveRequestsViewModel
    {
        public LeaveRequest LeaveRequest { get; set; }
        public UserDetails UserDetails { get; set; }

        public UserLeaveRequestsViewModel() 
        {
            LeaveRequest = new LeaveRequest();
            UserDetails = new UserDetails();
        }
    }
}
