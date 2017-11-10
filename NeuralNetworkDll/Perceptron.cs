using System;

namespace NeuralNetworkDll
{
    internal class Perceptron
    {
        private readonly double _threshold;
        private readonly double[] _weights;
        private double _delta;

        /// <param name="weights">Первоначальные весы</param>
        /// <param name="threshold">Порог</param>
        public Perceptron(double[] weights, double threshold)
        {
            this._weights = weights;
            this._threshold = threshold;
        }

        /// <summary>
        /// Даем данные персептрону и получаем от него результат
        /// </summary>
        /// <param name="values">Данные</param>
        /// <returns>Результат</returns>
        public bool Activate(int[] values)
        {
            // Проверяем, совпадают ли длинна массива данных и длинна массива весов
            if (values.Length != this._weights.Length)
            {
                throw new ArgumentException("Размер данных и размер весов не совпадают!");
            }

            double sum = 0;
            // Считаем сумму
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i] * this._weights[i];
            }
            // Считаем дельту
            this._delta = this._threshold - sum;

            return sum >= this._threshold;
        }

        /// <summary>
        /// Обучаем персептрон
        /// </summary>
        /// <param name="values">Данные</param>
        public void Train(int[] values)
        {
            // Проходим по всем значениям
            for (int i = 0; i < values.Length; i++)
            {
                // Если значение активно
                if (values[i] == 1)
                {
                    // Меняем его вес
                    this._weights[i] += this._delta;
                }
            }
        }

        public double GetDelta() => this._delta;

    }
}
