using System.Windows;

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
    }
}
