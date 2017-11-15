using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
    public sealed partial class DetailsActeurs : Page
    {
        public People Acteur;
        public List<Serie> ListeSeriesJouees;
        public string ImageUrl;
        public int ActeurId;

        // Construction de la page vaec l'Acteur reçu en argument de navigation
         protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                this.Frame.Navigate(typeof(NoConnection));
            }

            Acteur = (People)e.Parameter;
            TitrePage.Text = Acteur.Name;
            ListeSeriesJouees = Api.GetShowsForPeople(Acteur.Id.ToString());
            ActeurId = Acteur.Id;
            base.OnNavigatedTo(e);
            ImageUrl = Acteur.Image.Medium;
            ListeSeries.ItemsSource = ListeSeriesJouees;
        }
        


        public DetailsActeurs()
        {
            this.InitializeComponent();
        }

        // Navigation vers la série sélectionnée dans la liste
        private void SerieSelectionnee(object sender, RoutedEventArgs e)
        {
            Serie SerieSelectionne = ListeSeries.SelectedItem as Serie;
            this.Frame.Navigate(typeof(DetailsSerie), SerieSelectionne);
        }

        private void ClickBouttonRetour(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack(); ;
        }

        private void ClickBouttonAccueil(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
