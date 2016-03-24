using wasalney.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using wasalney.Mwasala;
using wasalney.Utl;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Popups;
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace wasalney
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Search1 : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Search1()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
       Vector end = new Vector();

        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var obj = App.Current as App;
            end = obj.pos[(int)e.NavigationParameter];
            await search();
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }
        private List<Mowasla> k;
        private  async Task search()
        {
            var obj = App.Current as App;
            k = obj.mowasla;
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
             Vector Start=new Vector();
            try
            {
                Geoposition postionlocator = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
                 Start = new Vector(postionlocator.Coordinate.Point.Position.Latitude, postionlocator.Coordinate.Point.Position.Longitude, "");
                await Map.TrySetViewAsync(postionlocator.Coordinate.Point, 18D);
            }
            catch (UnauthorizedAccessException)
            {
                MessageDialog error = new MessageDialog("Location is disabled in phone setting");
                await error.ShowAsync();
                return;
            }
            Queue<Mowasla> open = new Queue<Mowasla>();
            Queue<Mowasla> open2 = new Queue<Mowasla>();
           
          
            double dist = 0;
            double diste = 0;
            Vector near=new Vector();
            Vector neare;
            bool reached = false;
            foreach (Mowasla M in k)
            {
                foreach (Vector V in M.Pointsposition)
                {
                    if (dist == 0)
                        dist = V.distancevector(Start);
                    if (V.distancevector(Start) <= dist || dist < 350)
                    {

                        if (open.Count != 0 && (int)dist != (int)V.distancevector(Start) && dist > 350)
                            open.Dequeue();
                        open.Enqueue(M);
                        dist = V.distancevector(Start);
                        near = V;

                    }

                    if (diste == 0)
                        diste = V.distancevector(end);

                    if (V.distancevector(end) <= diste || diste < 350)
                    {

                        if (open2.Count != 0 && (int)diste != (int)V.distancevector(end) && diste > 350)
                            open2.Dequeue();
                        open2.Enqueue(M);
                        diste = V.distancevector(end);
                        neare = V;


                    }


                }
            }
            dist = 0;
            int i = 0;
            Vector near2 = new Vector();
            foreach (Mowasla have in open2)
                if (open.Contains(have))
                {
                    foreach (Vector v in have.Pointsposition)
                    {
                        if (near2.distancevector(Start) > v.distancevector(Start) || i==0)
                        {
                           
                            near2 = v;
                           
                        }
                       
                        if (i != 0)
                            Map.Routes.Add(await have.getLine(i));
                        i++;
                       // dist = v.distancevector(Start);
                    }
                    i = 0;
                    //Geopoint startPoint = new Geopoint(new BasicGeoposition() { Latitude  = Start.getLatitude(), Longitude = Start.getLongtitude() });

                    //Geopoint endPoint = new Geopoint(new BasicGeoposition() { Latitude = near.getLatitude(), Longitude = near.getLongtitude() });
                    //MapRouteFinderResult Route1 = await MapRouteFinder.GetWalkingRouteAsync(startPoint, endPoint);
                    //MapRouteView viewOfRoute1 = new MapRouteView(Route1.Route);
                    //viewOfRoute1.RouteColor = Colors.Gray; ;
                    //  Map.Routes.Add(viewOfRoute1);
                    //  for (int j = 1; j < have.Pointsposition.Count-1; j++)
                    //  Map.Routes.Add( await have.getLine(j));
                    MapRouteFinderResult Route = await MapRouteFinder.GetWalkingRouteAsync(new Geopoint(new BasicGeoposition() { Latitude = Start.getLatitude(), Longitude = Start.getLongtitude() }),

           new Geopoint(new BasicGeoposition() { Latitude = near2.getLatitude(), Longitude = near2.getLongtitude() }));
                    MapRouteView viewOfRoute = new MapRouteView(Route.Route);
                    viewOfRoute.RouteColor = Colors.Black;
                    Map.Routes.Add(viewOfRoute);
                }
           



        }
        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search));
        }
    }
}
