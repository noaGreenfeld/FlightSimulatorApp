using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using FlightSimulator.other;
using System.Text.RegularExpressions;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulator.Model
{
    partial class MyModelVariable : IModelVariable
    {
        public MyModelVariable(string s, int i)
        {
            connect(s, i);
        }

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
        double altitude;
        double lattiude;

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


        void connect(string ip, int port) 
        {
            client = new TcpClient();
            while (!client.Connected)
            {
                Console.WriteLine("Connecting to server...");
                try
                {
                    client.Connect(ip, port);
                } catch (Exception)
                {
                    Console.WriteLine("Can't connect. Trying again...");
                }
            }
            strm = client.GetStream();
            stop = false;
            start();
        }

        void sendCommand(string command)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] msgB = asen.GetBytes(command);
            strm.Write(msgB, 0, msgB.Length);
        }

        String readData()
        {
            byte[] dataB = new byte[256];
            strm.Read(dataB, 0, 100);
            return System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
        }

        void start()
        {
            new Thread(delegate ()
            {
                String ans;
                while (!stop)
                {
                    // get eight values for data board:
                    //1
                    sendCommand("get /indicated-heading-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Indicated_heading_deg = Double.Parse(ans);
                    }

                    //2
                    sendCommand("get /gps_indicated-vertical-speed\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Gps_indicated_vertical_speed = Double.Parse(ans);
                    }

                    //3
                    sendCommand("get /gps_indicated-ground-speed-kt\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Gps_indicated_ground_speed_kt = Double.Parse(ans);
                    }

                    //4
                    sendCommand("get /airspeed-indicator_indicated-speed-kt\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Airspeed_indicator_indicated_speed_kt = Double.Parse(ans);
                    }

                    //5
                    sendCommand("get /gps_indicated-altitude-ft\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Gps_indicated_altitude_ft = Double.Parse(ans);
                    } 

                    //6
                    sendCommand("get /attitude-indicator_internal-roll-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Attitude_indicator_internal_roll_deg = Double.Parse(ans);
                    }

                    //7
                    sendCommand("get /attitude-indicator_internal-pitch-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        this.Attitude_indicator_internal_pitch_deg = Double.Parse(ans);
                    }

                    //8
                    sendCommand("get /altimeter_indicated-altitude-ft\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Altimeter_indicated_altitude_ft = Double.Parse(ans);
                    }

                    //location 1
                    sendCommand("get /position/latitude-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        altitude = Double.Parse(ans);
                    }
         
                    //location 2
                    sendCommand("get /position/longitude-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        lattiude = Double.Parse(ans);
                    }

                    setLocation(lattiude, altitude);

                    if (changeRudder)
                    {
                        Console.WriteLine("rudder" + rudder);
                        sendCommand("set /controls/flight/rudder\n");
                        changeRudder = false;
                    }
                    if (changeElevator)
                    {
                        Console.WriteLine("elevator" + elevator);
                        sendCommand("set /controls/flight/elevator\n");
                        changeElevator = false;
                    }
                    if (changeAileron)
                    {
                        Console.WriteLine("aileron" + aileron);
                        sendCommand("set /controls/flight/aileron\n");
                        changeAileron = false;
                    }
                    if (changeThrottle)
                    {
                        Console.WriteLine("throttle" + throttle);
                        sendCommand("set /controls/flight/throttle\n");
                        changeThrottle = false;
                    }

                    Thread.Sleep(250);
                }

            }).Start();
        }

        void disconnect() 
        {
            stop = true;
            client.Close();
        }
        private Location location;
        public Location Location
        {
            get { return location; }
        }
        public void setLocation(double d1, double d2)
        {
            location.Latitude = d1;
            location.Altitude = d2;
            NotifyPropertyChanged("Location");

        }
        private double indicated_heading_deg =40;
        public double Indicated_heading_deg
        {
            get { return indicated_heading_deg; }
            set
            {
                indicated_heading_deg = value;
                NotifyPropertyChanged("Indicated_heading_deg");
            }
        }

        private double gps_indicated_vertical_speed=50;
        public double Gps_indicated_vertical_speed
        {
            get { return gps_indicated_vertical_speed; }
            set
            {
                gps_indicated_vertical_speed = value;
                NotifyPropertyChanged("Gps_indicated_vertical_speed");
            }
        }

        private double gps_indicated_ground_speed_kt;
        public double Gps_indicated_ground_speed_kt
        {
            get { return gps_indicated_ground_speed_kt; }
            set
            {
                gps_indicated_ground_speed_kt = value;
                NotifyPropertyChanged("Gps_indicated_ground_speed_kt");
            }
        }

        private double airspeed_indicator_indicated_speed_kt;
        public double Airspeed_indicator_indicated_speed_kt
        {
            get { return airspeed_indicator_indicated_speed_kt; }
            set
            {
                airspeed_indicator_indicated_speed_kt = value;
                NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt");
            }
        }

        private double gps_indicated_altitude_ft;
        public double Gps_indicated_altitude_ft
        {
            get { return gps_indicated_altitude_ft; }
            set
            {
                gps_indicated_altitude_ft = value;
                NotifyPropertyChanged("Gps_indicated_altitude_ft");
            }
        }

        private double attitude_indicator_internal_roll_deg;
        public double Attitude_indicator_internal_roll_deg
        {
            get { return attitude_indicator_internal_roll_deg; }
            set
            {
                attitude_indicator_internal_roll_deg = value;
                NotifyPropertyChanged("Attitude_indicator_internal_roll_deg");
            }
        }

        private double attitude_indicator_internal_pitch_deg;
        public double Attitude_indicator_internal_pitch_deg
        {
            get { return attitude_indicator_internal_pitch_deg; }
            set
            {
                attitude_indicator_internal_pitch_deg = value;
                NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg");
            }
        }

        private double altimeter_indicated_altitude_ft;
        public double Altimeter_indicated_altitude_ft
        {
            get { return altimeter_indicated_altitude_ft; }
            set
            {
                altimeter_indicated_altitude_ft = value;
                NotifyPropertyChanged("Altimeter_indicated_altitude_ft");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            Console.WriteLine(propName);
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string CutTheText(string s)
        {
            string[] lines = Regex.Split(s, "\n");
            Console.WriteLine(lines[0]);
            return lines[0];
        }
    }
}


    

