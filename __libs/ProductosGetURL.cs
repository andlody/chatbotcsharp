using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SimpleEchoBot.__libs
{
    public class ProductosGetURL
    {
        public static async Task get(IDialogContext context, IAwaitable<object> result,int tipo, string text)
        {
            string url = "";
            switch (tipo)
            {
                case 0: url = "movie/" + text;  break;
                case 1: url = "movie/popular"; break;
                case 2: url = "movie/" + text; break;
            }

            const string api_key = "33a35968f1e69c959a10c0322b1a205f";

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(new Uri($"https://api.themoviedb.org/3/{url}?api_key={api_key}&language=es"));
            
            switch (tipo)
            {
                case 0:
                    await Views.PeliculaView.attachment(context,result, JsonConvert.DeserializeObject<PeliculaJson>(response));
                    break;
                case 1:
                    await Views.PeliculaView.carruselPeliculas(context, result, JsonConvert.DeserializeObject<BusquedaJson>(response));
                    break;
            }
        }
    }
}