using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulator.ViewModel
{
    class VM_map : VariabaleViewModel
    {
        public VM_map(IModelVariable model): base(model) { }
        public Location VM_Location
        {
            get
            {
                //Console.WriteLine("get  " + model.Location.Altitude);
                return model.Location;
            }
        }
    }
}
