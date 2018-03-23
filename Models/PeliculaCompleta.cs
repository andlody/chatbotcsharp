using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Models
{
    public class PeliculaCompleta:PeliculaResumen
    {
        public ProductionCompany[] production_companies { get; set; }
        public ProductionCountry[] production_countries { get; set; }
        public Genre[] genres { get; set; }


        public class Genre
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class ProductionCompany
        {
            public int id { get; set; }
            public string logo_path { get; set; }
            public string name { get; set; }
            public string origin_country { get; set; }
        }

        public class ProductionCountry
        {
            public string iso_3166_1 { get; set; }
            public string name { get; set; }
        }
    }
}