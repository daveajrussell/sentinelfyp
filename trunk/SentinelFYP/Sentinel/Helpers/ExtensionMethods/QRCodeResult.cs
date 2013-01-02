using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Controls;

namespace Sentinel.Helpers.ExtensionMethods
{
    public class QRCodeResult : ActionResult
    {
        private string _qrString { get; set; }

        public QRCodeResult(string qrString)
        {
            _qrString = qrString;
        }

        public override void ExecuteResult(ControllerContext context)
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
                HttpContextBase httpContext = context.HttpContext;

                httpContext.Response.Clear();
                httpContext.Response.ContentType = "image/png";

                qrImage.Save(oStream, ImageFormat.Png);
                qrImage.Dispose();

                oStream.WriteTo(httpContext.Response.OutputStream);
            }
        }
    }
}