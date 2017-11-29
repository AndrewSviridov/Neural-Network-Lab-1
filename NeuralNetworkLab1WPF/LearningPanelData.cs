using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuralNetworkLab1WPF
{
    public sealed class LearningPanelData : INotifyPropertyChanged
    {
        private readonly MainWindow _mainWindow;
        private Model _currentModel;
        public Model CurrentModel
        {
            get { return this._currentModel; }
            set
            {
                this._currentModel = value;
                this.RaisePropertyChanged("CurrentModel");
            }
        }

        private string _currentModelName;
        public string CurrentModelName
        {
            get { return _currentModelName; }
            set
            {
                _currentModelName = value;
                this.RaisePropertyChanged("CurrentModelName");
            }
        }

        private readonly Dictionary<ListBoxItem, Model> _learningModels =
            new Dictionary<ListBoxItem, Model>();       

        /// <summary>
        /// Конструктор
        /// </summary>
        public LearningPanelData(MainWindow parent)
        {
            this._mainWindow = parent;
        }

        public void ChangeModel(ListBoxItem lbi)
        {
            this.CurrentModel = this.GetOrAddModelToDictionary(lbi);
            this.CurrentModelName = this.CurrentModel.Name;
        }

        public void Initialize()
        {
            this._mainWindow.LearningListBox.Items.Clear();
            this.AddModel("Модель 1");
            this.AddModel("Модель 2");
            this.AddModel("Модель 3");
            this._mainWindow.LearningListBox.SelectedIndex = 0;
        }

        public ListBoxItem AddModel(string name)
        {
            var label = new Label
            {
                Width = 80,
                Content = name
            };
            var lbi = new ListBoxItem
            {
                Content = label
            };
            var model = this.GetOrAddModelToDictionary(lbi, name);
            Binding binding = new Binding("Name")
            {
                Source = model
            };
            label.SetBinding(ContentControl.ContentProperty, binding);
            _mainWindow.LearningListBox.Items.Add(lbi);
            return lbi;
        }

        public void RemoveModel(ListBoxItem lbi)
        {
            this._learningModels.Remove(lbi);
        }

        private Model GetOrAddModelToDictionary(ListBoxItem lbi, string name = null)
        {
            if (this._learningModels.TryGetValue(lbi, out Model model))
            {
                return model;
            }

            model = new Model(name ?? "Модель");
            this._learningModels[lbi] = model;
            return model;
        }

        public void ChangeRectangleColor(int position)
        {
            if (this.CurrentModel == null)
            {
                return;
            }
            var color = this.CurrentModel.Colors[position];
            if (color.Equals(Brushes.Black))
            {
                this.CurrentModel.Colors[position] = Brushes.White;
            }
            else
            {
                this.CurrentModel.Colors[position] = Brushes.Black;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public partial class MainWindow
    {
        private void LearnRectangleMouseClick(object sender, MouseButtonEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle == null)
            {
                return;
            }
            int rectanglePos;
            if (int.TryParse(rectangle.Tag as string, out rectanglePos))
            {
                this.LearnPanel.ChangeRectangleColor(rectanglePos);
            }
        }

        private void LearnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 1)
            {
                return;
            }
            var selectedItem = e.AddedItems[0] as ListBoxItem;
            this.LearnPanel.ChangeModel(e.AddedItems[0] as ListBoxItem);
        }

        private void LearnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (this.LearnPanel.CurrentModel == null)
            {
                return;
            }
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
                this.LearnPanel.CurrentModel.Name = this.LearnPanel.CurrentModelName;
            }
        }

        private void AddLearningModelButtonClick(object sender, RoutedEventArgs e)
        {
            ListBoxItem lbi = this.LearnPanel.AddModel("Новая модель");
            this.LearningListBox.SelectedItem = lbi;
        }

        private void RemoveLearningModelButtonClick(object sender, RoutedEventArgs e)
        {
            ListBoxItem lbi = this.LearningListBox.SelectedItem as ListBoxItem;
            if (lbi == null)
            {
                return;
            }

            this.LearningListBox.Items.Remove(lbi);
            if (this.LearningListBox.Items.Count > 0)
            {
                this.LearningListBox.SelectedIndex = 0;
            }
            else
            {
                this.LearnPanel.CurrentModel = null;
                this.LearnPanel.CurrentModelName = null;
            }
            this.LearnPanel.RemoveModel(lbi);
        }
    }
}
