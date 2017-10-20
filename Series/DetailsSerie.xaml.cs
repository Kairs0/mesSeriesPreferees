﻿using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
// https://docs.microsoft.com/en-us/windows/uwp/layout/navigate-between-two-pages

namespace Series
{
    /// <Summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </Summary>
    public sealed partial class DetailsSerie : Page
    {

        public string NomSerie;
       public Models.Image ImageSerie { get; set; }
        public string ImageUrl;
        public string Nom_Serie;
        public string ID_Serie;
        private List<BindPersonToCharacter> ListePersonnes;


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NomSerie = (string)e.Parameter;
            Serie InfosSerie = Api.GetShowByName(NomSerie);
            ID_Serie = InfosSerie.Id.ToString();
            Nom_Serie = InfosSerie.Name;
            TitrePage.Text = Nom_Serie;

            Resume.Text = InfosSerie.Summary;
            ListePersonnes = Api.GetCastSerie(ID_Serie);
            ListeActeurs.ItemsSource = ListePersonnes;

            base.OnNavigatedTo(e);
        }

          public DetailsSerie()
        {
            this.InitializeComponent();     


        }

        private void ClickBouttonRetour(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void ClickAjoutFavoris(object sender, RoutedEventArgs e)
        {

        }
    }
}
