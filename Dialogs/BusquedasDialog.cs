using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace SimpleEchoBot.Dialogs
{
    public static class BusquedasDialog
    {
        public static async Task BusquedaGeneral(IDialogContext context, IAwaitable<object> result)
        {
            await Views.BusquedaView.BusquedaGeneral(context,result);
        }

        public static async Task BusquedaGeneral_Result(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;

            switch (message.Text.ToLower())
            {
                case "1_popular":
                    await __libs.ProductosGetURL.get(context, result, 1, message.Text);
                    break;
                case "2_votado":
                    await context.PostAsync("canceaste barrigon.1");
                    break;
                case "3_cartelera":
                    await context.PostAsync("canceaste barrigon.1");
                    break;
                default:

                    break;
            }
        }

        public static async Task BusquedaId_Result(IDialogContext context, IAwaitable<object> result)
        {            
            var message = await result as Activity;      
            
            switch (message.Text.ToLower())
            {
                case "cancelar":
                    await context.PostAsync("canceaste barrigon.2");
                    break;
                default:
                    await Views.PeliculaView.peliculaResumen(context, result);
                    break;
            }
        }
    }
}