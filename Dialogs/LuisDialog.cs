using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Sample.SimpleEchoBot;
using SimpleEchoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SimpleEchoBot.Dialogs
{
    public class LuisDialog
    {
        public static async Task recomendacion(IDialogContext context, IAwaitable<object> result, Luis luis)
        {
            var message = await result as Activity;
            //await context.PostAsync("entites recome: ");// +luis.entities[0] );

            await context.PostAsync("¿Que pelicula estas buscando? ");
            context.Wait(BusquedasDialog.BusquedaGeneral_Result);

            //context.Wait(_RouterDialog.router);
        }

        public static async Task buscar_producto(IDialogContext context, IAwaitable<object> result, Luis luis)
        {
            var message = await result as Activity;

            //await context.PostAsync("entites recome: ");// +luis.entities[0] );
            /*   await context.PostAsync("entites buscar: ");// + luis.entities[0]);
                if(luis.entities.Length>0)
                    await context.PostAsync("entites buscar: "+ luis.entities[0]);  */
            //context.Wait(_RouterDialog.router);

            await BusquedasDialog.BusquedaGeneral(context, result);
        }
    }
}