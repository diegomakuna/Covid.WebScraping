using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WEBSCRAPING.BLL;
using WEBSCRAPING.Config;
using WEBSCRAPING.Models;

namespace WEBSCRAPING
{
    public class CovidPage
    {
        private SeleniumConfig _configurations;
        private IWebDriver _driver;
        private int _navigate = 0;
        private int _navigateMax;
        private Logger _logger = new Logger();


        public CovidPage(SeleniumConfig configurations)
        {
            _configurations = configurations;
            _navigateMax = _configurations.NavigateMax;

            ChromeOptions options = new ChromeOptions();

            if (_configurations.Headless)
                options.AddArgument("--headless");

            _driver = new ChromeDriver(
                _configurations.DrivePath,
                options);
        }

        public void LoadPageCovid()
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configurations.Timeout);
            _driver.Navigate().GoToUrl(_configurations.PageUrl);
        }

        public Covid GetDataFromPage()
        {



            // Seta o filtro global
            ClickOnPage(_driver.FindElements(By.CssSelector(".selectedAreas .areaDiv"))[_navigate],
                By.CssSelector(".areas areaDiv"));

           
            var global = new Covid();

            var globafFullData = GetFullData();

            global.Confirmed = globafFullData.Confirmed;
            global.Active = globafFullData.Active;
            global.Recovered = globafFullData.Recovered;
            global.Deaths = globafFullData.Deaths;
            global.Create = DateTime.Now;


            global.Areas = GetAreasActive();


            return global;
        }


        private List<Area> GetAreasActive()
        {
            List<Area> listAreas = new List<Area>();

            var elemCountries = _driver.FindElement(By.ClassName("combinedArea"));
            var countAreas = elemCountries.FindElement(By.ClassName("areas")).FindElements(By.ClassName("areaDiv"));



            for (int i = 0; i < countAreas.Count; i++)
            {
                //Atualiza
                var listOfareas = elemCountries.FindElement(By.ClassName("areas")).FindElements(By.ClassName("areaDiv"));

                var area = new Area();
                area.IdAreaHtml = listOfareas[i].FindElement(By.CssSelector(".area")).GetAttribute("id");
                area.AreaName = listOfareas[i].FindElement(By.ClassName("areaName")).Text;
                area.Confirmed = ConvertToInt(listOfareas[i].FindElement(By.ClassName("secondaryInfo")).Text);



                if (_navigate == 0)
                {

                    Logger($"({i}) {area.AreaName} --------------- ".ToUpper());
                }

                var hasChildren = listOfareas[i].FindElements(By.ClassName("hasChildren")).Count;


                if (_navigate < _navigateMax && hasChildren > 0)
                {

                    AddFilter(listOfareas[i], By.ClassName("areaDiv"));

                    area.Areas = GetAreasActive();

                    //Atualiza
                    listOfareas = elemCountries.FindElement(By.ClassName("areas")).FindElements(By.ClassName("areaDiv"));
                }


                if (hasChildren > 0)
                {
                    AddFilter(listOfareas[i], By.ClassName("areaDiv"));

                 area = GetFullData(area);
                 

                    RemoveFilter(By.ClassName("areaDiv"));

                }
                else
                {
                    listOfareas[i].FindElement(By.ClassName("area")).Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(c =>
                        c.FindElements(By.ClassName("infoTileData")).Count > 1);

                    area = GetFullData(area);
                    
                }




                Logger($" -- AREA : {area.AreaName} | CONFIRMADO {area.Confirmed?.ToString("N0", new CultureInfo("pt-BR"))} | " +
                       $"ATIVOS : {area.Active?.ToString("N0", new CultureInfo("pt-BR"))} | " +
                       $"RECUPERADOS : {area.Recovered?.ToString("N0", new CultureInfo("pt-BR"))} | " +
                       $"MORTOS {area.Deaths?.ToString("N0", new CultureInfo("pt-BR"))}".ToUpper());



                listAreas.Add(area);

            }


            RemoveFilter(By.ClassName("areaDiv"));



            return listAreas;

        }

        private Area GetFullData(Area area = null)
        {
            var _area = area ?? new Area();

            var infoConfiemed = _driver.FindElements(By.CssSelector(".infoTile .confirmed"));

            var infoData = _driver.FindElements(By.ClassName("infoTileData"));

            _area.Confirmed = ConvertToInt(infoConfiemed[^1].Text.Split('\r')[0]);
            _area.Active = ConvertToInt(infoData[^1].FindElements(By.TagName("h2"))[0].FindElement(By.ClassName("total")).Text.Split('\r')[0]);
            _area.Recovered = ConvertToInt(infoData[^1].FindElements(By.TagName("h2"))[1].FindElement(By.ClassName("total")).Text.Split('\r')[0]);
            _area.Deaths = ConvertToInt(infoData[^1].FindElements(By.TagName("h2"))[2].FindElement(By.ClassName("total")).Text.Split('\r')[0]);

            return _area;
        }

        private void AddFilter(IWebElement element, By elementWait)
        {
            _navigate++;
            element.FindElement(By.ClassName("area")).Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(c =>
                c.FindElements(elementWait));
        }
        private void RemoveFilter(By elementWait)
        {

            if (_navigate == 0)
            {
                return;
            }

            _driver.FindElement(By.ClassName("selectedAreas")).FindElements(By.ClassName("areaDiv"))
                [_navigate].FindElement(By.TagName("span")).Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(c =>
                c.FindElements(elementWait));

            _navigate--;
        }


        private void ClickOnPage(IWebElement element, By elementWait)
        {
            if (element.Displayed)
            {
                element.Click();
                new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(c =>
                    c.FindElements(By.CssSelector(".areas areaDiv")));
            }
            else
            {
                throw new Exception("'Elemento' nao encontrado ");
            }
        }

        public void Close()
        {
            _driver.Quit();
            _driver = null;
        }

        private void Logger(string message)
        {
            _logger.LogMessage(message);
        }



        private int? ConvertToInt(string str)
        {
            return int.TryParse(str.Replace(".", ""), out var number) ? (int?)number : null;
        }



    }
}
