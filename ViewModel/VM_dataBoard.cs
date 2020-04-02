using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    public class VM_dataBoard : VariabaleViewModel
    {
        public VM_dataBoard(IModelVariable model) : base(model) {}

        public string VM_Indicated_heading_deg
        {
            get { return model.Indicated_heading_deg; }
        }

        public string VM_Gps_indicated_ground_speed_kt
        {
            get { return model.Gps_indicated_ground_speed_kt; }
        }

        public string VM_Gps_indicated_vertical_speed
        {
            get { return model.Gps_indicated_vertical_speed; }
        }

        public string VM_Airspeed_indicator_indicated_speed_kt
        {
            get { return model.Airspeed_indicator_indicated_speed_kt; }
        }

        public string VM_Gps_indicated_altitude_ft
        {
            get { return model.Gps_indicated_altitude_ft; }
        }

        public string VM_Attitude_indicator_internal_roll_deg
        {
            get { return model.Attitude_indicator_internal_roll_deg; }
        }

        public string VM_Attitude_indicator_internal_pitch_deg
        {
            get { return model.Attitude_indicator_internal_pitch_deg; }
        }

        public string VM_Altimeter_indicated_altitude_ft
        {
            get { return model.Altimeter_indicated_altitude_ft; }
        }
    }
}

