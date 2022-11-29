using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeurowebNumbers
{
    class Neuron
    {
        private readonly double _limit;
        private List<double> _inputs;
        private List<double> _weights = new();
        public double Sum { get; private set; } = 0;

        public Neuron(List<double>inputs, int signalsCount, double limit)
        {
            _limit = limit;
            _inputs = inputs;
            for (int i = 0; i < signalsCount; i++) _weights.Add(0.0d);
        }

        public void ShowWeights(TextBox textBox)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    textBox.Text += $"{_weights[i * 3 + j]} ";
                }
                textBox.Text += Environment.NewLine;
            }
        }

        public void ShowInputs(TextBox textBox)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    textBox.Text += $"{_inputs[i * 3 + j]} ";
                }
                textBox.Text += Environment.NewLine;
            }
        }

        public void Summator()
        {
            Sum = 0.0d;
            for (int i = 0; i < _inputs.Count; i++)
            {
                Sum += _inputs[i] * _weights[i];
            }
        }

        public bool ActivationFunction()
        {
            return Sum >= _limit;
        }

        public void FeedForward(double errorValue)
        {
            for (int i = 0; i < _inputs.Count; i++) _weights[i] += errorValue * _inputs[i];
        }
    }
}
