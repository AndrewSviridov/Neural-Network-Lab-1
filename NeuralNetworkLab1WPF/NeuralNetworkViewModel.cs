using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkDll;

namespace NeuralNetworkLab1WPF
{
    public sealed class NeuralNetworkViewModel
    {
        private const double Threshold = 0.7;

        public NeuralNetwork NeuralNetwork { get; }

        public NeuralNetworkViewModel()
        {
            var weights = new List<double>();
            var random = new Random();
            for (int i = 0; i < 15; i++)
            {
                weights.Add(random.NextDouble() * 2 - 1);
            }
            this.NeuralNetwork = new NeuralNetwork(weights.ToArray(), Threshold, 0.1);
        }
    }
}
