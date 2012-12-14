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
                throw new ArgumentNullException("gis repository");

            _gisRepository = gisRepository;
        }

        public GIS GetGIS()
        {
            return _gisRepository.GetGIS();
        }


        public void AddGIS(GIS oGIS)
        {
            _gisRepository.AddGIS(oGIS);
        }


        public IEnumerable<GIS> GetAllGISData()
        {
            return _gisRepository.GetAllGISData();
        }
    }
}
