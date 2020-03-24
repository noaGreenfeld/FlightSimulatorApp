using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        //bool push = false;

        public HomePage()
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
                this.NavigationService.Navigate(simulatorView);
            }
            catch(Exception)
            {
                text.Text = "ip and port wrong - try again";
        {

        }
            }
        }


        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
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