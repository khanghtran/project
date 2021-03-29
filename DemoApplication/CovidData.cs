using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DemoApplication
{
    public class CovidData
    {
        public DateTime submission_date { get; set; }
        public string state { get; set; }
        public string tot_cases { get; set; }
        public string conf_cases { get; set; }
        public string prob_cases { get; set; }
        public string new_case { get; set; }
        public string pnew_case { get; set; }
        public string tot_death { get; set; }
        public string conf_death { get; set; }
        public string prob_death { get; set; }
        public string new_death { get; set; }
        public string pnew_death { get; set; }
        public DateTime created_at { get; set; }
        public string consent_cases { get; set; }
        public string consent_deaths { get; set; }
    }
}