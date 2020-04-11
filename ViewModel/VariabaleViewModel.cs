using System;
using FlightSimulator.Model;
using System.ComponentModel;

namespace FlightSimulator.ViewModel
{
    public abstract class VariabaleViewModel : INotifyPropertyChanged
    {
      protected IModelVariable model;
        public VariabaleViewModel(IModelVariable model)
        {
            this.model = model;
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
