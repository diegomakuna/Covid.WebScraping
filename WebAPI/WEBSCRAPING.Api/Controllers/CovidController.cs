using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBSCRAPING.Api.Core.Models;
using WEBSCRAPING.Api.Core.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEBSCRAPING.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidController : ControllerBase
    {

        private readonly ICovidRepository _covidRepository;

        public CovidController(ICovidRepository covidRepository)
        {
            _covidRepository = covidRepository;
        }

        // GET: api/<CovidController>
        /// <summary>
        /// Pega todas as collections disponiveis 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllCollection")]
        public async Task<IActionResult> GetAll()
        {
            var covid = await _covidRepository.GetAll();

            if (covid == null)
                return new NotFoundResult();

            return new ObjectResult(covid);
        }

        /// <summary>
        /// Pega a Collection mais Nova do Banco 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLastUpdate")]
        public async Task<IActionResult> GetLastUpdate()
        {
            var covid = await _covidRepository.GetLastUpdate();

            if (covid == null)
                return new NotFoundResult();

            return new ObjectResult(covid);
        }
        /// <summary>
        /// Busca pelo nome dos paises  
        /// <param name="name"></param>
        ///  </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchAreaByName")]
        public async Task<IActionResult> SearchAreaByName(string name = "")
        {

            name = name.Trim().ToLower();

            var area = await _covidRepository.SearchAreaByName(name);

            if (area == null)
                return new NotFoundResult();

            return new ObjectResult(area);
        }

        /// <summary>
        /// Retorna uma lista de paises  por ordem de número de mortos 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RankingByDeaths")]
        public async Task<IActionResult> rankingByDeaths()
        {

            var covid = await _covidRepository.rankingByDeaths();

            if (covid == null)
                return new NotFoundResult();

            return new ObjectResult(covid);
        }

        

        /// <summary>
        /// Salva os dados da covid no MongoDb
        /// </summary>
        /// <param name="covid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Covid covid)
        {
            await _covidRepository.Create(covid);
            return new OkObjectResult(covid);
        }


    }
}
