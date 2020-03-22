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

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {

        public HomePage()
        {
            InitializeComponent();
        }

        private void Button_Click_Fly(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(ServerPort.Text);

            if (ServerIP.Text == "")
            {
                ServerIP.Text = "127.0.0.1";
            }
            if (ServerPort.Text == "")
            {
                ServerPort.Text = "5402";
            }
            SimulatorView simulatorView = new SimulatorView(ServerIP.Text, ServerPort.Text);
            this.NavigationService.Navigate(simulatorView);
        }

        private void ServerPort_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int il;
        }

        private void ServerPort_MouseEnter(object sender, MouseEventArgs e)
        {

        }

    }
}
