using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Controls;

namespace Sentinel.Handlers
{
    /// <summary>
    /// Summary description for QRHandler
    /// </summary>
    public class QRHandler : IHttpHandler
    {
        private string _qrString = "http://www.daveajrussell.com";

        public void ProcessRequest(HttpContext context)
        {
            if (string.IsNullOrEmpty(_qrString))
                throw new ArgumentNullException("QR String");

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            Image qrImage;

            qrEncoder.TryEncode(_qrString, out qrCode);

            Renderer renderer = new Renderer(5, Brushes.Black, Brushes.White);

            using (MemoryStream oStream = new MemoryStream())
            {
                renderer.WriteToStream(qrCode.Matrix, oStream, ImageFormat.Png);
                qrImage = Bitmap.FromStream(oStream);
            }

            using (MemoryStream oStream = new MemoryStream())
            {
                context.Response.Clear();
                context.Response.ContentType = "image/png";

                qrImage.Save(oStream, ImageFormat.Png);
                qrImage.Dispose();

                oStream.WriteTo(context.Response.OutputStream);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}