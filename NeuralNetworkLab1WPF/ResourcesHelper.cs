using System.Windows;
using NeuralNetworkDll;

namespace NeuralNetworkLab1WPF
{
    public static class ResourcesHelper
    {
        private static LearningPanel learningPanel;
        public static LearningPanel LearningPanel
        {
            get
            {
                if (learningPanel == null)
                {
                    learningPanel = Application.Current.FindResource("LearningPanel") as LearningPanel;
                }
                return learningPanel;
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
        public static NeuralNetwork NeuralNetwork
        {
            get
            {
                if (neuralNetwork == null)
                {
                    neuralNetwork = Application.Current.FindResource("NeuralNetwork") as NeuralNetworkViewModel;
                }
                return neuralNetwork.NeuralNetwork;
            }
        }
    }
}
