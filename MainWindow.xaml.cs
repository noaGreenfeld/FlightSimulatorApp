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
using FlightSimulator.Views;
using FlightSimulator.Model;
using FlightSimulator.ViewModel;
using FlightSimulator.other;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        VariabaleViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            string s = "127.0.0.1";
            vm = new VariabaleViewModel(new MyModelVariable(s, 5402));
            DataContext = vm;
            this.navigates.joystickN.PropertyChanged += 
                delegate (Object sender, PropertyChangedEventArgs e)
            {
                Console.WriteLine(e.getS() + "  delegate main window");
                string who = e.getS();
                switch (who)
                {
                    case "Rudder":
                        Console.WriteLine("switch main");
                        vm.notifyViewChange(this.navigates.joystickN.getRudder(), who);
                        break;
                }
                
            };


        }




        private void Navigates_Loaded(object sender, RoutedEventArgs e)
        {

           
        }
    }
}
