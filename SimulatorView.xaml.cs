using System;
using System.Windows;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for SimulatorView.xaml
    /// </summary>
    public partial class SimulatorView : Window
    {
        public SimulatorView(string ip, string port)
        {
            InitializeComponent();
            
            // Set all views data context to the appropriate view models and connect to server
            control.DataContext = (Application.Current as App).Vm_control;
            navigates.DataContext = (Application.Current as App).Vm_navigates;
            dataBoard.DataContext = (Application.Current as App).Vm_dataBoard;
            myMap.DataContext = (Application.Current as App).Vm_map;
            int portI = Int32.Parse(port);
            (Application.Current as App).Vm_control.Connect(ip, portI);
        }

        private void Navigates_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }

        private void Navigates_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }
    }
}