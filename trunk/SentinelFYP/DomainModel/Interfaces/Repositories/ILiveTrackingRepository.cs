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
        IEnumerable<User> GetLiveDrivers(User oUser);
        GeospatialInformation GetLiveUpdate(Guid oUserKey);
        IEnumerable<GeospatialInformation> GetLiveElapsedRoute(Guid oUserKey);
        IEnumerable<ElapsedGeospatialInformation> GetAllLiveElapsedRoutes(User oUser);
    }
}
