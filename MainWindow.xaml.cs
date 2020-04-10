using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using FlightSimulator;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        //public SimulatorView simulatorView;
        public MainWindow() 
        {
            InitializeComponent();
            ServerIP.Text = deafultIp;
            ServerPort.Text = deafultPort;
        }
       // public SimulatorView simulatorView;
        string deafultPort = ConfigurationManager.AppSettings["port"];
        string deafultIp = ConfigurationManager.AppSettings["ip"];

        private void Button_Click_Fly(object sender, RoutedEventArgs e)
        {
            if (ServerIP.Text == "")
            {
                ServerIP.Text = deafultIp;
            }
            if (ServerPort.Text == "")
            {
                ServerPort.Text = deafultPort;
            }
            try
            {
                (Application.Current as App).simulatorView = new SimulatorView(ServerIP.Text, ServerPort.Text);
                this.Hide();
                (Application.Current as App).simulatorView.ShowDialog();
            }
            catch (Exception)
            {
                text.Text = "Try again";
                ServerIP.Text = deafultIp;
                ServerPort.Text = deafultPort;
            }
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ServerIP.Text == "")
            {
                ServerIP.Text = deafultIp;
            }
            if (ServerPort.Text == "")
            {
                ServerPort.Text = deafultPort;
            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}


