using System.ComponentModel;

namespace NeuralNetworkLab1WPF
{
    public class ObservableItem<T> : INotifyPropertyChanged
    {
        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                this._value = value;
                this.OnPropertyChanged("Value");
            }
        }

        public ObservableItem(T value)
        {
            this.Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
