using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;  

namespace Sentinel.Infrastructure
{
    public class SentinelAuthProvider : ISentinelAuthProvider
    {
        public bool Authenticate(string strUsername, string strPassword)
        {
            bool blnResult = Membership.ValidateUser(strUsername, strPassword);

            if (blnResult)
                FormsAuthentication.SetAuthCookie(strUsername, false);

            return blnResult;
        }

        public void ClearAuthentication()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}