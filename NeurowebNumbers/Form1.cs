using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NeurowebNumbers
{
    public partial class Form1 : Form
    {
        private List<Neuron> _neurons = new();

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++) _neurons.Add(new(15, 9));
        }

        public void LoadPicture(object sender, EventArgs e)
        {   
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

            List<double> inputs = new();
            for (int i = 0; i < originalPicture.Height; i++)
            {
                for (int j = 0; j < originalPicture.Width; j++)
                {
                    double pixel = originalPicture.GetPixel(j, i).R;
                    if (pixel >= 250) pixel = 0;
                    else pixel = 1;

                    inputs.Add(pixel);
                    resultBox.Text += $"{pixel} ";
                }
                resultBox.Text += Environment.NewLine;
            }

            _neurons.ForEach(neuron => neuron.SetInput(inputs));
            Recognize();
        }

        private void Recognize()
        {
            _neurons.ForEach(neuron => neuron.Summator());
            List<double> neuronsResult = _neurons.Select(neuron => neuron.Sum).ToList();

            double sum = neuronsResult.Sum();
            for (int i = 0; i < neuronsResult.Count; i++) neuronsResult[i] /= sum;

            double maxValue = neuronsResult.Max();
            double maxValuePosition = neuronsResult.IndexOf(maxValue);

            resultBox.Text += Environment.NewLine + $"Нейрон {maxValuePosition + 1}: с вероятностью {Math.Round(maxValue, 4)} это цифра {maxValuePosition + 1}";
        }

        public void WrongError(object sender, EventArgs e)
        {
            resultBox.Clear();
            Recognize();
        }
    }
}
