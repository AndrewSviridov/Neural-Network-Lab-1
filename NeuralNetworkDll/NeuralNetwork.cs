using System;
using System.Threading;

namespace NeuralNetworkDll
{
    public class NeuralNetwork
    {
        // Персептрон
        private readonly Perceptron _perceptron;

        // Обучена ли наша модель
        private bool _trained;
        public bool Trained => _trained;

        /// <param name="weights">Первоначальные веса</param>
        /// <param name="threshold">Пороговое значение</param>
        /// <param name="learningSpeed">Скорость обучения</param>
        public NeuralNetwork(double[] weights, double threshold, double learningSpeed = 1)
        {
            this._perceptron = new Perceptron(weights, threshold, learningSpeed);
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
            this._trained = false;
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

            // Наша модель обучена
            this._trained = true;
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
