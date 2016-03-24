using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wasalney.Utl;
using Windows.UI;

namespace wasalney.Mwasala
{
    public class Mashroo3 : Mowasla
    {
        public Mashroo3(String Startadress, String Endadress, List<Vector> Pointsposition) : base(Startadress, Endadress, Pointsposition)
        {

            this.color = Colors.Red;


        }
        public Mashroo3():base()
        { this.color = Colors.Blue; }

    }
}
