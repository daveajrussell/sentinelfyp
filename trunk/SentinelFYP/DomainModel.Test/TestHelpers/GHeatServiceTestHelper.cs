using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using DomainModel.Models;
using DomainModel.Models.GISModels;
using GMap.NET;

namespace DomainModel.Test.TestHelpers
{
    public static class GHeatServiceTestHelper
    {
        private static string strDirectory = "C:\\Work\\FYP\\SentinelFYP\\trunk\\SentinelFYP\\Sentinel\\Content\\etc\\";

        public static List<PointLatLng> GetListOfMockPointLatLng()
        {
            return new List<PointLatLng>() 
            {
                new PointLatLng(1, 1),
                new PointLatLng(1, 2)
            };
        }

        public static string[] AvailableColourSchemesMock()
        {
            return new string[]
            {
                "classic.png",
                "fire.png",
                "omg.png",
                "pbj.png",
                "pgaglitch.png"
            };
        }

        public static Bitmap GetColourSchemeMock(string colourScheme)
        {
            return new Bitmap(System.IO.Directory.GetFiles(strDirectory + "color-schemes", colourScheme + "." + ImageFormat.Png.ToString().ToLower()).First());
        }

        public static Bitmap GetDotMock(int zoom)
        {
            return new Bitmap(System.IO.Directory.GetFiles(strDirectory + "dots", "dot" + zoom + "." + ImageFormat.Png.ToString().ToLower()).First());
        }

        public static Bitmap GetTileMock(List<PointLatLng> points, string colourScheme, int zoom, int x, int y)
        {
            var _points = new GMap.NET.Point[]
            {
                new GMap.NET.Point(1, 1),
                new GMap.NET.Point(1, 2),
                new GMap.NET.Point(1, 3),
                new GMap.NET.Point(1, 4),
                new GMap.NET.Point(1, 5),
                new GMap.NET.Point(1, 6),
            };

            return Tile.Generate(GetColourSchemeMock(colourScheme), GetDotMock(zoom), zoom, x, y, _points);
        }
    }
}
