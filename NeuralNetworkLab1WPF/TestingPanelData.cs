using System.ComponentModel;
using System.Windows;
using NeuralNetworkDll;

namespace NeuralNetworkLab1WPF
{
    public sealed class TestingPanel : INotifyPropertyChanged
    {
        private Model _testingModel;
        public Model TestingModel
        {
            get { return this._testingModel; }
            set
            {
                this._testingModel = value;
                this.RaisePropertyChanged("TestingModel");
            }
        }

        public TestingPanel()
        {
            this.TestingModel = new Model(null);
        }

        public void TestModel()
        {
            /*
            if (!this._neuralNetwork.Trained)
            {
                MessageBox.Show("Модель не обучена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool result = this._neuralNetwork.Test(this.TestingModel);
            MessageBox.Show(result ? "Да" : "Нет", "Результат");
            this.TestingModel.Clear();
            */
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
        private void TestModelButtonClick(object sender, RoutedEventArgs e)
        {
            //this.TestPanel.TestModel();
        }
    }
}
