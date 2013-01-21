﻿using System;
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
using DomainModel.Models.GISModels;

namespace DomainModel.Services
{
    public class PointService : IPointService
    {
        private readonly IPointRepository _pointRepository;

        public PointService(IPointRepository pointRepository)
        {
            if (pointRepository == null)
                throw new ArgumentNullException("Point Repository");

            _pointRepository = pointRepository;
        }

        public List<PointLatLng> LoadPoints()
        {
            return _pointRepository.LoadPoints();
        }

        public GMap.NET.Point AdjustMapPixelsToTilePixels(GMap.NET.Point tileXYPoint, GMap.NET.Point mapPixelPoint)
        {
            if (tileXYPoint == null)
                throw new ArgumentNullException("Tile X Y Point");

            if (mapPixelPoint == null)
                throw new ArgumentNullException("Map Pixel Point");

            return _pointRepository.AdjustMapPixelsToTilePixels(tileXYPoint, mapPixelPoint);
        }

        public GMap.NET.Point[] GetPointsForTile(int x, int y, Bitmap dot, int zoom, List<PointLatLng> _points)
        {
            if (dot == null)
                throw new ArgumentNullException("Dot");
            if (_points == null)
                throw new ArgumentNullException("Points");

            return _pointRepository.GetPointsForTile(x, y, dot, zoom, _points);
        }

        public void AddLocation(GeographicInformation location)
        {
            if (location == null)
                throw new ArgumentNullException("Location");

            _pointRepository.AddLocation(location.Latitude, location.Longitude);
        }
    }
}