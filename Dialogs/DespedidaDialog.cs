using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Sample.SimpleEchoBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Dialogs
{
    public class DespedidaDialog
    {
        public static async System.Threading.Tasks.Task despedida(IDialogContext context, IAwaitable<object> result)
        {
            string name = context.Activity.From.Name;
            string[] saludos = { $"Si aun necesitas ayuda estare aqui pra ayudarte ;)" };
            await context.PostAsync(saludos[0]);
            context.Wait(_RouterDialog.router);
        }
    }
}