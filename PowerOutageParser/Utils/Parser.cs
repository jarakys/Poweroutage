using PowerOutageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace PowerOutageParser.Utils
{
    public class Parser
    {
        public static async Task<List<PlaceModel>> GetPlaces(string page)
        {
            var config = Configuration.Default;
            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Parse the document from the content of a response to a virtual request
            var document = await context.OpenAsync(req => req.Content(page));
            var places = document.QuerySelector("[name='operator']").Html();
            document = await context.OpenAsync(req => req.Content(places));
            var listOfPlaces = document.QuerySelectorAll("option").ToList();
            List<PlaceModel> placeModel = listOfPlaces.Select(x => new PlaceModel()
            {
                Id = int.Parse(x.GetAttribute("value")),
                Name = x.Text()
            }).ToList();

            

            return placeModel;
        }

       public static async Task<HashSet<AddressModel>> GetDistrictPowerOffInformation(string page)
       {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(page));
            var table = document.QuerySelector("[class='sticky-enabled']").ToHtml();
            document = await context.OpenAsync(req => req.Content(table));
            var a = document.QuerySelector("tr");
            var tbody = document.QuerySelector("tbody").ToHtml();
            var listOfPlaces = document.QuerySelectorAll("tr").ToList();
            var addresses = new HashSet<AddressModel>();
            foreach (var column in listOfPlaces.Skip(1))
            {
                var row = column.QuerySelectorAll("td");
                var dateOff = DateTime.Parse(row[1].ChildNodes[0].TextContent);
                var dateOn = DateTime.Parse(row[2].ChildNodes[0].TextContent);
                var reason = row[3].ChildNodes[0].TextContent;
                var places = row[4].QuerySelectorAll("b").ToList();
                foreach (var place in places)
                {
                    row[4].QuerySelector("b")?.Remove();
                    var isCity = Utils.IsCity(place.TextContent);
                    var placeName = isCity ? place.TextContent.Split(",")[0] : place.TextContent.Split(",")[1];
                    var houses = row[4].ToHtml().Split("<br>")[1];
                    var addressModel = new AddressModel();

                    addressModel.PlaceName = placeName;
                    addressModel.Reason = reason;
                    addressModel.PowerOffDate = dateOff;
                    addressModel.PowerOnDate = dateOn;

                    var streetModel = new StreetModel();
                    streetModel.Name = isCity ? place.TextContent.Split(",")[1] : place.TextContent.Split(",")[2];
                    streetModel.Houses.AddRange(houses.Split(" "));
                    row[4].QuerySelector("br").Remove();
                    row[4].QuerySelector("br")?.Remove();
                    addresses.Add(addressModel);

                    var placeFromSet = addresses.Where(x => x.PlaceName == placeName).First();
                    placeFromSet.Streets.Add(streetModel);
                }
            }
            return addresses;
       }

        
    }
}
