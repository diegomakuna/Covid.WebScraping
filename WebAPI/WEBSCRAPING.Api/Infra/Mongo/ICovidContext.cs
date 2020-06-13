using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WEBSCRAPING.Api.Core.Models;

namespace WEBSCRAPING.Api.Infra.Mongo
{
    public interface ICovidContext
    {
        IMongoCollection<Covid> Covid { get; }
    }
}
