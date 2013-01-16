using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AuditModels
{
    [Serializable]
    public class Session
    {
        public int SessionID { get; set; }
        public DateTime SessionBeginDateTime { get; set; }

        public Session()
        {
        }

        public Session(int iSessionID, DateTime dtSessionBeginDateTime)
        {
            SessionID = iSessionID;
            SessionBeginDateTime = dtSessionBeginDateTime;
        }
    }
}
