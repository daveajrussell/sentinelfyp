using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Interfaces.Services;
using GMap.NET;
using GMap.NET.Projections;
using DomainModel.Interfaces;
using System.Drawing;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models;

namespace DomainModel.Services
{
    public class PointService : IPointService
    {
        private const int SIZE = 256; // # size of (square) tile; NB: changing this will break gmerc calls!
        private const int MAX_ZOOM = 31; // # this depends on Google API; 0 is furthest out as of recent ver

        private readonly IPointRepository _pointRepository;

        public PointService(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public List<PointLatLng> LoadPoints()
        {
            return _pointRepository.LoadPoints();
        }

        public GMap.NET.Point AdjustMapPixelsToTilePixels(GMap.NET.Point tileXYPoint, GMap.NET.Point mapPixelPoint)
        {
            return _pointRepository.AdjustMapPixelsToTilePixels(tileXYPoint, mapPixelPoint);
        }

        public GMap.NET.Point[] GetPointsForTile(int x, int y, Bitmap dot, int zoom, List<PointLatLng> _points)
        {
            return _pointRepository.GetPointsForTile(x, y, dot, zoom, _points);
        }


        public void AddLocation(GIS location)
        {
            _pointRepository.AddLocation(location.Latitude, location.Longitude);
        }
    }
}