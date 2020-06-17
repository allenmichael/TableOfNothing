using TableOfNothing.Tarot;

namespace TableOfNothing.Configuration.EsotericStrategies
{
    public class TarotStrategy : IEsotericStrategy
    {
        public string GetConfiguration()
        {
            var hand = TarotDeck.Deal();
            var json = "{";
            foreach (var card in hand.MajorArcanaCards)
            {
                json += $"\"{card.CardName}\":\"{card.Value}\",";
            }
            json = json.Substring(0, json.LastIndexOf(","));
            json += "}";
            return json;
        }
    }
}