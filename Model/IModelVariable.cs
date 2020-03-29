using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;
namespace FlightSimulator.Model

{
    interface IModelVariable : INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start();
        
        void setRudder(double rud);
        void setElevator(double ele);
        void setAileron(double ail);
        void setThrottle(double thr);

        // variables properties
        double Indicated_heading_deg { set; get; }
        double Gps_indicated_vertical_speed { set; get; }
        double Gps_indicated_ground_speed_kt { set; get; }
        double Airspeed_indicator_indicated_speed_kt  { set; get; }
        double Gps_indicated_altitude_ft { set; get; }
        double Attitude_indicator_internal_roll_deg { set; get; }
        double Attitude_indicator_internal_pitch_deg { set; get; }
        double Altimeter_indicated_altitude_ft { set; get; }
        Location Location { get; }
        string Error { set; get; }
        bool Connected { get; }

    }

}

