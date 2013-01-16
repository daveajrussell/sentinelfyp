﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;
using DomainModel.Models.GISModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface IGISRepository
    {
        GIS GetGIS();
        void AddGIS(GIS oGIS);
        IEnumerable<GIS> GetAllGISData();
    }
}
