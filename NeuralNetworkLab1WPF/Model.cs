using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace NeuralNetworkLab1WPF
{
    public sealed class Model
    {
        public ObservableCollection<SolidColorBrush> Colors { get; }
        public string Name { get; set; }
        public Model(string name)
        {
            this.Name = name;
            Colors = new ObservableCollection<SolidColorBrush>(Enumerable.Repeat(Brushes.White, 15));
        }
    }
}
