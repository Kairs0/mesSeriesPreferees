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
using Series.Models;


namespace Series
{
    public sealed partial class DetailsSaison : Page
    {
        private Saison DetailSaison;
        private string NumeroSaison;
        private List<Episode> ListeEpisodesSaison;
        private string ResumeSaison;
        private Models.Image ImageSaison;



         protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DetailSaison = e.Parameter as Saison;
            TitrePage.Text = DetailSaison.name;
            ResumeSaison = DetailSaison.summary;


        }
        


        public DetailsSaison()
        {
            this.InitializeComponent();
        }



        private void ClickBouttonRetour(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack(); ;
        }

        private void ClickBouttonAccueil(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void EpisodeSelectionne(object sender, RoutedEventArgs e)
        {

        }
    }
}
