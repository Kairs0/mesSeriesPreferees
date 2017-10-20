using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
// https://docs.microsoft.com/en-us/windows/uwp/layout/navigate-between-two-pages

namespace Series
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsSerie : Page
    {

        public string NomSerie;
       // public Image ImageSerie { get; set; }
       public string ImageUrl { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NomSerie = (string)e.Parameter;
            TitrePage.Text = NomSerie;
            base.OnNavigatedTo(e);
        }

          public DetailsSerie()
        {
            this.InitializeComponent();

             //         Serie InfosSerie = Api.GetShowByName(NomSerieRecherche);
            //           Nom_Serie = InfosSerie.name;
            //           ImageSerie = InfosSerie.image;

            ImageUrl = "http://static.tvmaze.com/uploads/images/medium_portrait/39/99906.jpg";

        }

        private void ClickBouttonRetour(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
