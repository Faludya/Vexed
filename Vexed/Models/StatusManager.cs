namespace Vexed.Models
{
    public static class StatusManager
    {
        public const string Submitted = "Submitted";
        public const string SuperiorApproval = "Approved by superior";
        public const string HRApproval = "Approved";
        public const string Cancelled = "Cancelled";

        public static string SetStatus(string? status = null)
        {
            switch(status)
            {
                default: 
                    return Submitted;
                case Submitted:
                    return SuperiorApproval;
                case SuperiorApproval:
                    return HRApproval;
                case Cancelled:
                    return Cancelled;
            }
        }

    }
}
