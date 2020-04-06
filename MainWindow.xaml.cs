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
        public MainWindow() 
        {
            InitializeComponent();
            ServerIP.Text = deafultIp;
            ServerPort.Text = deafultPort;
        }

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
                SimulatorView simulatorView = new SimulatorView(ServerIP.Text, ServerPort.Text);
                this.Hide();
                simulatorView.ShowDialog();
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
    }
}


