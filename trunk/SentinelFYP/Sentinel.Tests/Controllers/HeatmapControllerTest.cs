using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DomainModel.Interfaces.Services;
using Sentinel.Controllers;

namespace Sentinel.Tests.Controllers
{
    [TestClass]
    public class HeatmapControllerTest
    {
        private Mock<IPointService> _pointService;
        private Mock<IGHeatService> _gheatService;

        [TestInitialize]
        public void Init()
        {
            _pointService = new Mock<IPointService>();
            _gheatService = new Mock<IGHeatService>();
        }

        [TestMethod]
        public void IndexReturnsActionResult()
        {
            var controller = new HeatmapController(_pointService.Object, _gheatService.Object);

            var result = controller.Index();

            Assert.IsNotNull(result);
        }
    }
}
