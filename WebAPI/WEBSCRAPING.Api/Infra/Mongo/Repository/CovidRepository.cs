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
            return _context.Covid.AsQueryable().OrderByDescending(x => x.Create).FirstOrDefault();

        }

        public async Task<IEnumerable<Area>> SearchAreaByName(string name)
        {

            var query = _context.Covid.AsQueryable().OrderByDescending(x => x.Create).FirstOrDefault()
                ?.Areas
                .FindAll(a => a.AreaName != null && a.AreaName.ToLower().Contains(name));

            return query;

        }

        public async Task<Area> GetByRankingOrder(int number)
        {

            var query = _context.Covid.AsQueryable().OrderByDescending(x => x.Create).FirstOrDefault()
                ?.Areas
                .AsQueryable().FirstOrDefault(x => x.RankingOrder == number);

            return query;

        }

        public async Task<IEnumerable<Area>> RankingByDeaths()
        {

            var query = _context.Covid.AsQueryable().OrderByDescending(x => x.Areas).FirstOrDefault()
                ?.Areas
                .AsQueryable().OrderByDescending(d => d.Deaths).ToList();

            return query;

        }

        public async Task Create(Covid covid)
        {
            await _context.Covid.InsertOneAsync(covid);
        }

        public Task<bool> Update(Covid covid)
        {

            throw new NotImplementedException();
        }

        public Task<bool> Delete(string name)
        {

            throw new NotImplementedException();
        }
    }
}
