using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;

namespace DomainModel.Test.TestHelpers
{
    public static class PointServiceTestHelper
    {
        private const int SIZE = 256; // # size of (square) tile; NB: changing this will break gmerc calls!

        public static List<PointLatLng> MockPointLatLngList()
        {
            return new List<PointLatLng>()
            {
                new PointLatLng(123.000000, -14.000000),
                new PointLatLng(124.000000, -14.000000)
            };
        }

        public static Point MockPoint(Point tileXYPoint, Point mapPixelPoint)
        {
            return new GMap.NET.Point(mapPixelPoint.X - (tileXYPoint.X * SIZE), mapPixelPoint.Y - (tileXYPoint.Y * SIZE));
        }

        public static Point[] GetPointsForTileMock(int x, int y)
        {
            return new List<Point>()
            {
                new Point(12, 12), 
                new Point(13, 13)
            }.ToArray();
        }
    }
}
