using System;
using System.Windows;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;
using FlightSimulatorApp;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public VM_map Vm_map { get; internal set; }
        public VM_dataBoard Vm_dataBoard { get; internal set; }
        public VM_navigates Vm_navigates { get; internal set; }
        public VM_control Vm_control { get; internal set; }
        public IModelVariable model;
        public SimulatorView simulatorView;

        void App_Startup(object sender, StartupEventArgs e)
        {
            // Initialize model and view models and start the program by showing the main window
            model = new MyModelVariable();
            Vm_navigates = new VM_navigates(model);
            Vm_dataBoard = new VM_dataBoard(model);
            Vm_map = new VM_map(model);
            Vm_control = new VM_control(model);
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
