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
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Maps;
using wasalney.Mwasala;
using wasalney.Utl;

using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Windows.Services.Maps;
// To add offline sync support, add the NuGet package Microsoft.WindowsAzure.MobileServices.SQLiteStore
// to your project. Then, uncomment the lines marked // offline sync
// For more information, see: http://aka.ms/addofflinesync
// offline sync

namespace wasalney
{
    sealed partial class MainPage: Page
    {
        private MobileServiceCollection<Mowaslat, Mowaslat> items;
        //private IMobileServiceTable<Mowaslat> todoTable = App.MobileService.GetTable<Mowaslat>();
        private IMobileServiceSyncTable<Mowaslat> todoTable = App.MobileService.GetSyncTable<Mowaslat>(); // offline sync

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async Task InsertTodoItem(Mowaslat todoItem)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile App backend has assigned an Id, the item is added to the CollectionView.
            await todoTable.InsertAsync(todoItem);
           // items.Add(todoItem);

          //  await SyncAsync(); // offline sync
        }
        private async void Sync_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // try
            //{

            // if(items.Last().Id.Equals(//count of the list on the file)-1)
            //{ MessageDialog error = new MessageDialog("You are updated to our last data base").ShowAsync();
            //            return;       }
            //  for (int i = L1.Count; i < items.Count; i++)
            //{

            //  L1.Add(new Vector(items.ElementAt(i).Latitude, items.ElementAt(i).Longtude, items.ElementAt(i).place));

            //                }
            //              line1 = new Mashroo3("Ma7tta", "sedipashr", L1);

            //        }
            //      catch (System.Net.Http.HttpRequestException)
            //    {
            //      await new MessageDialog("No Connection").ShowAsync();
            //}
            //catch (MobileServiceInvalidOperationException)
            //{
            //  await new MessageDialog("Error loading items").ShowAsync();
            //}


