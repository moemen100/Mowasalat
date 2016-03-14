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
            protected int i = 0;
            protected String Endadress;
            protected String Startadress;
            protected List<Vector> Pointsposition;
            protected Color color;
            public Mowasla(String Startadress, System.String Endadress, List<Vector> Pointsposition)
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
                viewOfRoute.RouteColor = color;



                return viewOfRoute;
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

