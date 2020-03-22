﻿using System;
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
            InitializeComponent();
            string s = "127.0.0.1";
            vm = new VariabaleViewModel(new MyModelVariable(s, 5402));
            DataContext = vm;
            this.allView.navigates.joystickN.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    string who = e.getS();
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

        private void Mouse_Up_Navigate(object sender, MouseButtonEventArgs e)
        {
            allView.navigates.joystickN.knobPosition.X = 0;
            allView.navigates.joystickN.knobPosition.Y = 0;
        }

        private void Mouse_Leave_Navigate(object sender, MouseEventArgs e)
        {
            allView.navigates.joystickN.knobPosition.X = 0;
            allView.navigates.joystickN.knobPosition.Y = 0;
        }


    }
}
