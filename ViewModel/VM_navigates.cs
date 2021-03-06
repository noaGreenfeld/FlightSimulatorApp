﻿using System;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    // View model for navigates- joystick and sliders
    public class VM_navigates : VariabaleViewModel
    {
        public VM_navigates(IModelVariable model) : base(model) {}

        private double VM_aileron;
        public double VM_Aileron
        {
            get { return VM_aileron; }
            set
            {
                VM_aileron = value;
                model.SetAileron(value);
            }
        }

        private double VM_throttle;
        public double VM_Throttle
        {
            get { return VM_throttle; }
            set
            {
                VM_throttle = value;
                model.SetThrottle(value);
            }
        }

        private double VM_rudder;
        public double VM_Rudder
        {
            get { return VM_rudder; }
            set
            {
                VM_rudder = value;
                model.SetRudder(value);
            }
        }

        private double VM_elevator;
        public double VM_Elevator
        {
            get { return VM_elevator; }
            set
            {
                VM_elevator = value;
                model.SetElevator(value);
            }
        }
    }
}
