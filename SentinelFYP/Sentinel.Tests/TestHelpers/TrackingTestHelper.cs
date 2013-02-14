using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using DomainModel.Models.GISModels;
using GMap.NET;

namespace Sentinel.Tests.TestHelpers
{
    public static class TrackingTestHelper
    {
        private static string strDirectory = "C:\\Work\\FYP\\SentinelFYP\\trunk\\SentinelFYP\\Sentinel\\Content\\etc\\";

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


        public static Bitmap GetDotMock(int zoom)
        {
            return new Bitmap(System.IO.Directory.GetFiles(strDirectory + "dots", "dot" + zoom + "." + ImageFormat.Png.ToString().ToLower()).First());
        }

        public static Bitmap GetColourSchemeMock(string colourScheme)
        {
            return new Bitmap(System.IO.Directory.GetFiles(strDirectory + "color-schemes", colourScheme + "." + ImageFormat.Png.ToString().ToLower()).First());
        }

        public static IEnumerable<ElapsedGeospatialInformation> MockGetAllLiveElapsedRoutes()
        {
            Guid oDriverKey = Guid.NewGuid();

            return new List<ElapsedGeospatialInformation>()
            {
                new ElapsedGeospatialInformation()
                {
                    DriverKey = oDriverKey,
                    SessionID = 1,
                    GeospatialInformation = new List<GeospatialInformation>()
                    {
                        new GeospatialInformation()
                        {
                            DriverKey = oDriverKey,
                            SessionID = 1,
                            TimeStamp = DateTime.Now,
                            Orientation = 1,
                            Speed = 0,
                            Latitude = (decimal)52.000000,
                            Longitude = (decimal)-2.800000
                        },
                       new GeospatialInformation()
                        {
                            DriverKey = oDriverKey,
                            SessionID = 1,
                            TimeStamp = DateTime.Now,
                            Orientation = 1,
                            Speed = 0,
                            Latitude = (decimal)52.000020,
                            Longitude = (decimal)-2.800000
                        },
                        new GeospatialInformation()
                        {
                            DriverKey = oDriverKey,
                            SessionID = 1,
                            TimeStamp = DateTime.Now,
                            Orientation = 1,
                            Speed = 0,
                            Latitude = (decimal)52.000030,
                            Longitude = (decimal)-2.800000
                        }
                    }
                }
            }.AsEnumerable();
        }
    }
}
