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
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
      

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                Console.WriteLine(propName);
            this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


        private Point firstPoint = new Point();
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

                rudder = 3;
                firstPoint = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        public double getRudder()
        {
            return this.rudder;
        }
    }
}
