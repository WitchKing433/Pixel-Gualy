using GualyCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual_Gualy
{
    public partial class FormPreferencias : Form
    {
        public decimal CanvasSize { 
            get { return numericUpDownCanvasSize.Value; }  
        }
        public decimal ImageScale
        {
            get { return numericUpDownImageScale.Value; }
        }

        public FormPreferencias(int canvasSize, int imageScale)
        {
            InitializeComponent();
            numericUpDownCanvasSize.Value = canvasSize;
            numericUpDownImageScale.Value= imageScale;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
