namespace Shared
{
    public static class StatusManager
    {
        public const string Submitted = "Submitted";
        public const string SuperiorApproval = "Approved by superior";
        public const string HRApproval = "Approved";
        public const string Cancelled = "Cancelled";

        /// <summary>
        /// Returns the next state of the status, taking into account its current state.
        /// </summary>
        public static string SetStatus(string currentState, string? status = null)
        {
           if(currentState == null && status == null)
            {
                return Submitted;
            }
           else if(currentState == Submitted && status == SuperiorApproval)
            {
                return SuperiorApproval;
            }
           else if(currentState == SuperiorApproval && status == HRApproval)
            {
                return HRApproval;
            }
           else if(status == Cancelled) 
            {   
                return Cancelled;
            }
            else
            {
                return currentState;
            }
        }

        public static string ResetStatus()
        {
            return Submitted;
        }
    }
}
