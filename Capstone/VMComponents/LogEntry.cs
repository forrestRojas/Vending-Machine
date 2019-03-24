using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.VMComponents
{
    /// <summary>
    /// Represents a Log Entry.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// The date and time.
        /// </summary>
        readonly string _dateTime = DateTime.Now.ToLocalTime().ToString();

        /// <summary>
        /// Logs the action.
        /// </summary>
        readonly string _action;

        /// <summary>
        /// Logs the users available credits.
        /// </summary>
        private readonly decimal _userBalance;

        /// <summary>
        /// Logs the running total machine credits available.
        /// </summary>
        private readonly decimal _machineBalance;

        /// <summary>
        /// Creates a log entry with the given information.
        /// </summary>
        /// <param name="action">Passes in action.</param>
        /// <param name="userBalance">Passed in user balance.</param>
        /// <param name="machineBalance">Passed in machine balance.</param>
        public LogEntry(string action, decimal userBalance, decimal machineBalance)
        {
            this._action = action;
            this._userBalance = userBalance;
            this._machineBalance = machineBalance;
        }

        /// <summary>
        /// Assembles the log entry.
        /// </summary>
        /// <returns></returns>
        public string MakeLogEntry()
        {
            string makeLogEntry = this._dateTime + ' ' + $"{this._action}".PadRight(30, '.') + $"{this._userBalance:C2}".PadRight(10) + $"{this._machineBalance:C2}";
            return makeLogEntry;
        }
    }
}
