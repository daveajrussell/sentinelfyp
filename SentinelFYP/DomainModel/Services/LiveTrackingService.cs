using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models.GISModels;
using DomainModel.SecurityModels;

namespace DomainModel.Services
{
    public class LiveTrackingService : ILiveTrackingService
    {
        private ILiveTrackingRepository _liveTrackingRepository;

        public LiveTrackingService(ILiveTrackingRepository liveTrackingRepository)
        {
            _liveTrackingRepository = liveTrackingRepository;
        }

        public IEnumerable<User> GetLiveDrivers()
        {
            return _liveTrackingRepository.GetLiveDrivers();
        }

        public GeospatialInformation GetLiveUpdate(Guid oUserKey)
        {
            return _liveTrackingRepository.GetLiveUpdate(oUserKey);
        }

        public IEnumerable<GeospatialInformation> GetLiveElapsedRoute(Guid oUserKey)
        {
            return _liveTrackingRepository.GetLiveElapsedRoute(oUserKey);
        }

        public IEnumerable<ElapsedGeospatialInformation> GetAllLiveElapsedRoutes()
        {
            return _liveTrackingRepository.GetAllLiveElapsedRoutes();
        }
    }
}
