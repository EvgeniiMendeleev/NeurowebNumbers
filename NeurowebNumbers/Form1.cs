using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NeuroWeb;

namespace NeurowebNumbers
{
    public partial class Form1 : Form
    {
        private List<float> _inputs = new();
        private int answerNeuron;
        private NumbersNeuroWeb _neuroWeb = new NumbersNeuroWeb();

        public Form1()
        {
            InitializeComponent();
        }

        public void PredictPicture(object sender, EventArgs e)
        {
            _inputs.Clear();
            resultBox.Clear();

            Bitmap originalPicture = LoadPicture();
            if (originalPicture == null) return;
            TranslateToBits(originalPicture);

            (int neuronNumber, float answer) = _neuroWeb.Predict(_inputs.ToArray());
            answerNeuron = neuronNumber;
            resultBox.Text += $"Нейрон {answerNeuron}: С вероятностью {Math.Round(answer, 6)} это цифра {answerNeuron}";
        }

        private Bitmap LoadPicture()
        {
            Bitmap originalPicture;
            using (OpenFileDialog opf = new())
            {
                opf.Title = "Выбрать картинку";
                opf.Filter = "bmp files (*.bmp)|*.bmp";
                if (opf.ShowDialog() == DialogResult.Cancel) return null;
                originalPicture = new Bitmap(opf.FileName);
                Bitmap picture = new Bitmap(Image.FromHbitmap(originalPicture.GetHbitmap()), pictureBox.Width, pictureBox.Height);
                pictureBox.Image = picture;
            }
            return originalPicture;
        }

        private void TranslateToBits(Bitmap picture)
        {
            for (int i = 0; i < picture.Height; i++)
            {
                for (int j = 0; j < picture.Width; j++)
                {
                    float pixel = picture.GetPixel(j, i).R;
                    if (pixel >= 250) pixel = 0;
                    else pixel = 1;

                    _inputs.Add(pixel);
                    resultBox.Text += $"{pixel} ";
                }
                resultBox.Text += Environment.NewLine;
            }
        }

        private void TrainNeuroWeb(object sender, EventArgs e)
        {
            List<float> rightAnswers = new();
            for (int i = 0; i < 10; i++) rightAnswers.Add(i != numerics.Value ? 0.0f : 1.0f);
            _neuroWeb.Train(rightAnswers.ToArray());
        }
    }
}
