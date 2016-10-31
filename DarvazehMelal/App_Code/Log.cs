//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace MyWebPagesStarterKit
{

    /// <summary>
    /// Utility for writing error messages to a Logfile
    /// </summary>
    [Serializable]
    public class Log : Persistable<Log.LogData>
    {
        /// <summary>
        /// Load Log File from App_Data
        /// </summary>
        public Log()
        {
            try
            {
                LoadData();
            }
            catch { }
        }

        /// <summary>
        /// List of all Log-Entries in the Logfile
        /// </summary>
        public List<LogData.LogEntry> Entries
        {
            get { return _data.LogEntries; }
        }

        /// <summary>
        /// Adding a new Log-Entry to the LogData
        /// </summary>
        /// <param name="Created"></param>
        /// <param name="PageTitle"></param>
        public void AddLogEntry(HttpRequest httpRequest, Exception ex)
        {
            LogData.LogEntry entry = new LogData.LogEntry();

            //PageTitle
            entry.PageTitle = httpRequest.Url.AbsolutePath;

            //Date
            entry.Created = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();

            //Error
            entry.Error = ex.ToString();

            //HTTP-Values
            foreach (string key in httpRequest.ServerVariables)
            {
                string value = httpRequest.ServerVariables[key];
                if (value != string.Empty)
                {
                    if (key == "ALL_HTTP" || key == "ALL_RAW")
                        value = value.Replace(System.Environment.NewLine, ", ");
                    entry.ServerVariables.Add(key + ": " + value);
                   
                }
            }

            //Delete all LogEntries when there are more than 50 -> avoid outsize of the Logfile
            if (_data.LogEntries.Count > 50)
                _data.LogEntries.Clear();
            _data.LogEntries.Add(entry);
        }

        /// <summary>
        /// Returns file name of the Logfile
        /// </summary>
        /// <returns>string containting path to sidebar data file</returns>
        protected override string GetDataFilename()
        {
            return "~/App_Data/Error.log";
        }

        /// <summary>
        /// Data-Class for the Log
        /// </summary>
        public class LogData
        {
            public List<LogEntry> LogEntries;

            public LogData()
            {
                LogEntries = new List<LogEntry>();
            }

            public class LogEntry
            {
                public string PageTitle;
                public string Created;
                public string Error;
                public List<string> ServerVariables;

                public LogEntry()
                {
                    PageTitle = string.Empty;
                    Created = string.Empty;
                    Error = string.Empty;
                    ServerVariables = new List<string>();
                }
            }
           
        }
    }
}
