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
                    await context.PostAsync("Hola soy un bot.");
                    break;
                case "chao":
                    await context.PostAsync("Chao amiguito");
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
            const string idLuis     = "6af33858-401c-4d62-a4d2-2fe3c851a0a8";
            const string keyLuis    = "bb0918a4853048fa9e03090b9649fee7";

            var message = await result as Activity;
            string query = message.Text;

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(new Uri($"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/{idLuis}?subscription-key={keyLuis}&verbose=true&timezoneOffset=0&q={query}"));
            LuisJson luis = JsonConvert.DeserializeObject<LuisJson>(response);

            switch (luis.intents[0].intent)
            {
                case "saludo":

                    break;
                case "despedida":

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







                /* =================================================================== 
                [LuisIntent("None")]
                public async Task None(IDialogContext context, LuisResult result)
                {
                    await context.PostAsync("Lo siento pero no estoy programadao para este tipo de preguntas");
                    await Task.Delay(2000);
                    await context.PostAsync("¿En qué más te puedo ayudar?");
                }

                [LuisIntent("ObtenerAgradecimientos")]
                public async Task ObtenerAgradecimientos(IDialogContext context, LuisResult result)
                {
                    /* await context.PostAsync("Siempre estaré para ayudarte");
                     await Task.Delay(2000);
                     await context.PostAsync("¿En qué más te puedo ayudar?");
                    await SaludoDialog.saludo(context,result);
                }

                [LuisIntent("HacerReclamo")]
                public async Task HacerReclamo(IDialogContext context, LuisResult result)
                {
                    await context.PostAsync("Gracias por reportarlo: "+result.Query);
                    await Task.Delay(2000);
                    await context.PostAsync("Por favor registra tu problema en el siguiente enlace:");

                    EntityRecommendation enre;
                    if(result.TryFindEntity("Per",out enre))
                    {
                        enre.Entity.ToLower(); //get entidad value en minuscula


                    }
                }
                */
    }
}