using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TableOfNothing.Tarot
{
    public static class TarotDeck
    {
        public enum Element
        {
            Wands = 1,
            Cups = 2,
            Swords = 3,
            Pentacles = 4
        }

        public enum Number
        {
            Ace = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Page = 11,
            Knight = 12,
            Queen = 13,
            King = 14
        }

        public static readonly Dictionary<int, string> MajorArcanaDeck =
            new Dictionary<int, string>()
            {
                { 0, "The Fool" },
                { 1, "The Magician" },
                { 2, "The High Priestess" },
                { 3, "The Empress" },
                { 4, "The Emperor" },
                { 5, "The Hierophant" },
                { 6, "The Lovers" },
                { 7, "The Chariot" },
                { 8, "Strength" },
                { 9, "The Hermit" },
                { 10, "Wheel of Fortune" },
                { 11, "Justice" },
                { 12, "The Hanged Man" },
                { 13, "Death" },
                { 14, "Temperance" },
                { 15, "The Devil" },
                { 16, "The Tower" },
                { 17, "The Star" },
                { 18, "The Moon" },
                { 19, "The Sun" },
                { 20, "Judgement" },
                { 21, "The World" },
            };

        public static readonly IEnumerable<MinorArcanaCard> MinorArcanaDeck =
            Enumerable.Range(1, 4)
                .SelectMany(s => Enumerable.Range(1, 14)
                .Select(c => new MinorArcanaCard()
                {
                    Number = (Number)c,
                    Element = (Element)s,
                    Value = c,
                    CardName = ((Element)s).ToString()
                }));

        public class MinorArcanaCard : ICard
        {
            public int Value { get; set; }
            [JsonConverter(typeof(StringEnumConverter))]
            public Element Element { get; set; }
            [JsonConverter(typeof(StringEnumConverter))]
            public Number Number { get; set; }
            public bool? IsReversed { get; set; }
            [JsonIgnore]
            public string CardName { get; set; }
        }

        public class MajorArcanaCard : ICard
        {
            public int Value { get; set; }
            public string CardName { get; set; }
            public bool? IsReversed { get; set; }
        }
        public class Hand
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public IEnumerable<MajorArcanaCard> MajorArcanaCards { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public IEnumerable<MinorArcanaCard> MinorArcanaCards { get; set; }
        }

        interface ICard
        {
            string CardName { get; set; }
            int Value { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            bool? IsReversed { get; set; }
        }

        public static Hand Deal(int handSize = 5, bool majorArcanaOnly = false, bool minorArcanaOnly = false)
        {
            if (majorArcanaOnly)
            {
                return new Hand()
                {
                    MajorArcanaCards = TarotDeck.Shuffle<MajorArcanaCard>(handSize)
                };
            }
            else if (minorArcanaOnly)
            {
                return new Hand()
                {
                    MinorArcanaCards = TarotDeck.Shuffle<MinorArcanaCard>(handSize)
                };
            }
            else
            {
                return new Hand()
                {
                    MajorArcanaCards = TarotDeck.Shuffle<MajorArcanaCard>(handSize),
                    MinorArcanaCards = TarotDeck.Shuffle<MinorArcanaCard>(handSize)
                };
            }
        }

        public static IEnumerable<T> Shuffle<T>(int handSize)
        {
            var rnd = new Random();
            if (typeof(T) == typeof(MajorArcanaCard))
            {
                var hand = new List<MajorArcanaCard>();
                foreach (var i in Enumerable.Range(1, handSize))
                {
                    var r = rnd.Next(0, 21);
                    var flip = rnd.Next(2);
                    var rev = flip == 1 ? true : false;

                    var unique = false;
                    while (!unique)
                    {
                        try
                        {
                            hand.First(c => c.Value == r);
                            r = rnd.Next(0, 21);
                        }
                        catch (System.Exception)
                        {
                            break;
                        }
                    }

                    hand.Add(new MajorArcanaCard()
                    {
                        Value = r,
                        CardName = MajorArcanaDeck[r],
                        IsReversed = rev
                    });
                }
                return hand as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(MinorArcanaCard))
            {
                var deck = MinorArcanaDeck;
                var shuffled = deck.OrderBy(c => Guid.NewGuid());
                return shuffled.Take(handSize) as IEnumerable<T>;
            }
            else
            {
                return new List<T>();
            }
        }
    }

}
