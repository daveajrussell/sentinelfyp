using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.GISModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface IHistoricalTrackingRepository
    {
        IEnumerable<HistoricalGeographicInformation> GetAllHistoricalTrackingDataByDriverKey(Guid oDriverKey);
    }
}
