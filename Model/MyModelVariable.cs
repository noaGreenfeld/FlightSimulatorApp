using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Text.RegularExpressions;
using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;
using System.Diagnostics;

namespace FlightSimulator.Model
{
    partial class MyModelVariable : IModelVariable
    {
        public MyModelVariable(string s, int i)
        {
            connect(s, i);
            stopwatch = new Stopwatch();
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
        double longitude;
        double latitude;
        bool serverNotResponding = false;
        Stopwatch stopwatch;
        bool connected;

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
            Console.WriteLine("Connecting to server...");
            try
            {
                client.Connect(ip, port);
                connected = true;
            } catch
            {
                connected = false;
                throw new Exception("Can't connect");
            }
            strm = client.GetStream();
            client.ReceiveTimeout = 5000;
            client.SendTimeout = 5000;
            stop = false;
            start();
        }

        void sendCommand(string command)
        {
            try
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] msgB = asen.GetBytes(command);
                strm.Write(msgB, 0, msgB.Length);
                stopwatch.Stop();
            } catch
            {
                if (!client.Connected)
                {
                    Error = ("Not connected to server, go back to try connecting again");
                    connected = false;
                } else if (serverNotResponding)
                {
                    TimeSpan stopwatchElapsed = stopwatch.Elapsed;
                    if ((Convert.ToInt32(stopwatchElapsed.TotalSeconds)) >= 10)
                    {
                        Error = ("Server hasn't responded for 10 seconds");
                    }
                } else
                {
                    stopwatch.Start();
                    serverNotResponding = true;
                    Error = ("Server isn't responding");
                }
            }
            
        }

        String readData()
        {
            try
            {
                byte[] dataB = new byte[512];
                strm.Read(dataB, 0, 100);
                stopwatch.Stop();
                return System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                /*string ansStr = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                Console.WriteLine(ansStr);
                String[] ansArr = ansStr.Split('\n');
                int size = ansArr.Length;
                for (int i = 0; i < size; i++)
                {
                    if (ansArr[i].Contains("ERR"))
                    {
                        ansArr[i] = "";
                    }
                }
                return ansArr;*/
            } catch
            {
                if (!client.Connected)
                {
                    connected = false;
                    Error = ("Not connected to server, go back to try connecting again");
                }
                else if (serverNotResponding)
                {
                    TimeSpan stopwatchElapsed = stopwatch.Elapsed;
                    if ((Convert.ToInt32(stopwatchElapsed.TotalSeconds)) >= 10)
                    {
                        Error = ("Server hasn't responded for 10 seconds");
                    }
                }
                else
                {
                    stopwatch.Start();
                    serverNotResponding = true;
                    Error = ("Server isn't responding");
                }
                return "ERR";
                //return null;
            }
            
        }

        void start()
        {
            new Thread(delegate ()
            {
                string com;
                String ans;
                while (!stop)
                {
                    /*com = "get /instrumentation/heading-indicator/indicated-heading-deg\n" +
                    "get /instrumentation/gps/indicated-vertical-speed\n" +
                    "get /instrumentation/gps/indicated-ground-speed-kt\n" +
                    "get /instrumentation/airspeed-indicator/indicated-speed-kt\n" +
                    "get /instrumentation/gps/indicated-altitude-ft\n" +
                    "get /instrumentation/attitude-indicator/internal-roll-deg\n" +
                    "get /instrumentation/attitude-indicator/internal-pitch-deg\n" +
                    "get /instrumentation/gps/indicated-altitude-ft\n" +
                    "get /position/latitude-deg\n" +
                    "get /position/longitude-deg\n";

                    if (changeRudder)
                    {
                        com += "set /controls/flight/rudder " + rudder + "\n";
                        changeRudder = false;
                    }
                    if (changeElevator)
                    {
                        com += "set /controls/flight/elevator " + elevator + "\n";
                        changeElevator = false;
                    }
                    if (changeAileron)
                    {
                        com += "set /controls/flight/aileron " + aileron + "\n";
                        changeAileron = false;
                    }
                    if (changeThrottle)
                    {
                        com += "set /controls/engines/current-engine/throttle " + throttle + "\n";
                        changeThrottle = false;
                    }

                    sendCommand(com);
                    String[] ansArr = readData();
                    if (ansArr == null)
                    {
                        continue;
                    }
                    for (int i = 0; i < ansArr.Length; i++)
                    {
                        Console.WriteLine(ansArr[i] + '\n');
                    }
                    if (ansArr[0] != "")
                        Indicated_heading_deg = Math.Round(Double.Parse(ansArr[0]), 3);
                    if (ansArr[1] != "")
                        Console.WriteLine(ansArr[1]);
                        Gps_indicated_vertical_speed = Double.Parse(ansArr[1]);
                    if (ansArr[2] != "")
                        Gps_indicated_ground_speed_kt = Math.Round(Double.Parse(ansArr[2]), 3);
                    if (ansArr[3] != "")
                        Airspeed_indicator_indicated_speed_kt = Math.Round(Double.Parse(ansArr[3]), 3);
                    if (ansArr[4] != "")
                        Gps_indicated_altitude_ft = Math.Round(Double.Parse(ansArr[4]), 3);
                    if (ansArr[5] != "")
                        Attitude_indicator_internal_roll_deg = Math.Round(Double.Parse(ansArr[5]), 3);
                    if (ansArr[6] != "")
                        Attitude_indicator_internal_pitch_deg = Math.Round(Double.Parse(ansArr[6]), 3);
                    if (ansArr[7] != "")
                        Altimeter_indicated_altitude_ft = Math.Round(Double.Parse(ansArr[7]), 3);
                    if (ansArr[8] != "")
                      latitude = Double.Parse(ansArr[8]);
                    if (ansArr[9] != "")
                        Console.WriteLine(ansArr[9]);
                        longitude = Double.Parse(ansArr[9]);
                    latitude = 10;
                     longitude = 10;
                    setLocation(latitude, longitude);
                    */
                    
                    // get eight values for data board:
                    //1
                    sendCommand("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Indicated_heading_deg = Math.Round(Double.Parse(ans), 3);
                    }

                    //2
                    sendCommand("get /instrumentation/gps/indicated-vertical-speed\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Gps_indicated_vertical_speed = Math.Round(Double.Parse(ans), 3);
                    }

                    //3
                    sendCommand("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Gps_indicated_ground_speed_kt = Math.Round(Double.Parse(ans), 3);
                    }

                    //4
                    sendCommand("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Airspeed_indicator_indicated_speed_kt = Math.Round(Double.Parse(ans), 3);
                    }

                    //5
                    sendCommand("get /instrumentation/gps/indicated-altitude-ft\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Gps_indicated_altitude_ft = Math.Round(Double.Parse(ans), 3);
                    } 

                    //6
                    sendCommand("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Attitude_indicator_internal_roll_deg = Math.Round(Double.Parse(ans), 3);
                    }

                    //7
                    sendCommand("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Attitude_indicator_internal_pitch_deg = Math.Round(Double.Parse(ans), 3);
                    }

                    //8
                    sendCommand("get /instrumentation/gps/indicated-altitude-ft\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        Altimeter_indicated_altitude_ft = Math.Round(Double.Parse(ans), 3);
                    }

                    // latitude
                    sendCommand("get /position/latitude-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        latitude = Double.Parse(ans);
                    }
         
                    // longitude
                    sendCommand("get /position/longitude-deg\n");
                    ans = readData();
                    if (!ans.Contains("ERR"))
                    {
                        ans = CutTheText(ans);
                        longitude = Double.Parse(ans);
                    }
                    
                    setLocation(latitude, longitude); 
                    //Console.WriteLine(altitude + "   " + lattiude);
                    
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

        void disconnect() 
        {
            stop = true;
            client.Close();
        }

        private Location location;
        public Location Location
        {
            get { return new Location(location.Latitude, location.Longitude); }
        }

        public void setLocation(double latitude, double longitude)
        {
            location = new Location(latitude, longitude);
            //location.Latitude = d1;
            //location.Altitude = d2;
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
        private string error = "";
        public string Error
        {
            get { return error; }
            set
            {
                Console.WriteLine("set" + value);
                if (!error.Equals(value))
                {
                    error = value;
                    NotifyPropertyChanged("Error");
                }
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            //Console.WriteLine(propName);
            if (this.PropertyChanged != null)
                //Console.WriteLine(propName);
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string CutTheText(string s)
        {
            string[] lines = Regex.Split(s, "\n");
            //Console.WriteLine(lines[0]);
            return lines[0];
        }
    }
}


    

