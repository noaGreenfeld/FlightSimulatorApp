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
using FlightSimulator.other;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Navigates.xaml
    /// </summary>
    public partial class Navigates : UserControl, INotifyPropertyChanged
    {
        public Navigates()
        {
            InitializeComponent();
        }

        public double S1
        {
            get { return s1.Value; }
            set
            {
                Console.WriteLine("S1 set");
                NotifyPropertyChanged("Aileron");
            }
        }

        public double S2
        {
            get { return s2.Value; }
            set
            {
                s2.Value = value;
                NotifyPropertyChanged("Throttle");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void s1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            S1 = e.NewValue;
        }

        private void s2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            S2 = e.NewValue;
        }
    }
}
