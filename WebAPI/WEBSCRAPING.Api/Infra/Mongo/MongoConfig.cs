using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBSCRAPING.Api.Infra.Mongo
{
    public class MongoConfig
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
