using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wasalney.Utl
{
    public class Vector
    {
        private Double Latitude, Longtitude;
        public String name { get; set; }
        public Vector()
        {
            set(0, 0, name);
        }
        public Vector(Double x, Double y, String Place)
        {
            set(x, y, Place);
        }
        public void set(Double x, Double y, String Place)
        {
            // TODO Auto-generated method stub
            Latitude = x;
            Longtitude = y;
            name = Place;
        }
        public Vector(Vector vector)
        {
            set(vector.Latitude, vector.Longtitude, vector.name);
        }
        public Vector add(Vector vector)
        {
            Latitude += vector.Latitude;
            Longtitude += vector.Longtitude;
            return this;

        }
        public Vector subtract(Vector vector)
        {
            Latitude -= vector.Latitude;
            Longtitude -= vector.Longtitude;
            return this;

        }
        public float distancevector(Vector vector)
        {

            double earthRadius = 6371000; //meters
            double dLat = (Math.PI / 180) * (vector.Latitude - Latitude);
            double dLng = (Math.PI / 180) * (vector.Longtitude - Longtitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos((Math.PI / 180) * (vector.Latitude)) * Math.Cos((Math.PI / 180) * (Latitude)) *
                       Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
            return (float)(earthRadius * (2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a))));

        }
        public Double getLatitude()
        {
            return Latitude;
        }
        public Double getLongtitude()
        {
            return Longtitude;
        }
        public Vector setLatitude(Double x)
        {
            Latitude = x;
            return this;
        }
        public Vector setLongtitude(Double y)
        {
            Longtitude = y;
            return this;
        }
        public bool equals(object Object)
        {
            if (!(Object is Vector))
                return false;
            Vector vec = (Vector)Object;
            if (vec.getLatitude() == this.getLatitude() && vec.getLongtitude() == this.getLongtitude())
                return true;

            return false;
        }



    }
}
