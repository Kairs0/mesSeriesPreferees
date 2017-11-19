using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using Series.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.System.Threading;
using Windows.UI.Notifications;

namespace Series
{
    //Idée : contient une méthode asyncrhone qui va checker toutes les 10 minutes la sortie d'un épisode d'une série favorite.
    static class NotificationManager
    {
        private static int _compteurNotifs;
        private static List<Serie> _alreadySent;
        
        public static void Run()
        {
            /*
             * LIGNE A DECOMMENTER POUR TESTER LES NOTIFICATIONS (envoie les notifs pour les séries toutes les 30 secondes)
             */
            //TimeSpan period = TimeSpan.FromSeconds(30);

            /*
             * Ligne à commenter pendant le test des notification
             */
            TimeSpan period = TimeSpan.FromMinutes(10);


            //publie les notifs pour les séries "non notifiées" toutes les dix minutes, reset la liste alreadySent tous les jours.
            TimeSpan periodResetSent = TimeSpan.FromDays(1);
            _alreadySent = new List<Serie>();

            //Publication des notifs
            ThreadPoolTimer unused = ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                SendAllNotifs();
            }, period);

            //Reset compteur
            ThreadPoolTimer unused1 = ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                _alreadySent = new List<Serie>();
            }, periodResetSent);
        }

        private static void SendAllNotifs()
        {
            //On checke les ids de alreadySent pour ne pas réenvoyer de notifications pour la même série
            List<Serie> toNotif = GetFavoritesOnAirTonight().Where(s => _alreadySent.All(x => x.Id != s.Id)).ToList();

            foreach (var show in toNotif)
            {
                SendNotifNewEpisodeOfShow(show.Id.ToString());
                _alreadySent.Add(show);
            }
        }

        //Retourne la liste des séries dont un nouvel épisode est diffusé le soir
        private static List<Serie> GetFavoritesOnAirTonight()
        {
            var result = new List<Serie>();
            var favorites = Favoris.GetFavorites();
            var allCodesCountries = new List<string>();
            var onAirForAllCountriesFavorites = new List<Episode>();

            //Récupère le code pays de chacun des favoris
            foreach (var show in favorites)
            {
                if (show.Network?.Country != null)
                {
                    allCodesCountries.Add(show.Network.Country.Code);
                }
            }

            //Récupère la liste des épisodes diffusés pour chacun des pays 
            foreach (var codeCountry in allCodesCountries.Distinct())
            {
                onAirForAllCountriesFavorites = onAirForAllCountriesFavorites.Concat(Api.GetEpisodesToNight(codeCountry)).ToList();
            }

            //Depuis la liste précédente, construit la liste des séries pour lesquelles une notification va être envoyée
            foreach (var episode in onAirForAllCountriesFavorites)
            {
                if (favorites.Select(x => x.Id).Contains(episode.Show.Id))
                {
                    result.Add(episode.Show);
                }
            }
            return result;
        }

        //Envoie la notification pour une série
        private static void SendNotifNewEpisodeOfShow(string idShow)
        {
            var show = Api.GetShowById(idShow);
            
            //Données de la notification
            string title = "New Episode of " + show.Name + " tonight !";
            string image = show.Image.Medium;

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
                        { "action", "viewShow" },//Action (voir App.xaml.cs, OnActivated)
                        { "idShow", idShow }//Arguments de l'action

                    }.ToString())
                }
            };

            //Assemblage du contenu
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
                Actions = actions
            };

            //Crée la notification
            var toast = new ToastNotification(toastContent.GetXml())
            {
                ExpirationTime = DateTime.Now.AddDays(1),
                Tag = _compteurNotifs.ToString(),
                Group = "Series Notifications"
            };

            //Envoie la notification
            ToastNotificationManager.CreateToastNotifier().Show(toast);
            _compteurNotifs++;
        }
    }
}
