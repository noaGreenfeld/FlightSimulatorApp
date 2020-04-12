using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Navigates.xaml
    /// This view lets the user to change navigation components:
    /// Rudder, Elevator, Aileron and Throttle.
    /// </summary>
    public partial class Navigates : UserControl
    {
        public Navigates()
        {
            InitializeComponent();
        }

        private void joystickN_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Normalize the joystick movement (between -1 and 1)
                joystick_X = (joystickN.knobPosition.X / (joystickN.internalBase.Width / 2));
                joystick_Y = (joystickN.knobPosition.Y / (joystickN.internalBase.Width / 2));
                // Display movement values
                rudderValue.Text = Math.Round(joystick_X, 6).ToString();
                elevatorValue.Text = Math.Round(joystick_Y, 6).ToString();
                (Application.Current as App).vm_navigates.VM_Rudder = joystick_X;
                (Application.Current as App).vm_navigates.VM_Elevator = joystick_Y;
            }
        }

        private double joystick_x;
        public double joystick_X
        {
            get { return joystick_x; }
            set
            {
                if (joystick_x != value)
                {
                    joystick_x = value;
                }
            }
        }

        private double joystick_y;
        public double joystick_Y
        {
            get { return joystick_y; }
            set
            {
                if (joystick_y != value)
                {
                    joystick_y = value;
                }
            }
        }

        private void joystickN_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Return to the center point
            rudderValue.Text = "0";
            elevatorValue.Text = "0";
            (Application.Current as App).vm_navigates.VM_Rudder = 0;
            (Application.Current as App).vm_navigates.VM_Elevator = 0;
        }

        // Initialize sliders:
        private void s2_Loaded(object sender, RoutedEventArgs e)
        {
            s2.Value = 0;
        }

        private void s1_Loaded(object sender, RoutedEventArgs e)
        {
            s1.Value = 0;
        }
    }
}