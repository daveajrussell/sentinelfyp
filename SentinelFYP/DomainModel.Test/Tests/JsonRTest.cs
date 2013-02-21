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
            
            Xunit.Assert.DoesNotThrow(() => json = JsonR.JsonSerializer(user));
            Xunit.Assert.NotNull(json);
            Xunit.Assert.True(json.Contains("Test"));
        }

        [Fact]
        public void TestJsonDeserializer()
        {
            GeospatialInformation geodata = new GeospatialInformation() { DriverKey = Guid.NewGuid(), SessionID = new Random().Next(), TimeStamp = DateTime.Now, Latitude = 52, Longitude = -2, Orientation = 1, Speed = 0 };

            string json = JsonR.JsonSerializer(geodata);

            Xunit.Assert.NotNull(json);

            Xunit.Assert.DoesNotThrow(() => JsonR.JsonDeserializer<GeospatialInformation>(json));

            var deserializedGeodata = JsonR.JsonDeserializer<GeospatialInformation>(json);

            Xunit.Assert.NotNull(deserializedGeodata);
            Xunit.Assert.IsAssignableFrom<GeospatialInformation>(deserializedGeodata);
            
            Xunit.Assert.Equal(geodata.DriverKey, deserializedGeodata.DriverKey);
            Xunit.Assert.Equal(geodata.SessionID, deserializedGeodata.SessionID);
            Xunit.Assert.Equal(geodata.Latitude, deserializedGeodata.Latitude);
            Xunit.Assert.Equal(geodata.Longitude, deserializedGeodata.Longitude);
            Xunit.Assert.Equal(geodata.Speed, deserializedGeodata.Speed);
        }

        [Fact]
        public void TestJsonDeserializerThrowsIfPassedInvalidParameters()
        {
            User nullUser = new User();
            string json = JsonR.JsonSerializer(nullUser);

            Xunit.Assert.Throws<FormatException>(() => JsonR.JsonDeserializer<User>(json));
        }

        [Fact]
        public void TestJsonSerializerThrowsIfPassedNull()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => JsonR.JsonSerializer(null));
        }
    }
}
