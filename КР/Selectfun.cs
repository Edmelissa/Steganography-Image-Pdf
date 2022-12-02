using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace КР
{
    public partial class Selectfun : Form
    {
        public Form1 f1;
        private bool select = false;

        public Selectfun()
        {
            InitializeComponent();
        }

        private void ButImage_Click(object sender, EventArgs e)
        {
            select = true;
            f1.stegimageorpdf = false;
            Close();
        }

        private void ButPdf_Click(object sender, EventArgs e)
        {
            select = true;
            f1.stegimageorpdf = true;
            Close();
        }

        private void Selectfun_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!select)
                f1.stegimageorpdf = true;
        }
    }
}
