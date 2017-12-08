using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NeuralNetworkDll;

namespace NeuralNetworkProjectWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

        private PerceptronNeuralNetwork _neuralNetwork;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeNeuralNetwork()
        {
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
                    this._neuralNetwork = pnn;
                    break;
                }
            }
        }

        private void Test()
        {
            var colors = ResourcesHelper.ModelPanel.Colors;
            Category result =
                this._neuralNetwork.Test(colors.Select(x => x.Value.Equals(Brushes.Black) ? 1 : 0).ToArray());
            string resultText;
            if (result == null)
            {
                resultText = "Это не цифра";
            }
            else
            {
                resultText = $"Это цифра {result.Value}";
            }

            MessageBox.Show(resultText);
        }

        private void RectangleMouseClick(object sender, MouseButtonEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle == null)
            {
                return;
            }

            rectangle.Fill = rectangle.Fill.Equals(Brushes.White) ? Brushes.Black : Brushes.White;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.InitializeNeuralNetwork();
            MessageBox.Show("Нейронная сеть настроена!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_neuralNetwork == null)
            {
                MessageBox.Show("Нейронная сеть не настроена!");
                return;
            }

            this.Test();
        }
    }
}
