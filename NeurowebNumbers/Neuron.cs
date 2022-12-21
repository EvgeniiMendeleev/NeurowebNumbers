using System;
using System.Collections.Generic;
using System.Linq;

namespace NeurowebNumbers
{
    class Signal
    {
        public double InputValue { get; set; }
        public double Weight { set; get; }

        public Signal(double inputValue, double weight)
        {
            InputValue = inputValue;
            Weight = weight;
        }
    }

    class Neuron
    {
        private readonly double _limit;
        private List<Signal> _signals = new();
        public double Sum { get; private set; } = 0;

        public Neuron(int signalsCount, double limit)
        {
            Random numberGenerator = new Random();
            _limit = limit;
            for (int i = 0; i < signalsCount; i++) _signals.Add(new Signal(0.0d, numberGenerator.NextDouble()));
        }

        public void SetInput(List<double> inputs)
        {
            if (inputs.Count != _signals.Count) throw new Exception("NeuronException: Wrong signals count!");
            for (int i = 0; i < _signals.Count; i++) _signals[i].InputValue = inputs[i];
        }

        public void Summator() => Sum = _signals.Sum(signal => signal.InputValue * signal.Weight);
        public bool ActivationFunction() => Sum >= _limit;
        public void FeedForward(double errorValue) => _signals.ForEach(signal => signal.Weight += errorValue * signal.InputValue);
    }
}
