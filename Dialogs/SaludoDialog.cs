using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System.Threading.Tasks;

namespace SimpleEchoBot.Dialogs
{
    public static class SaludoDialog
    {
        public static async Task saludo(IDialogContext context, LuisResult result)
        {
            string name = context.Activity.From.Name;
            string[] saludos = {$"Hola {name}! ¿en que puedo ayudarte?"};
            await context.PostAsync(saludos[0]);
        }
    }
}