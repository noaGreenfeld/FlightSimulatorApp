﻿using System;
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
        public VM_map vm_map { get; internal set; }
        public VM_dataBoard vm_dataBoard { get; internal set; }
        public VM_navigates vm_navigates { get; internal set; }
        public VM_control vm_control { get; internal set; }
        public IModelVariable model;
        public SimulatorView simulatorView;

        void App_Startup(object sender, StartupEventArgs e)
        {
            // Initialize model and view models and start the program by showing the main window
            model = new MyModelVariable();
            vm_navigates = new VM_navigates(model);
            vm_dataBoard = new VM_dataBoard(model);
            vm_map = new VM_map(model);
            vm_control = new VM_control(model);
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
