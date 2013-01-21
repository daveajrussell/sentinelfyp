using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;
using DomainModel.Models.GISModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface IGISRepository
    {
        GeographicInformation GetGIS();
        void AddGIS(GeographicInformation oGIS);
        IEnumerable<GeographicInformation> GetAllGISData();
    }
}
