﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.VMComponents
{
    /// <summary>
    /// Represents a Log
    /// </summary>
    public class Log
    {
        /// <summary>
        /// The List of Log entery awating to be written to the log file
        /// </summary>
        private readonly List<LogEntry> logList = new List<LogEntry>();

<<<<<<< HEAD
        public Log() { }
   
=======
        /// <summary>
        /// Creats an instance of a Log
        /// </summary>
        public Log() { }

        /// <summary>
        /// adds entry to the log after each transaction
        /// </summary>
        /// <param name="logEntry"></param>
>>>>>>> b1ee48d49923a9c7bf8c2e066b94ff49a8901926
        public void AddLogEntry(LogEntry logEntry)
        {
            logList.Add(logEntry);
        }

        /// <summary>
        /// Appends audit log.
        /// </summary>
        public void WriteToLog()
        {
            using (StreamWriter sw = new StreamWriter("Log.txt", true))
            {
                sw.WriteLine($"Transaction entries recorded at: {DateTime.Now.ToLocalTime()}");
                foreach(LogEntry entry in logList)
                {
                    string entryLine = entry.MakeLogEntry();
                    sw.WriteLine(entryLine);
                }
            }
            logList.Clear();
        }
    }
}
