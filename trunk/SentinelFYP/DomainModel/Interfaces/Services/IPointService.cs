using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System.Drawing;
using DomainModel.Models;

namespace DomainModel.Interfaces.Services
{
    public interface IPointService
    {
        List<PointLatLng> LoadPoints();
        GMap.NET.Point[] GetPointsForTile(int x, int y, Bitmap bitmap, int zoom, List<PointLatLng> _points);
        GMap.NET.Point AdjustMapPixelsToTilePixels(GMap.NET.Point tileXYPoint, GMap.NET.Point mapPixelPoint);
        void AddLocation(GIS location);
    }
}
