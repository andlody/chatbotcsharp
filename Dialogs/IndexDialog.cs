using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [LuisModel(modelID: "6af33858-401c-4d62-a4d2-2fe3c851a0a8", subscriptionKey: "bb0918a4853048fa9e03090b9649fee7")]

    [Serializable]
    public class IndexDialog : LuisDialog<string>
    {
    /*    protected int count = 1;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (message.Text == "reset")
            {
                PromptDialog.Confirm(
                    context,
                    AfterResetAsync,
                    "Hola "+message.From.Name,
                    "Didn't get that!",
                    promptStyle: PromptStyle.Auto);
            }
            else
            {
                await context.PostAsync($"{this.count++}: Usted dijo: {message.Text}");
                context.Wait(MessageReceivedAsync);
            }
        }

        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }
            context.Wait(MessageReceivedAsync);
        }
        */

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
            await context.PostAsync("Siempre estaré para ayudarte");
            await Task.Delay(2000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
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

    }
}