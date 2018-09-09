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
using System.IO;

namespace Archivos
{
    public partial class Proyecto : Form
    {
        private List<Entidad> LEntidades;
        private long cabecera = -1;
        FileStream fs;
        BinaryWriter bw;
        public Proyecto()
        {
            InitializeComponent();
            LEntidades = new List<Entidad>();
            EntNueva.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fs = new FileStream("Ejemplo.dd", FileMode.OpenOrCreate);
            bw = new BinaryWriter(fs);
            bw.Write(cabecera);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Write("Hola");
        }

        private void AgregaEnt_Click(object sender, EventArgs e)
        {
            if (EntNueva.Text != " ")
            {
                Entidad entidad;
                char[] aux = EntNueva.Text.ToCharArray();
                if (aux.Length <= 30)
                {
                    entidad = new Entidad(fs.Position, -1, -1,fs.Seek(fs.Length,SeekOrigin.Current));
                    entidad.AgregaEspacio(aux);
                    for (int i = 0; i < entidad.nombreEnt.Length; i++)
                        Debug.Write(entidad.nombreEnt[i]);
                    string name = entidad.nombreEnt.ToString();
                    LEntidades.Add(entidad);

                    int n = DGEntidad.Rows.Add();
                    DGEntidad.Rows[n].Cells[0].Value = EntNueva.Text;
                    DGEntidad.Rows[n].Cells[1].Value = entidad.DE;
                    DGEntidad.Rows[n].Cells[2].Value = entidad.DA;
                    DGEntidad.Rows[n].Cells[3].Value = entidad.DD;
                    DGEntidad.Rows[n].Cells[4].Value = entidad.DSE;

                    EntNueva.Text = " ";
                }
                else
                    MessageBox.Show("El nombre de la entidad no debe superar los 30 caracteres");
            }
            else
            {
                MessageBox.Show("Escribe el nombre de la nueva entidad");
                EntNueva.Clear();
            }
           
        }
        public void escribir()
        {
            fs = new FileStream("Ejemplo.dd", FileMode.OpenOrCreate);
            bw = new BinaryWriter(fs);
            bw.Write(cabecera);
            foreach (Entidad ent in LEntidades)
            {
                ent.Guardar(bw);
            }
        }
        private void BGuardar_Click(object sender, EventArgs e)
        {
            /*fs = new FileStream("Ejemplo.dd", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bw = new BinaryWriter(fs);
            bw.Write(cabecera);*/
        }
    }
}
