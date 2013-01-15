using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace Sentinel.Helpers.ExtensionMethods
{
    public class TileResult : ActionResult
    {
        private Bitmap _tile { get; set; }

        public TileResult(Image tile)
        {
            _tile = new Bitmap(tile);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (_tile == null)
                throw new ArgumentNullException("Tile");

            using (MemoryStream stream = new MemoryStream())
            {
                HttpContextBase httpContext = context.HttpContext;

                httpContext.Response.Clear();
                httpContext.Response.ContentType = "image/png";

                _tile.Save(stream, ImageFormat.Png);
                _tile.Dispose();

                stream.WriteTo(httpContext.Response.OutputStream);
            }
        }
    }
}