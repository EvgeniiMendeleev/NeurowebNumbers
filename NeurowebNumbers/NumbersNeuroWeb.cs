using System;
using NeuroWeb.Layer;
using MathNet.Numerics.LinearAlgebra;

namespace NeuroWeb
{
    class NumbersNeuroWeb
    {
        private NeuroLayer _layer;
        private float _learningSpeed;

        public NumbersNeuroWeb(float learningSpeed = 0.5f)
        {
            _learningSpeed = learningSpeed;
            _layer = new NeuroLayer(15, 10);
            _layer.ActivationFunc += (int k, Matrix<float> outputSignals) =>
            {
                double sum = 0.0f;
                for (int j = 0; j < outputSignals.ColumnCount; j++) sum += Math.Exp(Convert.ToSingle(outputSignals[0, j]));
                return Convert.ToSingle(Math.Exp(Convert.ToSingle(outputSignals[0, k])) / sum);
            };
        }

        public void Train(float[] X_train, float[] Y_train)
        {
            _layer.SetInputSignals(X_train);
            _layer.CalculateOutputSignals();
            Matrix<float> errors = _layer.GetErrors(Y_train);
            _layer.Train(errors, _learningSpeed);
        }

        public (float, int) Predict(float[] X_input)
        {
            _layer.SetInputSignals(X_input);
            _layer.CalculateOutputSignals();
            return _layer.GetMaxFromOutputSignals();
        }
    }
}
