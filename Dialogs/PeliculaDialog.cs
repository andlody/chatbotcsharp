using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SimpleEchoBot.Dialogs
{
    public class PeliculaDialog
    {
        public static async Task verVideo(IDialogContext context, IAwaitable<object> result)
        {
            string id = (string)await result;
            await __libs.ProductosGetURL.get(context, result, 5, id, -1);
        }
    }
}