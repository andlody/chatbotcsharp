using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Sample.SimpleEchoBot;
using SimpleEchoBot.__libs;
using SimpleEchoBot.Dialogs;
using SimpleEchoBot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleEchoBot.Views
{
    public class PeliculaView
    {
        public static async Task carruselPeliculas(IDialogContext context, IAwaitable<object> result, Busqueda obj,int tipo, string query, int fin)
        {
            var reply = context.MakeMessage();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            int ini = fin;
            if (fin == -1)
            {
                ini = 0;
                fin = 0;
            }

            fin = fin + 5;


            while(fin > obj.results.Length)
            {
                fin--;
            }

            for (int i = ini; i < fin; i++)
            {                
                reply.Attachments.Add(peliculaResumen(obj.results[i]));
            }

            if (obj.results.Length>fin) {
                var h = new HeroCard
                {
                    Title = "Aun hay más..!",
                    Images = new List<CardImage> { new CardImage("http://tiendabots.azurewebsites.net/__public/bot_buscar.png") },
                    Buttons = new List<CardAction> {
                        new CardAction(ActionTypes.PostBack, "Buscar más", value: tipo+"#"+query+"#"+fin)
                    }
                };
                reply.Attachments.Add(h.ToAttachment());
            }

            await context.PostAsync(reply);


            if (obj.results.Length <= fin)
            {                
                reply.Type = ActivityTypes.Typing;
                await context.PostAsync(reply);
                await Task.Delay(4000);
                await context.PostAsync("Esos son todos mis resultados de busqueda.");
            }
            context.Wait(Dialogs.BusquedasDialog.carruselPeliculas_Result);
        }


        public static Attachment peliculaResumen(PeliculaResumen pelicula)
        {
            string overview = pelicula.overview;
            if (overview.Length > 150) pelicula.overview.Substring(0, 150);

            
            var h = new HeroCard
            {
                Title = pelicula.title + " (" + pelicula.release_date.Split('-')[0] + ")",
                Subtitle = pelicula.original_title + " ",
                Text = " " + pelicula.overview,
                Images = (pelicula.poster_path != null) ? new List<CardImage> { new CardImage("https://image.tmdb.org/t/p/w342/" + pelicula.poster_path) }:null,
                Buttons = new List<CardAction> {
                    new CardAction(ActionTypes.PostBack, "Más detalles", value: "id#"+pelicula.id)
                }
            };
            return h.ToAttachment();
        }

        public static async Task peliculaCompleta(IDialogContext context, IAwaitable<object> result, PeliculaCompleta pelicula)
        {
            var reply = context.MakeMessage();
            reply.Attachments.Add(new Attachment()
            {
                ContentUrl = "https://image.tmdb.org/t/p/w342/" + pelicula.poster_path,
                ContentType = "image/jpg",
                Name = pelicula.id + ".jpg"
            });
            if (pelicula.poster_path != null) await context.PostAsync(reply);

            reply.Type = ActivityTypes.Typing;
            await context.PostAsync(reply);
            await Task.Delay(6000);

            await context.PostAsync(pelicula.title + "es una película de "+pelicula.genres[0].name+" ( Fecha de extreno: " + pelicula.release_date + "), su nombre original es " + pelicula.original_title + ", fue producido en " + pelicula.production_countries[0].name + " por " + pelicula.production_companies[0].name);

            reply.Type = ActivityTypes.Typing;
            await context.PostAsync(reply);
            await Task.Delay(10000);

            if (!pelicula.overview.Equals("")) await context.PostAsync(pelicula.overview);

            reply.Type = ActivityTypes.Typing;
            await context.PostAsync(reply);
            await Task.Delay(3000);

            await PeliculaDialog.getVideo(context,result,""+pelicula.id);        
        }

        public static async Task verVideoConsulta(IDialogContext context, IAwaitable<object> result, Video video, string id)
        {
            var reply = context.MakeMessage();
            var h = new HeroCard
            {
                Text = "¿Deseas ver el Trailer de la pelicula?.",
                Buttons = new List<CardAction> {
                    new CardAction(ActionTypes.PostBack, "Si", value: id),
                    new CardAction(ActionTypes.PostBack, "No", value: "no")
                }
            };
            
            reply.Attachments.Add(h.ToAttachment());

            await context.PostAsync(reply);
            context.Wait(PeliculaDialog.verVideo);
        }

        public static async Task verVideo(IDialogContext context, IAwaitable<object> result,Video video,string id)
        {
            var reply = context.MakeMessage();
          /*  var videocard = new VideoCard
            {
                Subtitle = video.results[0].name,
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "https://youtu.be/"+video.results[0].key
                    }
                }
            };
            reply.Attachments.Add(videocard.ToAttachment());
            */


            reply.Attachments.Add(new Attachment()
            {
                ContentUrl = "https://youtu.be/" + video.results[0].key,
                ContentType = "video/*",
                Name = "Trailer: "+video.id
            });



            await context.PostAsync(reply);

            await buscarPeliculaSimilar(context,result,id);
        }

        public static async Task buscarPeliculaSimilar(IDialogContext context, IAwaitable<object> result, string id)
        {
            var reply = context.MakeMessage();
            var h = new HeroCard
            {
                Text = "Tal vez te interese...",
                Buttons = new List<CardAction> {
                    new CardAction(ActionTypes.PostBack, "Similares 🔵", value: id),
                    new CardAction(ActionTypes.PostBack, "Sugeridas 🔴", value: "no")
                }
            };
            reply.Attachments.Add(h.ToAttachment());
            await context.PostAsync(reply);

            context.Wait(_RouterDialog.router);
        }
    }
}