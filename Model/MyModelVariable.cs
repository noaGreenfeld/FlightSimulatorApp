using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;

namespace FlightSimulator.Model
{
    public partial class MyModelVariable : IModelVariable
    {
        public MyModelVariable() {}

        public event PropertyChangedEventHandler PropertyChanged;
        volatile Boolean stop;
        TcpClient client;
        NetworkStream strm;
        bool changeRudder = false;
        double rudder;
        bool changeElevator = false;
        double elevator;
        double aileron;
        bool changeAileron = false;
        double throttle;
        bool changeThrottle = false;
        double longitude;
        double latitude;

        // Change value of rudder and acknowledge the value has changed
        public void SetRudder(double rud)
        {
            changeRudder = true;
            rudder = rud;
        }
        // Change value of elevator and acknowledge the value has changed
        public void SetElevator(double ele)
        {
            changeElevator = true;
            elevator = ele;
        }
        // Change value of aileron and acknowledge the value has changed
        public void SetAileron(double ail)
        {
            changeAileron = true;
            aileron = ail;
        }
        // Change value of throttle and acknowledge the value has changed
        public void SetThrottle(double thr)
        {
            changeThrottle = true;
            throttle = thr;
        }

        public void Connect(string ip, int port) 
        {
            client = new TcpClient();
            try
            {
                client.Connect(ip, port);
                IsConnected = true;
                strm = client.GetStream();
                strm.ReadTimeout = 10000;
                stop = false;
                // If connected - don't display an error message
                Error = "";
                Start();
            } catch (Exception)
            {
                stop = true;
                IsConnected = false;
                // Let the vm know there is an error and throw an exception
                Error = "Can't connect";
                throw new Exception("Can't connect");
            }
        }
        // Send the given string to the server
        void SendCommand(string command)
        {
            try
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] msgB = asen.GetBytes(command);
                strm.Write(msgB, 0, msgB.Length);
            } catch (Exception)
            {
                // If the client is disconnected, write an error massage to the vm
                if (!client.Connected)
                {
                    Error = "Not connected to server, please connect again";
                    IsConnected = false;
                    stop = true;
                } else
                {
                    Error = "Server hasn't responded for 10 seconds";
                }
            }
        }

        // Read data from the server
        String ReadData()
        {
            try
            {
                byte[] dataB = new byte[512];
                strm.Read(dataB, 0, 100);
                return Encoding.ASCII.GetString(dataB, 0, dataB.Length);
            } catch (Exception)
            // If something went wrong - update the error message 
            {
                if (!client.Connected)
                {
                    IsConnected = false;
                    stop = true;
                    Error = ("Not connected to server, please connect again");
                } else
                {
                    Error = "Server hasn't responded for 10 seconds";
                }
                return "ERR";
            }
        }

        // Method for managing cummunication with the server
        public void Start()
        {
            new Thread(delegate ()
            {
                String ans;
                while (!stop)
                {
                    // Get eight values for data board:
                    //1
                    SendCommand("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    ans = ReadData();
                    try
                    {
                        ans = CutTheText(ans);
                        ans = Math.Round(Convert.ToDouble(ans), 5).ToString();
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    Indicated_heading_deg = ans;

                    //2
                    SendCommand("get /instrumentation/gps/indicated-vertical-speed\n");
                    ans = ReadData();
                    try 
                    {
                        ans = CutTheText(ans);
                        ans = Math.Round(Convert.ToDouble(ans), 5).ToString();
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    Gps_indicated_vertical_speed = ans;

                    //3
                    SendCommand("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    ans = ReadData();
                    try
                    {
                        ans = CutTheText(ans);
                        ans = Math.Round(Convert.ToDouble(ans), 5).ToString();
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    Gps_indicated_ground_speed_kt = ans;

                    //4
                    SendCommand("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    ans = ReadData();
                    try
                    {
                        ans = CutTheText(ans);
                        ans = Math.Round(Convert.ToDouble(ans), 5).ToString();
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    Airspeed_indicator_indicated_speed_kt = ans;

                    //5
                    SendCommand("get /instrumentation/gps/indicated-altitude-ft\n");
                    ans = ReadData();
                    try 
                    {
                        ans = CutTheText(ans);
                        ans = Math.Round(Convert.ToDouble(ans), 5).ToString();
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    Gps_indicated_altitude_ft = ans;

                    //6
                    SendCommand("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    ans = ReadData();
                    try
                    {
                        ans = CutTheText(ans);
                        ans = Math.Round(Convert.ToDouble(ans), 5).ToString();
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    Attitude_indicator_internal_roll_deg = ans;

                    //7
                    SendCommand("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    ans = ReadData();
                    try
                    {
                        ans = CutTheText(ans);
                        ans = Math.Round(Convert.ToDouble(ans), 5).ToString();
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    Attitude_indicator_internal_pitch_deg = ans;

                    //8
                    SendCommand("get /instrumentation/gps/indicated-altitude-ft\n");
                    ans = ReadData();
                    try
                    {
                        ans = CutTheText(ans);
                        ans = Math.Round(Convert.ToDouble(ans), 5).ToString();
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    Altimeter_indicated_altitude_ft = ans;

                    // latitude
                    SendCommand("get /position/latitude-deg\n");
                    ans = ReadData();
                    try
                    {
                        ans = CutTheText(ans);
                        latitude = Double.Parse(ans);
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    
                    // longitude
                    SendCommand("get /position/longitude-deg\n");
                    ans = ReadData();
                    try 
                    {
                        ans = CutTheText(ans);
                        longitude = Double.Parse(ans);
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    // Set the location based on the lattiude and longitude
                    SetLocation(latitude, longitude);

                    // Send messages according to whether the variables changed or not
                    if (changeRudder)
                    {
                        SendCommand("set /controls/flight/rudder " + rudder + "\n");
                        ans = ReadData();
                        changeRudder = false;
                    }
                    if (changeElevator)
                    {
                        SendCommand("set /controls/flight/elevator " + elevator + "\n");
                        ans = ReadData();
                        changeElevator = false;
                    }
                    if (changeAileron)
                    {
                        SendCommand("set /controls/flight/aileron " + aileron + "\n");
                        ans = ReadData();
                        changeAileron = false;
                    }
                    if (changeThrottle)
                    {
                        SendCommand("set /controls/engines/current-engine/throttle " + throttle + "\n");
                        ans = ReadData();
                        changeThrottle = false;
                    }
                    
                    Thread.Sleep(250);
                }
            }).Start();
        }

        public void Disconnect() 
        {
            stop = true;
            IsConnected = false;
            Error = "You are disconnected";
            client.Close();
        }

        // Properties:
        private Location location;
        public Location Location
        {
            get 
            { 
                if (location == null)
                {
                    return new Location(0, 0);
                }
                return new Location(location.Latitude, location.Longitude); 
            }
        }
  
        public void SetLocation(double latitude, double longitude)
        {
            // Check if the new location is in earth (valid location) 
            if ((latitude < 90) && (latitude > -90) && (longitude < 180) && (longitude>-180)) {
              // Change the location and notify the vm
                location = new Location(latitude, longitude);
                NotifyPropertyChanged("Location");
            }
            else
            {
                // Location isn't valid
                Error = "Location is out of earth";
            }
        }

        private string indicated_heading_deg ;
        public string Indicated_heading_deg
        {
            get { return indicated_heading_deg; }
            set
            {
                indicated_heading_deg = value;
                NotifyPropertyChanged("Indicated_heading_deg");
            }
        }

        private string gps_indicated_vertical_speed ;
        public string Gps_indicated_vertical_speed
        {
            get { return gps_indicated_vertical_speed; }
            set
            {
                gps_indicated_vertical_speed = value;
                NotifyPropertyChanged("Gps_indicated_vertical_speed");
            }
        }

        private string gps_indicated_ground_speed_kt;
        public string Gps_indicated_ground_speed_kt
        {
            get { return gps_indicated_ground_speed_kt; }
            set
            {
                gps_indicated_ground_speed_kt = value;
                NotifyPropertyChanged("Gps_indicated_ground_speed_kt");
            }
        }

        private string airspeed_indicator_indicated_speed_kt;
        public string Airspeed_indicator_indicated_speed_kt
        {
            get { return airspeed_indicator_indicated_speed_kt; }
            set
            {
                airspeed_indicator_indicated_speed_kt = value;
                NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt");
            }
        }

        private string gps_indicated_altitude_ft;
        public string Gps_indicated_altitude_ft
        {
            get { return gps_indicated_altitude_ft; }
            set
            {
                gps_indicated_altitude_ft = value;
                NotifyPropertyChanged("Gps_indicated_altitude_ft");
            }
        }

        private string attitude_indicator_internal_roll_deg;
        public string Attitude_indicator_internal_roll_deg
        {
            get { return attitude_indicator_internal_roll_deg; }
            set
            {
                attitude_indicator_internal_roll_deg = value;
                NotifyPropertyChanged("Attitude_indicator_internal_roll_deg");
            }
        }

        private string attitude_indicator_internal_pitch_deg;
        public string Attitude_indicator_internal_pitch_deg
        {
            get { return attitude_indicator_internal_pitch_deg; }
            set
            {
                attitude_indicator_internal_pitch_deg = value;
                NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg");
            }
        }

        private string altimeter_indicated_altitude_ft;
        public string Altimeter_indicated_altitude_ft
        {
            get { return altimeter_indicated_altitude_ft; }
            set
            {
                altimeter_indicated_altitude_ft = value;
                NotifyPropertyChanged("Altimeter_indicated_altitude_ft");
            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    NotifyPropertyChanged("IsConnected");
                }
            }
        }

        private string error = "";
        public string Error
        {
            get { return error; }
            set
            {
                if (!error.Equals(value))
                {
                    error = value;
                    NotifyPropertyChanged("Error");
                }
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Cut the given string to the end of line
        public string CutTheText(string s)
        {
            string[] lines = Regex.Split(s, "\n");
            return lines[0];
        }
    }
}

