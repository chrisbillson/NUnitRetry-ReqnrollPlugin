using System.ComponentModel;
using System.Runtime.Serialization;

namespace NUnitRetry.ReqnrollPlugin
{
    // Class containing definitions and its default values for config from reqnroll.json
    public class NRetrySettingsElement
    {
        [DefaultValue(3)]
        [DataMember(Name = "maxRetries")]
        public int MaxRetries { get; set; }

        [DefaultValue(true)]
        [DataMember(Name = "applyGlobally")]
        public bool ApplyGlobally { get; set; }
    }
}
