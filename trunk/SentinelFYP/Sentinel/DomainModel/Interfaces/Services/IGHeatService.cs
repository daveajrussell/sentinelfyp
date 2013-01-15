using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET;

namespace DomainModel.Interfaces.Services
{
    public interface IGHeatService
    {
        Bitmap GetTile(List<PointLatLng> _points, string colourScheme, int zoom, int x, int y);
        Bitmap GetDot(int zoom);
        Bitmap GetColourScheme(string schemeName);
        string[] AvailableColourSchemes();
    }
}
