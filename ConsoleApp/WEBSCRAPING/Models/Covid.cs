using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WEBSCRAPING.Models
{
    public class Covid : FullData
    {
        public ObjectId _id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Create { get; set; }
        public List<Area> Areas { get; set; } = new List<Area>();
    }
}
