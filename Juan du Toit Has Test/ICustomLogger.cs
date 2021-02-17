using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Juan_du_Toit_Has_Test
{
    public interface ICustomLogger
    {
        public void LogIt(Exception ex);
    }

    class Log : ICustomLogger
    {
        public void LogIt(Exception ex)
        {
            //do some aditional loggin
            EventLog.WriteEntry("Error occured", $"Dev Error1: {ex.Message}",
                     EventLogEntryType.Error);
        }
    }
    class Log1 : ICustomLogger
    {
        public void LogIt(Exception ex)
        {
            //do some aditional loggin
            EventLog.WriteEntry("Error occured", $" UAT Error1: {ex.Message}",
                     EventLogEntryType.Error);
        }
    }
    class Log2 : ICustomLogger
    {
        public void LogIt(Exception ex)
        {
            //do some aditional loggin
            EventLog.WriteEntry("Error occured", $"PROD Error1: {ex.Message}",
                     EventLogEntryType.Error);
        }
    }
}
