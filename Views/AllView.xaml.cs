using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlightSimulator.Views
{
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
