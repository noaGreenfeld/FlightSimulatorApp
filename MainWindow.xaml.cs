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
                this.Hide();
                simulatorView.ShowDialog();
            }
            catch (Exception)
            {
                text.Text = "Try again";
                ServerIP.Text = "127.0.0.1";
                ServerPort.Text = "5402";
            }
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ServerIP.Text == "")
            {
                ServerIP.Text = "127.0.0.1";
            }
            if (ServerPort.Text == "")
            {
                ServerPort.Text = "5402";
            }
        }
    }
}


