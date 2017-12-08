using System.Windows;

namespace NeuralNetworkProjectWPF
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

    }
}
