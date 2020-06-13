using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace WEBSCRAPING.Models
{
    public class FullData
    {
        public int? Confirmed { get; set; }
        public int? Active { get; set; }
        public int? Recovered { get; set; }
        public int? Deaths { get; set; }
    }
}
