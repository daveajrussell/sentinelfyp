using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models.GISModels;

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

        public IEnumerable<HistoricalGeographicInformation> GetAllHistoricalTrackingDataByDriverKey(Guid oDriverKey)
        {
            return _trackingRepository.GetAllHistoricalTrackingDataByDriverKey(oDriverKey);
        }
    }
}
