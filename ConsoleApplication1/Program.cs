using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    public class Perceptron
    {
        private double[] weights = 
        {
            0,0,0,
            0,0,0,
            0,0,0
        };

        private const double Threshold = 0.5;

        private double delta;

        public void Train(List<int[]> values, List<bool> expected)
        {
            while (true)
            {
                bool error = false;

                for (int i = 0; i < values.Count; i++)
                {
                    var val = values[i];
                    var exp = expected[i];

                    bool result = Activate(val);
                    if (result != exp)
                    {
                        error = true;

                        for (int j = 0; j < val.Length; j++)
                        {
                            weights[j] += val[i] * delta;
                        }
                    }
                }

                if (!error)
                {
                    break;
                }
            }
        }

        public bool Activate(int[] values)
        {
            double sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i] * weights[i];
            }

            delta = Threshold - sum;

            return sum > Threshold;
        }
    }
}
