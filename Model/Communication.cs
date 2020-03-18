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
                while (!stop)
                {
                    msg = "get/ indicated-heading-deg";
                    byte[] msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    byte[] dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    dataList.Add(ans);




                }
            }).Start();
        }
    }
}
