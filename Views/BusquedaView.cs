using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Sample.SimpleEchoBot;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleEchoBot.Views
{
    public static class BusquedaView
    {
        public static async Task BusquedaGeneral(IDialogContext context, IAwaitable<object> result)
        {
            var h = new HeroCard
            {
                //Title = "Tengo algunas opciones!",
                Text = "Si deseas escribe el nombre de la pelicula y lo buscaré.",
                //Images = new List<CardImage> { new CardImage("https://images.pexels.com/photos/247932/pexels-photo-247932.jpeg?w=940&h=650&auto=compress&cs=tinysrgb") },
                //Images = new List<CardImage> { new CardImage("http://tiendabots.azurewebsites.net/__public/buscar.png") },
                Buttons = new List<CardAction> {
                    new CardAction(ActionTypes.PostBack, "Más populares 🌐", value: "1_popular"),
                    new CardAction(ActionTypes.PostBack, "Más votados 🔵", value: "2_votado"),
                    new CardAction(ActionTypes.PostBack, "En cartelera 🔴", value: "3_cartelera")
                }
            };

            var reply = context.MakeMessage();
            reply.Attachments.Add(h.ToAttachment());

            await context.PostAsync(reply);
            context.Wait(Dialogs.BusquedasDialog.BusquedaGeneral_Result);
        }

        public static async Task noEncontrado(IDialogContext context, IAwaitable<object> result)
        {         
            await context.PostAsync("No encontre ningun resultado... :(");
            context.Wait(_RouterDialog.router);
        }

    }
}