using System.Windows;
using NeuralNetworkDll;

namespace NeuralNetworkLab1WPF
{
    public static class ResourcesHelper
    {
        private static ModelPanel modelPanel;
        public static ModelPanel ModelPanel
        {
            get
            {
                if (modelPanel == null)
                {
                    modelPanel = Application.Current.FindResource("ModelPanel") as ModelPanel;
                }
                return modelPanel;
            }
        }

        private static TestingPanel testingPanel;
        public static TestingPanel TestingPanel
        {
            get
            {
                if (testingPanel == null)
                {
                    testingPanel = Application.Current.FindResource("TestingPanel") as TestingPanel;
                }
                return testingPanel;
            }
        }

        private static NeuralNetworkViewModel neuralNetwork;
        public static NeuralNetworkViewModel NeuralNetwork
        {
            get
            {
                if (neuralNetwork == null)
                {
                    neuralNetwork = Application.Current.FindResource("NeuralNetwork") as NeuralNetworkViewModel;
                }
                return neuralNetwork;
            }
        }
    }
}
