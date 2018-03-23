using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleEchoBot.Views
{
    public static class BusquedaView
    {
        public static async Task BusquedaGeneral(IDialogContext context, IAwaitable<object> result)
        {
         /*   PromptDialog.Choice(
                    context,
                    Dialogs.BusquedasDialog.BusquedaGeneral_Result,
                    new[] {"Genero","Idioma","ID","Cancelar" },
                    "Puedes escribir lo que buscas, o elegir una opcion mas avanzada de busqueda.",
                    promptStyle: PromptStyle.Keyboard
                 );  */
            var h = new HeroCard
            {
                //Title = "Tengo algunas opciones!",
                Text = "Si deseas escribe el nombre de la pelicula y la buscaré.",
                //Images = new List<CardImage> { new CardImage("https://images.pexels.com/photos/247932/pexels-photo-247932.jpeg?w=940&h=650&auto=compress&cs=tinysrgb") },
                //Images = new List<CardImage> { new CardImage("http://tiendabots.azurewebsites.net/__public/buscar.png") },
                Buttons = new List<CardAction> {
                    new CardAction(ActionTypes.PostBack, "Más populares", value: "1_popular"),
                    new CardAction(ActionTypes.PostBack, "Más votados", value: "2_votado"),
                    new CardAction(ActionTypes.PostBack, "En cartelera", value: "3_cartelera")
                }
            };

            var reply = context.MakeMessage();
            reply.Attachments.Add(h.ToAttachment());

            await context.PostAsync(reply);
            context.Wait(Dialogs.BusquedasDialog.BusquedaGeneral_Result);
        }
        public static async Task BusquedaId(IDialogContext context, IAwaitable<object> result)
        {/*
            PromptDialog.Choice(
                    context,
                    Dialogs.BusquedasDialog.BusquedaId_Result,
                    new[] { "Cancelar" },
                    "Escribe el ID de la Pelicula. Tambien Puedes cancelar esta opción.",
                    promptStyle: PromptStyle.Keyboard
                 ); // */
            //await context.PostAsync("Escribe el ID de la Pelicula o escribe cancelar.");
            //context.Wait(Dialogs.BusquedasDialog.BusquedaId_Result);

            var h = new HeroCard
            {
                Title = "Tarjeta URL",
                Subtitle = "Mi tarjeta",
                Text = "mi texto",
                Images = new List<CardImage> { new CardImage("https://images.pexels.com/photos/247932/pexels-photo-247932.jpeg?w=940&h=650&auto=compress&cs=tinysrgb") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.PostBack, "Opcion", value: "321") }
            };

            var reply = context.MakeMessage();

            var att = h.ToAttachment();
            reply.Attachments.Add(att);

            await context.PostAsync(reply);
            context.Wait(Dialogs.BusquedasDialog.BusquedaId_Result);
         
        }
    }
}