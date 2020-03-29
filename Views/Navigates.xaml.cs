using System;
using System.Collections.Generic;
using System.Text;
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
    public partial class Navigates : UserControl
    {
        public Navigates()
        {
            InitializeComponent();
            joystickN.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    string who = e.PropertyName;
                    // Console.WriteLine(who + " simulator view");
                    switch (who)
                    {
                        case "X":
                            Rudder = joystickN.X / (joystickN.internalBase.Width / 2.0);
                            break;
                        case "Y":
                            Elevator = joystickN.Y / (joystickN.internalBase.Width / 2.0);
                            break;
                    }
                };
                }
        private double rudder;
        public double Rudder
        {
            get { return rudder; }
            set
            {
                //Console.WriteLine(value+"  set");
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        private double elevator;
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;

                NotifyPropertyChanged("Elevator");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}