namespace TableOfNothing.Configuration.EsotericStrategies
{
    public class EsotericContext
    {
        private IEsotericStrategy _strategy;
        public EsotericContext(IEsotericStrategy strat)
        {
            this._strategy = strat;
        }

        public string GetSettings()
        {
            return this._strategy.GetConfiguration();
        }
    }
}