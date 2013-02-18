using Moq;
using Sentinel.Helpers.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Xunit;

namespace Sentinel.Tests.ExtensionMethodTests
{
    public class QRCodeHtmlHelperTest
    {
        public QRCodeHtmlHelperTest()
        {
        }

        [Fact]
        public void TestQRCode()
        {
            var data = Guid.NewGuid().ToString();
            HtmlHelper helper = null;
            var result = QRCodeHtmlHelper.QRCode(helper, data);
            var stringResult = result.ToHtmlString();

            Assert.NotNull(result);
            Assert.NotNull(stringResult);

            Assert.IsAssignableFrom<MvcHtmlString>(result);
            Assert.True(stringResult.Contains("<img src=\"http://chart.apis.google.com/chart?cht=qr&amp;chs=150x150&amp;chl="));
            Assert.True(stringResult.Contains(data));
        }

        [Fact]
        public void TestQRCodeWithInvalidGuid()
        {
            var data = "RUBBISH";
            HtmlHelper helper = null;
            var result = QRCodeHtmlHelper.QRCode(helper, data);

            Assert.Null(result);
        }
    }
}
