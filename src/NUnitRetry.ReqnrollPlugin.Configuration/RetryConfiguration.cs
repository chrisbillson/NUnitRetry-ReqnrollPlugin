using NUnitRetry.ReqnrollPlugin.JsonConfig;
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
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReadCommentHandling = JsonCommentHandling.Skip,
                PropertyNameCaseInsensitive = true
            };

            var fileName = _reqnrollJsonLocator.GetReqnrollJsonFilePath();

            if (File.Exists(fileName))
            {
                var jsonFileContent = File.ReadAllText(fileName);

                using var reqnrollConfigDoc = JsonDocument.Parse(jsonFileContent, new JsonDocumentOptions
                {
                    CommentHandling = JsonCommentHandling.Skip
                });

                if (reqnrollConfigDoc.RootElement.TryGetProperty("NRetrySettings", out JsonElement settings))
                {
                    var nRetrySettings = JsonSerializer.Deserialize<NRetrySettingsElement>(settings.GetRawText(), jsonOptions);

                    MaxRetries = nRetrySettings.MaxRetries;
                    ApplyGlobally = nRetrySettings.ApplyGlobally;
                }
            }
            else
            {
                throw new FileNotFoundException("reqnroll.json is missing! Ensure that you've provided the reqnroll.json file to your project and added the correct section. For more info proceed to the projects page: https://github.com/chrisbillson/NUnitRetry.ReqnrollPlugin");
            }
        }
    }

}
