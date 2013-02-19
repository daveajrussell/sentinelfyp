using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Sentinel.Infrastructure
{
    public interface ISentinelAuthProvider
    {
        bool Authenticate(string strUsername, string strPassword);
        void ClearAuthentication();
    }
}
