﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wasalney.Utl;
using Windows.UI;

namespace wasalney.Mwasala
{
    public class Bus : Mowasla
    {
        public Bus(String Startadress, String Endadress, List<Vector> Pointsposition) : base(Startadress, Endadress, Pointsposition)
        {

            this.color = Colors.Blue;


        }
        public Bus():base()
        { this.color = Colors.Blue; }
    }
}
