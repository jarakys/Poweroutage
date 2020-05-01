using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerOutageParser.Models;

namespace PowerOutageParser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerOutageController : ControllerBase
    {

        [HttpGet("districsPowerInfo")]
        public async Task<HashSet<AddressModel>> DistricsPowerInfo(int placeId, string date, PowerOutageEnum type)
        {
            var page = APIClient.GetOblenergoPage(placeId, "26-04-2020", type);
            var placesInfo = await Utils.Parser.GetDistrictPowerOffInformation(page);
            return placesInfo;
        }

        [HttpGet("districs")]
        public async Task<List<PlaceModel>> Districs(PowerOutageEnum type)
        {
            var page = APIClient.GetOblenergoPage(24, "26-04-2020", type);
            var places = await Utils.Parser.GetPlaces(page);
            return places;
        }

    }
}