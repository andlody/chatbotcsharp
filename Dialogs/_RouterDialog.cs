using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using SimpleEchoBot.Dialogs;
using Newtonsoft.Json;
using SimpleEchoBot.__libs;
using SimpleEchoBot.Models;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class _RouterDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(router);
        }    

        public static async Task router(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            switch (message.Text.ToLower())
            {
                case "hola":
                    await context.PostAsync("Hola soy un chatbot, escribe: buscar"); context.Wait(router);
                    break;
                case "hello":
                    await context.PostAsync("Hola como estas, yo hablo español, escribe: buscar"); context.Wait(router);
                    break;
                case "hi":
                    await context.PostAsync("Yo hablo español, escribe: buscar"); context.Wait(router);
                    break;
                case "info":
                    await context.PostAsync("Esta es una pagina de guia de peliculas, escribe: buscar"); context.Wait(router);
                    break;
                case "help":
                    await context.PostAsync("Me gustan ver peliculas. Esta es una pagina de guia de peliculas, escribe: buscar");context.Wait(router);
                    break;
                case "buscar":
                    await BusquedasDialog.BusquedaGeneral(context,result);
                    break;
                default:
                    await luis(context, result);
                    break;
            }
        }

        private static async Task luis(IDialogContext context, IAwaitable<object> result)
        {
            const string idLuis     = "3f85c553-3eec-4ff1-8a8d-d409800e7142";
            const string keyLuis    = "bb0918a4853048fa9e03090b9649fee7";

            var message = await result as Activity;
            string query = message.Text;

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(new Uri($"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/{idLuis}?subscription-key={keyLuis}&verbose=true&timezoneOffset=0&q={query}"));
            Luis luis = JsonConvert.DeserializeObject<Luis>(response);

            switch (luis.intents[0].intent)
            {
                case "saludo":
                    await SaludoDialog.saludo(context, result);
                    break;
                case "despedida":
                    await DespedidaDialog.despedida(context, result);
                    break;
                case "recomendacion":
                    await LuisDialog.recomendacion(context, result,luis);
                    break;
                case "buscar_producto":
                    await LuisDialog.buscar_producto(context, result, luis);
                    break;
                case "None":
                    await context.PostAsync("creo que no te he entendido");
                    context.Wait(router);
                    break;
            }
        }
















        /*
            [LuisModel(modelID: "6af33858-401c-4d62-a4d2-2fe3c851a0a8", subscriptionKey: "bb0918a4853048fa9e03090b9649fee7")]

            [Serializable]
            public class _IndexLuisDialog : LuisDialog<string>
            {
                [LuisIntent("saludo")]
                public async Task saludo(IDialogContext context, LuisResult result)
                {
                    await SaludoDialog.saludo(context, result);
                }

                [LuisIntent("despedida")]
                public async Task despedida(IDialogContext context, LuisResult result)
                {
                    await SaludoDialog.saludo(context, result);
                }

                [LuisIntent("buscar_productos")]
                public async Task buscar_productos(IDialogContext context, LuisResult result)
                {
                    await SaludoDialog.saludo(context, result);
                }

                [LuisIntent("recomendacion")]
                public async Task recomendacion(IDialogContext context, LuisResult result)
                {
                    await SaludoDialog.saludo(context, result);
                }

                [LuisIntent("comprar_producto")]
                public async Task comprar_producto(IDialogContext context, LuisResult result)
                {
                    await SaludoDialog.saludo(context, result);
                }

                [LuisIntent("hacer_reclamo")]
                public async Task hacer_reclamo(IDialogContext context, LuisResult result)
                {
                    await SaludoDialog.saludo(context, result);
                }

                [LuisIntent("None")]
                public async Task None(IDialogContext context, LuisResult result)
                {
                    await SaludoDialog.saludo(context, result);
                }
                */
    }
}