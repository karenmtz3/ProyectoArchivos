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
        /**
         * Lista en donde se guardarán las entidades del diccionario de datos
         * cabecera que está inicializada a null y después apuntara al primer elemento de la lista
         * auxiliar para crear un nuevo archivo
         * auxiliar para abrir un archivo
         * crear archivo
         * variable par escribir en el archivo 
         * Variable para leer el archivo
        **/

        private List<Entidad> LEntidades;
        private long cabecera = 0;

        SaveFileDialog nuevo;
        OpenFileDialog abrir;
        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;

        private Entidad entidad;
        public Proyecto()
        {
            InitializeComponent();
            LEntidades = new List<Entidad>();
            nuevo = new SaveFileDialog();
            abrir = new OpenFileDialog();
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

        //Crea un nuevo archivo pidiendo el nombre de este 
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
        /**
         * Se agrega una entidad nueva al archivo y a la lista
         * Se valida que el nombre de la entidad no sea mayor a 30 caracteres
         * Se valida que el textbox no este vacio 
        **/
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
                    if (LEntidades.Count == 0)
                    {
                        entidad = new Entidad(nombre, TamArch, -1, -1, -1);
                        entidad.AgregaEspacio();
                        LEntidades.Add(entidad);
                        entidad.Guardar(bw);
                    }
                    else
                    {

                        fs.Seek(TamArch, SeekOrigin.Begin);
                        entidad = new Entidad(nombre, TamArch, -1, -1, -1);
                        entidad.AgregaEspacio();
                        LEntidades.Add(entidad);
                        Ordena();
                        entidad.Guardar(bw);

                        fs.Seek(LEntidades[tam-1].DE, SeekOrigin.Begin);
                        LEntidades[tam-1].DSE = LEntidades[LEntidades.Count-1].DE;
                        Ordena();
                        LEntidades[tam-1].Guardar(bw);

                    }

                    
                    
                    AgregaFila();
                    

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
        //Método que agrega los datos al datagridview de entidades
        public void AgregaFila()
        {
            DGEntidad.Rows.Clear();
            foreach (Entidad entidad in LEntidades)
                DGEntidad.Rows.Add(entidad.NE, entidad.DE, entidad.DA, entidad.DD, entidad.DSE);
            EntNueva.Clear();

        }
        //Método que ordena de manera alfabética los elementos de la lista de entidades
        public void Ordena()
        {
            LEntidades = LEntidades.OrderBy(o => o.NE).ToList();
            cabecera = LEntidades[0].DE;
            for (int i = 0; i < LEntidades.Count - 1; i++)
            {
                LEntidades[i].DSE = LEntidades[i + 1].DE;
            }
            Debug.WriteLine(cabecera);
        }
        //
        private void ModEnt_Click(object sender, EventArgs e)
        {
           //string NuevoNombre = Interaction.InputBox("Ingrese el nuevo nombre de la entidad: ", "Modificación de Entidad", " ", 100, 50);
        }

        //Se abrirá un nuevo archivo y la información se mostrará en los datagrid
        private void button1_Click(object sender, EventArgs e)
        {
            LEntidades.Clear();
            long aux = 0;
            string nomb;
            abrir.Filter = "Diccionario de Datos(*.dd)|*.dd";
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("se abrio");
                fs = new FileStream(abrir.FileName,FileMode.Open, FileAccess.Read);
                fs.Seek(0, SeekOrigin.Begin);
                br = new BinaryReader(fs);
                cabecera = br.ReadInt64();
                aux = cabecera;
                
                while(aux != -1)
                {
                    char[] nombre = br.ReadChars(30);
                    nomb = string.Join("", nombre);
                    //Debug.WriteLine(fs.Position);
                    long DirEnt = br.ReadInt64();
                    long DirAtrib = br.ReadInt64();
                    long DirDatos = br.ReadInt64();
                    long DSE = br.ReadInt64();
                    aux = DSE;
                    entidad = new Entidad(nomb, DirEnt, DirAtrib, DirDatos, DSE);
                    LEntidades.Add(entidad);
                }
                AgregaFila();
                //br.ReadInt64();
            }
        }
    }
}
