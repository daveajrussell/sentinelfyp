using Moq;
using Sentinel.Helpers.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Xunit;

namespace Sentinel.Tests.ExtensionMethodTests
{
    public class TileResultViewModel
    {
        private Mock<ControllerContext> _mock;

        public TileResultViewModel()
        {
            _mock = new Mock<ControllerContext>();
        }

        [Fact]
        public void TestTileResult()
        {
            var result = new TileResult(null);
        }
    }
}
