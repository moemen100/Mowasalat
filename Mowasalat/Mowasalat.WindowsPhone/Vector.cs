using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mowasalat
{
    public class Vector
    {
        private Double Latitude, Longtitude;
        public Vector()
        {
            set(0, 0);
        }
        public Vector(Double x, Double y)
        {
            set(x, y);
        }
        public void set(Double x, Double y)
        {
            // TODO Auto-generated method stub
            Latitude = x;
            Longtitude = y;
        }
        public Vector(Vector vector)
        {
            set(vector.Latitude, vector.Longtitude);
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
        public bool equals(object Object )
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
