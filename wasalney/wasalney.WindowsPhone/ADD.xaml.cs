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
using Windows.Devices.Geolocation;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Popups;
using wasalney.Mwasala;
using wasalney.Utl;
using Windows.Services.Maps;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace wasalney
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ADD : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public ADD()
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
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            try
            {
                Geoposition postionlocator = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
                await map.TrySetViewAsync(postionlocator.Coordinate.Point, 18D);


            }
            catch (UnauthorizedAccessException)
            {
                MessageDialog error = new MessageDialog("Location is disabled in phone setting we will Move the map to a Location in alex");
                await error.ShowAsync();
                var myPosition = new Windows.Devices.Geolocation.BasicGeoposition();
                myPosition.Latitude = 31.1982571669281;/// add the point you where you want the map to be directed to when click get location if gps didn't work on your app
                myPosition.Longitude = 29.9168192688971;
                 var myPoint = new Windows.Devices.Geolocation.Geopoint(myPosition);
              
            }
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

        private  void DeleteLast_Click(object sender, RoutedEventArgs e)
        {
            if(map.MapElements.Count!=0)
            map.MapElements.Remove(map.MapElements.Last());
            if (map.Routes .Count!=0)
                map.Routes.RemoveAt(map.Routes.IndexOf(map.Routes.Last()));
            if (L1.Count != 0)
                L1.RemoveAt(L1.IndexOf(L1.Last()));
                if (i>0)
            i--;

        }
        
        private IMobileServiceTable<Mowaslat> todoTable = App.MobileService.GetTable<Mowaslat>();
        private async void Finsh_Click(object sender, RoutedEventArgs e)
        {
           

            foreach (Vector v in L1)
            {
                
                if (L1.IndexOf(v) == 0)
                {
                    var todoItem = new Mowaslat { place = v.name, Latitude = v.getLatitude(), Longtude = v.getLongtitude(), Complete = true, iden = 0 };
                    try { await InsertTodoItem(todoItem); }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        await new MessageDialog("No Connection").ShowAsync();
                        return;
                    }
                    // System.Net.Http.HttpRequestException
                    catch (MobileServiceInvalidOperationException )
                    {
                        await new MessageDialog("Error loading items").ShowAsync();
                        return;
                    }
                }
                if (L1.IndexOf(v) == L1.Count - 1)
                {
                    var todoItem = new Mowaslat { place = v.name, Latitude = v.getLatitude(), Longtude = v.getLongtitude(), Complete = true, iden = 2 };
                    try { await InsertTodoItem(todoItem); }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        await new MessageDialog("No Connection").ShowAsync();
                        return;
                    }
                    // System.Net.Http.HttpRequestException
                    catch (MobileServiceInvalidOperationException )
                    {
                        await new MessageDialog("Error loading items").ShowAsync();
                        return;
                    }
                }
                if (L1.IndexOf(v) != 0 && L1.IndexOf(v) != L1.Count - 1)

                {
                    var todoItem = new Mowaslat { place = v.name, Latitude = v.getLatitude(), Longtude = v.getLongtitude(), Complete = true, iden = 1 };
                    try { await InsertTodoItem(todoItem); }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        await new MessageDialog("No Connection").ShowAsync();
                        return;
                    }
                    // System.Net.Http.HttpRequestException
                    catch (MobileServiceInvalidOperationException )
                    {
                        await new MessageDialog("Error loading items").ShowAsync();
                        return;
                    }
                }
               

            }
            await new MessageDialog("The ROute is added to Data Base").ShowAsync();
            Frame.Navigate(typeof(StartMenu));
        }



        private async Task InsertTodoItem(Mowaslat todoItem)
{
    // This code inserts a new TodoItem into the database. When the operation completes
    // and Mobile App backend has assigned an Id, the item is added to the CollectionView.
    await todoTable.InsertAsync(todoItem);
     

    //await SyncAsync(); // offline sync
}
private void Delete_Route_Click(object sender, RoutedEventArgs e)
        {
            map.MapElements.Clear();
            map.Routes.Clear();
            L1.Clear();
        i = 0;
        }

        private int i = 0;
        private Mowasla M;
        private List<Vector> L1 = new List<Vector>();
        MapIcon Note;
        private async void map_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            Geopoint Point = new Geopoint(new BasicGeoposition { Latitude = args.Location.Position.Latitude, Longitude = args.Location.Position.Longitude });
            MapLocationFinderResult Place = await MapLocationFinder.FindLocationsAtAsync(Point );
            if(Place.Locations.Count==0)
            {
                MessageDialog error = new MessageDialog("Can't add this point PLease try again");
                await error.ShowAsync();
                return;
            }
            L1.Add(new Vector(Point.Position.Latitude, Point.Position.Longitude, Place.Locations.ElementAt(0).Address.Street));
            if (i > 0)
            {
                M = new Mowasla(L1.ElementAt(0).name, L1.Last().name, L1);
                if( M.getLine(i)!=null)
                map.Routes.Add(await M.getLine(i));
            }

            Note = new MapIcon();
            Note.Title = Place.Locations.ElementAt(0).Address.Street;
            
            Note.Location = new Geopoint(new BasicGeoposition { Latitude =Point.Position.Latitude,Longitude=Point.Position.Longitude });
            map.MapElements.Add(Note);
            
            i++;
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StartMenu));
        }
    }
}
