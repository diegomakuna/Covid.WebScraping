using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using WEBSCRAPING.Models;

namespace WEBSCRAPING
{
    public class CovidRepository
    {
        private MongoClient _client;
        private IMongoDatabase _db;
        private IConfiguration _configuration;

        public CovidRepository(
            IConfiguration configuration)
        {
            _configuration = configuration;
            
            _client = new MongoClient(
                _configuration.GetSection("MongoDb:ConnectionString").Value);
            _db = _client.GetDatabase(configuration.GetSection("MongoDb:Database").Value);
        }

        public void Incluir(Covid covid)
        {
            //_db.DropCollection("CovidProcess");
            
            var covidProcess = _db.GetCollection<Covid>(_configuration.GetSection("MongoDb:CovidCollectionName").Value);
           
            covidProcess.InsertOne(covid);
        }
    }
}
