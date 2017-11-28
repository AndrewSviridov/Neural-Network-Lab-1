using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuralNetworkLab1WPF
{
    public sealed class LearningPanelData : INotifyPropertyChanged
    {
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

        private readonly Dictionary<string, Model> _learningModels =
            new Dictionary<string, Model>();       

        /// <summary>
        /// Конструктор
        /// </summary>
        public LearningPanelData()
        {
            this.InitializeLearningData();
        }

        public void ChangeModel(string model)
        {
            this.CurrentModel = this.GetOrAddModelToDictionary(model);
            this.CurrentModelName = this.CurrentModel.Name;
        }

        public bool ContainsModel(string name)
        {
            return this._learningModels.ContainsKey(name);
        }

        private void InitializeLearningData()
        {
            this.CurrentModel = this.GetOrAddModelToDictionary("Модель 1");
            this.GetOrAddModelToDictionary("Модель 2");
            this.GetOrAddModelToDictionary("Модель 3");
        }

        private Model GetOrAddModelToDictionary(string name)
        {
            Model model;
            if (this._learningModels.TryGetValue(name, out model))
            {
                return model;
            }

            model = new Model(name);
            this._learningModels[name] = model;
            return model;
        }

        public void ChangeRectangleColor(int position)
        {
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
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
            var selectedItemLabel = selectedItem.Content as Label;
            string newModel = selectedItemLabel.Content.ToString();
            this.LearnPanel.ChangeModel(newModel);
        }

        private void LearnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
                string oldName = this.LearnPanel.CurrentModel.Name;
                string newName = this.LearnPanel.CurrentModelName;
                if (oldName != newName && !this.LearnPanel.ContainsModel(newName))
                {
                    this.LearnPanel.CurrentModel.Name = this.LearnPanel.CurrentModelName;
                }
            }
        }
    }
}
