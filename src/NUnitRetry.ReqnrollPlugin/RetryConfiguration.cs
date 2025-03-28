using Reqnroll.Configuration;
using System.IO;
using System.Text.Json;

namespace NUnitRetry.ReqnrollPlugin.Configuration
{
    // Class which holds configuration from reqnroll.json
    public class RetryConfiguration
    {
        public int MaxRetries;
        public bool ApplyGlobally;

        private readonly IReqnrollJsonLocator _reqnrollJsonLocator;

        public RetryConfiguration(IReqnrollJsonLocator reqnrollJsonLocator)
        {
            _reqnrollJsonLocator = reqnrollJsonLocator;
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            string reqnrollJsonFile;
            if (File.Exists(_reqnrollJsonLocator.GetReqnrollJsonFilePath()))
            {
                reqnrollJsonFile = File.ReadAllText(_reqnrollJsonLocator.GetReqnrollJsonFilePath());
            }
            else
            {
                throw new FileNotFoundException("reqnroll.json is missing! Ensure that you've provided the reqnroll.json file to your project and added the correct section. For more info proceed to the projects page: https://github.com/farum12/NUnitRetry.SpecFlowPlugin");
            }

            try
            {
                var jsonConfig = JsonSerializer.Deserialize<JsonConfig.JsonConfig>(reqnrollJsonFile);

                MaxRetries = jsonConfig.NRetrySettings.MaxRetries;
                ApplyGlobally = jsonConfig.NRetrySettings.ApplyGlobally;
            }
            catch
            {
                // Apply default values if reqnroll.json is present, but section is not added to the JSON.
                MaxRetries = 3;
                ApplyGlobally = true;
            }
        }
    }
}
