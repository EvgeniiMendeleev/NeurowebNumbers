using MathNet.Numerics.LinearAlgebra;

namespace NeuroWeb.Layer
{
    class NeuroLayer
    {
        public delegate float ActivationFuncHandler(int k, Matrix<float> signals);
        public ActivationFuncHandler ActivationFunc;

        private Matrix<float> _inputSignals;
        private Matrix<float> _outputSignals;
        private Matrix<float> _weights;

        public NeuroLayer(int inputSignalsCount, int outputSignalsCount) => _weights = Matrix<float>.Build.Random(inputSignalsCount, outputSignalsCount);

        public void SetInputSignals(float[] inputSignals) => _inputSignals = Matrix<float>.Build.Dense(1, _weights.RowCount, inputSignals);

        public void CalculateOutputSignals()
        {
            _outputSignals = _inputSignals * _weights;
            for (int j = 0; j < _outputSignals.ColumnCount; j++) _outputSignals[0, j] = ActivationFunc(j, _outputSignals);
        }

        public void Train(Matrix<float> errors, float learningSpeed)
        {
            for (int i = 0; i < _weights.RowCount; i++)
            {
                for (int j = 0; j < _weights.ColumnCount; j++) _weights[i, j] -= learningSpeed * errors[i, j];
            }
        }

        public Matrix<float> GetErrors(float[] y_train)
        {
            Matrix<float> errors = Matrix<float>.Build.Random(_weights.RowCount, _weights.ColumnCount);
            for (int i = 0; i < errors.RowCount; i++)
            {
                for (int j = 0; j < errors.ColumnCount; j++) errors[i, j] = (_outputSignals[0, j] - y_train[j]) * _inputSignals[0, i];
            }
            return errors;
        }

        public (float, int) GetMaxFromOutputSignals()
        {
            float maxValue = 0.0f;
            int neuronNumber = 0;
            for (int j = 0; j < _outputSignals.ColumnCount; j++)
            {
                if (maxValue < _outputSignals[0, j])
                { 
                    maxValue = _outputSignals[0, j];
                    neuronNumber = j;
                }
            }
            return (maxValue, neuronNumber);
        }
    }
}
