using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WEBSCRAPING.Api.Core.Models;

namespace WEBSCRAPING.Api.Infra.Mongo
{
    public class CovidContext : ICovidContext
    {
        private readonly IMongoDatabase _db;

        public CovidContext(IOptions<MongoConfig> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Covid> Covid => _db.GetCollection<Covid>("CovidProcess");
    }
}
