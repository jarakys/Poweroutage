using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PowerOutageParser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public String Get()
        {
            var ggg = WebUtility.HtmlEncode("<p>I <3 This ger mjuka strukturerade fransar för en fulländad och sofistikerad look. Most Wanted kollektion består av silkesliknande fransar som är tillverkade för hand. Kitet innehåller ögonfranslim. </p>"); 
            var df = Regex.Replace("", "\\<[^\\>]*\\>", string.Empty);
            var page = APIClient.GetOblenergoPage(24, "26-04-2020");
            var places = Utils.Parser.GetPlaces(page);
            var ff = Utils.Parser.GetDistrictPowerOffInformation(page);
            return page;
        }
    }
}
