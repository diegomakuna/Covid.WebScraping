using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBSCRAPING.Api.Core.Models;

namespace WEBSCRAPING.Api.Core.Repository
{
    public interface ICovidRepository
    {
        Task<IEnumerable<Covid>> GetAll();
        Task<Covid> GetLastUpdate();
        Task<IEnumerable<Area>> SearchAreaByName(string name);
        Task<IEnumerable<Area>> rankingByDeaths();
        Task Create(Covid covid);
        Task<bool> Update(Covid covid);
        Task<bool> Delete(string name);
    }
}
