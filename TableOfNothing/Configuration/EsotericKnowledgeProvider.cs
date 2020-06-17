using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TableOfNothing.Configuration.EsotericStrategies;

namespace TableOfNothing.Configuration
{

    public class EsotericKnowledgeProvider : ConfigurationProvider
    {
        public EsotericKnowledgeSource Source { get; }
        public EsotericKnowledgeProvider(EsotericKnowledgeSource source)
        {
            Source = source;
        }

        public override void Load()
        {
            LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public async Task LoadAsync()
        {
            var esotericSetting = Environment.GetEnvironmentVariable("ESOTERIC_KNOWLEDGE");
            var tarotSetting = false;
            bool.TryParse(Environment.GetEnvironmentVariable("USE_TAROT"), out tarotSetting);
            esotericSetting = esotericSetting != null ? esotericSetting : "TAROT";
            System.Console.WriteLine(esotericSetting);

            EsotericContext ctx = null;
            switch (esotericSetting)
            {
                case "TAROT":
                    ctx = new EsotericContext(new TarotStrategy());
                    break;
                default:
                    throw new Exception("No strategy set.");
            }

            if (tarotSetting)
            {
                var tarot = new EsotericContext(new TarotStrategy());
                Set("tarot", tarot.GetSettings());
            }
            var settings = ctx.GetSettings();
            await Task.Delay(2000);
            Set("esotericSettings", settings);
            Set("wordOfPower", esotericSetting);
            // await File.WriteAllTextAsync("secrets/appsettings.secrets.json", $"{{\"wordOfPower\": \"tarot\", \"esotericSetting\": {settings}}}");
        }
    }
}