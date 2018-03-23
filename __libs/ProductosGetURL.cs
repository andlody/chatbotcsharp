using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;
using SimpleEchoBot.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SimpleEchoBot.__libs
{
    public class ProductosGetURL
    {
        public static async Task get(IDialogContext context, IAwaitable<object> result,int tipo, string text, int fin)
        {
            string url = "";
            switch (tipo)
            {
                case 0: url = $"movie/{text}?"; break;
                case 1: url = "movie/popular?"; break;
                case 2: url = "movie/top_rated?"; break;
                case 3: url = "movie/now_playing?"; break;
                case 4:
                    text = HttpUtility.UrlEncode(text);
                    url = $"search/movie?query={text}&"; break;
                case 5: url = $"movie/{text}/videos?"; break;
            }

            const string api_key = "33a35968f1e69c959a10c0322b1a205f";

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(new Uri($"https://api.themoviedb.org/3/{url}api_key={api_key}&language=es"));

            switch (tipo)
            {
                case 0:
                    await Views.PeliculaView.peliculaCompleta(context, result, JsonConvert.DeserializeObject<PeliculaCompleta>(response));
                    break;
                case 5:
                    await Views.PeliculaView.verVideo(context, result, JsonConvert.DeserializeObject<Video>(response));
                    break;
                default:
                    Busqueda bsq = JsonConvert.DeserializeObject<Busqueda>(response);
                    if (bsq.results.Length > 0)
                    {
                        await Views.PeliculaView.carruselPeliculas(context, result, bsq, tipo, text, fin);
                    }else
                    {
                        await Views.BusquedaView.noEncontrado(context, result);
                    }
                    break;
            }                    
        }
    }
}