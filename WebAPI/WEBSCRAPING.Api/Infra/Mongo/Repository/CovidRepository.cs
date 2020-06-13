using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WEBSCRAPING.Api.Core.Models;
using WEBSCRAPING.Api.Core.Repository;
using MongoDB.Driver.Linq;

namespace WEBSCRAPING.Api.Infra.Mongo.Repository
{
    public class CovidRepository : ICovidRepository
    {

        private readonly ICovidContext _context;

        public CovidRepository(ICovidContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Covid>> GetAll()
        {
            return await _context.Covid.AsQueryable().OrderByDescending(x => x.Create).ToListAsync();
        }

        public async Task<Covid> GetLastUpdate()
        {
            var t = _context.Covid.AsQueryable().OrderByDescending(x => x.Create).FirstOrDefault();
            return t;
        }

        public async Task<IEnumerable<Area>> SearchAreaByName(string name)
        {

            var query = _context.Covid.AsQueryable().OrderByDescending(x => x.Areas).FirstOrDefault()
                ?.Areas
                .FindAll(a => a.AreaName != null && a.AreaName.ToLower().Contains(name));    
           
            return query;

        }

        public async Task<IEnumerable<Area>> rankingByDeaths ()
        {

            var query = _context.Covid.AsQueryable().OrderByDescending(x => x.Areas).FirstOrDefault()
                ?.Areas
                .AsQueryable().OrderByDescending(d => d.Deaths).ToList();
            //.Find(a => a.AreaName.ToLower().Equals(name));



            return query;

        }

        public async Task Create(Covid covid)
        {
            await _context.Covid.InsertOneAsync(covid);
        }

        public Task<bool> Update(Covid covid)
        {

            //ReplaceOneResult updateResult =
            //    await _context
            //        .Games
            //        .ReplaceOneAsync(
            //            filter: g => g.Id == game.Id,
            //            replacement: game);

            //return updateResult.IsAcknowledged
            //       && updateResult.ModifiedCount > 0;
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string name)
        {
            //FilterDefinition<Game> filter = Builders<Game>.Filter.Eq(m => m.Name, name);

            //DeleteResult deleteResult = await _context
            //    .Games
            //    .DeleteOneAsync(filter);

            //return deleteResult.IsAcknowledged
            //       && deleteResult.DeletedCount > 0;
            throw new NotImplementedException();
        }
    }
}
