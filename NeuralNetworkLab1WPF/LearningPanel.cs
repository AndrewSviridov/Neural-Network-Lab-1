using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

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

        public ObservableCollection<Model> Models { get; set; }

        public void AddModel(Model model)
        {
            this.Models.Add(model);
            this.CurrentModel = model;
        }

        public void RemoveCurrentModel()
        {
            this.Models.Remove(this.CurrentModel);
            this.CurrentModel = null;
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
        private void AddLearningModelButtonClick(object sender, RoutedEventArgs e)
        {
            ResourcesHelper.LearningPanel.AddModel(new Model("Новая модель"));
        }

        private void RemoveLearningModelButtonClick(object sender, RoutedEventArgs e)
        {
            ResourcesHelper.LearningPanel.RemoveCurrentModel();
        }

        private void TeachButtonClick(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                var values = new List<int[]>();
                var expected = new List<bool>();
                foreach (var model in ResourcesHelper.LearningPanel.Models)
                {
                    values.Add(model);
                    expected.Add(model.Result);
                }
                ResourcesHelper.NeuralNetwork.Train(values.ToArray(), expected.ToArray());
                MessageBox.Show("Модель обучена!", "Обучение", MessageBoxButton.OK);
            });
        }
    }
}