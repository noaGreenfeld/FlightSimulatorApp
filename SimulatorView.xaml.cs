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

        // VariabaleViewModel vm;
        public SimulatorView(string ip, string port)
        {
            InitializeComponent();
            int portI = Int32.Parse(port);
            this.ip = ip;
            this.port = portI;

            //vm = new VariabaleViewModel(new MyModelVariable(ip, portI));
            model = new MyModelVariable(ip, portI);

            vm_databoard = new VM_dataBoard(model);
            vm_map = new VM_map(model);
            vm_navigates = new VM_navigates(model);
            vm_control = new VM_control(model);
            //DataContext = vm_databoard;
            
            this.allView.navigates.joystickN.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    string who = e.PropertyName;
                    Console.WriteLine(who + " simulator view");
                    switch (who)
                    {
                        case "Rudder":
                            vm_navigates.notifyViewChange(this.allView.navigates.joystickN.Rudder, who);
                            break;
                        case "Elevator":
                            vm_navigates.notifyViewChange(this.allView.navigates.joystickN.Elevator, who);
                            break;
                    }
                };

            this.allView.navigates.DataContext = vm_navigates;
            this.allView.dataBoard.DataContext = vm_databoard;
            this.allView.myMap.DataContext = vm_map;
            this.allView.errorBox.DataContext = vm_control;
        }

        private void allView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            vm_control.connect(ip, port);
        }

        private void connect_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (vm_control.isConnected())
            {
                IsEnabled = false;
            } else
            {
                IsEnabled = true;
            }
        }
    }
}