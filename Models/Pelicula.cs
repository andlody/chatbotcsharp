using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Models
{
    public class Pelicula
    {
        public string nombre;// { get; set; }

        public Pelicula()
        {

        }
        public string getNombre()
        {
            return nombre;
        }
    }
}