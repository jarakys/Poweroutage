using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerOutageParser.Models
{
    public class StreetModel
    {
        public string Name { get; set; }

        public List<String> Houses { get; set; } = new List<string>();

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

    }
}
