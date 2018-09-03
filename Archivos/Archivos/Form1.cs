using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Archivos
{
    public partial class Proyecto : Form
    {
        Entidad ent;
        Atributo atrib;
        public Proyecto()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Write("Hola");
        }

        private void AgregaEnt_Click(object sender, EventArgs e)
        {

        }
    }
}
