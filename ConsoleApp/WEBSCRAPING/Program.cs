using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WEBSCRAPING.BLL;
using WEBSCRAPING.Config;

namespace WEBSCRAPING
{
    class Program
    {
        static void Main(string[] args)
        {
          

            var logger = new Logger();
            var processPage = new ScrapingProcess(logger);
          
           processPage.Start();
          

            Console.ReadKey();
        }




    }
}
