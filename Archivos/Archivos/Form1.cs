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
         *LEntidades - Lista en donde se guardarán las entidades del diccionario de datos
         * cabecera - cabecera que está inicializada a null y después apuntara al primer elemento de la lista
         * nuevo - auxiliar para crear un nuevo archivo
         * abrir - auxiliar para abrir un archivo
         * fs - crear archivo
         * bw - variable par escribir en el archivo 
         * br - Variable para leer el archivo
         * entidad - auxiliar para crear nuevas entidades y agregarlas a la lista
         * TamArch - Se almacena el tamaño del archivo
         * nom - Variable que guarda el nombre de la entidad que se agregará 
         * NomArch - Variable que guardará el nombre del archivo 
         * NomEntNuev - Nombre de la entidad a modificar
         * NomEModif - Nuevo nombre que se le pondrá a la entidad
        **/

        private List<Entidad> LEntidades;
        private long cabecera = 0;

        SaveFileDialog nuevo;
        OpenFileDialog abrir;
        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;

        private Entidad entidad;
        long TamArch;
        string Nom = "";
        string NomArch = "";
        string NomEntNuev;
        string NomEModif;
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
        }
        //Se actualizan las direcciones siguientes de las entidades y se ordenan las entidades
        private void BGuardar_Click(object sender, EventArgs e)
        {
            Ordena(); //Ordena las entidades
            GuardaCombo(); //Se guardan los nombres de las entidades en el combo box

            //char[] name = new char[30]; //Auxiliar para guardar el nombre de las entidades
            fs = File.Open(NomArch, FileMode.Open, FileAccess.Write); //Se abre el archivo
            bw = new BinaryWriter(fs); //Se escribe un BinaryWriter

            //Ciclo para acceder a la lista de entidades
            foreach(Entidad entidad in LEntidades)
            {
                //Se posiciona en la dirección de la entidad actual
                fs.Seek(entidad.DE, SeekOrigin.Begin);
                entidad.Guardar(bw); //Se escribe la entidad en el archivo
            }
            fs.Close(); //Se cierra el archivo
            AgregaFila(); //Se muestran los datos en el DataGrid
        }

        //Crea un nuevo archivo pidiendo el nombre de este 
        private void BNuevo_Click(object sender, EventArgs e)
        {
            //Se limpia la lista de entidades
            LEntidades.Clear();
            nuevo.Filter = "Diccionario de Datos(*.dd)|*.dd"; //Se crea el filtro para abrir un tipo de archivo
            if(nuevo.ShowDialog() == DialogResult.OK) //Condición para saber si el usuario crea un archivo 
            {
                //Crea un archivo nuevo         nuevo.FileName es el nombre que el usuario le dio al archivo
                fs = new FileStream(nuevo.FileName, FileMode.OpenOrCreate);
                bw = new BinaryWriter(fs); //Se crea un BinaryWriter
                bw.Write(cabecera); //Se escribe la cabecera al archivo
                NomArch = nuevo.FileName; //Se guarda el nombre del archivo en una variable auxiliar
                fs.Close();
            }
            //Se limpia el DataGrid de entidades
            DGEntidad.Rows.Clear();
           
            //Se habilitan los bontones de agregar, modificar y eliminar de enitidades y atributos
            EntNueva.Enabled = NuevoAtrib.Enabled =  NomNuevo.Enabled=true;
            AgregaEnt.Enabled = ModEnt.Enabled = EliminaEnt.Enabled = true;
            AgregarAtrib.Enabled = ModifAtrib.Enabled = ElimAtrib.Enabled = true;
            ListNombres.Enabled = true;

        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        /**
         * Se agrega una entidad nueva al archivo y a la lista
         * Se valida que el nombre de la entidad no sea mayor a 30 caracteres
         * Se valida que el textbox no este vacio 
        **/
        private void AgregaEnt_Click(object sender, EventArgs e)
        {
            fs = File.Open(NomArch, FileMode.Open, FileAccess.Write);
            int tam = LEntidades.Count;
            TamArch = fs.Length;
            Nom = EntNueva.Text;
            if (Nom != " " || Nom != "")
            {
                char[] aux = EntNueva.Text.ToCharArray();
                if (aux.Length <= 30)
                {
                    if (LEntidades.Count == 0) //Condición para saber si la lista está vacia
                    {
                        //Se escribe la cabecera al archivo 
                        cabecera = 8;
                        fs.Seek(0, SeekOrigin.Begin);
                        bw = new BinaryWriter(fs);
                        bw.Write(cabecera);

                        //Se crea una nueva entidad, se agrega a la lista de entidades y se escribe en el archivo
                        entidad = new Entidad(Nom, TamArch, -1, -1, -1);
                        entidad.AgregaEspacio();
                        LEntidades.Add(entidad);
                        entidad.Guardar(bw);
                    }
                    else
                    {
                        //Se crea una nueva entidad y se agrega a la lista
                        entidad = new Entidad(Nom, TamArch, -1, -1, -1);
                        entidad.AgregaEspacio();
                        LEntidades.Add(entidad);

                        fs.Seek(TamArch, SeekOrigin.Begin);
                        bw = new BinaryWriter(fs);
                        //Se escrribe la nueva entidad al archivo
                        entidad.Guardar(bw);

                        /*fs.Seek(LEntidades[tam-1].DE, SeekOrigin.Begin);
                        LEntidades[tam-1].DSE = LEntidades[LEntidades.Count-1].DE;
                        Ordena();
                        LEntidades[tam-1].Guardar(bw);*/

                    }
                    AgregaFila();
                    fs.Close();
                }
                else
                    MessageBox.Show("El nombre de la entidad no debe superar los 30 caracteres");
            }
            else
                MessageBox.Show("Escribe el nombre de la nueva entidad");
            cab.Text = cabecera.ToString();
            EntNueva.Clear();
        }
        //Método que agrega los datos al datagridview de entidades
        public void AgregaFila()
        {
            //Se limpia el DatGrid de entidades 
            DGEntidad.Rows.Clear();
            //Ciclo para acceder a los datos de la lista y mostrarlos en el DataGrid
            foreach (Entidad entidad in LEntidades)
                DGEntidad.Rows.Add(entidad.NE, entidad.DE, entidad.DA, entidad.DD, entidad.DSE);
            //Se limpia el textbox en donde se escribe el nombre de la nueva entidad
            EntNueva.Clear();

        }
        //Método que ordena de manera alfabética los elementos de la lista de entidades
        public void Ordena()
        {
            //Se ordena la lista de entidaddes usando el atributo de nombre
            LEntidades = LEntidades.OrderBy(o => o.NE).ToList();
            cabecera = LEntidades[0].DE;    //La cabecera se iguala a la dirección de la primer entidad de la lista

            fs = File.Open(NomArch, FileMode.Open, FileAccess.Write); //Se abre el archivo con el que se trabajará
            fs.Seek(0, SeekOrigin.Begin); //Se posiciona al inicio del archivo
            bw = new BinaryWriter(fs); //Se crea una BinaryWriter con el archivo actual
            bw.Write(cabecera); //Se escribe la cabecera en el archivo
            fs.Close(); //Se cierra el archivo

            //Ciclo para modificar el cambio de 'Dirección de siguiente entidad'
            for (int i = 0; i < LEntidades.Count - 1; i++)
                //Se modifica la dirección de la siguiente entidad
                LEntidades[i].DSE = LEntidades[i + 1].DE;
            cab.Text = cabecera.ToString();
            Debug.WriteLine(cabecera);
        }
        //Se guardan los nombre de las entidades en el ComboBox
        public void GuardaCombo()
        {
            ListNombres.Items.Clear();
            foreach (Entidad ent in LEntidades)
                ListNombres.Items.Add(ent.NE);
        }
        //Se modifica el nombre de una entidad
        private void ModEnt_Click(object sender, EventArgs e)
        {
            NomEntNuev = NomNuevo.Text; //Se guarda el nuevo nombre de la entidad
            EncuentraEntidad(); //Busca la entidad a modificar
            AgregaFila(); //Se muestran los valores en el DataGrid
            GuardaCombo();
            ListNombres.Text = "";
            NomNuevo.Clear();
        }
        //Método para encontrar una entidad por su nombre y modificar el nomobre
        public void EncuentraEntidad()
        {
            NomEModif = ListNombres.Text;
            //Ciclo para recorrer la lista de entidades
            for(int i = 0; i < LEntidades.Count; i++)
            {
                //Se busca el nombre de la entidad a modificar en la lista de entidades
                if (NomEModif == LEntidades[i].NE)
                {
                    LEntidades[i].NE = NomEntNuev; //Se cambia el nombre de la entidad
                    LEntidades[i].AgregaEspacio(); //El nombre se convierte en arreglo de char
                    break;
                }
            }
        }

        //Se abrirá un nuevo archivo y la información se mostrará en los datagrid
        private void BtnAbrir_Click(object sender, EventArgs e)
        {
            LEntidades.Clear(); //Se limpia la lista de entidades
            long aux = 0; //Auxiliar de la cabecera
            string nomb; //Auxilir del nombre de la entidad

            abrir.Filter = "Diccionario de Datos(*.dd)|*.dd"; //Filtro de la extensión del archivo
            //Condición para saber si el usuario abrió un archivo
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                //abrir.FileName es el nombre del archivo seleccionado
                fs = new FileStream(abrir.FileName,FileMode.Open); //Se abre el archivo
                fs.Seek(0, SeekOrigin.Begin); //Se posiciona al inicio del archivo
                //Se crea un BinaryReader y un BinaryWriter
                br = new BinaryReader(fs);
                bw = new BinaryWriter(fs);
                //Se leen los primero 8 bytes del archivo, los cuales representan a la cabecera
                cabecera = br.ReadInt64();  
                aux = cabecera; //Se guarda el valor de la cabecera en un auxiliar
                NomArch = abrir.FileName;
                //Ciclo para leer el archivo usando la dirección de las siguientes entidades
                while(aux != -1)
                {
                    fs.Seek(aux, SeekOrigin.Begin); //Se posiciona en la dirección de la cabecera
                    char[] nombre = br.ReadChars(30); //Se guarda el nombre de la entidad
                    nomb = string.Join("", nombre); //Se transforma a string
                    long DirEnt = br.ReadInt64(); //Se guarda la dirección de la entidad
                    long DirAtrib = br.ReadInt64(); //Se guarda la dirección del atributo
                    long DirDatos = br.ReadInt64(); //Se guarda la dirección de datos
                    long DSE = br.ReadInt64();      //Se guarda la dirección de la siguiente entidad
                    aux = DSE;                      //El auxiliar se iguala al valor de la dirección de la siguiente entidad

                    entidad = new Entidad(nomb, DirEnt, DirAtrib, DirDatos, DSE); //Se crea la entidad
                    LEntidades.Add(entidad); //Se agrega la entidad a la lista
                }
                fs.Close(); //Se cierra el archivo
                AgregaFila(); //Se muestran los datos del archivo en el DataGrid
            }

            //Se habilitan los bontones de agregar, modificar y eliminar de enitidades y atributos
            EntNueva.Enabled = NuevoAtrib.Enabled = NomNuevo.Enabled = true;
            AgregaEnt.Enabled = ModEnt.Enabled = EliminaEnt.Enabled = true;
            AgregarAtrib.Enabled = ModifAtrib.Enabled = ElimAtrib.Enabled = true;
            ListNombres.Enabled = true;
        }
        //Se elimina una entidad de la lista
        private void EliminaEnt_Click(object sender, EventArgs e)
        {
            //Se almacena el nombre de la entidad a borrar
            NomEModif = ListNombres.Text;
            //Ciclo para acceder a la lista de entidades
            for (int i = 0; i < LEntidades.Count; i++)
            {
                //Se busca el nombre de la entidad a borrar
                if(NomEModif == LEntidades[i].NE)
                {
                    //Se borra la entidad de la lista
                    LEntidades.Remove(LEntidades[i]);
                    AgregaFila(); //Se muestra la información en el DatGrid con los cambios realizados
                    break;
                }
            }
            ListNombres.Text = "";
            GuardaCombo();
            NomNuevo.Clear();
        }
    }
}
