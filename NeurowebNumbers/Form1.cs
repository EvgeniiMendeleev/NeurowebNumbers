using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics;
using NeuroWeb;

namespace NeurowebNumbers
{
    public partial class Form1 : Form
    {
        private List<float> _inputs = new();
        private NumbersNeuroWeb _neuroWeb = new NumbersNeuroWeb();

        public Form1()
        {
            InitializeComponent();

            float[][] X_train = new float[][] 
            {
                new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                new float[] { 1, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0 },
                new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1 },
                new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 1, 1 },
                new float[] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 0, 1 },
                new float[] { 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1 }
            };
            float[] Y_train = new float[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };

            for (int i = 0; i < X_train.Length; i++) _neuroWeb.Train(X_train[i], Y_train);
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
                    float pixel = originalPicture.GetPixel(j, i).R;
                    if (pixel >= 250) pixel = 0;
                    else pixel = 1;

                    _inputs.Add(pixel);
                    resultBox.Text += $"{pixel} ";
                }
                resultBox.Text += Environment.NewLine;
            }

            (float answer, int neuronNumber) = _neuroWeb.Predict(_inputs.ToArray());
            resultBox.Text += $"Нейрон {neuronNumber + 1}: С вероятностью {Math.Round(answer, 6)} это цифра {neuronNumber + 1}";
        }
    }
}
