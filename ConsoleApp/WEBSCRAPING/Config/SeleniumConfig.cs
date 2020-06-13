using System;
using System.Collections.Generic;
using System.Text;

namespace WEBSCRAPING.Config
{
    public class SeleniumConfig
    {
        public string DrivePath { get; set; }
        public string PageUrl { get; set; }
        public int Timeout { get; set; }
        public int NavigateMax { get; set; }
        public bool ApiSave { get; set; }
        public bool Headless { get; set; }

       
    }
}
