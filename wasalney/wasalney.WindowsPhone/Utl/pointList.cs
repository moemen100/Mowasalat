using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace wasalney.Utl
{
    class PointList
    {
        public PointList()
        {
            Points = new List<Geopoint>();
        }
        public List<Geopoint> Points { get; set; }
        public static IEnumerable<PointList> GetLines()
        {
            var result = new List<PointList>
            {
                new PointList
                {

                    Points = new List<Geopoint>
                    {
                        new Geopoint(new BasicGeoposition{Latitude = 31.2176, Longitude = 029.9397}),
                        new Geopoint(new BasicGeoposition{Latitude = 31.2195, Longitude = 029.9425}),
                        new Geopoint(new BasicGeoposition{Latitude = 31.2326, Longitude = 029.9572}),
                        new Geopoint(new BasicGeoposition{Latitude = 31.2385, Longitude = 029.9628})
                    }
                },
            };

            return result;
        }
        public static IEnumerable<PointList> GetLines(List<Vector> VectorPosition)
        {
            List<Geopoint> MyPoints = new List<Geopoint>();
            var result = new List<PointList>();

            try
            {

                foreach (Vector p in VectorPosition)
                {



                    MyPoints.Add(new Geopoint(new BasicGeoposition { Latitude = p.getLatitude(), Longitude = p.getLongtitude() }));
                }
                PointList pl = new PointList();
                pl.Points = MyPoints;


                result.Add(pl);
            }
            catch (Exception e)
            {
                string errMsg = e.Message;
            }




            return result;
        }


    }
}
