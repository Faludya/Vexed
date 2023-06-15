namespace DataModels.ViewModels
{
    public class TeamLeaveRequestVM
    {
        public string UserName { get; set; }
        public List<LeaveRequest> Leaves { get; set; }
    }
}
