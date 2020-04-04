using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlightSimulator.Views
{
    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
        }

        private Point startPoint = new Point();
        
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

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            double x, y;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                x = e.GetPosition(this).X - startPoint.X;
                y = e.GetPosition(this).Y - startPoint.Y;
                if (Math.Sqrt(x * x + y * y) < (internalBase.Width / 2))
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Mouse.Capture(null);
        }
    }
}