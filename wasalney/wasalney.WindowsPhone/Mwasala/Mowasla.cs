using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wasalney.Utl;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;

namespace wasalney.Mwasala
    {
        public class Mowasla
        {
           
            protected String Endadress;
            protected String Startadress;
             public List<Vector> Pointsposition;
            protected Color color=Colors.Yellow;
            public Mowasla(String Startadress, System.String Endadress, List<Vector> Pointsposition)
            {
                this.Startadress = Startadress;
                this.Endadress = Endadress;
                this.Pointsposition = Pointsposition;


            }
        public Mowasla()
        { }
            public async Task<MapRouteView> getLine(int i)
            {
                MapPolyline shape1 = new MapPolyline();

                
            if (i != 0)
            {
                Geopoint startPoint = new Geopoint(new BasicGeoposition() { Latitude = Pointsposition.ElementAt(i-1).getLatitude(), Longitude = Pointsposition.ElementAt(i-1).getLongtitude() });
                
                Geopoint endPoint = new Geopoint(new BasicGeoposition() { Latitude = Pointsposition.ElementAt(i).getLatitude(), Longitude = Pointsposition.ElementAt(i).getLongtitude() });
                MapRouteFinderResult Route = await MapRouteFinder.GetWalkingRouteAsync(startPoint, endPoint);
                MapRouteView viewOfRoute = new MapRouteView(Route.Route);
                viewOfRoute.RouteColor = color;
                return viewOfRoute;
            }
            return null;
                
            
           
            }
            public MapPolyline getLine2()
            {

                double dist = 0;
                MapPolyline shape = new MapPolyline();
                List<BasicGeoposition> positions = new List<BasicGeoposition>();
                foreach (var point in PointList.GetLines(Pointsposition))
                    foreach (var p in Pointsposition)
                        positions.Add(new BasicGeoposition() { Latitude = p.getLatitude(), Longitude = p.getLongtitude() });
                shape.StrokeColor = color;
                shape.StrokeDashed = false;
                shape.ZIndex = 4;
                shape.StrokeThickness = 10;
                shape.Path = new Geopath(positions);
                for (int i = 0; i < Pointsposition.Count - 1; i++)
                    dist = dist + Pointsposition.ElementAt(i).distancevector(Pointsposition.ElementAt(i + 1));
                var obj = App.Current as App;
                obj.dist = dist / 1000;
                return shape;






            }
        }
    }

