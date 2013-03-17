using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using System.Web.Mvc;
using DomainModel.Models.AuditModels;
using DomainModel.Models.AssetModels;
using PdfSharp.Drawing.BarCodes;
using Gma.QrCodeNet.Encoding;
using System.Drawing;
using Gma.QrCodeNet.Encoding.Windows.Controls;
using System.Drawing.Imaging;

namespace Sentinel.Helpers.ExtensionMethods
{
    public class PDFResult : ActionResult
    {
        private IEnumerable<AssignedDeliveryItem> _items;

        public PDFResult(IEnumerable<AssignedDeliveryItem> items)
        {
            if (items.Count() <= 0)
                throw new ArgumentNullException("Collection contains no elements");

            _items = items;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = context.HttpContext;

            // Create new PDF document
            PdfDocument document = new PdfDocument();

            // Set document info
            document.Info.Title = DateTime.Now.Ticks.ToString();
            document.Info.Author = State.User.UserName;
            document.Info.CreationDate = DateTime.Now;

            // Create a font
            XFont font = new XFont("Verdana", 12, XFontStyle.Bold);

            // Create QR objects
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            Renderer renderer = new Renderer(2, Brushes.Black, Brushes.White);

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            int _Y = 15;
            int count = 0;

            // Draw the text
            foreach (var item in _items)
            {
                count++;

                if ((count % 10) == 0)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    _Y = 15;
                }

                gfx.DrawString(item.RecipientFirstName, font, XBrushes.Black, new XRect(10, _Y += 15, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(item.RecipientLastName, font, XBrushes.Black, new XRect(10, _Y += 15, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(item.RecipientAddress, font, XBrushes.Black, new XRect(10, _Y += 15, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(item.RecipientTown, font, XBrushes.Black, new XRect(10, _Y += 15, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(item.RecipientPostCode, font, XBrushes.Black, new XRect(10, _Y += 15, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(item.DeliveryItemKey.ToString(), font, XBrushes.Black, new XRect(10, _Y += 15, page.Width, page.Height), XStringFormats.TopLeft);

                var qrString = item.DeliveryItemKey.ToString();

                Image qrImage;
                qrEncoder.TryEncode(qrString, out qrCode);

                using (MemoryStream oStream = new MemoryStream())
                {
                    renderer.WriteToStream(qrCode.Matrix, oStream, ImageFormat.Png);
                    qrImage = Bitmap.FromStream(oStream);
                }

                gfx.DrawImage(XImage.FromGdiPlusImage(qrImage), new XPoint(300, (_Y - 55)));
                
                qrImage.Dispose();

                _Y += 15;
            }

            // Send PDF to browser
            using (MemoryStream oStream = new MemoryStream())
            {
                httpContext.Response.Clear();
                httpContext.Response.ContentType = "application/pdf";
                httpContext.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks.ToString() + ".pdf");
                document.Save(oStream, false);
                oStream.WriteTo(httpContext.Response.OutputStream);
            }
        }
    }
}