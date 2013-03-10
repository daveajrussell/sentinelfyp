using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.GISModels;
using Sentinel.Services;
using Xunit;

namespace Sentinel.Tests.Services
{
    public class SeverityHelperTest
    {
        [Fact]
        public void TestUpdateShouldBeCautionaryFromOrientation()
        {
            var data = new GeospatialInformation() { DriverKey = Guid.NewGuid(), Speed = 25, Orientation = 0 };
            var result = SeverityHelper.Severity(data);

            Assert.Equal(SeverityHelper.CAUTION, result);
        }

        [Fact]
        public void TestUpdateShouldBeSevereFromOrientationAndSpeed()
        {
            var data = new GeospatialInformation() { DriverKey = Guid.NewGuid(), Speed = 0, Orientation = 2 };
            var result = SeverityHelper.Severity(data);

            Assert.Equal(SeverityHelper.SEVERE, result);
        }

        [Fact]
        public void TestUpdateShouldBeServereFromSpeed()
        {
            var data = new GeospatialInformation() { DriverKey = Guid.NewGuid(), Speed = 61 };
            var result = SeverityHelper.Severity(data);

            Assert.Equal(SeverityHelper.SEVERE, result);
        }

        [Fact]
        public void TestUpdateShouldBeSevere()
        {
            var data = new GeospatialInformation() { DriverKey = Guid.NewGuid(), Speed = 0 };
            var result = SeverityHelper.Severity(data);

            Assert.Equal(SeverityHelper.SEVERE, result);
        }

        [Fact]
        public void TestUpdateShouldBeNormal()
        {
            var data = new GeospatialInformation() { DriverKey = Guid.NewGuid(), Speed = 25, Orientation = 1 };
            var result = SeverityHelper.Severity(data);

            Assert.Equal(SeverityHelper.NORMAL, result);
        }
    }
}
