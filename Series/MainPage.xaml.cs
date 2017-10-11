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

namespace Series
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string ImageSource { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ImageSource = @"http://static.tvmaze.com/uploads/images/medium_portrait/39/99906.jpg";
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            // do nothing yet
        }

        private void Button_Favorites(object sender, RoutedEventArgs e)
        {
            // do nothing yet either
        }

        private void ImageGridView_ItemClick(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
