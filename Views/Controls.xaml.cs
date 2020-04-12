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
    /// This view gives the user a control over the program- connecting/disconnecting
    /// and seeing error messages sent from the model
    /// </summary>
    public partial class Controls : UserControl
    {
        string ip;
        int port;

        public Controls()
        {
            InitializeComponent();
            connect.IsEnabled = false;
        }
        
        public void getIpPort(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }
       
        private void connect_Click(object sender, RoutedEventArgs e)
        {
            // Close the window and display the home page (main window) to let the user connect
            disconnect.IsEnabled = true;
            (Application.Current as App).simulatorView.Close();
            (Application.Current as App).MainWindow.Show();
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            // Disconnect from the server
            (Application.Current as App).vm_control.disconnect();
            disconnect.IsEnabled = false;
            connect.IsEnabled = true;
        }

        private void disconnect_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Change availability of the connect button once the user has disconnected
            connect.IsEnabled = !connect.IsEnabled;
        }
    }
}
