using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerOutageParser.Models
{
    public class AddressModel
    {

        public string PlaceName { get; set; }

        public DateTime PowerOffDate { get; set; }

        public DateTime PowerOnDate { get; set; }

        public string Reason { get; set; }

        public List<StreetModel> Streets { get; set; } = new List<StreetModel>();

        public override bool Equals(object obj)
        {
            return obj is AddressModel && Equals((AddressModel)obj);
        }

        public bool Equals(AddressModel p)
        {
            return PlaceName == p.PlaceName;
        }

        public override int GetHashCode()
        {
            return PlaceName.GetHashCode();
        }
    }
}
