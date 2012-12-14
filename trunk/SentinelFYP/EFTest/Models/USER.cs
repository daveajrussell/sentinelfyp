using System;
using System.Collections.Generic;

namespace EFTest.Models
{
    public class USER
    {
        public USER()
        {
            this.SESSIONs = new List<SESSION>();
        }

        public Guid USER_KEY { get; set; }
        public Guid USER_CULTURE_KEY { get; set; }
        public string USERNAME { get; set; }
        public string SALT { get; set; }
        public string HASH { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string EMAIL { get; set; }
        public DateTime USER_ACCOUNT_CREATED_ON_DATE_TIME { get; set; }
        public DateTime USER_LAST_LOGON_DATE_TIME { get; set; }
        public DateTime USER_ACCOUNT_EXPIRES_DATE_TIME { get; set; }
        public virtual ICollection<SESSION> SESSIONs { get; set; }
        public virtual CULTURE CULTURE { get; set; }
    }
}
