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


// To add offline sync support, add the NuGet package Microsoft.WindowsAzure.MobileServices.SQLiteStore
// to your project. Then, uncomment the lines marked // offline sync
// For more information, see: http://aka.ms/addofflinesync
//using Microsoft.WindowsAzure.MobileServices.SQLiteStore;  // offline sync
//using Microsoft.WindowsAzure.MobileServices.Sync;         // offline sync

namespace wasalney
{
    sealed partial class MainPage: Page
    {
        private MobileServiceCollection<Mowaslat, Mowaslat> items;
        private IMobileServiceTable<Mowaslat> todoTable = App.MobileService.GetTable<Mowaslat>();
        //private IMobileServiceSyncTable<TodoItem> todoTable = App.MobileService.GetSyncTable<TodoItem>(); // offline sync

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

            //await SyncAsync(); // offline sync
        }
        private async void Sync_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                items = await todoTable
                   .Where(todoItem => todoItem.Complete == false)
                   .ToCollectionAsync();
                // if(items.Last().Id.Equals(//count of the list on the file)-1)
                //{ MessageDialog error = new MessageDialog("You are updated to our last data base").ShowAsync();
                //            return;       }
                for (int i = L1.Count; i < items.Count; i++)
                {

                    L1.Add(new Vector(items.ElementAt(i).Latitude, items.ElementAt(i).Longtude, items.ElementAt(i).place));

                }
                line1 = new Mashroo3("Ma7tta", "sedipashr", L1);

            }
            catch (System.Net.Http.HttpRequestException)
            {
                await new MessageDialog("No Connection").ShowAsync();
            }
            catch (MobileServiceInvalidOperationException)
            {
                await new MessageDialog("Error loading items").ShowAsync();
            }



        }
        private async Task RefreshTodoItems()
        {
            
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems.
                items = await todoTable
                    .Where(todoItem => todoItem.Complete == false)
                    . ToCollectionAsync();
               // ListItems.ItemsSource = items;
                for(int i=0; i<items.Count;i++)
                {
                    L1.Add(new Vector(items.ElementAt(i).Latitude, items.ElementAt(i).Longtude,items.ElementAt(i).place));

                }
                line1 = new Mashroo3("Ma7tta", "sedipashr", L1);
                Map.MapElements.Add(line1.getLine2());
              //  this.ButtonSave.IsEnabled = true;
            }
            catch (System.Net.Http.HttpRequestException )
            {
                await new MessageDialog("No Connection").ShowAsync();
            }
            // System.Net.Http.HttpRequestException
            catch (MobileServiceInvalidOperationException e )
            {
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

            //await SyncAsync(); // offline sync
        }

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            //ButtonRefresh.IsEnabled = false;

            //await SyncAsync(); // offline sync
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
            // Geolocator geolocator = new Geolocator();
            //  geolocator.DesiredAccuracyInMeters = 50;
            //   try
            //   {
            //      Geoposition postionlocator = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
            //   await Map.TrySetViewAsync(postionlocator.Coordinate.Point, 18D);
            //  }
            // catch (UnauthorizedAccessException)
            // {
            // MessageDialog error = new MessageDialog("Location is disabled in phone setting");
            //  await error.ShowAsync();
            // }
            //***********************************
            int i=0;
            // L1.Add(new Vector(31.1982571669281, 29.9168192688971, "ss"));
            // L1.Add(new Vector(31.2195221020255, 29.9424814515894, "ss"));
            // L1.Add(new Vector(31.2326330949089, 29.9572216672541, "ss"));
            // L1.Add(new Vector(31.238086066003, 29.9623003162132, "ss"));
            // L1.Add(new Vector(31.2395920411529, 29.964234863754, "ss"));
            // L1.Add(new Vector(31.2414903192683, 29.9673884172723, "ss"));
            // L1.Add(new Vector(31.2434731932479, 29.9701884334006, "ss"));
            // L1.Add(new Vector(31.2455164771495, 29.9738086562737, "ss"));
            //   foreach (Vector v in L1)
            // {

            //   if (L1.IndexOf(v) == 0)
            //  {
            //    var todoItem = new Mowaslat { place = v.name, Latitude = v.getLatitude(), Longtude = v.getLongtitude(), Complete = false, Id = i.ToString(), iden = 0 };
            //  await InsertTodoItem(todoItem);
            //}
            // if (L1.IndexOf(v) == L1.Count-1)
            // {
            //   var todoItem = new Mowaslat { place = v.name, Latitude = v.getLatitude(), Longtude = v.getLongtitude(), Complete = false, Id = i.ToString(), iden = 2 };
            //  await InsertTodoItem(todoItem);
            //}
            //if (L1.IndexOf(v) != 0&& L1.IndexOf(v) != L1.Count - 1)

            //                {
            //                  var todoItem = new Mowaslat { place = v.name, Latitude = v.getLatitude(), Longtude = v.getLongtitude(), Complete = false, Id =i.ToString() , iden = 1 };
            //                await InsertTodoItem(todoItem);
            //          }
            //        i++;
            //
            //  }
            //await new MessageDialog("finished adding").ShowAsync();


           
            
            var obj = App.Current as App;
         //   distance.Text = "Distance" + obj.dist;
            //await InitLocalStoreAsync(); // offline sync
            await RefreshTodoItems();
        }

        #region Offline sync

        //private async Task InitLocalStoreAsync()
        //{
        //    if (!App.MobileService.SyncContext.IsInitialized)
        //    {
         //      var store = new MobileServiceSQLiteStore("localstore.db");
        //        store.DefineTable<TodoItem>();
        //        await App.MobileService.SyncContext.InitializeAsync(store);
        //    }
        //
        //    await SyncAsync();
        //}

        //private async Task SyncAsync()
        //{
        //    await App.MobileService.SyncContext.PushAsync();
        //    await todoTable.PullAsync("todoItems", todoTable.CreateQuery());
        //}

        #endregion 
    }
}
