using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace NeuralNetworkLab1WPF
{
    public sealed class ModelPanel : INotifyPropertyChanged
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

        public string ExportModels()
        {
            return JsonConvert.SerializeObject(Models.ToArray());
        }

        public void ImportModels(string text)
        {
            Model[] models = JsonConvert.DeserializeObject<Model[]>(text);
            this.Models.Clear();
            foreach (var model in models)
            {
                this.Models.Add(model);
            }   
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ModelPanel()
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
            ResourcesHelper.ModelPanel.AddModel(new Model("Новая модель", true));
        }

        private void RemoveLearningModelButtonClick(object sender, RoutedEventArgs e)
        {
            ResourcesHelper.ModelPanel.RemoveCurrentModel();
        }

        private void MenuImportClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string models = File.ReadAllText(dialog.FileName);
                ResourcesHelper.ModelPanel.ImportModels(models);
            }
        }

        private void MenuExportClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string models = ResourcesHelper.ModelPanel.ExportModels();
                File.WriteAllText(dialog.FileName, models);
            }
        }
    }
}