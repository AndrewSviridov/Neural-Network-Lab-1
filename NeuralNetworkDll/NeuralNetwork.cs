using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkDll
{
    public class NeuralNetwork
    {
        private readonly Perceptron _perceptron;

        /// <param name="weights">Первоначальные веса</param>
        /// <param name="threshold">Пороговое значение</param>
        public NeuralNetwork(double[] weights, double threshold)
        {
            this._perceptron = new Perceptron(weights, threshold);
        }

        /// <summary>
        /// Обучаем персептрон
        /// </summary>
        /// <param name="values">Данные</param>
        /// <param name="expected">Ожидаемые значения</param>
        /// <param name="breakCycle">Проверяем на бесконечный цикл</param>
        public void Train(int[][] values, bool[] expected, bool breakCycle = true)
        {
            // Предыдущая дельта
            double prevDelta = 0;
            // Вечный цикл
            while (true)
            {
                // Флаг проверки на ошибку
                bool error = false;
                // Текущая дельта
                double delta = 0;

                // Проходим по всем данным
                for (int i = 0; i < values.Length; i++)
                {
                    // Получаем результат от персептрона
                    bool result = this._perceptron.Activate(values[i]);
                    // Если он не совпадает с ожидаемым
                    if (result != expected[i])
                    {
                        // Ошибка!
                        error = true;
                        // Тренируем персептрон
                        this._perceptron.Train(values[i]);
                        delta += this._perceptron.GetDelta();
                    }
                }

                // Если нет ошибок
                if (!error)
                {
                    // Выходим из цикла
                    break;
                }

                // Проверка на вечный цикл
                if (breakCycle)
                {
                    // Находим среднюю дельту
                    delta /= values.Length;
                    // Если изменение дельты меньше порога
                    if (Math.Abs(delta - prevDelta) <= 0.001)
                    {
                        // Выходим из цикла
                        break;
                    }
                    prevDelta = delta;
                }
            }
        }

        /// <summary>
        /// Тестируем персептрон
        /// </summary>
        /// <param name="values">Данные</param>
        /// <returns>Результат</returns>
        public bool Test(int[] values)
        {
            return this._perceptron.Activate(values);
        }
    }
}
