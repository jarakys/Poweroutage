using System;
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

        [HttpGet]
        public async Task<HashSet<AddressModel>> DistricsPowerInfo(int placeId, string date = "26-04-2020")
        {
            var page = APIClient.GetOblenergoPage(placeId, "26-04-2020");
            var placesInfo = await Utils.Parser.GetDistrictPowerOffInformation(page);
            return placesInfo;
        }

        [HttpGet]
        public async Task<List<PlaceModel>> Districs()
        {
            var page = APIClient.GetOblenergoPage(24, "26-04-2020");
            var places = await Utils.Parser.GetPlaces(page);
            return places;
        }

    }
}