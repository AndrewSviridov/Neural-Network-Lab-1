using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NeuralNetworkLab1WPF
{
    public sealed class LearningPanel : INotifyPropertyChanged
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

        public ObservableCollection<Model> Models { get; set; }

        public void AddModel(Model model)
        {
            this.Models.Add(model);
            this.CurrentModel = model;
            this.CurrentModelName = model.Name;
        }

        public void RemoveCurrentModel()
        {
            this.Models.Remove(this.CurrentModel);
            this.CurrentModel = null;
            this.CurrentModelName = null;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public LearningPanel()
        {
            this.Models = new ObservableCollection<Model>();
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
        private void LearnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (ResourcesHelper.LearningPanel.CurrentModel == null)
            {
                return;
            }
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
                ResourcesHelper.LearningPanel.CurrentModel.Name = ResourcesHelper.LearningPanel.CurrentModelName;
            }
        }

        private void AddLearningModelButtonClick(object sender, RoutedEventArgs e)
        {
            ResourcesHelper.LearningPanel.AddModel(new Model("Новая модель"));
        }

        private void RemoveLearningModelButtonClick(object sender, RoutedEventArgs e)
        {
            ResourcesHelper.LearningPanel.RemoveCurrentModel();
        }

        private void LearningListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourcesHelper.LearningPanel.CurrentModelName = ResourcesHelper.LearningPanel.CurrentModel.Name;
        }
    }
}