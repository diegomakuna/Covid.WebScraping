using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBSCRAPING.Api.Core.Models
{
    public class Area : FullData
    {
        public string IdAreaHtml { get; set; }
        public string AreaName { get; set; }
        public List<Area> Areas { get; set; } = new List<Area>();
    }
}
