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

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Controls.xaml
    /// </summary>
    public partial class Controls : UserControl
    {
        string ip;
        int port;
       // bool disconect = false;

        public Controls()
        {
            InitializeComponent();
        }
        
        public void getIpPort(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (Application.Current as App).vm_control.connect(ip, port);
                disconnect.IsEnabled = true;
                (Application.Current as App).simulatorView.Show();
                (Application.Current as App).MainWindow.Hide();
            }
            catch { }
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).MainWindow.Show();
            (Application.Current as App).vm_control.disconnect();
            disconnect.IsEnabled = false;
            (Application.Current as App).simulatorView.Close();
        }

    }
}
