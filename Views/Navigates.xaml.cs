using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Navigates.xaml
    /// </summary>
    public partial class Navigates : UserControl, INotifyPropertyChanged
    {
        public Navigates()
        {
            InitializeComponent();
            
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        Point startPoint;
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Mouse.Capture(this.joystickN.KnobBase);
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
                if (Math.Sqrt(x * x + y * y) < (internalBase.Width / 2))
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                    Rudder = x / (internalBase.Width / 2.0);
                    Elevator = y / (internalBase.Width / 2.0);
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
