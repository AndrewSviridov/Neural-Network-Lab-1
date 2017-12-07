using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NeuralNetworkDll;

namespace NeuralNetworkLab1WPF
{
    public sealed class NeuralNetworkViewModel : INotifyPropertyChanged
    {
        private const double Threshold = 0.7;

        private double _learningSpeed = 0.1;

        public double LearningSpeed
        {
            get { return this._learningSpeed; }
            set
            {
                this._learningSpeed = value;
                this.OnPropertyChanged("LearningSpeed");
            }
        }

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

        public void Train(int[][] values, bool[] expected, bool breakCycle = true,
            CancellationToken token = default(CancellationToken))
        {
            this.NeuralNetwork.Train(values, expected, breakCycle, token);
        }

        public void ResetLearningSpeed()
        {
            this.NeuralNetwork.ResetLearningSpeed(this.LearningSpeed);
        }

        public bool Trained => this.NeuralNetwork.Trained;

        public bool Test(int[] values)
        {
            return this.NeuralNetwork.Test(values);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class MainWindow
    {
        private void TeachButtonClick(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                var values = new List<int[]>();
                var expected = new List<bool>();
                foreach (var model in ResourcesHelper.ModelPanel.Models)
                {
                    values.Add(model);
                    expected.Add(model.Result);
                }
                ResourcesHelper.NeuralNetwork.ResetLearningSpeed();
                ResourcesHelper.NeuralNetwork.Train(values.ToArray(), expected.ToArray());
                MessageBox.Show("Модель обучена!", "Обучение", MessageBoxButton.OK);
            });
        }
    }
}
