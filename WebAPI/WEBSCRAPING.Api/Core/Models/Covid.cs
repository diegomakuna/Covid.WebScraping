using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WEBSCRAPING.Api.Core.Models
{
    public class Covid : FullData
    {
        public ObjectId _id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Create { get; set; }
        public List<Area> Areas { get; set; } = new List<Area>();
    }
}
