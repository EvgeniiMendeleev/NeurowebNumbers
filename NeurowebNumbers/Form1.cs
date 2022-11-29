using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NeurowebNumbers
{
    public partial class Form1 : Form
    {
        private const int NEURONS_COUNT = 10;
        private Neuron[] _neurons = new Neuron[NEURONS_COUNT];
        private List<double> _inputs = new();

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < NEURONS_COUNT; i++) _neurons[i] = new Neuron(_inputs, 15);
        }

        public void LoadPicture(object sender, EventArgs e)
        {
            _inputs.Clear();
            resultBox.Clear();
            Bitmap originalPicture;
            using (OpenFileDialog opf = new())
            {
                opf.Title = "Выбрать картинку";
                opf.Filter = "bmp files (*.bmp)|*.bmp";
                if (opf.ShowDialog() == DialogResult.Cancel) return;
                originalPicture = new Bitmap(opf.FileName);
                Bitmap picture = new Bitmap(Image.FromHbitmap(originalPicture.GetHbitmap()), pictureBox.Width, pictureBox.Height);
                pictureBox.Image = picture;
            }

            for (int i = 0; i < originalPicture.Height; i++)
            {
                for (int j = 0; j < originalPicture.Width; j++)
                {
                    double pixel = originalPicture.GetPixel(j, i).R;
                    if (pixel >= 250) pixel = 0;
                    else pixel = 1;

                    _inputs.Add(pixel);
                    resultBox.Text += $"{pixel} ";
                }
                resultBox.Text += Environment.NewLine;
            }
            Recognize();
        }

        public void WrongError(object sender, EventArgs e)
        {
        }

        public void Recognize()
        {
            foreach (Neuron neuron in _neurons) neuron.Recognize();

            double maxOutputSignalValue = 0.0d;
            int neuronNumber = 0;
            for (int i = 0; i < _neurons.Count(); i++)
            {
                if (_neurons[i].OutputSignal.Value >= maxOutputSignalValue)
                {
                    maxOutputSignalValue = _neurons[i].OutputSignal.Value;
                    neuronNumber = i;
                }
            }
            resultBox.Text += Environment.NewLine + $"Нейрон {neuronNumber}: с вероятностью {Math.Round(_neurons[neuronNumber].OutputSignal.Value, 4)} это цифра {neuronNumber + 1}";
        }
    }
}
