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

        public void DivineNumber(object sender, EventArgs e)
        {
        }
    }
}
