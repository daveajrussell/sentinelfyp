using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.GISModels;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface ILiveTrackingRepository
    {
        IEnumerable<User> GetLiveDrivers();
        GeospatialInformation GetLiveUpdate(Guid oUserKey, int iSessionID);
        IEnumerable<GeospatialInformation> GetLiveElapsedRoute(Guid oUserKey, int iSessionID);
    }
}
