using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace NeuroWeb.Layer
{
    class NeuroLayer
    {
        private Vector<float> inputSignals;
        private Matrix<float> weights;
        private Vector<float> outputSignals;

        public NeuroLayer(int inputSignalsCount, int outputSignalsCount)
        {
            inputSignals = Vector<float>.Build.Dense(inputSignalsCount);
            weights = Matrix<float>.Build.Random(inputSignalsCount, outputSignalsCount);
        }

        public void SetInputSignals(float[] inputs)
        {
            if (inputSignals.Count != inputs.Length) throw new Exception("Число входных данных не совпадает с числом входов нейронной сети!");
            for (int i = 0; i < inputSignals.Count; i++) inputSignals[i] = inputs[i];
        }

        public void CalculateOutputSignals() => outputSignals = inputSignals * weights;

        public (int, float) GetMaxOutputValue()
        {
            float maxProbability = outputSignals.Max();
            int maxProbabilityPosition = outputSignals.MaximumIndex();
            return (maxProbabilityPosition, maxProbability);
        }

        //NormalizeOutputSignals - программмная реализации функции SoftMax.
        public void NormalizeOutputSignals()
        {
            float sum = outputSignals.Sum(signal => MathF.Exp(signal));
            for (int i = 0; i < outputSignals.Count; i++) outputSignals[i] = MathF.Exp(outputSignals[i]) / sum;
        }

        public void RecalculateWeights(float[] answers)
        {
            if (answers.Length != outputSignals.Count) throw new Exception("Число выходов не совпадает с числом правильных ответов!");

            for (int j = 0; j < weights.ColumnCount; j++)
            {
                float error = outputSignals[j] - answers[j];
                float gradient = error / MathF.Pow(MathF.Cosh(outputSignals[j]), 2); //Умножение ошибки (error) на производную функции активации
                for (int i = 0; i < weights.RowCount; i++) weights[i, j] -= 0.9f * gradient * inputSignals[i];
            }
        }
    }
}
