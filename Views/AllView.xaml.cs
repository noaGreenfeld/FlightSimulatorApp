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
    /// Interaction logic for AllView.xaml
    /// </summary>
    public partial class AllView : UserControl
    {
        public AllView()
        {
            InitializeComponent();
        }

        private void navigates_MouseUp(object sender, MouseButtonEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }

        private void navigates_MouseLeave(object sender, MouseEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }
    }
}
