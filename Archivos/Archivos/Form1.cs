using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Archivos
{
    public partial class Proyecto : Form
    {
        private List<Entidad> LEntidades;
        private long cabecera = -1;

        SaveFileDialog nuevo;
        private FileStream fs;
        private BinaryWriter bw;
        private Entidad entidad;
        public Proyecto()
        {
            InitializeComponent();
            LEntidades = new List<Entidad>();
            nuevo = new SaveFileDialog();
            EntNueva.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*fs = new FileStream("Ejemplo.dd", FileMode.OpenOrCreate);
            bw = new BinaryWriter(fs);
            bw.Write(cabecera);*/
            //escribir();
        }

        private void BGuardar_Click(object sender, EventArgs e)
        {
            /*fs = new FileStream("Ejemplo.dd", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bw = new BinaryWriter(fs);
            bw.Write(cabecera);*/
        }

        private void BNuevo_Click(object sender, EventArgs e)
        {

            nuevo.Filter = "Diccionario de Datos(*.dd)|*.dd";
            if(nuevo.ShowDialog() == DialogResult.OK)
            {
                fs = new FileStream(nuevo.FileName, FileMode.OpenOrCreate);
                bw = new BinaryWriter(fs);
                bw.Write(cabecera);
                //fs.Close();
            }
           
            EntNueva.Enabled = NuevoAtrib.Enabled = true;
            AgregaEnt.Enabled = ModEnt.Enabled = EliminaEnt.Enabled = true;
            AgregarAtrib.Enabled = ModifAtrib.Enabled = ElimAtrib.Enabled = true;

        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Write("Hola");
        }

        private void AgregaEnt_Click(object sender, EventArgs e)
        {
            int tam = LEntidades.Count;
            long TamArch = fs.Length;
            string nombre = EntNueva.Text;
            if (EntNueva.Text != " ")
            {
                char[] aux = EntNueva.Text.ToCharArray();
                if (aux.Length <= 30)
                {
                    //LEntidades = LEntidades.OrderBy(o => o.NE).ToList();
                    if (LEntidades.Count == 0)
                    {
                        entidad = new Entidad(nombre, TamArch, -1, -1, -1);
                        entidad.AgregaEspacio();
                        LEntidades.Add(entidad);
                        entidad.Guardar(bw);
                    }
                    else
                    {
                        fs.Seek(LEntidades[tam - 1].DE, SeekOrigin.Begin);
                        LEntidades[tam - 1].DSE = TamArch;
                        LEntidades[tam - 1].Guardar(bw);

                        fs.Seek(TamArch, SeekOrigin.Begin);
                        entidad = new Entidad(nombre, TamArch, -1, -1, -1);
                        entidad.AgregaEspacio();
                        LEntidades.Add(entidad);
                        entidad.Guardar(bw);
                    }
                    AgregaFila();
                    //Debug.WriteLine(fs.Length);
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
        public void AgregaFila()
        {
            DGEntidad.Rows.Clear();

            /*LEntidades=LEntidades.OrderBy(o => o.NE).ToList();
            foreach (Entidad entidad in LEntidades)
                Debug.WriteLine(entidad.NE);*/
            foreach (Entidad entidad in LEntidades)
                DGEntidad.Rows.Add(entidad.NE, entidad.DE, entidad.DA, entidad.DD, entidad.DSE);
            EntNueva.Clear();

        }

        private void ModEnt_Click(object sender, EventArgs e)
        {
           string NuevoNombre = Interaction.InputBox("Ingrese el nuevo nombre de la entidad: ", "Modificación de Entidad", " ", 100, 50);
        }
    }
}
