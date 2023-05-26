using Shared.ViewModels.CombinedClasses;

namespace Vexed.Models.ViewModels
{
    public class EmploymentViewModel
    {
        public UserEmployment UserEmployment { get; set; }
        public List<UserNameVM> UserNamesVM { get; set; }
        public List<UserNameVM> SuperiorNamesVM { get; set; }
        public string SelectedUserId { get; set; }
        public string SelectedSuperiorId { get; set; }
    }
}
