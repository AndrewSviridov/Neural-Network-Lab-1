using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkDll
{
    public sealed class PerceptronNeuralNetwork
    {
        private readonly List<Category> _categories;
        private readonly double _threshold;

        public PerceptronNeuralNetwork(List<Category> categories, double threshold)
        {
            this._categories = categories;
            this._threshold = threshold;
        }

        public Category Test(int[] values)
        {
            if (this._categories.Count == 0)
            {
                return null;
            }

            Category maxCat = null;
            double maxVal = this._threshold;

            for (int i = 0; i < this._categories.Count; i++)
            {
                double testVal = this._categories[i].Test(values);
                if (testVal >= maxVal)
                {
                    maxCat = this._categories[i];
                    maxVal = testVal;
                }
            }

            return maxCat;
        }
    }

    public class Category
    {
        public string Name { get; }
        public int Value { get; }
        private readonly Perceptron _perceptron;

        public Category(string name, int value, NeuralNetwork nn)
        {
            this.Name = name;
            this.Value = value;
            this._perceptron = nn.Perceptron;
        }

        public double Test(int[] values)
        {
            return this._perceptron.ActivateDouble(values);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
