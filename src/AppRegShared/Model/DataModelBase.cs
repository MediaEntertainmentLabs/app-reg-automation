using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppRegShared.Model
{
    /// <summary>
    /// Base class for DTOs to preserve additional data that might not be part of the class.
    /// </summary>
    public class DataModelBase
    {
        [JsonExtensionData]
        public Dictionary<string, object> ExtensionData { get; set; } = new Dictionary<string, object>();
    }
}
