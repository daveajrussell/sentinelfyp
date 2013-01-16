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
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.GISModels;

namespace DomainModel.Services
{
    public class GHeatRepository : IGHeatRepository
    {
        public const string DOTS_FOLDER = "dots";
        public const string COLOR_SCHMES_FOLDER = "color-schemes";

        private readonly ISettingsRepository _settingsRepository;
        private readonly IPointRepository _pointRepository;

        private Dictionary<string, Bitmap> _dotsList;
        private Dictionary<string, Bitmap> _colourSchemeList;

        public GHeatRepository(ISettingsRepository settingsRepository, IPointRepository pointRepository)
        {
            if (settingsRepository == null)
                throw new ArgumentNullException("Settings");

            _settingsRepository = settingsRepository;

            if (pointRepository == null)
                throw new ArgumentNullException("Point");

            _pointRepository = pointRepository;

            string directory = _settingsRepository.BaseDirectory;

            if (string.IsNullOrEmpty(directory))
                throw new ArgumentNullException("Base Directroy");

            _dotsList = new Dictionary<string, Bitmap>();
            _colourSchemeList = new Dictionary<string, Bitmap>();

            foreach (string file in System.IO.Directory.GetFiles(directory + DOTS_FOLDER, "*." + ImageFormat.Png.ToString().ToLower()))
                _dotsList.Add(Path.GetFileName(file), new Bitmap(file));

            foreach (string file in System.IO.Directory.GetFiles(directory + COLOR_SCHMES_FOLDER, "*." + ImageFormat.Png.ToString().ToLower()))
                _colourSchemeList.Add(Path.GetFileName(file), new Bitmap(file));
        }

        public Bitmap GetTile(List<PointLatLng> _points, string colourScheme, int zoom, int x, int y)
        {
            return Tile.Generate(GetColourScheme(colourScheme), GetDot(zoom), zoom, x, y, _pointRepository.GetPointsForTile(x, y, GetDot(zoom), zoom, _points));
        }

        public Bitmap GetDot(int zoom)
        {
            return _dotsList["dot" + zoom.ToString() + "." + ImageFormat.Png.ToString().ToLower()];
        }

        public Bitmap GetColourScheme(string schemeName)
        {
            return _colourSchemeList[schemeName + "." + ImageFormat.Png.ToString().ToLower()];
        }

        public string[] AvailableColourSchemes()
        {
            List<string> colourSchemes = new List<string>();

            foreach (string key in _colourSchemeList.Keys)
                colourSchemes.Add(key.Replace("." + ImageFormat.Png.ToString().ToLower(), ""));

            return colourSchemes.ToArray(); 
        }
    }
}