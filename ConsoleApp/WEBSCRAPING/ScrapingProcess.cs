using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WEBSCRAPING.BLL;
using WEBSCRAPING.Config;
using WEBSCRAPING.Data;
using WEBSCRAPING.Models;

namespace WEBSCRAPING
{
    public class ScrapingProcess
    {
        private readonly string _configFile = "appsettings.json";
        private Logger _logger;
        private CovidPage _covidPage;
        private Covid _covidData ;
        private IConfiguration _configuration;
        private SeleniumConfig  _slConfig;


        public ScrapingProcess(Logger logger)
        {
            _logger = logger;
        }
        private void StartConfig()
        {
            _logger.LogMessage("Carregando configurações...");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_configFile);
            _configuration = builder.Build();

             _slConfig = new SeleniumConfig();
           
            new ConfigureFromConfigurationOptions<SeleniumConfig>(_configuration.GetSection("SeleniumConfigurations"))
                .Configure(_slConfig);

            _logger.LogMessage("Carregando driver do Selenium para Chrome");
            
            _covidPage = new CovidPage(_slConfig);


        }

        private void LoadPage()
        {
            _logger.LogMessage("Carregando pagina com os dados da COVID-19");

            _covidPage.LoadPageCovid();

        }


        private void StartProcess()
        {
            _logger.LogMessage("Iniciando Processo de Extrair dados ..");

            _covidData = _covidPage.GetDataFromPage();
        }

        private void CloseProcess()
        {
            _logger.LogMessage("Fechando o Browser");

            _covidPage.Close();
        }

        private void  SaveData()
        {
            _logger.LogMessage("Gravando dados extraídos...");


            if (_slConfig.ApiSave)
            {
                var api = new ApiServices(_configuration);
                api.ExecutarAsync(_covidData);
            }
            else
            {
                new CovidRepository(_configuration).Incluir(_covidData);
            }
      
            _logger.LogMessage("Carga de dados concluída com sucesso!");

        }

        public void Start()
        {
            try
            {
                StartConfig();
                LoadPage();
                StartProcess();
                CloseProcess();
                SaveData();
            }
            catch (Exception e)
            {
               _logger.LogError(e);
            }
         

        }
    }
}
