using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DomainModel.Abstracts;
using DomainModel.Models;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Sentinel.Hubs;

namespace Sentinel.Services
{
    public class NotifierService : INotifierService
    {
        public void GISNotify(string message)
        {
            GISDataContract obj = JsonR.JsonDeserializer<GISDataContract>(message);
            GIS oGIS = new GIS { TimeStamp = new DateTime(1970, 1, 1).AddMilliseconds(obj.lngTimeStamp), Latitude = obj.dLatitude, Longitude = obj.dLongitude, Speed = obj.dSpeed, Orientation = obj.intOrientation };

            string strSeverity;

            if (oGIS.Speed <= 0)
                strSeverity = "caution";
            else if (oGIS.Orientation != 1)
                strSeverity = "severe";
            else
                strSeverity = "normal";

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SentinelHub>();
            context.Clients.Group("00000000-0000-0000-0000-000000000000", null).getGISMessage(oGIS.TimeStamp.ToShortDateString(), oGIS.Latitude, oGIS.Longitude, oGIS.Speed, oGIS.Orientation, strSeverity);
        }
    }
}
