using System;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace FlightSimulator.Views
{
    public partial class Navigates : UserControl
    {
        public Navigates()
        {
            InitializeComponent();
            /*joystickN.PropertyChanged +=
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
                };*/
        }
        /*
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
        }*/

        private void joystickN_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                joystick_X = (joystickN.knobPosition.X / (joystickN.internalBase.Width / 2));
                joystick_Y = (joystickN.knobPosition.Y / (joystickN.internalBase.Width / 2));
                rudderValue.Text = joystick_X.ToString();
                elevatorValue.Text = joystick_Y.ToString();
                (Application.Current as App).vm_navigates.VM_Rudder = joystick_X;
                (Application.Current as App).vm_navigates.VM_Elevator = joystick_Y;
            }/*
            else
            {
                joystick_X = 0;
                joystick_Y = 0;
                rudderValue.Text = joystick_X.ToString();
                elevatorValue.Text = joystick_Y.ToString();
                (Application.Current as App).vm_navigates.VM_Rudder = joystick_X;
                (Application.Current as App).vm_navigates.VM_Elevator = joystick_Y;
            }*/
        }

        private double joystick_x;
        public double joystick_X
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
        public double joystick_Y
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
    }
}