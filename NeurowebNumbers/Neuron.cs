using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeurowebNumbers
{
    class Neuron
    {
        private readonly double _limit;
        private List<double> _inputs;
        private List<double> _weights = new List<double>();
        
        public Neuron(double limit)
        {
            _limit = limit;
        }

        public void SaveInputs(List<double> inputs)
        {
            Random rnd = new();
            _inputs = inputs;
            for (int i = 0; i < _inputs.Count; i++) _weights.Add(rnd.NextDouble());
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

        public bool ActivationFunction(double signal)
        {
            return signal >= _limit;
        }

        public void FeedForward(double errorValue)
        {
            for (int i = 0; i < _inputs.Count; i++) _weights[i] = _weights[i] + errorValue * _inputs[i];
        }
    }
}
