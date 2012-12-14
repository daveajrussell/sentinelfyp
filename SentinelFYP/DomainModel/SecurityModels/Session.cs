using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.SecurityModels
{
    [Serializable]
    public class Session
    {
        public int SessionID { get; set; }
        public DateTime SessionBeginDateTime { get; set; }
        public string UserAgent { get; set; }
        public string IPAddress { get; set; }

        public Session()
        {
        }

        public Session(int iSessionID, DateTime dtSessionBeginDateTime, string strUserAgent, string strIPAddress)
        {
            SessionID = iSessionID;
            SessionBeginDateTime = dtSessionBeginDateTime;
            UserAgent = strUserAgent;
            IPAddress = strIPAddress;
        }
    }
}
