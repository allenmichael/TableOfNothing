using Microsoft.Extensions.Configuration;

namespace TableOfNothing.Configuration
{
    public class EsotericKnowledgeSource : IConfigurationSource
    {
        public EsotericKnowledgeSource(EsotericKnowledgeOptions options)
        {

        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EsotericKnowledgeProvider(this);
        }
    }
}