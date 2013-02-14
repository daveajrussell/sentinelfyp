using System;
using System.ServiceModel;
using DomainModel.Abstracts;
using WebServices.DataContracts;
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

        [Fact]
        public void TestSubmittingMalformedJsonShouldThrow()
        {
            string strMalformedJson = "{\"RUBBSIH\"}";
            Assert.Throws<ProtocolException>(() => client.GeoTagDelivery(strMalformedJson));
        }

        [Fact]
        public void TestSubmittingNonExistentDeliveryItemShouldThrow()
        {
            var oAsset = new GeotaggedAssetDataContract()
            {
                oAssetKey = Guid.NewGuid().ToString(),
                iSessionID = new Random().Next(),
                oUserIdentification = Guid.NewGuid().ToString(),
                lTimeStamp = DateTime.Now.Millisecond,
                dLatitude = 52.800000M,
                dLongitude = -2.000000M,
                dSpeed = 0,
                iOrientation = 1
            };
            string strInvalidDeliveryItemJson = JsonR.JsonSerializer(oAsset);

            
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
