using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Interfaces.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DomainModel.Models;
using GMap.NET;

namespace DomainModel.Services
{
    public class GHeatService : IGHeatService
    {
        /// <summary>
        /// Dots folder
        /// </summary>
        public const string DOTS_FOLDER = "dots";
        /// <summary>
        /// Color scheme folder name
        /// </summary>
        public const string COLOR_SCHMES_FOLDER = "color-schemes";

        private readonly ISettingsService _settingsService;
        private readonly IPointService _pointService;
        private Dictionary<string, Bitmap> _dotsList;
        private Dictionary<string, Bitmap> _colourSchemeList;

        public GHeatService(ISettingsService settingsService, IPointService pointService)
        {
            _settingsService = settingsService;

            _pointService = pointService;

            string directory = _settingsService.BaseDirectory;

            _dotsList = new Dictionary<string, Bitmap>();
            _colourSchemeList = new Dictionary<string, Bitmap>();

            foreach (string file in System.IO.Directory.GetFiles(directory + DOTS_FOLDER, "*." + ImageFormat.Png.ToString().ToLower()))
                _dotsList.Add(Path.GetFileName(file), new Bitmap(file));

            foreach (string file in System.IO.Directory.GetFiles(directory + COLOR_SCHMES_FOLDER, "*." + ImageFormat.Png.ToString().ToLower()))
                _colourSchemeList.Add(Path.GetFileName(file), new Bitmap(file));
        }

        public Bitmap GetTile(List<PointLatLng> _points, string colourScheme, int zoom, int x, int y)
        {
            //Do a little error checking
            if (colourScheme == string.Empty) throw new Exception("A color scheme is required");
            return Tile.Generate(GetColourScheme(colourScheme), GetDot(zoom), zoom, x, y, _pointService.GetPointsForTile(x, y, GetDot(zoom), zoom, _points));
        }

        public Bitmap GetDot(int zoom)
        {
            return _dotsList["dot" + zoom.ToString() + "." + ImageFormat.Png.ToString().ToLower()];
        }

        public Bitmap GetColourScheme(string schemeName)
        {
            if (!_colourSchemeList.ContainsKey(schemeName + "." + ImageFormat.Png.ToString().ToLower()))
                throw new Exception("Color scheme '" + schemeName + " could not be found");
            return _colourSchemeList[schemeName + "." + ImageFormat.Png.ToString().ToLower()];
        }

        public string[] AvailableColourSchemes()
        {
            List<string> colourSchemes = new List<string>();

            //I dont want to return the file extention just the name
            foreach (string key in _colourSchemeList.Keys)
                colourSchemes.Add(key.Replace("." + ImageFormat.Png.ToString().ToLower(), ""));
            return colourSchemes.ToArray(); 
        }
    }
}