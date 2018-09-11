using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archivos
{
    public partial class ModEnt : Form
    {
        string NuevoNom;
        string NombreEnt;
        public ModEnt()
        {
            InitializeComponent();
        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            NuevoNom = txtNuevNombre.Text;
            NombreEnt = cbNomEnt.Text;
        }
        public void Guarda(List<Entidad> entidad)
        {
            foreach (Entidad en in entidad)
                cbNomEnt.Items.Add(en.NE);
        }

    }
}
