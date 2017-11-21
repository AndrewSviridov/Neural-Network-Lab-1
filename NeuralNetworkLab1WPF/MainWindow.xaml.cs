using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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

namespace NeuralNetworkLab1WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<SolidColorBrush> LearningCurrentRectangleCollors { get; } = 
            new ObservableCollection<SolidColorBrush>(Enumerable.Repeat(Brushes.White, 15));

        private readonly Dictionary<string, List<SolidColorBrush>> _learningRectangleCollors =
            new Dictionary<string, List<SolidColorBrush>>();

        public MainWindow()
        {
            InitializeComponent();
            this.InitializeLearningData();
            this.DataContext = this;
        }

        private void RectangleMouseClick(object sender, MouseButtonEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle == null)
            {
                return;
            }
            int rectanglePos;
            if (int.TryParse(rectangle.Tag as string, out rectanglePos))
            {
                this.ChangeRectangleColor(rectanglePos);
            }
        }

        private void ChangeRectangleColor(int position)
        {
            var color = this.LearningCurrentRectangleCollors[position];
            if (color.Equals(Brushes.Black))
            {
                this.LearningCurrentRectangleCollors[position] = Brushes.White;
            }
            else
            {
                this.LearningCurrentRectangleCollors[position] = Brushes.Black;
            }
        }

        private void ResetRectangleColor(string oldModel, string newModel)
        {
            for (int i = 0; i < this.LearningCurrentRectangleCollors.Count; i++)
            {
                this._learningRectangleCollors[oldModel][i] = this.LearningCurrentRectangleCollors[i];
            }

            List<SolidColorBrush> colors;
            this.GetOrAddCollorsToDictionary(newModel, out colors);

            for (int i = 0; i < colors.Count; i++)
            {
                this.LearningCurrentRectangleCollors[i] = colors[i];
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1 && e.RemovedItems.Count == 1)
            {
                var selectedItem = e.AddedItems[0] as ListBoxItem;
                var selectedItemLabel = selectedItem.Content as Label;
                var deselectedItem = e.RemovedItems[0] as ListBoxItem;
                var deselectedItemLabel = deselectedItem.Content as Label;
                string oldModel = deselectedItemLabel.Content.ToString();
                string newModel = selectedItemLabel.Content.ToString();
                this.ResetRectangleColor(oldModel, newModel);
                this.TeachingTextboxModelName.Text = newModel;
            }
        }

        private void InitializeLearningData()
        {
            List<SolidColorBrush> temp;
            this.GetOrAddCollorsToDictionary("Модель 1", out temp);
            this.GetOrAddCollorsToDictionary("Модель 2", out temp);
            this.GetOrAddCollorsToDictionary("Модель 3", out temp);
        }

        private void GetOrAddCollorsToDictionary(string model, out List<SolidColorBrush> colors)
        {
            if (!this._learningRectangleCollors.TryGetValue(model, out colors))
            {
                colors = new List<SolidColorBrush>(15);
                colors.AddRange(Enumerable.Repeat(Brushes.White, 15));
                this._learningRectangleCollors[model] = colors;
            }
        }
    }
}
