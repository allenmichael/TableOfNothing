using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TableOfNothing.Tarot;

namespace TableOfNothing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarotController : ControllerBase
    {
        private readonly ILogger<TarotController> _logger;
        private readonly IConfiguration _config;

        public TarotController(ILogger<TarotController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public TarotDeck.Hand Get(int cards = 5)
        {
            System.Console.WriteLine(_config.GetValue(typeof(string), "esotericSettings"));
            System.Console.WriteLine("TAROT SETTINGS: ");
            System.Console.WriteLine(_config.GetValue(typeof(string), "tarot"));
            return TarotDeck.Deal();
        }
        [HttpGet]
        [Route("minor-arcana")]
        public TarotDeck.Hand GetMinorArcanaCards(int cards = 5)
        {
            System.Console.WriteLine(_config.GetValue(typeof(string), "esotericSettings"));
            System.Console.WriteLine("TAROT SETTINGS: ");
            System.Console.WriteLine(_config.GetValue(typeof(string), "tarot"));
            return TarotDeck.Deal(handSize: cards, minorArcanaOnly: true);
        }
    }
}
