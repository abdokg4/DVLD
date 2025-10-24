using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLogger
    {
        private static string SourceName = "DVLD";
        

        static clsLogger()
        {
            if(!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }
        }

        public static void Log(Exception e, EventLogEntryType Type = EventLogEntryType.Error)
        {
            EventLog.WriteEntry(SourceName, WriteLogMessage(e), Type);
        }

        private static string WriteLogMessage(Exception e)
        {
            string logMessage =
                $"\tException:\n" +
                $"Date: {DateTime.Now}\n" +
                $"Message: {e.Message}\n" +
                $"Inner Message: {(e.InnerException == null ? "N/A" : e.InnerException.Message)}\n" +
                $"Stack Trace: {e.StackTrace}\n" +
                $"Source: {e.Source}\n";

            return logMessage;
        }
    }
}
