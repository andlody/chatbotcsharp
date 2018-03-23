using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace SimpleEchoBot.Views
{
    public static class BusquedaView
    {
        public static async Task BusquedaGeneral(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Choice(
                    context,
                    Dialogs.BusquedasDialog.BusquedaGeneral_Result,
                    new[] {"Genero","Idioma","ID","Cancelar" },
                    "Puedes escribir lo que buscas, o elegir una opcion mas avanzada de busqueda.",
                    promptStyle: PromptStyle.Keyboard
                 );
        }
        public static async Task BusquedaId(IDialogContext context, IAwaitable<object> result)
        {
         /*   PromptDialog.Choice(
                    context,
                    Dialogs.BusquedasDialog.BusquedaId_Result,
                    new[] { "Cancelar" },
                    "Escribe el ID de la Pelicula. Tambien Puedes cancelar esta opción.",
                    "Escribe el ID de la Pelicula o click en cancelar",
                    promptStyle: PromptStyle.Keyboard
                 );  */
            await context.PostAsync("Escribe el ID de la Pelicula o escribe cancelar.");
            context.Wait(Dialogs.BusquedasDialog.BusquedaId_Result);
        }
    }
}