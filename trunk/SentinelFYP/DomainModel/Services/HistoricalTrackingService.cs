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
    public class HistoricalTrackingService : IHistoricalTrackingService
    {
        private IHistoricalTrackingRepository _trackingRepository;

        public HistoricalTrackingService(IHistoricalTrackingRepository trackingRepository)
        {
            if (trackingRepository == null)
                throw new ArgumentNullException("tracking repository");

            _trackingRepository = trackingRepository;
        }

        public IEnumerable<HistoricalGeospatialInformation> GetAllHistoricalTrackingDataByDriverKey(Guid oDriverKey)
        {
            return _trackingRepository.GetAllHistoricalTrackingDataByDriverKey(oDriverKey);
        }


        public HistoricalGeospatialInformation GetFilteredHistoricalDataByDriverKey(Guid oDriverKey, int iSessionID)
        {
            return _trackingRepository.GetFilteredHistoricalDataByDriverKey(oDriverKey, iSessionID);
        }

        public IEnumerable<User> GetDrivers()
        {
            return _trackingRepository.GetDrivers();
        }
    }
}
