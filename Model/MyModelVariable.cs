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
        
        public void setRudder(double rud)
        {
            changeRudder = true;
            rudder = rud;
        }
        public void setElevator(double ele)
        {
            changeElevator = true;
            elevator = ele;
        }
        public void setAileron(double ail)
        {
            changeAileron = true;
            aileron = ail;
        }
        public void setThrottle(double thr)
        {
            changeThrottle = true;
            throttle = thr;
        }

        public void connect(string ip, int port) 
        {
            client = new TcpClient();
            try
            {
                client.Connect(ip, port);
                IsConnected = true;
                strm = client.GetStream();
                strm.ReadTimeout = 10000;
                stop = false;
                Error = "";
                start();
            } catch (Exception)
            {
                stop = true;
                IsConnected = false;
                Error = "Can't connect";
                throw new Exception("Can't connect");
            }
        }

        void sendCommand(string command)
        {
            try
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] msgB = asen.GetBytes(command);
                strm.Write(msgB, 0, msgB.Length);
            } catch (Exception)
            {
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

        String readData()
        {
            try
            {
                byte[] dataB = new byte[512];
                strm.Read(dataB, 0, 100);
                return Encoding.ASCII.GetString(dataB, 0, dataB.Length);
            } catch (Exception)
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

        public void start()
        {
            new Thread(delegate ()
            {
                String ans;
                while (!stop)
                {
                    // get eight values for data board:
                    //1
                    sendCommand("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    ans = readData();
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
                    sendCommand("get /instrumentation/gps/indicated-vertical-speed\n");
                    ans = readData();
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
                    sendCommand("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    ans = readData();
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
                    sendCommand("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    ans = readData();
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
                    sendCommand("get /instrumentation/gps/indicated-altitude-ft\n");
                    ans = readData();
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
                    sendCommand("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    ans = readData();
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
                    sendCommand("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    ans = readData();
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
                    sendCommand("get /instrumentation/gps/indicated-altitude-ft\n");
                    ans = readData();
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
                    sendCommand("get /position/latitude-deg\n");
                    ans = readData();
                    try
                    {
                        ans = CutTheText(ans);
                        latitude = Double.Parse(ans);
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }
                    
                    // longitude
                    sendCommand("get /position/longitude-deg\n");
                    ans = readData();
                    try 
                    {
                        ans = CutTheText(ans);
                        longitude = Double.Parse(ans);
                    } catch (FormatException)
                    {
                        ans = "ERR";
                    }

                    setLocation(latitude, longitude);

                    if (changeRudder)
                    {
                        sendCommand("set /controls/flight/rudder " + rudder + "\n");
                        ans = readData();
                        changeRudder = false;
                    }
                    if (changeElevator)
                    {
                        sendCommand("set /controls/flight/elevator " + elevator + "\n");
                        ans = readData();
                        changeElevator = false;
                    }
                    if (changeAileron)
                    {
                        sendCommand("set /controls/flight/aileron " + aileron + "\n");
                        ans = readData();
                        changeAileron = false;
                    }
                    if (changeThrottle)
                    {
                        sendCommand("set /controls/engines/current-engine/throttle " + throttle + "\n");
                        ans = readData();
                        changeThrottle = false;
                    }
                    
                    Thread.Sleep(250);
                }
            }).Start();
        }

        public void disconnect() 
        {
            stop = true;
            IsConnected = false;
            Error = "You are disconnected";
            client.Close();
        }

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

        public void setLocation(double latitude, double longitude)
        {
            if ((latitude < 90) && (latitude > -90) && (longitude < 180) && (longitude>-180)) {
                location = new Location(latitude, longitude);
                NotifyPropertyChanged("Location");
            }
            else
            {
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
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string CutTheText(string s)
        {
            string[] lines = Regex.Split(s, "\n");
            return lines[0];
        }
    }
}

