using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;

namespace Sentinel.Tests.TestHelpers
{
    public static class AuthenticationTestHelper
    {
        public static bool MockAuthenticate(string strUsername, string strPassword, out User oUser, out Session oSession)
        {
            if (strUsername == "DR_ARCHITECT" && strPassword == "Password")
            {
                oUser = new User() { UserName = strUsername };
                oSession = new Session() { SessionID = 1, SessionBeginDateTime = DateTime.Now };
                return true;
            }
            else
            {
                oUser = null;
                oSession = null;
                return false;
            }
        }

        public static void LogIn(string strUsername, string strPassword)
        {
            State.Session = new Session(1, DateTime.Now);
            State.User = new User(Guid.NewGuid(), strUsername, "Dave", "Russell", "dave@daveajrussell.com");
        }

    }
}
