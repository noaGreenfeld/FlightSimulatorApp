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
        volatile Boolean stop;
        //private Socket server;
        TcpClient client;
        NetworkStream strm;
        void IModelVariable.connect(string ip, int port)
        {
            client = new TcpClient();
            client.Connect(ip, port);

            //server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Console.WriteLine("Establishing Connection to {0}", ip);
            //server.Connect(ip, port);
            //Console.WriteLine("Connection established");

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
                int i;
                while (!stop)
                {
                    // get eight values for data board:
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
                        byte[] msgB = asen.GetBytes(msg);
                        strm.Write(msgB, 0, msgB.Length);
                        byte[] dataB = new byte[100];
                        strm.Read(dataB, 0, 100);
                        ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                        dataList.Add(ans);
                    }
                }
            }).Start();
        }
    }
}
