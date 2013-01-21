using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.GISModels;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Services
{
    public interface IHistoricalTrackingService
    {
        IEnumerable<HistoricalGeographicInformation> GetAllHistoricalTrackingDataByDriverKey(Guid oDriverKey);
        HistoricalGeographicInformation GetFilteredHistoricalDataByDriverKey(Guid oDriverKey, DateTime oRange);
        IEnumerable<User> GetDrivers();
    }
}
