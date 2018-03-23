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
    public class PeliculaDialog
    {
        public static async Task getVideo(IDialogContext context, IAwaitable<object> result,string id)
        {            
            var message = await result as Activity;
            await __libs.ProductosGetURL.get(context, result, 5, id, 0);
        }
        public static async Task verVideo(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            if (!message.Text.ToLower().Equals("no"))
                await __libs.ProductosGetURL.get(context, result, 5, message.Text, 1);
            else
                context.Wait(_RouterDialog.router);
        }
    }
}