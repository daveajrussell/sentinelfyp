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
                oUserIdentification = Guid.NewGuid().ToString(),
                lTimeStamp = DateTime.Now.Millisecond,
                dLatitude = 52.800000M,
                dLongitude = -2.000000M,
            };
            string strInvalidDeliveryItemJson = JsonR.JsonSerializer(oAsset);

            Assert.Throws<ProtocolException>(() => client.GeoTagDelivery(strInvalidDeliveryItemJson));
        }

        [Fact]
        public void TestSubmittingUnAssignedDeliveryItemShouldThrow()
        {
            var oAsset = new GeotaggedAssetDataContract()
            {
                oAssetKey = "051CD6B1-DE50-465A-8427-04EA267ED442",
                oUserIdentification = Guid.NewGuid().ToString(),
                lTimeStamp = DateTime.Now.Millisecond,
                dLatitude = 52.800000M,
                dLongitude = -2.000000M,
            };

            string strInvalidDeliveryItemJson = JsonR.JsonSerializer(oAsset);

            Assert.Throws<ProtocolException>(() => client.GeoTagDelivery(strInvalidDeliveryItemJson));
        }

        [Fact]
        public void TestSubmittingValidDeliveryItemFromInvalidDriver()
        {
            var oAsset = new GeotaggedAssetDataContract()
            {
                oAssetKey = "1CBC383E-7FA6-492C-9B50-851CEC0983CD",
                oUserIdentification = Guid.NewGuid().ToString(),
                lTimeStamp = DateTime.Now.Millisecond,
                dLatitude = 52.800000M,
                dLongitude = -2.000000M,
            };

            string strInvalidDeliveryItemJson = JsonR.JsonSerializer(oAsset);

            Assert.Throws<ProtocolException>(() => client.GeoTagDelivery(strInvalidDeliveryItemJson));
        }

        [Fact]
        public void TestSubmittingValidDeliveryItem()
        {
            var oAsset = new GeotaggedAssetDataContract()
            {
                oAssetKey = "1CBC383E-7FA6-492C-9B50-851CEC0983CD",
                oUserIdentification = "66FBA0E1-6429-4999-9538-6566DEE70048",
                lTimeStamp = DateTime.Now.Ticks,
                dLatitude = 52.800000M,
                dLongitude = -2.000000M,
            };

            string strInvalidDeliveryItemJson = JsonR.JsonSerializer(oAsset);

            Assert.DoesNotThrow(() => client.GeoTagDelivery(strInvalidDeliveryItemJson));
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
