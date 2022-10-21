using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeurowebNumbers
{
    public partial class Form1 : Form
    {
        private Neuron neuron = new Neuron(5);

        public Form1()
        {
            InitializeComponent();
        }

        public void LoadPicture(object sender, EventArgs e)
        {
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                opf.Title = "Выбрать картинку";
                opf.Filter = "bmp files (*.bmp)|*.bmp";
                if (opf.ShowDialog() == DialogResult.Cancel) return;
                Bitmap originalPicture = new Bitmap(opf.FileName);
                Bitmap picture = new Bitmap(Image.FromHbitmap(originalPicture.GetHbitmap()), pictureBox.Width, pictureBox.Height);
                pictureBox.Image = picture;
            }
        }

        public void DivineNumber(object sender, EventArgs e)
        {
        }
    }
}
