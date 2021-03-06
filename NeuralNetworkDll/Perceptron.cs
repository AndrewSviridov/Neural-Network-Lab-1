﻿using System;

namespace NeuralNetworkDll
{
    internal class Perceptron
    {
        private readonly double _threshold;
        private double _learningSpeed;
        private double[] _weights;
        private double _delta;

        /// <param name="weights">Первоначальные весы</param>
        /// <param name="threshold">Порог</param>
        /// <param name="learningSpeed">Скорость обучения</param>
        public Perceptron(double[] weights, double threshold, double learningSpeed)
        {
            this.ResetWeights(weights);
            this._threshold = threshold;
            this._learningSpeed = learningSpeed;
        }

        /// <summary>
        /// Даем данные персептрону и получаем от него результат
        /// </summary>
        /// <param name="values">Данные</param>
        /// <returns>Результат</returns>
        public bool ActivateBool(int[] values)
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

        public double ActivateDouble(int[] values)
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

            return sum;
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
                    this._weights[i] += this._delta * this._learningSpeed;
                }
            }
        }

        /// <summary>
        /// Дельта
        /// </summary>
        /// <returns>Дельта</returns>
        public double GetDelta() => this._delta;

        public void ResetWeights(double[] weights)
        {
            this._weights = new double[weights.Length];
            weights.CopyTo(this._weights, 0);
        }

        public void ResetLearningSpeed(double learningSpeed)
        {
            this._learningSpeed = learningSpeed;
        }
    }
}
