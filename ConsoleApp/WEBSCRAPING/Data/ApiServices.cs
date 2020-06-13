using System;
using System.Collections.Generic;
using System.Net.Http;

using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Newtonsoft.Json;
using WEBSCRAPING.Models;


namespace WEBSCRAPING.Data
{
    public class ApiServices
    {
        private readonly IConfiguration _configuration;

        private static HttpClient _httpClient = new HttpClient();
        public ApiServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ExecutarAsync(Covid covid)
        {
            if (covid != null)
            {


                using (var content = new StringContent(JsonConvert.SerializeObject(covid), System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage result = _httpClient.PostAsync($"{_configuration.GetSection("ApiConfiguration:ApiUrl").Value}/api/Covid", content).Result;
                    
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        return ;

                    string returnValue = result.Content.ReadAsStringAsync().Result;
                    throw new Exception($"O Post Falhou: ({result.StatusCode}): {returnValue}");
                }


            }
        }

        // return URI of the created resource.
        
   
    }
}
