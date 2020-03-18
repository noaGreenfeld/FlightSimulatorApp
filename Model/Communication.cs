using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlightSimulator.Model
{
    partial class MyModelVariable : IModelVariable
    {
        volatile Boolean stop;
        private Socket server;
        void IModelVariable.connect(string ip, int port)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Establishing Connection to {0}", ip);
            s.Connect(ip, port);
            Console.WriteLine("Connection established");
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
                while(!stop)
                {
                    //int i = server.Send(new String("get/ indicated-heading-deg"));

                }
            }).Start();
        }
    }
}
