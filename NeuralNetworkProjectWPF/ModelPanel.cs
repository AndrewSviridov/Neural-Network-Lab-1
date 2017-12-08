using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NeuralNetworkProjectWPF
{
    public sealed class ModelPanel
    {
        public ObservableCollection<ObservableItem<Brush>> Colors { get; }

        public ModelPanel()
        {
            this.Colors = new ObservableCollection<ObservableItem<Brush>>();
            for (int i = 0; i < 15; i++)
            {
                this.Colors.Add(new ObservableItem<Brush>(Brushes.White));
            }
        }
    }
}
