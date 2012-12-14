using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.SecurityModels
{
    [Serializable]
    public class User
    {
        public Guid UserKey { get; set; }
        public Session Session { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime UserAccountCreatedOn { get; set; }
        public DateTime UserLastLogon { get; set; }
        public DateTime UserAccountExpires { get; set; }

        public User()
        {
        }

        public User(Guid oUserKey, string strUserName, string strFirstName, string strLastName, string strEmail)
        {
            UserKey = oUserKey;
            UserName = strUserName;
            FirstName = strFirstName;
            LastName = strLastName;
            Email = strEmail;
        }

        public User(Guid oUserKey, string strUserName, string strFirstName, string strLastName, string strEmail, DateTime dtUserAccountCreatedOn, DateTime dtUserLastLogon, DateTime dtUserAccountExpires)
        {
            UserKey = oUserKey;
            UserName = strUserName;
            FirstName = strFirstName;
            LastName = strLastName;
            Email = strEmail;
            UserAccountCreatedOn = dtUserAccountCreatedOn;
            UserLastLogon = dtUserLastLogon;
            UserAccountExpires = dtUserAccountExpires;
        }
    }
}
