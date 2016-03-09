using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;

namespace Mowasalat
{
   public class Mashroo3
    {
        private int i = 0;
        private String Endadress;
        private String Startadress;
        private List<Vector> Pointsposition;
        private Color color = Colors.Red;
        public Mashroo3(String Startadress,String Endadress,List<Vector> Pointsposition)
        {
            this.Startadress = Startadress;
            this.Endadress = Endadress;  
            this.Pointsposition = Pointsposition;
            

        }
        public async Task<MapRouteView> getLine()
        {
            MapPolyline shape1 = new MapPolyline();
            
            List<BasicGeoposition> positions = new List<BasicGeoposition>();
            foreach (var p in Pointsposition)
            positions.Add(new BasicGeoposition() { Latitude = p.getLatitude(), Longitude = p.getLongtitude() });
            Geopoint startPoint = new Geopoint(positions.ElementAt(i));
            i++;
            Geopoint endPoint = new Geopoint(positions.ElementAt(i));
            MapRouteFinderResult Route = await MapRouteFinder.GetDrivingRouteAsync(startPoint, endPoint, MapRouteOptimization.Time, MapRouteRestrictions.None, 290);
            MapRouteView viewOfRoute = new MapRouteView(Route.Route);
            viewOfRoute.RouteColor = Colors.Red;
            
            

            return viewOfRoute;
        }
       

    }
}
