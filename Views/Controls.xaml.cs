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
        public Controls()
        {
            InitializeComponent();
            connect.IsEnabled = false;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            // Close the window and display the home page (main window) to let the user connect
            disconnect.IsEnabled = true;
            (Application.Current as App).simulatorView.Close();
            (Application.Current as App).MainWindow.Show();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            // Disconnect from the server
            (Application.Current as App).Vm_control.Disconnect();
            disconnect.IsEnabled = false;
            connect.IsEnabled = true;
        }

        private void Disconnect_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Change availability of the connect button once the user has disconnected
            connect.IsEnabled = !connect.IsEnabled;
        }
    }
}
