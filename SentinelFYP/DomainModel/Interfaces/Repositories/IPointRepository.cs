using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System.Drawing;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface IPointRepository
    {
        List<PointLatLng> LoadSignalBlackspotPoints();
        List<PointLatLng> LoadActivityPoints(User oUser);

        PointLatLng[] GetList(GMap.NET.Point tlb, GMap.NET.Point lrb, int zoom, IEnumerable<PointLatLng> _points);
        GMap.NET.Point AdjustMapPixelsToTilePixels(GMap.NET.Point tileXYPoint, GMap.NET.Point mapPixelPoint);
        GMap.NET.Point[] GetPointsForTile(int x, int y, Bitmap dot, int zoom, IEnumerable<PointLatLng> _points);
    }
}
