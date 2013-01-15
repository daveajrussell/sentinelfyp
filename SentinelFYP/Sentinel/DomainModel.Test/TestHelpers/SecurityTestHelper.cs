using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.SecurityModels;

namespace DomainModel.Test.TestHelpers
{
    public static class SecurityTestHelper
    {
        public static void MockLogin(string username, string password, out User user, out Session session)
        {
            user = new User();
            session = new Session();
        }
    }
}
