using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls.Maps;

namespace Mowasalat
{
    
        class Mashroo3 : Mowasla
        {
            public Mashroo3(String Startadress, String Endadress, List<Vector> Pointsposition) : base(Startadress, Endadress, Pointsposition)
            {

                this.color = Colors.Red;


            }

        }
    
}
