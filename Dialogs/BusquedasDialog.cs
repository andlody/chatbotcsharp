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
            string text = (string)await result;

            switch (text.ToLower())
            {
                case "id":
                    await Views.BusquedaView.BusquedaId(context, result);
                    break;
                case "cancelar":
                    await context.PostAsync("canceaste barrigon.1");
                    break;
            }
        }

        public static async Task BusquedaId_Result(IDialogContext context, IAwaitable<object> result)
        {
            //bool bnd = false;
            string text = "cancelar";
            try
            {
                text = (string)await result;
            }
            catch (Exception e) {
                var message = await result as Activity;
                text = message.Text;
            }
            
            switch (text.ToLower())
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