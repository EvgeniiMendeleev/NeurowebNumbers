using System;
using System.Collections.Generic;
using System.Linq;

namespace NeurowebNumbers
{
    class Signal
    {
        public double Input { get; set; }
        public double Weight { get; set; }

        public Signal(double input, double weight)
        {
            Input = input;
            Weight = weight;
        }
    }

    class Neuron
    {
        private List<Signal> _inputSignals;
        public readonly Signal _outputSignal;

        public Neuron(List<double>inputs, int signalsCount)
        {
            _inputSignals = new();
            Random numbersGenerator = new Random();
            for (int i = 0; i < signalsCount; i++) _inputSignals.Add(new Signal(inputs[i], numbersGenerator.NextDouble()));
            _outputSignal = new Signal(0.0d, numbersGenerator.NextDouble());
        }

        public Neuron(List<Signal> inputsSignals)
        {
            Random numbersGenerator = new Random();
            _inputSignals = new(inputsSignals);
            _outputSignal = new Signal(0.0d, numbersGenerator.NextDouble());
        }

        public void Recognize()
        {
            double sum = _inputSignals.Sum(signal => signal.Input * signal.Weight);
            _outputSignal.Input = 1.0d / (1.0d + Math.Exp(-sum));
        }

        public void FeedForward(double errorValue) => _inputSignals.ForEach(signal => signal.Weight += errorValue * signal.Input);
    }
}
