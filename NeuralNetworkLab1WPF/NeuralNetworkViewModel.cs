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
        private readonly double[] Weights = Enumerable.Repeat(0.5, 15).ToArray();
        private const double Threshold = 0.7;

        public NeuralNetwork NeuralNetwork { get; }

        public NeuralNetworkViewModel()
        {
            this.NeuralNetwork = new NeuralNetwork(this.Weights, Threshold);
        }
    }
}
