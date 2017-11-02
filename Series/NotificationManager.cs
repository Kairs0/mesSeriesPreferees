using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
using Microsoft.QueryStringDotNET;

namespace Series
{
    //https://docs.microsoft.com/en-us/windows/uwp/controls-and-patterns/tiles-and-notifications-send-local-toast

    //Idée : contient une méthode asyncrhone qui va checker toutes les 5 minutes la sortie d'un épisode d'une série favorite.
    static class NotificationManager
    {
        public static void SendNotifNewEpisodeOfShow(string idShow)
        {
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

            ToastActionsCustom actions = new ToastActionsCustom()
            {
                Buttons =
                {
                    new ToastButton("View", new QueryString()
                    {
                        //TODO
                        { "action", "viewImage" },
                        { "imageUrl", image }

                    }.ToString())
                }
            };


            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
                Actions = actions
            };

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());

            //--------------------- Set an expiration time, Provide a primary key for your toast     ---------------------
            toast.ExpirationTime = DateTime.Now.AddDays(2);

            toast.Tag = "18365";
            toast.Group = "wallPosts";

            //--------------------- Send the notification     ---------------------
            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }

    }
}
