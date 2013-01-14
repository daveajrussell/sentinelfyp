using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;  

namespace Sentinel.Infrastructure
{
    public class SentinelAuthProvider : ISentinelAuthProvider
    {
        public bool Authenticate(string strUsername, string strPassword)
        {
            bool blnResult = Membership.ValidateUser(strUsername, strPassword);//FormsAuthentication.Authenticate(strUsername, strPassword);
            if (blnResult)
                FormsAuthentication.SetAuthCookie(strUsername, false);

            return blnResult;
        }

        public void ClearAuthentication(Controller oContext)
        {
            FormsAuthentication.SignOut();
            oContext.Session.Clear();
            oContext.Session.Abandon();
        }
    }
}