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
        private string _qrString = "http://webservices.daveajrussell.com/Services/DeliveryService.svc/GetDeliveryInformation";

        public void ProcessRequest(HttpContext context)
        {
            string strKey = context.Request.QueryString["Key"];

            if (string.IsNullOrEmpty(_qrString))
                throw new ArgumentNullException("QR String");

            if(!string.IsNullOrEmpty(strKey)) 
            {
                _qrString = HttpUtility.HtmlEncode(_qrString + "/" + strKey);
            }

            QrEncoder qrEncoder = new QrEncoder();
            QrCode qrCode = qrEncoder.Encode(_qrString);

            Renderer renderer = new Renderer(3, Brushes.Black, Brushes.White);

            using (MemoryStream oStream = new MemoryStream())
            {
                renderer.WriteToStream(qrCode.Matrix, oStream, ImageFormat.Png);
                using (Image qrImage = Bitmap.FromStream(oStream))
                {
                    context.Response.Clear();
                    context.Response.ContentType = "image/png";

                    qrImage.Save(oStream, ImageFormat.Png);
                    oStream.WriteTo(context.Response.OutputStream);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /*
        private AsyncProcessorDelegate _Delegate;
        protected delegate void AsyncProcessorDelegate(HttpContext context);

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            _Delegate = new AsyncProcessorDelegate(ProcessRequest);
            return _Delegate.BeginInvoke(context, cb, extraData);
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            _Delegate.EndInvoke(result);
        }*/
    }
}