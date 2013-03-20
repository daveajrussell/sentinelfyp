using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.GISModels;

namespace DomainModel.Services
{
    public class GISService : IGISService
    {
        private IGISRepository _gisRepository;

        public GISService(IGISRepository gisRepository)
        {
            if (gisRepository == null)
                throw new ArgumentNullException("GIS repository");

            _gisRepository = gisRepository;
        }

        public void AddGeospatialInformation(GeospatialInformation oGeoInformation)
        {
            _gisRepository.AddGeospatialInformation(oGeoInformation);
        }

        public void AddGeospatialInformationSet(IEnumerable<GeospatialInformation> oGeoInformationSet)
        {
            _gisRepository.AddGeospatialInformationSet(oGeoInformationSet);
        }

        public void AddHistoricalInformation(GeospatialInformation oGeoInformation)
        {
            _gisRepository.AddHistoricalInformation(oGeoInformation);
        }

        public void AddHistoricalGeospatialInformationSet(IEnumerable<GeospatialInformation> oHistoricalGeoInformationSet)
        {
            _gisRepository.AddHistoricalGeospatialInformationSet(oHistoricalGeoInformationSet);
        }
    }
}
