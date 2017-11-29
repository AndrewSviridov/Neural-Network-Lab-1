using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace NeuralNetworkLab1WPF
{
    public sealed class Model : INotifyPropertyChanged
    {
        public ObservableCollection<SolidColorBrush> Colors { get; }

        private string _name;
        public string Name
        {
            get { return this._name; }
            set
            {
                this._name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        public Model(string name)
        {
            this.Name = name;
            Colors = new ObservableCollection<SolidColorBrush>(Enumerable.Repeat(Brushes.White, 15));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
