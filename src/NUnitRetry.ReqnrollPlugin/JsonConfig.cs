using System.Runtime.Serialization;

namespace NUnitRetry.ReqnrollPlugin
{
    public class JsonConfig
    {
        [DataMember(Name = "NRetrySettings")]
        public NRetrySettingsElement NRetrySettings { get; set; }
    }
}
