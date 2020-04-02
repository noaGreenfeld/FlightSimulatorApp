using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FlightSimulator.Model;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        IModelVariable m;

        public HomePage(IModelVariable model)
        {
            InitializeComponent();
            m = model;
        }

        private void Button_Click_Fly(object sender, RoutedEventArgs e)
        {
            if (ServerIP.Text == "")
            {
                ServerIP.Text = "127.0.0.1";
            }
            if (ServerPort.Text == "")
            {
                ServerPort.Text = "5402";
            }
            try
            {
                SimulatorView simulatorView = new SimulatorView(ServerIP.Text, ServerPort.Text);
                this.NavigationService.Navigate(simulatorView);
            }
            catch(Exception)
            {
                text.Text = "ip and port wrong - try again";
            }
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ServerIP.Text == "")
            {
                ServerIP.Text = "127.0.0.2";
            }
            if (ServerPort.Text == "")
            {
                ServerPort.Text = "5402";
            }
        }
    }
}