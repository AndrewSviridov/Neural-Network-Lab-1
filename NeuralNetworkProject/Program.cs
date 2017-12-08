using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkDll;

namespace NeuralNetworkProject
{
    class Program
    {

        #region Numbers

        private static readonly int[] One =
        {
            0, 0, 1,
            0, 1, 1,
            1, 0, 1,
            0, 0, 1,
            0, 0, 1
        };

        private static readonly int[] Two =
        {
            1, 1, 1,
            0, 0, 1,
            1, 1, 1,
            1, 0, 0,
            1, 1, 1
        };

        private static readonly int[] Three =
        {
            1, 1, 1,
            0, 0, 1,
            1, 1, 1,
            0, 0, 1,
            1, 1, 1
        };

        private static readonly int[] Four =
        {
            1, 0, 1,
            1, 0, 1,
            1, 1, 1,
            0, 0, 1,
            0, 0, 1
        };

        private static readonly int[] Five =
        {
            1, 1, 1,
            1, 0, 0,
            1, 1, 1,
            0, 0, 1,
            1, 1, 1
        };

        private static readonly int[] Six =
        {
            1, 1, 1,
            1, 0, 0,
            1, 1, 1,
            1, 0, 1,
            1, 1, 1
        };

        private static readonly int[] Seven =
        {
            1, 1, 1,
            0, 0, 1,
            0, 0, 1,
            0, 0, 1,
            0, 0, 1
        };

        private static readonly int[] Eight =
        {
            1, 1, 1,
            1, 0, 1,
            1, 1, 1,
            1, 0, 1,
            1, 1, 1
        };

        private static readonly int[] Nine =
        {
            1, 1, 1,
            1, 0, 1,
            1, 1, 1,
            0, 0, 1,
            1, 1, 1
        };

        private static readonly int[] Zero =
        {
            1, 1, 1,
            1, 0, 1,
            1, 0, 1,
            1, 0, 1,
            1, 1, 1
        };

        #endregion

        private const double LearningSpeed = 0.1;
        private const double PerceptronThreshold = 0.7;
        private const double NetworkThreshold = 0.6;

        static void Main(string[] args)
        {
            List<double> idealWeights;
            var random = new Random();
            while (true)
            {
                var weightsList = new List<double>();
                for (int i = 0; i < 15; i++)
                {
                    weightsList.Add(random.NextDouble() * 2 - 1);
                }
                double[] weights = weightsList.ToArray();

                bool[] expected;

                List<Category> categories = new List<Category>();

                int[][] trainNumbers =
                {
                    Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine
                };

                for (int i = 0; i < 10; i++)
                {
                    NeuralNetwork nn = new NeuralNetwork(weights, PerceptronThreshold, LearningSpeed);
                    expected = new bool[10];
                    expected[i] = true;
                    nn.Train(trainNumbers, expected);
                    Category category = new Category(i.ToString(), i, nn);
                    categories.Add(category);
                }

                List<bool> results = new List<bool>();

                PerceptronNeuralNetwork pnn = new PerceptronNeuralNetwork(categories, NetworkThreshold);
                for (int i = 0; i < 10; i++)
                {
                    Category result = pnn.Test(trainNumbers[i]);
                    results.Add(result != null && result.Value == i);
                }

                if (!results.Contains(false))
                {
                    idealWeights = weightsList;
                    break;
                }
            }
        }
    }
}
