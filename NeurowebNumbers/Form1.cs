using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NeurowebNumbers
{
    public partial class Form1 : Form
    {
        private Neuron _neuron;
        private List<double> _inputs = new();

        public Form1()
        {
            InitializeComponent();
            _neuron = new(_inputs, 15, 9);
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
            if (_neuron.ActivationFunction()) _neuron.FeedForward(-1);
            else _neuron.FeedForward(1);
            resultBox.Clear();
            _neuron.ShowInputs(resultBox);
            Recognize();
        }

        public void Recognize()
        {
            _neuron.Summator();
            bool res = _neuron.ActivationFunction();
            resultBox.Text += Environment.NewLine + $"Result = {res}, Sum = {_neuron.Sum}";
            weightsBox.Clear();
            _neuron.ShowWeights(weightsBox);
        }
    }
}
