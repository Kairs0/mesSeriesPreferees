using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using Series.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Series
{
    //https://docs.microsoft.com/en-us/windows/uwp/controls-and-patterns/tiles-and-notifications-send-local-toast
    //https://docs.microsoft.com/en-us/windows/uwp/controls-and-patterns/tiles-and-notifications-adaptive-interactive-toasts

    //https://docs.microsoft.com/en-us/windows/uwp/threading-async/create-a-periodic-work-item

    //Idée : contient une méthode asyncrhone qui va checker toutes les 5 minutes la sortie d'un épisode d'une série favorite.
    static class NotificationManager
    {
        private static int _compteurNotifs;

        //NE MARCHE PAS ENCORE
        public static void RunNotifService()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1 * 60 * 1000);
                    var toNotif = GetFavoritesOnAirTonight();
                    foreach (var show in toNotif)
                    {
                        SendNotifNewEpisodeOfShow(show.id.ToString());
                    }
                }
            }).GetAwaiter().GetResult();
        }

        public static List<Serie> GetFavoritesOnAirTonight()
        {
            var result = new List<Serie>();
            var favorites = Favoris.GetFavorites();
            var allCodesCountries = new List<string>();
            var onAirForAllCountriesFavorites = new List<Episode>();

            foreach (var show in favorites)
            {
                if (show.network.country != null)
                {
                    allCodesCountries.Add(show.network.country.code);
                }
            }
            allCodesCountries.Add("FR");
            var codesContries = allCodesCountries.Distinct();

            foreach (var codeCountry in codesContries)
            {
                onAirForAllCountriesFavorites = onAirForAllCountriesFavorites.Concat(Api.GetEpisodesToNight(codeCountry)).ToList();
            }

            foreach (var episode in onAirForAllCountriesFavorites)
            {
                if (favorites.Select(x => x.id).Contains(episode.show.id))
                {
                    result.Add(episode.show);
                }
            }
            return result;
        }


        public static void SendNotifNewEpisodeOfShow(string idShow)
        {
            //todo a changer p - e : donner directement un object show?
            var show = Api.GetShowById(idShow);
            //Data notification
            string title = "New Episode of " + show.name + " tonight !";
            string image = show.image.medium;

            //Construction Partie visuelle
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },
                        new AdaptiveImage()
                        {
                            Source = image
                        }
                    }
                }
            };

            //Construction partie actions
            ToastActionsCustom actions = new ToastActionsCustom()
            {
                Buttons =
                {
                    new ToastButton("See show details", new QueryString()
                    {
                        { "action", "viewShow" },
                        { "idShow", idShow }

                    }.ToString())
                }
            };

            //Assemblage du contenu
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
                Actions = actions
            };

            //Cree la notif
            var toast = new ToastNotification(toastContent.GetXml())
            {
                ExpirationTime = DateTime.Now.AddDays(1),
                Tag = _compteurNotifs.ToString(),
                Group = "Series Notifications"
            };

            //envoie la notif
            ToastNotificationManager.CreateToastNotifier().Show(toast);
            _compteurNotifs++;
        }
    }
}
