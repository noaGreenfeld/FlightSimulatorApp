using System;
using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Views;
using FlightSimulator.Model;
using System.ComponentModel;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for SimulatorView.xaml
    /// </summary>
    public partial class SimulatorView : Page
    {
        string ip;
        int port;
        IModelVariable model;
        VM_dataBoard vm_databoard;
        VM_map vm_map;
        VM_navigates vm_navigates;
        VM_control vm_control;

        public SimulatorView(string ip, string port)
        {
            InitializeComponent();
            int portI = Int32.Parse(port);
            this.ip = ip;
            this.port = portI;
            model = new MyModelVariable(ip, portI);
            vm_databoard = new VM_dataBoard(model);
            vm_map = new VM_map(model);
            vm_navigates = new VM_navigates(model);
            vm_control = new VM_control(model);
            
            navigates.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    string who = e.PropertyName;
                    Console.WriteLine(who + " simulator view");
                    switch (who)
                    {
                        case "Rudder":
                            vm_navigates.notifyViewChange(this.navigates.Rudder, who);
                            break;
                        case "Elevator":
                            vm_navigates.notifyViewChange(this.navigates.Elevator, who);
                            break;
                    }
                };
            navigates.DataContext = vm_navigates;
            dataBoard.DataContext = vm_databoard;
            myMap.DataContext = vm_map;
            errorBox.DataContext = vm_control;
            DataContext = vm_control;
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm_control.connect(ip, port);
                disconnect.IsEnabled = true;
            } catch {}
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            vm_control.disconnect();
            disconnect.IsEnabled = false;
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