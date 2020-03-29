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
        string Indicated_heading_deg { set; get; }
        string Gps_indicated_vertical_speed { set; get; }
        string Gps_indicated_ground_speed_kt { set; get; }
        string Airspeed_indicator_indicated_speed_kt  { set; get; }
        string Gps_indicated_altitude_ft { set; get; }
        string Attitude_indicator_internal_roll_deg { set; get; }
        string Attitude_indicator_internal_pitch_deg { set; get; }
        string Altimeter_indicated_altitude_ft { set; get; }
        Location Location { get; }
        string Error { set; get; }
        bool Connected { set; get; }

    }

}

