using Windows.UI.Xaml.Controls;

namespace wasalney
{
    public sealed partial class MainPage : Page
    {
        private void Search_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof (Search));
        }

       

       
    }
}
