using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using DomainModel.Abstracts;
using Moq;
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
                lTimeStamp = 6000000,
                dLatitude = 52.800000M,
                dLongitude = -2.000000M,
            };

            var oUnTagAsset = new AssetKeyDataContract()
            {
                oAssetKey = "1CBC383E-7FA6-492C-9B50-851CEC0983CD"
            };

            string strAssetKey = JsonR.JsonSerializer(oUnTagAsset);
            client.UnTagDelivery(strAssetKey);

            string strValidDeliveryItemJson = JsonR.JsonSerializer(oAsset);
            Assert.DoesNotThrow(() => client.GeoTagDelivery(strValidDeliveryItemJson));
        }

        [Fact]
        public void TestSubmittingValidDeliveryItemAsync()
        {
            var oAsset = new GeotaggedAssetDataContract()
            {
                oAssetKey = "1CBC383E-7FA6-492C-9B50-851CEC0983CD",
                oUserIdentification = "66FBA0E1-6429-4999-9538-6566DEE70048",
                lTimeStamp = 6000000,
                dLatitude = 52.800000M,
                dLongitude = -2.000000M,
            };

            var oUnTagAsset = new AssetKeyDataContract()
            {
                oAssetKey = "1CBC383E-7FA6-492C-9B50-851CEC0983CD"
            };

            string strAssetKey = JsonR.JsonSerializer(oUnTagAsset);
            client.UnTagDelivery(strAssetKey);

            string strValidDeliveryItemJson = JsonR.JsonSerializer(oAsset);
            Assert.DoesNotThrow(() => client.GeoTagDeliveryAsync(strValidDeliveryItemJson));
        }
    }
}
