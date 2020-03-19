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
    /// Interaction logic for DataBoard1.xaml
    /// </summary>
    public partial class DataBoard : UserControl
    {
        public DataBoard()
        {
            InitializeComponent();
        }

        private void dataContextChanged_d1(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("dataContextChanged_d1");
        }
    }
}
