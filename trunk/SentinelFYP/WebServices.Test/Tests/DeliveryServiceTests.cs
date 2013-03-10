using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using DomainModel.Abstracts;
using Moq;
using WebServices.DataContracts;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebServices.Test.Tests
{
    [TestClass]
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
            Xunit.Assert.NotNull(client);
            Xunit.Assert.IsAssignableFrom<DeliveryServiceClient>(client);
            Xunit.Assert.NotNull(client.Endpoint);
        }

        [Fact]
        public void TestSubmittingMalformedJsonShouldThrow()
        {
            string strMalformedJson = "{\"RUBBSIH\"}";
            Xunit.Assert.Throws<ProtocolException>(() => client.GeoTagDelivery(strMalformedJson));
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

            Xunit.Assert.Throws<ProtocolException>(() => client.GeoTagDelivery(strInvalidDeliveryItemJson));
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

            Xunit.Assert.Throws<ProtocolException>(() => client.GeoTagDelivery(strInvalidDeliveryItemJson));
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

            Xunit.Assert.Throws<ProtocolException>(() => client.GeoTagDelivery(strInvalidDeliveryItemJson));
        }

        /*[Fact]
        [TestMethod]
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
            Xunit.Assert.DoesNotThrow(() => client.GeoTagDelivery(strValidDeliveryItemJson));
        }*/

        //[TestMethod]
        public void TestSubmitRandomDeliveryItemLoadTest()
        {
            var key = Guid.NewGuid().ToString();

            var oAsset = new GeotaggedAssetDataContract()
            {
                oAssetKey = key,
                oUserIdentification = "66FBA0E1-6429-4999-9538-6566DEE70048",
                lTimeStamp = 6000000,
                dLatitude = 52.800000M,
                dLongitude = -2.000000M,
            };

            var oUnTagAsset = new AssetKeyDataContract()
            {
                oAssetKey = key
            };
            
            string strValidDeliveryItemJson = JsonR.JsonSerializer(oAsset);
            string strAssetKey = JsonR.JsonSerializer(oUnTagAsset);

            Xunit.Assert.DoesNotThrow(() => client.GeoTagDelivery(strValidDeliveryItemJson));
            client.UnTagDelivery(strAssetKey);
        }
    }
}
