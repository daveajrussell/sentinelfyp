using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.GISModels;

namespace DomainModel.Interfaces.Services
{
    public interface IHistoricalTrackingService
    {
        IEnumerable<HistoricalGeographicInformation> GetAllHistoricalTrackingDataByDriverKey(Guid oDriverKey);
    }
}
