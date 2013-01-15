using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using GMap.NET;
using Sentinel.SqlDataAccess;
using System.Data;
using GMap.NET.Projections;
using SqlRepositories.Helper.Extensions;
using SqlRepositories.Helper.Builders;
using System.Drawing;
using System.Data.SqlClient;

namespace SqlRepositories
{
    public class SqlPointRepository : IPointRepository
    {
        private readonly string _connectionString;
        private const int SIZE = 256; // # size of (square) tile; NB: changing this will break gmerc calls!
        private const int MAX_ZOOM = 31; // # this depends on Google API; 0 is furthest out as of recent ver.#

        private readonly MercatorProjection _projection;

        public SqlPointRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connection string");

            _connectionString = connectionString;

            _projection = new MercatorProjection();
        }

        public List<PointLatLng> LoadPoints()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [GIS].[LATITUDE_LONGITUDE]"))
            {
                return (from row in oSet.FirstDataTableAsEnumerable()
                        select row.ToPointLatLng()).ToList();
            }
        }

        public GMap.NET.Point[] GetPointsForTile(int x, int y, Bitmap dot, int zoom, IEnumerable<PointLatLng> _points)
        {
            List<GMap.NET.Point> points = new List<GMap.NET.Point>();
            GMap.NET.Size maxTileSize;
            GMap.NET.Point adjustedPoint;
            GMap.NET.Point pixelCoordinate;
            GMap.NET.Point mapPoint;

            maxTileSize = _projection.GetTileMatrixMaxXY(zoom);
            //Top Left Bounds
            GMap.NET.Point tlb = _projection.FromTileXYToPixel(new GMap.NET.Point(x, y));

            maxTileSize = new GMap.NET.Size(SIZE, SIZE);
            //Lower right bounds
            GMap.NET.Point lrb = new GMap.NET.Point((tlb.X + maxTileSize.Width) + dot.Width, (tlb.Y + maxTileSize.Height) + dot.Width);

            //pad the Top left bounds
            tlb = new GMap.NET.Point(tlb.X - dot.Width, tlb.Y - dot.Height);


            //Go throught the list and convert the points to pixel cooridents
            foreach (GMap.NET.PointLatLng llPoint in GetList(tlb, lrb, zoom, _points))
            {
                //Now go through the list and turn it into pixel points
                pixelCoordinate = _projection.FromLatLngToPixel(llPoint.Lat, llPoint.Lng, zoom);

                //Make sure the weight and data is still pointing after the conversion
                pixelCoordinate.Data = llPoint.Data;
                pixelCoordinate.Weight = llPoint.Weight;

                mapPoint = _projection.FromPixelToTileXY(pixelCoordinate);
                mapPoint.Data = pixelCoordinate.Data;

                //Adjust the point to the specific tile
                adjustedPoint = AdjustMapPixelsToTilePixels(new GMap.NET.Point(x, y), pixelCoordinate);

                //Make sure the weight and data is still pointing after the conversion
                adjustedPoint.Data = pixelCoordinate.Data;
                adjustedPoint.Weight = pixelCoordinate.Weight;

                //Add the point to the list
                points.Add(adjustedPoint);
            }

            return points.ToArray();
        }

        public PointLatLng[] GetList(GMap.NET.Point tlb, GMap.NET.Point lrb, int zoom, IEnumerable<PointLatLng> _points)
        {
            IEnumerable<PointLatLng> _llList;

            GMap.NET.PointLatLng ptlb;
            GMap.NET.PointLatLng plrb;

            ptlb = _projection.FromPixelToLatLng(tlb, zoom);
            plrb = _projection.FromPixelToLatLng(lrb, zoom);

            _llList = from point in _points
                     where
                            point.Lat <= ptlb.Lat && point.Lng >= ptlb.Lng &&
                            point.Lat >= plrb.Lat && point.Lng <= plrb.Lng
                     select point;

            return _llList.ToArray();
        }

        public GMap.NET.Point AdjustMapPixelsToTilePixels(GMap.NET.Point tileXYPoint, GMap.NET.Point mapPixelPoint)
        {
            return new GMap.NET.Point(mapPixelPoint.X - (tileXYPoint.X * SIZE), mapPixelPoint.Y - (tileXYPoint.Y * SIZE));
        }

        public void AddLocation(decimal latitude, decimal longitude)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_LATITUDE", latitude),
                new SqlParameter("@IP_LONGITUDE", longitude)
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "dbo.INSERT_LOCATION", arrParams);
        }
    }
}
