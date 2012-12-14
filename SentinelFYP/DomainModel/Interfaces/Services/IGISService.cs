using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;

namespace DomainModel.Interfaces.Services
{
    public interface IGISService
    {
        GIS GetGIS();
        void AddGIS(GIS oGIS);
        IEnumerable<GIS> GetAllGISData();
    }
}
