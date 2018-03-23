using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Sample.SimpleEchoBot;
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
                    await __libs.ProductosGetURL.get(context, result, 1, message.Text,-1);
                    break;
                case "2_votado":
                    await __libs.ProductosGetURL.get(context, result, 2, message.Text, -1);
                    break;
                case "3_cartelera":
                    await __libs.ProductosGetURL.get(context, result, 3, message.Text, -1);
                    break;
                default:
                    await __libs.ProductosGetURL.get(context, result, 4, message.Text, -1);
                    break;
            }
        }

        public static async Task carruselPeliculas_Result(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            string[] a = message.Text.Split('#');
            if (a.Length == 3)
            {
                int tipo = Int32.Parse(a[0]);
                string query = a[1];
                int fin = Int32.Parse(a[2]);
                await __libs.ProductosGetURL.get(context, result, tipo, query,fin);
            }
            else if(a.Length == 2)
            {
                await __libs.ProductosGetURL.get(context, result, 0, a[1], -1);
            }
            else
            {
                await _RouterDialog.router(context,result);
            }
        }

    }
}