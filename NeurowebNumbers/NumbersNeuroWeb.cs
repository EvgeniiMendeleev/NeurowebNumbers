using System.Linq;
using System.Collections.Generic;
using NeuroWeb.Layer;
using MathNet.Numerics.LinearAlgebra;
using System;

namespace NeuroWeb
{
    class NumbersNeuroWeb
    {
        private NeuroLayer _layer;

        public NumbersNeuroWeb() => _layer = new NeuroLayer(15, 10);

        public void Train(float[] rightAnswers) => _layer.RecalculateWeights(rightAnswers);

        public (int neuronNumber, float maxProbability) Predict(float[] input)
        {
            _layer.SetInputSignals(input);
            _layer.CalculateOutputSignals();
            _layer.NormalizeOutputSignals();
            return _layer.GetMaxOutputValue();
        }
    }
}