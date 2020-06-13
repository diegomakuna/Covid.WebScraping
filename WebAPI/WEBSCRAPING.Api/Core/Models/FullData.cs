using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBSCRAPING.Api.Core.Models
{
    public class FullData
    {
        public int? Confirmed { get; set; }
        public int? Active { get; set; }
        public int? Recovered { get; set; }
        public int? Deaths { get; set; }
    }
}
