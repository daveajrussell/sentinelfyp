using System;
using System.ServiceModel;
using Xunit;

namespace WebServices.Test.Tests
{
    public class DeliveryServiceTests
    {

        private DeliveryServiceClient client;

        public DeliveryServiceTests()
        {
            client = new DeliveryServiceClient();
        }
        
        [Fact]
        public void SubmittingNullDeliveryInformationShouldThrow()
        {
            Xunit.Assert.Throws<FaultException<ExceptionDetail>>(() => client.SubmitGeoTaggedDelivery(null));
        }

        [Fact]
        public void SubmittingNullLocationReferenceShouldThrow()
        {
            Xunit.Assert.Throws<FaultException<ExceptionDetail>>(() => client.SubmitGeoTaggedDelivery(null));
        }

        [Fact]
        public void SubmittingValidJSONStringShouldNotThrow()
        {
            Xunit.Assert.DoesNotThrow(() => client.SubmitGeoTaggedDelivery("{\"TestData\": \"Test\"}"));
        }

        [Fact]
        public void SubmittingNullDeliveryItemIDShouldThrow()
        {
            Xunit.Assert.Throws<FaultException<ExceptionDetail>>(() => client.GetDeliveryInformation(null));
        }

        [Fact]
        public void SubmittingValidDeliveryItemIDShouldWork()
        {
            Xunit.Assert.DoesNotThrow(() => client.GetDeliveryInformation("{\"TestData\": \"Test\"}"));
        }

        [Fact]
        public void GetDeliveryInformationReturnsInformationJSONString()
        {
            var result = client.GetDeliveryInformation("{\"TestData\": \"Test\"}");

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<string>(result);
        }
    }
}
