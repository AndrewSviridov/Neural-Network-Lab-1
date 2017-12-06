using System.Windows;
using System.Linq;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;

namespace NeuralNetworkLab1WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Панель обучения
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
