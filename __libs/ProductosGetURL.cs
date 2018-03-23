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
                case 1: url = "movie/" + text;  break;
            }

            const string api_key = "33a35968f1e69c959a10c0322b1a205f";

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(new Uri($"https://api.themoviedb.org/3/{url}?api_key={api_key}&language=es"));
            LuisClass luis = JsonConvert.DeserializeObject<LuisClass>(response);
            switch (tipo)
            {
                case 1:
                    PeliculaJson obj = JsonConvert.DeserializeObject<PeliculaJson>(response);
                    await Views.PeliculaView.attachment(context,result,obj);
                    break;
            }
        }
    }
}