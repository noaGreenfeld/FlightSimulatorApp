using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using FlightSimulator.Views;
using FlightSimulator.other;
using Microsoft.Maps.MapControl.WPF;


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
                NotifyPropertyChanged("VM_"+e.getS());
                Console.WriteLine(e.getS());
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {

            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void notifyViewChange( double val, string who) {
            switch (who)
            {
                case "Rudder":
                    Console.WriteLine("case vm");
                    model.setRudder(val);
                    break;
                case "Elevator":
                    model.setElevator(val);
                    break;
                case "Aileron":
                    model.setAileron(val);
                    break;
                case "Throttle":
                    model.setThrottle(val);
                    break;

            }

        }

        private double VM_aileron;
        public double VM_Aileron
        {
            get { return VM_aileron; }
            set
            {
                VM_aileron = value;
                model.setAileron(value);
            }
        }
        private double VM_throttle;
        public double VM_Throttle
        {
            get { return VM_throttle; }
            set
            {
                VM_throttle = value;
                model.setThrottle(value);
            }
        }


        public Location VM_Location
        {
            get { return model.Location; }
        }
        public double VM_Indicated_heading_deg
        {
            get { return model.Indicated_heading_deg; }
        }

        public double VM_Gps_indicated_ground_speed_kt
        {
            get { return model.Gps_indicated_ground_speed_kt; }
        }
        public double VM_Gps_indicated_vertical_speed
        {
            get { return model.Gps_indicated_vertical_speed; }
        }
        public double VM_Airspeed_indicator_indicated_speed_kt
        {
            get { return model.Airspeed_indicator_indicated_speed_kt; }
        }
        public double VM_Gps_indicated_altitude_ft
        {
            get { return model.Gps_indicated_altitude_ft; }
        }
        public double VM_Attitude_indicator_internal_roll_deg
        {
            get { return model.Attitude_indicator_internal_roll_deg; }
        }
        public double VM_Attitude_indicator_internal_pitch_deg
        {
            get { return model.Attitude_indicator_internal_pitch_deg; }
        }
        public double VM_Altimeter_indicated_altitude_ft
        {
            get { return model.Altimeter_indicated_altitude_ft; }
        }
    }
}
