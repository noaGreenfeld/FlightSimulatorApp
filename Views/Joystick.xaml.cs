using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator.other;
using FlightSimulator.Model;

namespace FlightSimulator.Views
{
    public partial class Joystick : UserControl , INotifyPropertyChanged
    {
        private double rudder;
        public double Rudder
        {
            get { return rudder; }
            set
            {
                Console.WriteLine(value+"  set");
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        private double elevator;
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
      
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private Point startPoint = new Point();

        public Joystick()
        {
            InitializeComponent();
        }

        private void centerKnob_Completed(object sender, EventArgs e) {}

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
           if (e.LeftButton == MouseButtonState.Pressed)
            {
                //Rudder = 7;
                startPoint = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            double x;
            double y;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                x = e.GetPosition(this).X - startPoint.X;
                y = e.GetPosition(this).Y - startPoint.Y;
                if (Math.Sqrt(x * x + y * y) < (Base.Width - KnobBase.Width) / 2)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }
                Rudder = x / ((Base.Width - KnobBase.Width))*2;
                Elevator = y / ((Base.Width - KnobBase.Width))*2;
            } 
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }

        public double getRudder()
        {
            return this.rudder;
        }
        public double getElevator()
        {
            return this.elevator;
        }
    }
}
