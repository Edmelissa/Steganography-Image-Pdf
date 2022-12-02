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
    public partial class KeyForm : Form
    {
        public Form1 f1;
        public KeyForm()
        {
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (f1.mode)
            {
                if (TBKey.Text == TBConf.Text)
                {
                    if (TBKey.Text != "")
                    {
                        f1.key = TBKey.Text;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Введен пустой ключ", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ключ не совпадает с подтверждением", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (TBKey.Text != "")
                {
                    f1.key = TBKey.Text;
                    Close();
                }
                else
                {
                    MessageBox.Show("Введен пустой ключ", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void KeyForm_Shown(object sender, EventArgs e)
        {
            if (f1.mode)
            {
                PanelConf.Visible = true;
                PanelKey.Location = new Point(6, 2);
            }
            else
            {
                PanelConf.Visible = false;
                PanelKey.Location = new Point(6, 20);
            }
        }
    }
}
