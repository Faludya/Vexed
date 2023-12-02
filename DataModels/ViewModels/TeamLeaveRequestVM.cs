namespace DataModels.ViewModels
{
    public class TeamLeaveRequestVM
    {
        public string UserName { get; set; }
        public List<LeaveRequest> Leaves { get; set; }

        public TeamLeaveRequestVM() 
        {
            Leaves = new List<LeaveRequest>();
            UserName = string.Empty;
        }
    }
}
