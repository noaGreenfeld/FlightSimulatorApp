using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    class VM_navigates : VariabaleViewModel
    {
        public VM_navigates(IModelVariable model) : base(model) { }
        public void notifyViewChange(double val, string who)
        {
            switch (who)
            {
                case "Rudder":
                    model.setRudder(val);
                    break;
                case "Elevator":
                    model.setElevator(val);
                    break;
                case "Aileron":
                    model.setAileron(val);
                    break;
                case "Throttle":
                    model.setThrottle(val);
                    break;

            }


        }
        private double VM_aileron;
        public double VM_Aileron
        {
            get { return VM_aileron; }
            set
            {
                Console.WriteLine("vn_navigates set aileron");
                VM_aileron = value;
                model.setAileron(value);
            }
        }
        private double VM_throttle;
        public double VM_Throttle
        {
            get { return VM_throttle; }
            set
            {
                VM_throttle = value;
                model.setThrottle(value);
            }
        }

    }

}
