using System;
using Microsoft.Extensions.Configuration;

namespace TableOfNothing.Configuration
{
    public static class EsotericKnowledgeExtension
    {
        public static IConfigurationBuilder AddEsotericKnowledge(this IConfigurationBuilder configuration,
        Action<EsotericKnowledgeOptions> options = null)
        {

            var configOptions = new EsotericKnowledgeOptions();

            options?.Invoke(configOptions);
            configuration.Add(new EsotericKnowledgeSource(configOptions));
            return configuration;
        }
    }
}