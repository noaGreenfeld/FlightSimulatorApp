using System;
using FlightSimulator.Model;
using System.ComponentModel;

namespace FlightSimulator.ViewModel
{
    // Abstract class for the all view model classes
    public abstract class VariabaleViewModel : INotifyPropertyChanged
    {
        // The model that all the diffrent VM's will have:
        protected IModelVariable model;

        public VariabaleViewModel(IModelVariable model)
        {
            // Intialize the model
            this.model = model;
            // Notify the changes that occurred in the model
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_"+e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
