using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;

namespace FlightSimulator.Views
{
    public partial class Joystick : UserControl//, INotifyPropertyChanged
    {
        private double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
              //  NotifyPropertyChanged("X");
            }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
               // NotifyPropertyChanged("Y");
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

        /*public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }*/

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            double x1, y1;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                x1 = e.GetPosition(this).X - startPoint.X;
                y1 = e.GetPosition(this).Y - startPoint.Y;
                if (Math.Sqrt(x1 * x1 + y1 * y1) < (internalBase.Width / 2))
                {
                    X = x1;
                    Y = y1;
                    knobPosition.X = x1;
                    knobPosition.Y = y1;
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
         //   X = 0;
           // Y = 0;
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Mouse.Capture(null);
        }
    }
}