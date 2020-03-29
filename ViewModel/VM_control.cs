﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    class VM_control : VariabaleViewModel
    {
        public VM_control(IModelVariable model) : base(model) { }

        public string VM_Error
        {
            get
            {
                //Console.WriteLine("get" + model.Error);
                return model.Error;
            }
        }

        public void connect(string ip, int port)
        {
            model.connect(ip, port);
        }

        public bool isConnected()
        {
            return model.Connected;
        }
    }
}