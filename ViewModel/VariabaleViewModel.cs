using System;
using FlightSimulator.Model;
using System.ComponentModel;

namespace FlightSimulator.ViewModel
{
    //abstract class for the all view model class
    public abstract class VariabaleViewModel : INotifyPropertyChanged
    {
        //only one model that all the diffrent VM will have.
      protected IModelVariable model;
        public VariabaleViewModel(IModelVariable model)
        {
            //intilije the model
            this.model = model;
            //notify the checged.
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
