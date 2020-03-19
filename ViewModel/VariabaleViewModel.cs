using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using FlightSimulator.Views;

namespace FlightSimulator.ViewModel
{
    class VariabaleViewModel : INotifyPropertyChanged
    {
        private IModelVariable model;
        public VariabaleViewModel(IModelVariable model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                Console.WriteLine(e.getS()+"  delegate");
                NotifyPropertyChanged("VM_"+e.getS());
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            Console.WriteLine("viewModel");
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


        public double VM_Indicated_heading_deg
        {
            get { return model.Indicated_heading_deg; }
        }

        public double VM_gps_indicated_ground_speed_kt
        {
            get { return model.gps_indicated_ground_speed_kt; }
        }
        public double VM_gps_indicated_vertical_speed
        {
            get { return model.gps_indicated_vertical_speed; }
        }
        public double VM_airspeed_indicator_indicated_speed_kt
        {
            get { return model.airspeed_indicator_indicated_speed_kt; }
        }
        public double VM_gps_indicated_altitude_ft
        {
            get { return model.gps_indicated_altitude_ft; }
        }
        public double VM_attitude_indicator_internal_roll_deg
        {
            get { return model.attitude_indicator_internal_roll_deg; }
        }
        public double VM_attitude_indicator_internal_pitch_deg
        {
            get { return model.attitude_indicator_internal_pitch_deg; }
        }
        public double VM_altimeter_indicated_altitude_ft
        {
            get { return model.altimeter_indicated_altitude_ft; }
        }
    }
}
