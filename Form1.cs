using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void veículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Veiculos veiculos = new Veiculos();
            veiculos.MdiParent = this;
            veiculos.Show();

        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Marcas marca = new Marcas();
            marca.MdiParent = this;
            marca.Show();
        }
    }
}
