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
using FlightSimulator.Model;
using System.ComponentModel;

namespace FlightSimulator.Views
{
    public partial class Joystick : UserControl, INotifyPropertyChanged
    {
        private double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                NotifyPropertyChanged("X");
            }
        }
        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                NotifyPropertyChanged("Y");

            }
        }

        private Point startPoint = new Point();

        public Joystick()
        {
            InitializeComponent();
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Mouse.Capture(this.KnobBase);
                startPoint = e.GetPosition(this);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                X = e.GetPosition(this).X - startPoint.X;
                Y = e.GetPosition(this).Y - startPoint.Y;
                if (Math.Sqrt(x * x + y * y) < (internalBase.Width / 2))
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                   // Rudder = x / (internalBase.Width / 2.0);
                  //  Elevator = y / (internalBase.Width / 2.0);
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // knobPosition.X = 0;
            // knobPosition.Y = 0;
            Mouse.Capture(null);
        }
    }
}