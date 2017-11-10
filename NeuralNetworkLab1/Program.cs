using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkDll;

namespace NeuralNetworkLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем нейронную сеть с первоначальными настройками
            double[] weights = {0.2, 0.2, 0.2, 0.2};
            double threshold = 0.5; 
            NeuralNetwork nn = new NeuralNetwork(weights, threshold);

            #region Обучающие данные

            int[] val1 =
            {
                1, 0,
                0, 0
            };

            bool val1Result = true;

            int[] val2 =
            {
                1, 0,
                1, 0
            };

            bool val2Result = false;

            int[] val3 =
            {
                1, 1,
                1, 0
            };

            bool val3Result = false;

            int[][] values = {val1, val2, val3};
            bool[] expected = {val1Result, val2Result, val3Result};

            #endregion

            // Обучаем
            nn.Train(values, expected, false);

            // Тестируем
            int[] test =
            {
                1, 0,
                0, 1
            };

            bool result = nn.Test(test);
        }
    }
}
