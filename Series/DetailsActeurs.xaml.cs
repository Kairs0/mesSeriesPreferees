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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Series
{
    public sealed partial class DetailsActeurs : Page
    {

        public string NomActeur;
        public List<Serie> ListeSeriesJouees;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NomActeur = (string)e.Parameter;
            TitrePage.Text = NomActeur;
            ListeSeriesJouees = Api.GetShowsForPeople(NomActeur);
            // ImageUrl = InfosSerie.image.medium;
            base.OnNavigatedTo(e);
        }



        public DetailsActeurs()
        {
            this.InitializeComponent();
        }

        private void ClickVersSerie(object sender, ItemClickEventArgs e)
        {
            string NomSerie = e.OriginalSource.ToString();
            this.Frame.Navigate(typeof(DetailsSerie), NomSerie);
        }

        private void ClickBouttonRetour(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
