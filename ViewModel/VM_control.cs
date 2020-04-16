using System;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    // View model for connect,disconnect buttons and error line
    public class VM_control : VariabaleViewModel
    {
        public VM_control(IModelVariable model) : base(model) { }

        public string VM_Error
        {
            get
            { return model.Error; }
        }

        public bool VM_IsConnected
        {
            get { return model.IsConnected; }
        }

        public void Connect(string ip, int port)
        {
            model.Connect(ip, port);
        }

        public void Disconnect()
        {
            model.Disconnect();
        }
    }
}
