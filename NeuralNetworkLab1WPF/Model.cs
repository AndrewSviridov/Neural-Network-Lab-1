using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace NeuralNetworkLab1WPF
{
    public sealed class Model : INotifyPropertyChanged
    {
        public List<ObservableItem<Brush>> Colors { get; }

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

        private bool _result;
        public bool Result
        {
            get { return this._result; }
            set
            {
                this._result = value;
                this.RaisePropertyChanged("Result");
            }
        }

        public void Clear()
        {
            for (int i = 0; i < this.Colors.Count; i++)
            {
               this.Colors[i].Value = Brushes.White;
            }
        }

        public static implicit operator int[](Model model)
        {
            return model.Colors.Select(x => x.Value.Equals(Brushes.Black) ? 1 : 0).ToArray();
        }

        public Model(string name, bool instantiateColors)
        {
            this.Name = name;
            this.Colors = new List<ObservableItem<Brush>>();
            if (instantiateColors)
            {
                for (int i = 0; i < 15; i++)
                {
                    this.Colors.Add(new ObservableItem<Brush>(Brushes.White));
                }
            }
            
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
