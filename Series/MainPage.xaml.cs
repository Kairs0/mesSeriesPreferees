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

        private void Button_Favorites(object sender, RoutedEventArgs e)
        {
            // Displays list of series from user's favorite
           
        }
        

        private void ImageGridView_ItemClick(object sender, RoutedEventArgs e)
        {
            //Montre la page DetailSeries lors d'un click sur une image de série
        }

        private void BarreRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

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
                //ImageGridView.ItemsSource:
                List<Image> images = new List<Image>();
                string texteSaisi = BarreRechercheAuto.Text;
                List<Models.Serie> seriesList = Api.ShowSearch(texteSaisi);
                foreach (Models.Serie serie in seriesList)
                {
                    var image = new Image();

                    if(serie.image.medium != null) {
                        var fullFilePath = serie.image.medium; // @ url
                        //string fullFilePath = @"http://static.tvmaze.com/uploads/images/medium_portrait/39/99906.jpg";
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                        image.Source = bitmap;
                        //wrapPanel1.Children.Add(image);
                        images.Add(image);
                    }
                }
                
                  
                ImageGridView.ItemsSource = images;
                
                
                /*
                var image = new Image();
                var fullFilePath = @"http://www.americanlayout.com/wp/wp-content/uploads/2012/08/C-To-Go-300x300.png";

                BitmapImage bitmap = new BitmapImage();
                
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                

                image.Source = bitmap;
                //wrapPanel1.Children.Add(image);
                images.Add( image);
                ImageGridView.ItemsSource= images;
                */

            }
        }

        private void ClickBouttonNav(object sender, RoutedEventArgs e)
        {
            string NomSerie = "Friends";
            this.Frame.Navigate(typeof(DetailsSerie), NomSerie);
        }

        private void ImageGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
