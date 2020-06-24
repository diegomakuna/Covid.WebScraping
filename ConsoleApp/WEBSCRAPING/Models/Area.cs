using System;
using System.Collections.Generic;
using System.Text;

namespace WEBSCRAPING.Models
{
    public class Area : FullData
    {
        public string IdAreaHtml { get; set; }
        public string AreaName { get; set; }
        public int? RankingOrder { get; set; }
        public List<Area> Areas { get; set; } = new List<Area>();
    }
}
