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
        public void TestAuthenticationServiceClientIsNotNull()
        {
            Assert.NotNull(client);
            Assert.IsAssignableFrom<DeliveryServiceClient>(client);
            Assert.NotNull(client.Endpoint);
        }
        
        /*
        [Fact]
        public void SubmittingNullDeliveryInformationShouldThrow()
        {
            Assert.Throws<FaultException<ExceptionDetail>>(() => client.SubmitGeoTaggedDelivery(null));
        }

        [Fact]
        public void SubmittingNullLocationReferenceShouldThrow()
        {
            Assert.Throws<FaultException<ExceptionDetail>>(() => client.SubmitGeoTaggedDelivery(null));
        }

        [Fact]
        public void SubmittingValidJSONStringShouldNotThrow()
        {
            Assert.DoesNotThrow(() => client.SubmitGeoTaggedDelivery("{\"TestData\": \"Test\"}"));
        }

        [Fact]
        public void SubmittingNullDeliveryItemIDShouldThrow()
        {
            Assert.Throws<FaultException<ExceptionDetail>>(() => client.GetDeliveryInformation(null));
        }

        [Fact]
        public void SubmittingValidDeliveryItemIDShouldNotThrow()
        {
            Assert.DoesNotThrow(() => client.GetDeliveryInformation("{\"TestData\": \"Test\"}"));
        }

        [Fact]
        public void GetDeliveryInformationReturnsInformationJSONString()
        {
            var result = client.GetDeliveryInformation("{\"TestData\": \"Test\"}");

            Assert.NotNull(result);
            Assert.IsAssignableFrom<string>(result);
        }
        */ 
    }
}
