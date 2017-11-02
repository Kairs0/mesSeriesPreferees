using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly List<string> ImageSource = new List<string>(); /*{ get; set; }*/
        private List<string> listeSeries;

        public MainPage()
        {
            this.InitializeComponent();

        }

        private void Button_Favorites(object sender, RoutedEventArgs e)
        {
            // Displays list of series from user's favorite
           
        }
        


        private void BarreRechercheAuto_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string texteSaisi = BarreRechercheAuto.Text;
            List<Models.Serie> seriesList = Api.ShowSearch(texteSaisi);

            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //var listeSuggestions = listeSeries.Where(i => i.StartsWith(texteSaisi)).ToList(); // /!\ listeSeries à créer avec l'API index
                String[] listeSuggestions = new String[seriesList.Count];
                for (int i = 0; i < seriesList.Count; i++)
                {
                    listeSuggestions[i] = seriesList[i].name;
                }
                BarreRechercheAuto.ItemsSource = listeSuggestions;
            }
        }


        private void BarreRechercheAuto_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            BarreRechercheAuto.Text = args.SelectedItem as string;

        }

        List<Models.Serie> seriesList;
        private void BarreRechercheAuto_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // Serie choisie depuis la liste, utiliser la recherche ciblée par ID (ou nom ?).
                string NomSerie = args.QueryText;
                this.Frame.Navigate(typeof(DetailsSerie), NomSerie);
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

        private void ClickBouttonNav(object sender, RoutedEventArgs e)
        {
            string NomSerie = "Friends";
            this.Frame.Navigate(typeof(DetailsSerie), NomSerie);
            //TESTS ARNAUD
            //var test = Api.GetShowById(1.ToString());
            //var test2 = "";
            //END TEST ARNAUD
        }

        private void ImageGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Serie SerieSelectionne = ImageGridView.SelectedItem as Serie;
            string NomSerie = SerieSelectionne.name;
            this.Frame.Navigate(typeof(DetailsSerie), NomSerie);
        }
    }
}
