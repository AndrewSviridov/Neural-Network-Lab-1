using System.Windows;

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
        public LearningPanelData LearnPanel { get; } = new LearningPanelData();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
