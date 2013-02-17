using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sentinel.Services;
using Xunit;

namespace Sentinel.Tests.Services
{
    public class NotifierServiceTest
    {
        private NotifierService client;

        public NotifierServiceTest()
        {
            client = new NotifierService();
        }

        [Fact]
        public void TestGISNotify()
        {
            throw new NotImplementedException();
        }
    }
}
