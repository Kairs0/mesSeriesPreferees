using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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

    public sealed partial class DetailsSerie : Page
    {

        public string NomSerie;
       public Models.Image ImageSerie { get; set; }
        public string Image_Url;

        private string Nom_Serie;
        private string ID_Serie;
        private List<BindPersonToCharacter> ListePersonnes;
        private List<Saison> ListeSaisons;
        private List<Episode> ListeEpisode;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //ID_Serie = (string)e.Parameter;
            if (e.Parameter != null)
            {
                Serie InfosSerie = e.Parameter as Serie; //Api.GetShowById(ID_Serie);
                ID_Serie = InfosSerie.id.ToString();
                Nom_Serie = InfosSerie.name;
                TitrePage.Text = Nom_Serie;
                Image_Url = InfosSerie.image.medium;
                var resumeHtml = InfosSerie.summary;
                //TODO pour le résumé : gérer le cas resumeHtml null
                if (resumeHtml != null)
                {
                    resumeHtml = Regex.Replace(resumeHtml, @"<(.|\n)*?>", string.Empty); //Remplace les balises html 
                }
                else
                {
                    resumeHtml = "Résumé pas disponible";
                }
                Resume.Text = resumeHtml;
                ListePersonnes = Api.GetCastSerie(ID_Serie);
                ListeActeurs.ItemsSource = ListePersonnes;

                ListeSaisons = Api.GetSeasonsForShow(ID_Serie);
                AffichageListeSaisons.ItemsSource = ListeSaisons;
                if (ListeSaisons.Count > 0)
                {
                    AffichageListeSaisons.SelectedItem = AffichageListeSaisons.Items[0];
                }
                if (Favoris.CheckFavorite(ID_Serie))
                {
                    AjoutFavoris.Visibility = Visibility.Collapsed;
                    RetraitFavoris.Visibility = Visibility.Visible;
                }
                else
                {
                    AjoutFavoris.Visibility = Visibility.Visible;
                    RetraitFavoris.Visibility = Visibility.Collapsed;
                }
            }
            base.OnNavigatedTo(e);
        }

          public DetailsSerie()
        {
            this.InitializeComponent();     

        }

        private void SelectionActeur (object sender, RoutedEventArgs e)
        {
            BindPersonToCharacter ActeurSelectionne = ListeActeurs.SelectedItem as BindPersonToCharacter;
            People NomActeur = ActeurSelectionne.Person;
            this.Frame.Navigate(typeof(DetailsActeurs),NomActeur);
        }

        private void SelectionSaison (object sender, RoutedEventArgs e)
        {
            Saison SaisonSelectionne = AffichageListeSaisons.SelectedItem as Saison;
            //this.Frame.Navigate(typeof(DetailsSaison), SaisonSelectionne);
            ListeEpisode = Api.GetEpisodesForSeason(SaisonSelectionne.id.ToString());
            AffichageListeEpisodes.ItemsSource = ListeEpisode;
        }

        private void ClickBouttonRetour(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack(); ;
        }

        private void ClickBouttonAccueil(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void ClickAjoutFavoris(object sender, RoutedEventArgs e)
        {
           Favoris.AddToFavorite(ID_Serie);
            AjoutFavoris.Visibility = Visibility.Collapsed;
            RetraitFavoris.Visibility = Visibility.Visible;
        }

        private void ClickRetraitFavoris(object sender, RoutedEventArgs e)
        {
            Favoris.RemoveFromFavorite(ID_Serie);
            AjoutFavoris.Visibility = Visibility.Visible;
            RetraitFavoris.Visibility = Visibility.Collapsed;
        }
        List<Episode> EpisodeAAfficher;
        private void AffichageListeEpisodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EpisodeAAfficher.Add((Episode)AffichageListeEpisodes.SelectedItem);
            DetailsEpisode.ItemsSource = EpisodeAAfficher;
        }
    }
}
