using System;
using System.Collections.Generic;
using System.Linq;

namespace NeurowebNumbers
{
    class Signal
    {
        public double Value { get; set; }
        public double Weight { get; set; }

        public Signal(double value, double weight)
        {
            Value = value;
            Weight = weight;
        }
    }

    class Neuron
    {
        private List<Signal> _inputSignals = new();
        public readonly Signal OutputSignal;

        public Neuron(int signalsCount)
        {
            Random numbersGenerator = new Random();
            for (int i = 0; i < signalsCount; i++) _inputSignals.Add(new Signal(0.0d, numbersGenerator.NextDouble()));
            OutputSignal = new Signal(0.0d, numbersGenerator.NextDouble());
        }

        public void SetInputSignals(List<Signal> signals)
        {
            if (signals.Count != _inputSignals.Count) throw new Exception("Количество входных сигналов отличается от количества принимающих сигналов в нейроне!");

        }

        public void Recognize()
        {
            double sum = _inputSignals.Sum(signal => signal.Value * signal.Weight);
            OutputSignal.Value = 1.0d / (1.0d + Math.Exp(-sum));
        }

        public void FeedForward(double errorValue) => _inputSignals.ForEach(signal => signal.Weight += errorValue * signal.Value);
    }
}