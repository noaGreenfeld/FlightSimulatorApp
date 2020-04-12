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
            int portI = Int32.Parse(port);
            //conect the all view data context into the correct view model.
            control.DataContext = (Application.Current as App).vm_control;
            control.getIpPort(ip, portI);
            (Application.Current as App).vm_control.connect(ip, portI);
            navigates.DataContext = (Application.Current as App).vm_navigates;
            dataBoard.DataContext = (Application.Current as App).vm_dataBoard;
            myMap.DataContext = (Application.Current as App).vm_map;
        }

        private void navigates_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }

        private void navigates_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }
    }
}