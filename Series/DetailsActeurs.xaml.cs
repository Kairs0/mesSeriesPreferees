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
    public sealed partial class DetailsActeurs : Page
    {
        public People Acteur;
        public List<Serie> ListeSeriesJouees;
        public string ImageUrl;
        public int ActeurId;


         protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Acteur = (People)e.Parameter;
            TitrePage.Text = Acteur.name;
            ListeSeriesJouees = Api.GetShowsForPeople(Acteur.id.ToString());
            ActeurId = Acteur.id;
            base.OnNavigatedTo(e);
            ImageUrl = Acteur.image.medium;
            ListeSeries.ItemsSource = ListeSeriesJouees;
        }
        


        public DetailsActeurs()
        {
            this.InitializeComponent();
        }


        private void SerieSelectionnee(object sender, RoutedEventArgs e)
        {
            Serie SerieSelectionne = ListeSeries.SelectedItem as Serie;
        //    string NomSerie = SerieSelectionne.name;
            this.Frame.Navigate(typeof(DetailsSerie), SerieSelectionne);
        }

        private void ClickBouttonRetour(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
