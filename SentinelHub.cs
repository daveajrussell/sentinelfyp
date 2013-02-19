using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Sentinel.Hubs
{
    public class SentinelHub : Hub
    {
        public void AddGroup(string strGroupID)
        {
            Groups.Add(Context.ConnectionId, strGroupID);
        }
    }
}
