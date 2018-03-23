using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using SimpleEchoBot.__libs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleEchoBot.Views
{
    public class PeliculaView
    {
        public static async Task peliculaResumen(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            //await context.PostAsync("canceaste barrigon<<: "+message.Text);
            await ProductosGetURL.get(context,result,1, message.Text);
            /*
            var reply = context.MakeMessage();

            var att = hero();
            reply.Attachments.Add(att);

            await context.PostAsync(reply);
            */
        }

        public static async Task attachment(IDialogContext context, IAwaitable<object> result,object obj)
        {
            var reply = context.MakeMessage();

            var att = PeliculaHeroResumen((PeliculaJson)obj);
            reply.Attachments.Add(att);

            await context.PostAsync(reply);
        }

        public static Attachment PeliculaHeroResumen(PeliculaJson pelicula)
        {
            string overview = pelicula.overview;
            if(overview.Length>150) pelicula.overview.Substring(0, 150);

            var h = new HeroCard
            {
                Title = pelicula.title,
                Subtitle = pelicula.original_title+" ("+pelicula.release_date.Split('-')[0]+")",
                Text = pelicula.overview,
                Images = new List<CardImage> { new CardImage("https://image.tmdb.org/t/p/w185/"+pelicula.poster_path) },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Ver mas", value: "http://google.com") }
            };
            return h.ToAttachment();
        }
    }
}