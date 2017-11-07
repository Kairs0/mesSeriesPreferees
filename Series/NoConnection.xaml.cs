using System.Net.NetworkInformation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Series
{
    public sealed partial class NoConnection : Page
    {
        public NoConnection()
        {
            this.InitializeComponent();
        }

        private void ReTry_OnClick(object sender, RoutedEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
