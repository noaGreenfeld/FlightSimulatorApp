using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace FlightSimulator.Model
{
    partial class MyModelVariable : IModelVariable
    {

        void IModelVariable.connect(string ip, int port)
        {
          
            client = new TcpClient();
            client.Connect(ip, port);
            strm = client.GetStream();
            stop = false;
            start();
        }

        void IModelVariable.disconnect()
        {
            stop = true;
            ////
        }

        void IModelVariable.start()
        {
            new Thread(delegate ()
            {
                String msg;
                String ans;
                ASCIIEncoding asen = new ASCIIEncoding();
                List<string> dataList = new List<string>();
                byte[] msgB = new byte[256];
                byte[] dataB = new byte[256];
                while (!stop)
                {
                    // get eight values for data board:
                    //1
                    msg = "get/ indicated-heading-deg\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    indicated_heading_deg = Double.Parse(ans);
                    //2
                    msg = "get/ gps_indicated-vertical-speed\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    gps_indicated_vertical_speed = double.Parse(ans);
                    //3
                    msg = "get/ gps_indicated-ground-speed-kt\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    gps_indicated_ground_speed_kt = Double.Parse(ans);
                    //4
                    msg = "get/ airspeed-indicator_indicated-speed-kt\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    airspeed_indicator_indicated_speed_kt = Double.Parse(ans);
                    //5
                    msg = "get/ gps_indicated-altitude-ft\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    gps_indicated_altitude_ft = Double.Parse(ans);
                    //6
                    msg = "get/ attitude-indicator_internal-roll-deg\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    attitude_indicator_internal_roll_deg = Double.Parse(ans);
                    //7
                    msg = "get/ attitude-indicator_internal-pitch-deg\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    attitude_indicator_internal_pitch_deg = Double.Parse(ans);
                    //8
                    msg = "get/ altimeter_indicated-altitude-ft\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    altimeter_indicated_altitude_ft = Double.Parse(ans);

                    Thread.Sleep(250);
                }
            }).Start();
        }


        
        void ignore()
        {
            int i;
            String msg;
            for (i = 1; i <= 8; i++)
            {
                switch (i)
                {
                    case 1:
                        msg = "get/ indicated-heading-deg\n";
                        break;
                    case 2:
                        msg = "get/ gps_indicated-vertical-speed\n";
                        break;
                    case 3:
                        msg = "get/ gps_indicated-ground-speed-kt\n";
                        break;
                    case 4:
                        msg = "get/ airspeed-indicator_indicated-speed-kt\n";
                        break;
                    case 5:
                        msg = "get/ gps_indicated-altitude-ft\n";
                        break;
                    case 6:
                        msg = "get/ attitude-indicator_internal-roll-deg\n";
                        break;
                    case 7:
                        msg = "get/ attitude-indicator_internal-pitch-deg\n";
                        break;
                    case 8:
                        msg = "get/ altimeter_indicated-altitude-ft\n";
                        break;
                    default:
                        msg = "";
                        break;
                }
            }
        }
    }
}



