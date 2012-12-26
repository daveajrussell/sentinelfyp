using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GMap.NET;

namespace DomainModel.Interfaces.Repositories
{
    public interface IGHeatRepository
    {
        Bitmap GetTile(List<PointLatLng> _points, string colourScheme, int zoom, int x, int y);
        Bitmap GetDot(int zoom);
        Bitmap GetColourScheme(string schemeName);
        string[] AvailableColourSchemes();
    }
}
