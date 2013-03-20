using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;
using DomainModel.Models.GISModels;

namespace DomainModel.Interfaces.Services
{
    public interface IGISService
    {
        void AddGeospatialInformation(GeospatialInformation oGeoInformation);
        void AddGeospatialInformationSet(IEnumerable<GeospatialInformation> oGeoInformationSet);
        void AddHistoricalInformation(GeospatialInformation oGeoInformation);
        void AddHistoricalGeospatialInformationSet(IEnumerable<GeospatialInformation> oHistoricalGeoInformationSet);

        /*GeospatialInformation GetGIS();
        void AddGIS(GeospatialInformation oGIS);
        IEnumerable<GeospatialInformation> GetAllGISData();*/
    }
}
