using FlightSimulator.Model;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulator.ViewModel
{
    public class VM_map : VariabaleViewModel
    {
        public VM_map(IModelVariable model): base(model) {}

        public Location VM_Location
        {
            get
            {
                return model.Location;
            }
        }
    }
}
