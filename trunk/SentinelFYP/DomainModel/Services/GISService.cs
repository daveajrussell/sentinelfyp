using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Interfaces.Repositories;

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

        public void AddGIS(GIS oGIS)
        {
            if (oGIS == null)
                throw new ArgumentNullException("Geo Data");

            _gisRepository.AddGIS(oGIS);
        }

        public GIS GetGIS()
        {
            return _gisRepository.GetGIS();
        }

        public IEnumerable<GIS> GetAllGISData()
        {
            return _gisRepository.GetAllGISData();
        }
    }
}
