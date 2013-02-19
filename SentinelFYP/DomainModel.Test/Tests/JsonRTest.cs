using DomainModel.Abstracts;
using DomainModel.Models.GISModels;
using DomainModel.SecurityModels;
using System;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class JsonRTest
    {
        [Fact]
        public void TestJsonSerializer()
        {
            User user = new User() { UserName = "Test" };
            string json = "";
            
            Assert.DoesNotThrow(() => json = JsonR.JsonSerializer(user));
            Assert.NotNull(json);
            Assert.True(json.Contains("Test"));
        }

        [Fact]
        public void TestJsonDeserializer()
        {
            GeospatialInformation geodata = new GeospatialInformation() { DriverKey = Guid.NewGuid(), SessionID = new Random().Next(), TimeStamp = DateTime.Now, Latitude = 52, Longitude = -2, Orientation = 1, Speed = 0 };

            string json = JsonR.JsonSerializer(geodata);

            Assert.NotNull(json);

            Assert.DoesNotThrow(() => JsonR.JsonDeserializer<GeospatialInformation>(json));

            var deserializedGeodata = JsonR.JsonDeserializer<GeospatialInformation>(json);

            Assert.NotNull(deserializedGeodata);
            Assert.IsAssignableFrom<GeospatialInformation>(deserializedGeodata);
            
            Assert.Equal(geodata.DriverKey, deserializedGeodata.DriverKey);
            Assert.Equal(geodata.SessionID, deserializedGeodata.SessionID);
            Assert.Equal(geodata.Latitude, deserializedGeodata.Latitude);
            Assert.Equal(geodata.Longitude, deserializedGeodata.Longitude);
            Assert.Equal(geodata.Speed, deserializedGeodata.Speed);
        }

        [Fact]
        public void TestJsonDeserializerThrowsIfPassedInvalidParameters()
        {
            User nullUser = new User();
            string json = JsonR.JsonSerializer(nullUser);

            Assert.Throws<FormatException>(() => JsonR.JsonDeserializer<User>(json));
        }

        [Fact]
        public void TestJsonSerializerThrowsIfPassedNull()
        {
            Assert.Throws<ArgumentNullException>(() => JsonR.JsonSerializer(null));
        }
    }
}