            // Prevent extra clicking while Push is in progress
            this.IsEnabled = false;
            pr.IsActive = true;
            try { await App.MobileService.SyncContext.PushAsync(); }
            catch (System.Net.Http.HttpRequestException)
            {
                this.IsEnabled = true;
                pr.IsActive = false;
                await new MessageDialog("No Connection").ShowAsync();
            }
            // System.Net.Http.HttpRequestException
            catch (MobileServiceInvalidOperationException )
            {
                this.IsEnabled = true;
                pr.IsActive = false;
                await new MessageDialog("Error loading items").ShowAsync();
            }
            try
            { await todoTable.PullAsync("Mowaslat", todoTable.CreateQuery());
                await new MessageDialog("The ROute is added to Data Base").ShowAsync();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                this.IsEnabled = true;
                pr.IsActive = false;
                await new MessageDialog("No Connection").ShowAsync();
            }
            // System.Net.Http.HttpRequestException
            catch (MobileServiceInvalidOperationException )
            {
                this.IsEnabled = true;
                pr.IsActive = false;
                await new MessageDialog("Error loading items").ShowAsync();
            }
           await RefreshTodoItems();
            this.IsEnabled = true;
            pr.IsActive = false;
          
            
        


    }
        private async Task RefreshTodoItems()
        {
            var obj = App.Current as App;
            Mowasla m = new Mowasla();
            int temp = 0;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems.

                items = await todoTable
                    .Where(Mowaslat => Mowaslat.Complete == true)
                    .ToCollectionAsync();
                              
                for (int i = 0; i <items.Count;i++)
                {if (items.ElementAt(i).iden == 0)
                    {
                        if (items.ElementAt(i).Type != null)
                        {
                            if (items.ElementAt(i).Type.Equals("Bus"))
                                m = new Bus();
                            if (items.ElementAt(i).Type.Equals("mashroo3"))
                                m = new Mashroo3();
                            else
                                m = new Mowasla();
                        }
                        else
                            m = new Mowasla();
                        m.Pointsposition = new List<Vector>();
                        obj.pos.Add((new Vector(items.ElementAt(i).Latitude, items.ElementAt(i).Longtude, items.ElementAt(i).place)));
                        obj.place.Add(items.ElementAt(i).place);
                        m.Pointsposition.Add(new Vector(items.ElementAt(i).Latitude, items.ElementAt(i).Longtude, items.ElementAt(i).place));
                        m.Startadress = items.ElementAt(i).place;
                    }
                    if (items.ElementAt(i).iden == 1)
                    {
                        obj.pos.Add((new Vector(items.ElementAt(i).Latitude, items.ElementAt(i).Longtude, items.ElementAt(i).place)));
                        obj.place.Add(items.ElementAt(i).place);
                        m.Pointsposition.Add(new Vector(items.ElementAt(i).Latitude, items.ElementAt(i).Longtude, items.ElementAt(i).place));
                        Map.Routes.Add(await m.getLine(i- temp));
                    }
                    if (items.ElementAt(i).iden == 2)
                    {
                        obj.pos.Add((new Vector(items.ElementAt(i).Latitude, items.ElementAt(i).Longtude, items.ElementAt(i).place)));
                        obj.place.Add(items.ElementAt(i).place);
                        m.Pointsposition.Add(new Vector(items.ElementAt(i).Latitude, items.ElementAt(i).Longtude, items.ElementAt(i).place));
                        Map.Routes.Add(await m.getLine(i- temp));
                        m.Endadress = items.ElementAt(i).place;
                        obj.mowasla.Add(m);
                        temp = i+1;
                    }

                }
                //  this.ButtonSave.IsEnabled = true;
            }
            catch (System.Net.Http.HttpRequestException )
            {
                this.IsEnabled = true;
                pr.IsActive = false;
                await new MessageDialog("No Connection").ShowAsync();
            }
            // System.Net.Http.HttpRequestException
            catch (MobileServiceInvalidOperationException e )
            {
                this.IsEnabled = true;
                pr.IsActive = false;
                await new MessageDialog( "Error loading items").ShowAsync();
            }
           

           
        }

        private async Task UpdateCheckedTodoItem(Mowaslat item)
        {
            // This code takes a freshly completed TodoItem and updates the database. When the service 
            // responds, the item is removed from the list.
            await todoTable.UpdateAsync(item);
            items.Remove(item);
           // ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);

         //  await SyncAsync(); // offline sync
        }

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            //ButtonRefresh.IsEnabled = false;

          // await SyncAsync(); // offline sync
          //  await RefreshTodoItems();

            //ButtonRefresh.IsEnabled = true;
            //var myPosition = new Windows.Devices.Geolocation.BasicGeoposition();
            //myPosition.Latitude = 31.1982571669281;/// add the point you where you want the map to be directed to when click get location if gps didn't work on your app
            //myPosition.Longitude = 29.9168192688971;
           // var myPoint = new Windows.Devices.Geolocation.Geopoint(myPosition);
         //   if (await Map.TrySetViewAsync(myPoint, 18D))
           // { }
        }

        private  void Add_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(ADD));
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Mowaslat item = cb.DataContext as Mowaslat;
            await UpdateCheckedTodoItem(item);
        }
        private Mashroo3 line1;
        private List<Vector> L1 = new List<Vector>();
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.IsEnabled = false;
            pr.IsActive = true;
            if (!App.MobileService.SyncContext.IsInitialized)
            {
              var store = new MobileServiceSQLiteStore("localsync.db");
            store.DefineTable<Mowaslat>();
           await App.MobileService.SyncContext.InitializeAsync(store, new SyncHandler(App.MobileService));
            }
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            try
            {
                 //Geoposition postionlocator = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
                // await Map.TrySetViewAsync(postionlocator.Coordinate.Point, 18D);
                BasicGeoposition myPosition = new BasicGeoposition();
                myPosition.Latitude = 31.1982571669281;/// add the point you where you want the map to be directed to when click get location if gps didn't work on your app
                myPosition.Longitude = 29.9168192688971;
                await Map.TrySetViewAsync(new Geopoint(myPosition),18D);
            }
            catch (UnauthorizedAccessException)
            {
                this.IsEnabled = true;
                pr.IsActive = false;
                MessageDialog error = new MessageDialog("Location is disabled in phone setting");
                await error.ShowAsync();
            }
            await RefreshTodoItems();
            //   distance.Text = "Distance" + obj.dist;
            //  await InitLocalStoreAsync(); // offline sync
           
            this.IsEnabled = true;
            pr.IsActive = false; 
        }

        #region Offline sync

        // private async Task InitLocalStoreAsync()
        //{
        //  await new MessageDialog("No Connection").ShowAsync();

        //if (!App.MobileService.SyncContext.IsInitialized)
        //{
        //  var store = new MobileServiceSQLiteStore("localstore.db");
        //store.DefineTable<Mowaslat>();
        // await App.MobileService.SyncContext.InitializeAsync(store);
        //}

        //await SyncAsync();
        //        }

        private void Optin_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {if (Ca.Visibility == Visibility.Visible)
            Ca.Visibility = Visibility.Collapsed;
            if (Ca.Visibility == Visibility.Collapsed)
                Ca.Visibility = Visibility.Visible;

        }
        private void button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {if (Normal.IsChecked == true)
                Map.Style = MapStyle.Terrain;
            if (Online.IsChecked == true)
            {
                var httpsource = new HttpMapTileDataSource("http://a.tile.openstreetmap.org/{zoomlevel}/{x}/{y}.png");
                var ts = new MapTileSource(httpsource)
                {
                    Layer = MapTileLayer.BackgroundReplacement
                };
                Map.Style = MapStyle.None;
                Map.TileSources.Add(ts);
               
            }
            
                if (Aeriel.IsChecked == true)
                {
                    Map.Style = MapStyle.Aerial;
                }
            Ca.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}
