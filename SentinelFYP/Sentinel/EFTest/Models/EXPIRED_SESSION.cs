using System;
using System.Collections.Generic;

namespace EFTest.Models
{
    public class EXPIRED_SESSION
    {
        public int SESSION_ID { get; set; }
        public Guid USER_KEY { get; set; }
        public string USER_AGENT { get; set; }
        public string IP_ADDRESS { get; set; }
        public DateTime SESSION_BEGIN_DATE_TIME { get; set; }
        public string EXPIRY_REASON { get; set; }
    }
}
