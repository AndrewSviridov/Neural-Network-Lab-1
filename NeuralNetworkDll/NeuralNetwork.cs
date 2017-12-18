using System;
using System.Threading;

namespace NeuralNetworkDll
{
    public sealed class NeuralNetwork
    {
        // Персептрон
        internal Perceptron Perceptron { get; }

        // Обучена ли наша модель
        public bool Trained { get; private set; }

        /// <param name="weights">Первоначальные веса</param>
        /// <param name="threshold">Пороговое значение</param>
        /// <param name="learningSpeed">Скорость обучения</param>
        public NeuralNetwork(double[] weights, double threshold, double learningSpeed = 1)
        {
            this.Perceptron = new Perceptron(weights, threshold, learningSpeed);
        }

        public void ResetWeights(double[] weights)
        {
            this.Perceptron.ResetWeights(weights);
        }

        public void ResetLearningSpeed(double learningSpeed)
        {
            Perceptron?.ResetLearningSpeed(learningSpeed);
        }

        /// <summary>
        /// Обучаем персептрон
        /// </summary>
        /// <param name="values">Данные</param>
        /// <param name="expected">Ожидаемые значения</param>
        /// <param name="breakCycle">Проверяем на бесконечный цикл</param>
        /// <param name="token">Токен на отмену</param>
        public void Train(int[][] values, bool[] expected, bool breakCycle = true, CancellationToken token = default(CancellationToken))
        {
            // Обнуляем обучение
            this.Trained = false;
            // Предыдущая дельта
            double prevDelta = 0;
            // Вечный цикл
            while (!token.IsCancellationRequested)
            {
                // Флаг проверки на ошибку
                bool error = false;
                // Текущая дельта
                double delta = 0;

                // Проходим по всем данным
                for (int i = 0; i < values.Length; i++)
                {
                    // Получаем результат от персептрона
                    bool result = this.Perceptron.ActivateBool(values[i]);
                    // Если он не совпадает с ожидаемым
                    if (result != expected[i])
                    {
                        // Ошибка!
                        error = true;
                        // Тренируем персептрон
                        this.Perceptron.Train(values[i]);
                        delta += this.Perceptron.GetDelta();
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
                    if (Math.Abs(delta - prevDelta) <= 0.001 || double.IsNaN(delta))
                    {
                        // Выходим из цикла
                        return;
                    }
                    prevDelta = delta;
                }
            }

            // Наша модель обучена
            this.Trained = true;
        }

        /// <summary>
        /// Тестируем персептрон
        /// </summary>
        /// <param name="values">Данные</param>
        /// <returns>Результат</returns>
        public bool Test(int[] values)
        {
            return this.Perceptron.ActivateBool(values);
        }
    }
}
