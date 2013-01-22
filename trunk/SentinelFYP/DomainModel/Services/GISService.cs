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

        public void AddGIS(GeospatialInformation oGIS)
        {
            if (oGIS == null)
                throw new ArgumentNullException("Geo Data");

            _gisRepository.AddGIS(oGIS);
        }

        public GeospatialInformation GetGIS()
        {
            return _gisRepository.GetGIS();
        }

        public IEnumerable<GeospatialInformation> GetAllGISData()
        {
            return _gisRepository.GetAllGISData();
        }
    }
}
