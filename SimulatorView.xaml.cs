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
        VariabaleViewModel vm;
        public SimulatorView(string ip, string port)
        {
            
            //string s = "127.0.0.1";
            int portI = Int32.Parse(port);
            vm = new VariabaleViewModel(new MyModelVariable(ip, portI));
            DataContext = vm;
            InitializeComponent();
            this.allView.navigates.joystickN.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    string who = e.PropertyName;
                    switch (who)
                    {
                        case "Rudder":
                            vm.notifyViewChange(this.allView.navigates.joystickN.Rudder, who);
                            break;
                        case "Elevator":
                            vm.notifyViewChange(this.allView.navigates.joystickN.Elevator, who);
                            break;
                    }
                };
        }


    }
}