using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Models
{
    public class Busqueda
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public PeliculaResumen[] results { get; set; }        
    }
}