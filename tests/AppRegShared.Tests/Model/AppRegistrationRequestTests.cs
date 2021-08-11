using AppRegShared.Model;

using System.Text.Json;

using TestUtilities;

using Xunit;

namespace AppRegShared.Tests.Model
{
    public class AppRegistrationRequestTests
    {
        public class RegRequestWithAdditionalFields : AppRegistrationRequest
        {
            public string AdditionalString { get; set; }
            public int AdditionalInt { get; set; }
        }

        [Theory, AutoDataNSubstitute]
        public void Must_Preserve_Unknown_Fields(RegRequestWithAdditionalFields data)
        {
            string serializedData = JsonSerializer.Serialize(data);

            AppRegistrationRequest sut = JsonSerializer.Deserialize<AppRegistrationRequest>(serializedData);
            Assert.Equal(data.RequestorUserName, sut.RequestorUserName);
            Assert.Equal(data.RequestorUserId, sut.RequestorUserId);

            string reSerialized = JsonSerializer.Serialize(sut);

            RegRequestWithAdditionalFields restored = JsonSerializer.Deserialize<RegRequestWithAdditionalFields>(reSerialized);

            Assert.Equal(data.RequestorUserId, restored.RequestorUserId);
            Assert.Equal(data.RequestorUserName, restored.RequestorUserName);

            Assert.Equal(data.AdditionalString, restored.AdditionalString);
            Assert.Equal(data.AdditionalInt, restored.AdditionalInt);
        }
    }
}
