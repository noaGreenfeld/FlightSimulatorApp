﻿using System;
using System.Windows.Controls;
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
                            rudderValue.Text = Math.Round(Rudder, 3).ToString();
                            break;
                        case "Y":
                            Elevator = joystickN.Y / (joystickN.internalBase.Width / 2.0);
                            elevatorValue.Text = Math.Round(Elevator, 3).ToString();
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