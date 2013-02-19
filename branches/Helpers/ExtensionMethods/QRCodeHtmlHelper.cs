using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sentinel.Helpers.ExtensionMethods
{
    public static class QRCodeHtmlHelper
    {
        public static MvcHtmlString QRCode(this HtmlHelper htmlHelper, string data)
        {
            Guid guid;

            if (Guid.TryParse(data, out guid))
            {
                var qrString = "http://webservices.daveajrussell.com/Services/DeliveryService.svc/GetDeliveryInformation/";

                var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs=150x150&chl={0}", HttpUtility.UrlEncode(qrString + guid.ToString()));

                var tag = new TagBuilder("img");
                tag.Attributes.Add("src", url);

                return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
            }
            else
            {
                return null;
            }
        }
    }
}