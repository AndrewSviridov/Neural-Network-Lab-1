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
        public LearningPanelData LearnPanel { get; }
        public MainWindow()
        {
            this.LearnPanel = new LearningPanelData(this);
            InitializeComponent();
            this.DataContext = this;
            this.LearnPanel.Initialize();
        }
    }
}
