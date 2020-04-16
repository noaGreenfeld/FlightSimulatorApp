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

        private void JoystickN_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Normalize the joystick movement (between -1 and 1)
                Joystick_X = (joystickN.knobPosition.X / (joystickN.internalBase.Width / 2));
                Joystick_Y = (joystickN.knobPosition.Y / (joystickN.internalBase.Width / 2));
                // Display movement values
                rudderValue.Text = Math.Round(Joystick_X, 6).ToString();
                elevatorValue.Text = Math.Round(Joystick_Y, 6).ToString();
                (Application.Current as App).Vm_navigates.VM_Rudder = Joystick_X;
                (Application.Current as App).Vm_navigates.VM_Elevator = Joystick_Y;
            }
        }

        private double joystick_x;
        public double Joystick_X
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
        public double Joystick_Y
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

        private void JoystickN_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Return to the center point
            rudderValue.Text = "0";
            elevatorValue.Text = "0";
            (Application.Current as App).Vm_navigates.VM_Rudder = 0;
            (Application.Current as App).Vm_navigates.VM_Elevator = 0;
        }

        // Initialize sliders:
        private void S2_Loaded(object sender, RoutedEventArgs e)
        {
            S2.Value = 0;
        }

        private void S1_Loaded(object sender, RoutedEventArgs e)
        {
            S1.Value = 0;
        }
    }
}