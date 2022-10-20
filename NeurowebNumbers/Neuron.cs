using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeurowebNumbers
{
    class Neuron
    {
        public delegate double ActivationFunc(double value);

        private List<double> _inputs;
        private List<double> _weights = new List<double>();
        public ActivationFunc func;
        
        public Neuron(List<double> inputs)
        {
            _inputs = inputs;
            Random rnd = new();
            _inputs.ForEach(input => _weights.Add(rnd.NextDouble()));
        }

        public double Summator()
        {
            double sum = 0.0d;
            for (int i = 0; i < _inputs.Count; i++)
            {
                sum += _inputs[i] * _weights[i];
            }
            return sum;
        }

        public void FeedForward(double )
        {
            
        }
    }
}
