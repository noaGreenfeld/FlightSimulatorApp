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
using FlightSimulator.other;
using FlightSimulator.ViewModel;
using FlightSimulator.Views;
using FlightSimulator.Model;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for SimulatorView.xaml
    /// </summary>
    public partial class SimulatorView : Page
    {
        VariabaleViewModel vm;
        public SimulatorView()
        {
            Console.WriteLine("construsctorrr");
            InitializeComponent();
            string s = "127.0.0.1";
            vm = new VariabaleViewModel(new MyModelVariable(s, 5402));
            DataContext = vm;
            this.navigates.joystickN.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    string who = e.getS();
                    switch (who)
                    {
                        case "Rudder":
                            Console.WriteLine("switch main");
                            vm.notifyViewChange(this.navigates.joystickN.getRudder(), who);
                            break;
                        case "Elevator":
                            vm.notifyViewChange(this.navigates.joystickN.getElevator(), who);
                            break;
                    }

                };
        }

        private void Mouse_Up_Navigate(object sender, MouseButtonEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }

        private void Mouse_Leave_Navigate(object sender, MouseEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }
    }
}

