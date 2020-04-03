using System;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    // view model for connect,disconnect buttons and error line
    public class VM_control : VariabaleViewModel
    {
        public VM_control(IModelVariable model) : base(model) { }

        public string VM_Error
        {
            get
            {
                return model.Error;
            }
        }

        public void connect(string ip, int port)
        {
            model.connect(ip, port);
        }

        public void disconnect()
        {
            model.disconnect();
        }

        public bool isConnected()
        {
            return model.Connected;
        }

        public bool VM_CanConnect
        {
            get { return !model.Connected; }
        }
    }
}
