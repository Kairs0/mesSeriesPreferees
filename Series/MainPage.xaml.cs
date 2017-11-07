using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Animation;
using Series.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// GridView - https://www.tutorialspoint.com/xaml/xaml_gridview.htm
// https://stackoverflow.com/questions/17056989/windows-8-xaml-displaying-a-list-of-images-in-a-gridview-through-data-binding

namespace Series
{
    public sealed partial class MainPage : Page
    {
        private readonly List<string> ImageSource = new List<string>();
        private List<Serie> listeFavoris ;
        private List<Episode> listEpisodeTonight;
        private List<Serie> listSerieTonight = new List<Serie>();


        public MainPage()
        {
            
            this.InitializeComponent();
        }


        private void PageChargee(object sender, RoutedEventArgs args)
        {

            // affiche les séries ce soir à la TV US
            listEpisodeTonight = Api.GetEpisodesToNight("US");
            foreach(Episode episode in listEpisodeTonight)
            {
                if (episode.show != null && !(listSerieTonight.Select(x => x.id).Contains(episode.show.id)))
                {
                        listSerieTonight.Add(episode.show);
                }
            }
            ImageGridView.ItemsSource = listSerieTonight;


            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                this.Frame.Navigate(typeof(NoConnection));
            }

        }
    
        private void Button_Favorites(object sender, RoutedEventArgs e)
        {
            // Displays list of series from user's favorite
            listeFavoris = Series.Favoris.GetFavorites();
            ImageGridView.ItemsSource = listeFavoris;
        }

        private void Button_ShowTonight(object sender, RoutedEventArgs E)
        {
            ImageGridView.ItemsSource = listSerieTonight;
        }
        


        List<Models.Serie> seriesListAutoSuggest;
        private void BarreRechercheAuto_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string texteSaisi = BarreRechercheAuto.Text;
            seriesListAutoSuggest = Api.ShowSearch(texteSaisi);
            
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                BarreRechercheAuto.ItemsSource = seriesListAutoSuggest;
            }
        }


        private void BarreRechercheAuto_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            BarreRechercheAuto.Text = ((Models.Serie)args.SelectedItem).name;

        }

        List<Models.Serie> seriesList;
        private void BarreRechercheAuto_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // Serie choisie depuis la liste, utiliser la recherche ciblée par ID.
                
                Serie SerieChoisie = (Serie)args.ChosenSuggestion;
                this.Frame.Navigate(typeof(DetailsSerie), SerieChoisie);
            }
            else
            {
                // Série non reconnue, utiliser la recherche large par texte.
                //on affiche les vignettes
                string texteSaisi = BarreRechercheAuto.Text;
                seriesList = Api.ShowSearch(texteSaisi);
                ImageGridView.ItemsSource = seriesList;
            }
        }


        private void ImageGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Serie SerieSelectionne = ImageGridView.SelectedItem as Serie;
            this.Frame.Navigate(typeof(DetailsSerie), SerieSelectionne);
        }
    }
}
