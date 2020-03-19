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
        public event PropertyChangedEventHandler PropertyChanged;
      

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                Console.WriteLine(propName +"  notify joystick");
            this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


        private Point startPoint = new Point();
        private Point endPoint = new Point();

        public Joystick()
        {
            InitializeComponent();
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {}

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
           if (e.LeftButton == MouseButtonState.Pressed)
            {
                Console.WriteLine("joystickPush");
                Rudder = 7;
                startPoint = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            double x = 0;
            double y = 0;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                x = e.GetPosition(this).X - startPoint.X;
                y = e.GetPosition(this).Y - startPoint.Y;
                if (Math.Sqrt(x*x+ y*y) < (Base.Width - KnobBase.Width) / 2)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }
                if (Math.Sqrt(x * x + y * y) >= (Base.Width - KnobBase.Width) / 2)
                {
                    knobPosition.X = 0;
                    knobPosition.Y = 0;
                }
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
    }
}
