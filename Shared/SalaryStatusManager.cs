using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class SalaryStatusManager
    {
        public const string NotGenerated = "Not generated";
        public const string Generated = "Generated";
        public const string SalarySent = "Sent";
        public const string Cancelled = "Cancelled";

        /// <summary>
        /// Returns the next state of the status, taking into account its current state.
        /// </summary>
        public static string? SetStatus(string currentState, string? status = null)
        {
            if (currentState == null && status == null)
            {
                return NotGenerated;
            }
            else if (currentState == NotGenerated && status == Generated)
            {
                return Generated;
            }
            else if (currentState == Generated && status == SalarySent)
            {
                return SalarySent;
            }
            else if (status == Cancelled)
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
            return NotGenerated;
        }
    }
}
