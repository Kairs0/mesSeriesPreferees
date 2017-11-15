using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
        public Models.Image ImageSerie;
        public string Image_Url;

        private string Nom_Serie;
        private string ID_Serie;
        private List<BindPersonToCharacter> ListePersonnes;
        private List<Saison> ListeSaisons;
        private List<Episode> ListeEpisode;
        private Episode DetailsEpisode;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                this.Frame.Navigate(typeof(NoConnection));
            }

            // Construction de la page avec la Série reçue en paramètre de la navigation
            if (e.Parameter != null)
            {
                Serie InfosSerie = e.Parameter as Serie; 
                ID_Serie = InfosSerie.Id.ToString();
                Nom_Serie = InfosSerie.Name;
                TitrePage.Text = Nom_Serie;
                Image_Url = InfosSerie.Image.Medium;
                var resumeHtml = InfosSerie.Summary;
                Resume.Text = Regex.Replace(resumeHtml, @"<(.|\n)*?>", string.Empty);
                ListePersonnes = Api.GetCastSerie(ID_Serie);
                ListeActeurs.ItemsSource = ListePersonnes;

                ListeSaisons = Api.GetSeasonsForShow(ID_Serie);
                AffichageListeSaisons.ItemsSource = ListeSaisons;
                if (ListeSaisons.Count > 0)
                {
                    AffichageListeSaisons.SelectedItem = AffichageListeSaisons.Items[0];
                    if (ListeEpisode.Count > 0)
                    {
                    AffichageListeEpisodes.SelectedItem = AffichageListeEpisodes.Items[0];
                    }
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

        // Selection d'un acteur depuis la liste et navigation vers sa page
        private void SelectionActeur (object sender, RoutedEventArgs e)
        {
            BindPersonToCharacter ActeurSelectionne = ListeActeurs.SelectedItem as BindPersonToCharacter;
            People NomActeur = ActeurSelectionne.Person;
            this.Frame.Navigate(typeof(DetailsActeurs),NomActeur);
        }

        // Affichage de la liste des épisodes de la saison sélectionnée
        private void SelectionSaison(object sender, RoutedEventArgs e)
        {
            Saison SaisonSelectionne = AffichageListeSaisons.SelectedItem as Saison;
            ListeEpisode = Api.GetEpisodesForSeason(SaisonSelectionne.Id.ToString());
            AffichageListeEpisodes.ItemsSource = ListeEpisode;
            if (ListeEpisode.Count > 0)
            {
                AffichageListeEpisodes.SelectedItem = AffichageListeEpisodes.Items[0];
            }
            else
            {
                DetailsEpisode_Titre.Text = "";
                DetailsEpisode_Dates.Text = "";
                DetailsEpisode_Resume.Text = "";
            }
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

        //Affichage des détails de l'épisode sélectionné dans la liste
        private void AffichageListeEpisodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

                if (AffichageListeEpisodes.SelectedItem != null)
                {
                    DetailsEpisode = AffichageListeEpisodes.SelectedItem as Episode;
                }

                DetailsEpisode_Titre.Text = DetailsEpisode.Name;
                DetailsEpisode_Dates.Text = "Saison " + ((Saison)AffichageListeSaisons.SelectedItem).Number +
                    ", Episode " + DetailsEpisode.Number + "\n" +
                    "Date de diffusion : " + DetailsEpisode.Airdate;
                var resumeEpisodeHtml = DetailsEpisode.Summary;
                DetailsEpisode_Resume.Text = Regex.Replace(resumeEpisodeHtml, @"<(.|\n)*?>", string.Empty);
       
            }
        }
    }
