using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mowasalat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        private Mashroo3 line1;
        private List<Vector> L1 = new List<Vector>();
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            

            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            try
            {
                Geoposition postionlocator = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
                await Map.TrySetViewAsync(postionlocator.Coordinate.Point, 18D);
            }
            catch (UnauthorizedAccessException)
            {
                MessageDialog error = new MessageDialog("Location is disabled in phone setting");
                await error.ShowAsync();
            }
            //***********************************
            L1.Add(new Vector(31.1982571669281, 29.9168192688971));
            L1.Add(new Vector(31.2195221020255, 29.9424814515894));
            L1.Add(new Vector(31.2326330949089, 29.9572216672541));
            L1.Add(new Vector(31.238086066003, 29.9623003162132));
            L1.Add(new Vector(31.2395920411529, 29.964234863754));
            L1.Add(new Vector(31.2414903192683, 29.9673884172723));
            L1.Add(new Vector(31.2434731932479, 29.9701884334006));
            L1.Add(new Vector(31.2455164771495, 29.9738086562737));
            line1 = new Mashroo3("Ma7tta", "sedipashr", L1);
            Map.MapElements.Add(line1.getLine2());
            //********************************
            //  for (int i = 0; i < L1.Count - 1; i++)/// add line by routes
            //   try
            //{ Map.Routes.Add(await line1.getLine()); }
            //catch (ArgumentException)
            // {
            // MessageDialog error = new MessageDialog("Eror in Conection");
            //await error.ShowAsync();
            // break;
            //}
            MapIcon m = new MapIcon();
            m.Title = "Mmmmma";
            m.ZIndex =5;
            m.Location = new Geopoint(new BasicGeoposition { Latitude = 31.1982571669281, Longitude = 29.9168192688971 });  
          // try
           // {
              //  Map.Routes.Add(await line1.getLine());///addline by points
            //}
            //catch(ArgumentException)
            //{
              //  MessageDialog error = new MessageDialog("Eror in Conection");
               // await error.ShowAsync(); }
                Map.MapElements.Add(m);
            var obj = App.Current as App;
           
            
                distance.Text = "Distance" + obj.dist;
        }
        private async void Getlocation_Click(object sender, RoutedEventArgs e)
        {//**************************
            var myPosition = new Windows.Devices.Geolocation.BasicGeoposition();
            myPosition.Latitude = 31.1982571669281;/// add the point you where you want the map to be directed to when click get location if gps didn't work on your app
            myPosition.Longitude = 29.9168192688971;
            var myPoint = new Windows.Devices.Geolocation.Geopoint(myPosition);
            if (await Map.TrySetViewAsync(myPoint, 18D))
            { }
            //************************************
        }

        private void SetLocation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
