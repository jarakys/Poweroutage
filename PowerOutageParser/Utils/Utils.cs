using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerOutageParser.Utils
{
    public class Utils
    {
        public static bool IsCity(string data)
        {
            if(data.Split(",")?.Count() > 2)
            {
                return false;
            }
            return true;
        }
    }
}
