using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    interface IModelVariable : INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start();
        // variables properties
       double Indicated_heading_deg { set; get; }
        double gps_indicated_vertical_speed { set; get; }
        double gps_indicated_ground_speed_kt { set; get; }
        double airspeed_indicator_indicated_speed_kt  { set; get; }
        double gps_indicated_altitude_ft { set; get; }
        double attitude_indicator_internal_roll_deg { set; get; }
        double attitude_indicator_internal_pitch_deg { set; get; }
        double altimeter_indicated_altitude_ft { set; get; }

    }

}

