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
            // use 
            ImageSource.Add(@"http://static.tvmaze.com/uploads/images/medium_portrait/39/99906.jpg");
            ImageSource.Add(@"http://static.tvmaze.com/uploads/images/medium_portrait/72/181728.jpg");
         //   BarreRecherche.Visibility = Visibility.Collapsed;
         //   TriggerSearch.Visibility = Visibility.Collapsed;
        }

        /*
        private void Button_Search(object sender, RoutedEventArgs e)
        {
            //Displays search box + button
            BarreRecherche.Visibility = BarreRecherche.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
            TriggerSearch.Visibility = TriggerSearch.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;

            //Hide last episodes text
            LastEpisodes.Visibility = Visibility.Collapsed; 

            //Hide previous results of search Or hide list from welcome view
            //TODO
            ImageGridView.Visibility = Visibility.Collapsed;
        }
        */
        private void Button_Favorites(object sender, RoutedEventArgs e)
        {
            // Displays list of series from user's favorite
        }
        

        private void ImageGridView_ItemClick(object sender, RoutedEventArgs e)
        {
            // Will display element on a serie when user clicks on an item
        }


      /*  private void TriggerSearch_Click(object sender, RoutedEventArgs e)
        {
            // idée : cherche à l'aide de la recheche exact (ShowByName), si ne retourne pas de résultat chercher avec ShowSearch
            string textToSearch = BarreRecherche.Text;
            var result = Api.ShowSearch(textToSearch);
            //var result = Api.GetShowByName(textToSearch);
            if (result.Count != 0)
            {
                ImageSource.Clear();
                foreach (var serie in result)
                {
                    //! L'objet image peut être nul
                    if (serie.image != null)
                    {
                        string urlImage = serie.image.medium;
                        ImageSource.Add(@urlImage);
                    }
                    else
                    {
                        //TODO add an empty image

                    }
                    
                }
                //TODO trouver le moyen de reload l'image gridview depuis son initialisation
                //TEST
                ImageGridView.Items.Clear();
                ImageGridView.Items.Add(@ImageSource[0]);
                //END TEST

                //ImageGridView.ItemsSource = null;
                //ImageGridView.ItemsSource = ImageSource;
                ImageGridView.Visibility = Visibility.Visible;
            }
        }

        private void BarreRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        */
        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        

        private void BarreRechercheAuto_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string texteSaisi = BarreRechercheAuto.Text;
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //TODO arnaud
                var listeSuggestions = listeSeries.Where(i => i.StartsWith(texteSaisi)).ToList(); // /!\ listeSeries à créer avec l'API index
                BarreRechercheAuto.ItemsSource = listeSuggestions;
            }
        }


        private void BarreRechercheAuto_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            BarreRechercheAuto.Text = args.SelectedItem as string;

        }


        private void BarreRechercheAuto_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // Serie choisie depuis la liste, utiliser la recherche ciblée par ID (ou nom ?).
            }
            else
            {
                // Série non reconnue, utiliser la recherche large par texte.
            }
        }

        private void ClickBouttonNav(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DetailsSerie));
        }
    }
}
