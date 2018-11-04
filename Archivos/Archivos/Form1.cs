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
         * atributo - auxiliar para crear nuevos atributos
         * EntModificar - auxiliar para buscar la entidad a modificar o agregar atributos a esta entidad
         * TamArch - Se almacena el tamaño del archivo
         * nom - Variable que guarda el nombre de la entidad que se agregará 
         * NomArch - Variable que guardará el nombre del archivo 
         * NomEntNuev - Nombre de la entidad a modificar
         * NomEModif - Nuevo nombre que se le pondrá a la entidad
        **/

        private List<Entidad> LEntidades;
        // private List<Atributo> LAtributos;
        private long cabecera = 0;

        SaveFileDialog nuevo;
        OpenFileDialog abrir;

        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;

        private Entidad entidad;
        private Atributo atributo;
        private Entidad EntModificar;
        private Entidad EntReg;

        long TamArch;
        string Nom = "";
        string NomArch = "";
        string NomEntNuev;
        string NomEModif;
        public Proyecto()
        {
            InitializeComponent();
            LEntidades = new List<Entidad>();


            EntNueva.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nuevo = new SaveFileDialog();
            abrir = new OpenFileDialog();
        }
        //Crea un nuevo archivo pidiendo el nombre de este 
        private void nuevoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            //Se limpia la lista de entidades
            LEntidades.Clear();
            nuevo.Filter = "Diccionario de Datos(*.dd)|*.dd"; //Se crea el filtro para abrir un tipo de archivo
            if (nuevo.ShowDialog() == DialogResult.OK) //Condición para saber si el usuario crea un archivo 
            {
                //Crea un archivo nuevo         nuevo.FileName es el nombre que el usuario le dio al archivo
                fs = new FileStream(nuevo.FileName, FileMode.OpenOrCreate);
                bw = new BinaryWriter(fs); //Se crea un BinaryWriter
                bw.Write(cabecera); //Se escribe la cabecera al archivo
                NomArch = nuevo.FileName; //Se guarda el nombre del archivo en una variable auxiliar
                fs.Close(); //Se cierra el archivo
            }
            //Se limpia el DataGrid de entidades
            DGEntidad.Rows.Clear();

            //Se habilitan los bontones de agregar, modificar y eliminar de enitidades y atributos
            EntNueva.Enabled = NuevoAtrib.Enabled = NomNuevo.Enabled = txtTDato.Enabled = true;
            AgregaEnt.Enabled = ModEnt.Enabled = EliminaEnt.Enabled = true;
            AgregarAtrib.Enabled = ModifAtrib.Enabled = ElimAtrib.Enabled = true;
            ListNombres.Enabled = ListaAtributos.Enabled = CBDatos.Enabled = CBIndice.Enabled = true;
        }
        //Se abrirá un nuevo archivo y la información se mostrará en los datagrid
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LEntidades.Clear(); //Se limpia la lista de entidades
            long aux = 0; //Auxiliar de la cabecera

            long aux2 = 0; //Auxiliar que guarda la dirección del siguiente atributo

            abrir.Filter = "Diccionario de Datos(*.dd)|*.dd"; //Filtro de la extensión del archivo
            //Condición para saber si el usuario abrió un archivo
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                //abrir.FileName es el nombre del archivo seleccionado
                fs = new FileStream(abrir.FileName, FileMode.Open); //Se abre el archivo
                fs.Seek(0, SeekOrigin.Begin); //Se posiciona al inicio del archivo
                //Se crea un BinaryReader y un BinaryWriter
                br = new BinaryReader(fs);
                bw = new BinaryWriter(fs);
                //Se leen los primero 8 bytes del archivo, los cuales representan a la cabecera
                cabecera = br.ReadInt64();
                aux = cabecera; //Se guarda el valor de la cabecera en un auxiliar
                NomArch = abrir.FileName;
                //Ciclo para leer el archivo usando la dirección de las siguientes entidades
                while (aux != -1)
                {
                    string nomb = ""; //Auxiliar del nombre de la entidad
                    fs.Seek(aux, SeekOrigin.Begin); //Se posiciona en la dirección de la cabecera
                    char[] nombre = br.ReadChars(30); //Se guarda el nombre de la entidad
                    //Ciclo para concatenar el nombre y guardarlo en un string
                    foreach (char n in nombre)
                    {
                        if (char.IsLetter(n))
                            nomb += n;
                    }
                    long DirEnt = br.ReadInt64(); //Se guarda la dirección de la entidad
                    long DirAtrib = br.ReadInt64(); //Se guarda la dirección del atributo
                    long DirDatos = br.ReadInt64(); //Se guarda la dirección de datos
                    long DSE = br.ReadInt64();      //Se guarda la dirección de la siguiente entidad
                    aux = DSE;                      //El auxiliar se iguala al valor de la dirección de la siguiente entidad

                    entidad = new Entidad(nomb, DirEnt, DirAtrib, DirDatos, DSE); //Se crea la entidad
                    LEntidades.Add(entidad); //Se agrega la entidad a la lista

                }
                //Ciclo para acceder a la lista de entidades
                foreach (Entidad entidad in LEntidades)
                {
                    //El auxiliar se iguala a la dirección de atributo que tiene la entidad
                    aux2 = entidad.DA;
                    //Ciclo para leer los atributos de una entidad 
                    while (aux2 != -1)
                    {
                        string nomb = ""; //Auxiliar del nombre de la entidad
                        fs.Seek(aux2, SeekOrigin.Begin); //Se posiciona en la dirección del atributo
                        //Variables auxiliares para leer la información que pertenecen al atributo
                        char[] nombreA = br.ReadChars(30);
                        //Ciclo para concatenear el nombre y guardarlo en un string
                        foreach (char n in nombreA)
                        {
                            if (char.IsLetter(n))
                                nomb += n;
                        }
                        long DirA = br.ReadInt64();
                        char TipD = br.ReadChar();
                        int LongD = br.ReadInt32();
                        int TipInd = br.ReadInt32();
                        long DirInd = br.ReadInt64();
                        long DirSA = br.ReadInt64();
                        //Se iguala el auxiliar a la dirección de siguiente atributo
                        aux2 = DirSA;
                        atributo = new Atributo(nomb, DirA, TipD, LongD, TipInd, DirInd, DirSA);//Se crea un nuevo atributo
                        entidad.AgregaAtributo(atributo); //Se agrega el atributo a la lista de atributos de la entidad
                    }
                }
                fs.Close(); //Se cierra el archivo
                Actualiza();
                //Se muestran los datos del archivo en los DataGrid
                AgregaFila();
                AgregaAtribDG();
            }

            //Se habilitan los bontones de agregar, modificar y eliminar de enitidades y atributos
            EntNueva.Enabled = NuevoAtrib.Enabled = NomNuevo.Enabled = txtTDato.Enabled = true;
            AgregaEnt.Enabled = ModEnt.Enabled = EliminaEnt.Enabled = true;
            AgregarAtrib.Enabled = ModifAtrib.Enabled = ElimAtrib.Enabled = true;
            ListNombres.Enabled = ListaAtributos.Enabled = CBDatos.Enabled = CBIndice.Enabled = true;
        }
        //Se limpian los datagrid con la información del archivo
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cabecera = -1;
            cab.Text = cabecera.ToString();
            DGEntidad.Rows.Clear();
            DGAtributos.Rows.Clear();
            //Se deshabilitan los bontones de agregar, modificar y eliminar de enitidades y atributos
            EntNueva.Enabled = NuevoAtrib.Enabled = NomNuevo.Enabled = txtTDato.Enabled = false;
            AgregaEnt.Enabled = ModEnt.Enabled = EliminaEnt.Enabled = false;
            AgregarAtrib.Enabled = ModifAtrib.Enabled = ElimAtrib.Enabled = false;
            ListNombres.Enabled = ListaAtributos.Enabled = CBDatos.Enabled = CBIndice.Enabled = false;
        }
        //Método que actualiza la información
        public void Actualiza()
        {
            Ordena(); //Ordena las entidades
            DirSigAtrib(); //Asigna la dirección del siguiente atributo
            GuardaCombo(); //Se guardan los nombres de las entidades en el combo box

            //char[] name = new char[30]; //Auxiliar para guardar el nombre de las entidades
            fs = File.Open(NomArch, FileMode.Open, FileAccess.Write); //Se abre el archivo
            bw = new BinaryWriter(fs); //Se escribe un BinaryWriter

            //Ciclo para acceder a la lista de entidades
            foreach (Entidad entidad in LEntidades)
            {
                //Se posiciona en la dirección de la entidad actual
                fs.Seek(entidad.DE, SeekOrigin.Begin);
                entidad.AgregaEspacio();//Se llama al método que agrega el nombre en el arreglo de char
                entidad.Guardar(bw); //Se escribe la entidad en el archivo
                //Ciclo para acceder a la lista de atributos de cada entidad
                foreach (Atributo atrib in entidad.LAtributo1)
                {
                    //Se posiciona en la dirección del atributo
                    fs.Seek(atrib.DA, SeekOrigin.Begin);
                    atrib.ConvierteChar(); //Se llama al método que agrega el nombre en el arreglo de char
                    atrib.EscribeAtributo(bw); //Se escibe el atributo en el archivo
                }
            }
            fs.Close(); //Se cierra el archivo
            AgregaAtribDG(); //Se muestra la información de los atributos en el DataGrid
            AgregaFila(); //Se muestran los datos en el DataGrid
        }
        //Se actualizan las direcciones siguientes de las entidades y se ordenan las entidades
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Ordena(); //Ordena las entidades
             DirSigAtrib(); //Asigna la dirección de la siguiente entidad
             GuardaCombo(); //Se guardan los nombres de las entidades en el combo box

             //char[] name = new char[30]; //Auxiliar para guardar el nombre de las entidades
             fs = File.Open(NomArch, FileMode.Open, FileAccess.Write); //Se abre el archivo
             bw = new BinaryWriter(fs); //Se escribe un BinaryWriter

             //Ciclo para acceder a la lista de entidades
             foreach (Entidad entidad in LEntidades)
             {
                 //Se posiciona en la dirección de la entidad actual
                 fs.Seek(entidad.DE, SeekOrigin.Begin);
                 entidad.Guardar(bw); //Se escribe la entidad en el archivo
             }
             fs.Close(); //Se cierra el archivo
             AgregaAtribDG(); //Se muestra la información de los atributos en el DataGrid
             AgregaFila(); //Se muestran los datos en el DataGrid*/
        }

        //Método que valida que solo puede existir un atributo que sea clave de búsqueda o que sea índice primario
        public void ChecaIndices()
        {
            foreach (Entidad entidad in LEntidades)
            {
                foreach (Atributo atrib in entidad.LAtributo1)
                    switch(atrib.TI)
                    {
                        case 1:
                            if (CBIndice.SelectedIndex == 1)
                                CBIndice.Text = "0";
                            break;
                        case 2:
                            if (CBIndice.SelectedIndex == 2)
                                CBIndice.Text = "0";
                            break;
                    }
            }
        }

        /**
         * Se agrega una entidad nueva al archivo y a la lista
         * Se valida que el nombre de la entidad no sea mayor a 30 caracteres
         * Se valida que el textbox no este vacio 
        **/
        private void AgregaEnt_Click(object sender, EventArgs e)
        {
            //Se abre el archivo seleccionado
            fs = File.Open(NomArch, FileMode.Open, FileAccess.ReadWrite);
            int tam = LEntidades.Count; //Auxiliar para el tamaño de la lista de entidades
            TamArch = fs.Length;    //Auxiliar que guarda el tamaño del archivo
            Nom = EntNueva.Text;    //Auxiliar que guarda el nombre de la nueva entidad
            if (Nom != " " || Nom != "") //Se valida que el nombre sea diferente de una cadena vacía
            {
                char[] aux = EntNueva.Text.ToCharArray();
                if (aux.Length <= 30) //Condición para saber si el nombre es menor o igual a 30 caracteres
                {
                    if (LEntidades.Count == 0) //Condición para saber si la lista está vacia
                    {
                        cabecera = 8; //Se iguala la cabecera a 8
                        fs.Seek(0, SeekOrigin.Begin); //Se posiciona al inicio del archivo
                        bw = new BinaryWriter(fs); //Se crea un BinaryWriter
                        bw.Write(cabecera); //Se escribe la cabecera al archivo 

                        //Se crea una nueva entidad
                        entidad = new Entidad(Nom, TamArch, -1, -1, -1);
                        entidad.AgregaEspacio(); //Se convierte el nombre de la entidad a arreglo de char
                        LEntidades.Add(entidad); //Se agrega la nueva entidad a la lista
                        entidad.Guardar(bw); //Se escribe la entidad en el archivo
                    }
                    else
                    {
                        //Se crea una nueva entidad y se agrega a la lista
                        entidad = new Entidad(Nom, TamArch, -1, -1, -1);
                        bool NDif = NombreDif(Nom);
                        if (NDif == true)
                        {
                            entidad.AgregaEspacio(); //Se convierte el nombre de la entidad a arreglo de char
                            LEntidades.Add(entidad); //Se agrega la nueva entidad a la lista

                            fs.Seek(TamArch, SeekOrigin.Begin); //Se posiciona al final del archivo
                            bw = new BinaryWriter(fs); //se crea un BinaryWriter
                            //Se escribe la nueva entidad al archivo
                            entidad.Guardar(bw);

                        }
                        else
                            MessageBox.Show("No puede haber dos entidades con el mismo nombre");
                    }
                    AgregaFila(); //Se agregan los valores al DataGrid de entidades
                    fs.Close(); //Se cierra el archivo
                    Actualiza();

                }
                else
                    MessageBox.Show("El nombre de la entidad no debe superar los 30 caracteres");
            }
            else
                MessageBox.Show("Escribe el nombre de la nueva entidad");
            //Se muestra el valor de la cabecera en un label
            cab.Text = cabecera.ToString();
            EntNueva.Clear(); //Se limpia el textbox donde se escribe el nombre de la nnueva entidad
        }

        //Método que verifica que no haya entidades con el mismo nombre
        public bool NombreDif(string nombre)
        {
            bool dif = true;
            foreach (Entidad ent in LEntidades)
            {
                //Si se encuentra una entidad con el mismo nombre se cambia la bandera a false
                if (ent.NE == nombre)
                    dif = false;
            }
            return dif;
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

        //Método que agrega los datos al datagridview de atributos
        public void AgregaAtribDG()
        {
            //Se limpia el DataGrid de atributos
            DGAtributos.Rows.Clear();
            //Ciclo para acceder a la lista de entidades 
            foreach (Entidad entid in LEntidades)
            {
                //Ciclo para acceder a la lista de atributos de cada entidad 
                foreach (Atributo atrib in entid.LAtributo1)
                    //Se agrega la información de los atributos al DataGrid
                    DGAtributos.Rows.Add(atrib.NA, atrib.DA, atrib.TD, atrib.LD, atrib.TI, atrib.DI, atrib.DSA);
            }
        }

        //Método que ordena de manera alfabética los elementos de la lista de entidades
        public void Ordena()
        {
            //Se ordena la lista de entidaddes usando el atributo de nombre
            LEntidades = LEntidades.OrderBy(o => o.NE).ToList();
            if (LEntidades.Count > 0)
            {
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
                LEntidades[LEntidades.Count - 1].DSE = -1;
            }
            else
            {
                cabecera = -1;
                fs = File.Open(NomArch, FileMode.Open, FileAccess.Write);//Se abre el archivo sobre el que se trabaja
                fs.Seek(0, SeekOrigin.Begin);//Se direcciona al inicio del archivo
                bw = new BinaryWriter(fs);//Se crea un BinaryWriter con el archivo actual
                bw.Write(cabecera);//Se escribe la cabecera
                fs.Close();//Se cierra el archivo
            }
            cab.Text = cabecera.ToString();
            Debug.WriteLine(cabecera);
        }
        //Método que asigna la dirección de siguiente atributo
        public void DirSigAtrib()
        {
            //Ciclo para acceder a la lista de entidades 
            foreach (Entidad ent in LEntidades)
            {
                //Condición en donde se pondrá en -1 si la lista de atributos de una entidad está vacia
                if (ent.LAtributo1.Count == 0)
                    ent.DA = -1;
                else
                {
                    //Ciclo para acceder a la lista de atributos que tiene cada entidad
                    for (int i = 0; i < ent.LAtributo1.Count - 1; i++)
                        //Al campo de dirección de atributo de la entidad se le asigna la dirección del nuevo atributo
                        ent.LAtributo1[i].DSA = ent.LAtributo1[i + 1].DA;
                }
            }
        }
        //Se guardan los nombre de las entidades en el ComboBox
        public void GuardaCombo()
        {
            //Se limpia el combo box de las entidades 
            ListNombres.Items.Clear();
            //Ciclo para agregar las entidades al combo box
            foreach (Entidad ent in LEntidades)
                ListNombres.Items.Add(ent.NE);
        }
        //Se guardan los nombres de los atributos de la entidad seleccionada 
        public void ComboAtributo()
        {
            //Se limpia el combo box de los atributos
            ListaAtributos.Items.Clear();
            EncuentraEntidad(); //Se busca la entidad seleccionada en el combo box de entidades
            foreach (Atributo atrib in EntModificar.LAtributo1) //Ciclo para agregar los atributos al combo box
                ListaAtributos.Items.Add(atrib.NA);
        }
        //Se modifica el nombre de una entidad
        private void ModEnt_Click(object sender, EventArgs e)
        {
            //Condicion para verificar que el textbox no este vacio
            if (NomNuevo.Text != "" || NomNuevo.Text != " ")
            {
                NomEntNuev = NomNuevo.Text; //Se guarda el nuevo nombre en una variable auxiliar
                EncuentraEntidad();//Se encuentra la entidad quese va a modificar
                EntModificar.NE = NomEntNuev; //Se cambiar el nombre 
                EntModificar.AgregaEspacio(); //Se convierte a char elnuevo nombre 
                AgregaFila(); //Se actualiza el DataGrid
                GuardaCombo(); //Se actualizan los valores del combo box
                ListNombres.Text = ""; //Se limpia el combo box
                NomNuevo.Clear(); //Se limpia el textbox del nuevo nombre
            }
            else
                MessageBox.Show("Escribe el nuevo nombre de la entidad");
            Actualiza();
            /*NomEntNuev = NomNuevo.Text; //Se guarda el nuevo nombre de la entidad
            EncuentraEntidad(); //Busca la entidad a modificar
            AgregaFila(); //Se muestran los valores en el DataGrid
            GuardaCombo();
            ListNombres.Text = "";
            NomNuevo.Clear();*/
        }
        //Método para encontrar una entidad por su nombre y modificar el nomobre
        public void EncuentraEntidad()
        {
            NomEModif = ListNombres.Text;
            //Ciclo para recorrer la lista de entidades
            for (int i = 0; i < LEntidades.Count; i++)
            {
                //Se busca el nombre de la entidad a modificar en la lista de entidades
                if (NomEModif == LEntidades[i].NE)
                {
                    EntModificar = LEntidades[i];
                    EntReg = LEntidades[i];
                    /*LEntidades[i].NE = NomEntNuev; //Se cambia el nombre de la entidad
                    LEntidades[i].AgregaEspacio(); //El nombre se convierte en arreglo de char
                    break;*/
                }
            }
        }

        //Se elimina una entidad de la lista
        private void EliminaEnt_Click(object sender, EventArgs e)
        {
            //Se busca la entidad seleccionana del combo box en la lista de entidades
            EncuentraEntidad();
            LEntidades.Remove(EntModificar); //Se elimina la entidad de la lista 
            //Se almacena el nombre de la entidad a borrar
            /*NomEModif = ListNombres.Text;
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
            }*/
            //Se limpian el text box y comobo box de entidades 
            ListNombres.Text = "";
            NomNuevo.Clear();
            Actualiza();
            //Se muestra el nombre de las entidades en el combo box
            GuardaCombo();
        }
        //Se agrega un nuevo atributo a la entidad seleccionada
        private void AgregarAtrib_Click(object sender, EventArgs e)
        {
            //Se abre el archivo seleccionado
            fs = File.Open(NomArch, FileMode.Open, FileAccess.Write);
            TamArch = fs.Length;    //Auxiliar que guarda el tamaño del archivo
            //int TamAtrib = EntModificar.LAtributo1.Count;
            TamArch = fs.Length;  //Variable que guarda el tamaño del archivo
            EncuentraEntidad(); //Método que encuentrá la variable a la que se le asignarán los atributos
            char TDato = Convert.ToChar(CBDatos.Text); //auxiliar que guarda el tipo de dato
            int TIndice = Convert.ToInt32(CBIndice.Text);//auxiliar qie guarda el tipo de indice 
            int LDato = Convert.ToInt32(txtTDato.Text);//auxiliar que guarda la logitud de dato
            //validación para que el nombre del atributo no este vacio
            if (NuevoAtrib.Text != "" || NuevoAtrib.Text != " ")
            {
                //auxiliar que convierte el nombre en arreglo de char
                char[] aux = NuevoAtrib.Text.ToCharArray();
                //Validación del nombre del atributo, que sea menor o igual a 30 caracteres
                if (aux.Length <= 30)
                {
                    //Se posiciona al final del archivo
                    fs.Seek(TamArch, SeekOrigin.Begin);
                    bw = new BinaryWriter(fs); //Se crea un binarywriter 
                                               //Se crea un nuevo atributo
                    atributo = new Atributo(NuevoAtrib.Text, TamArch, TDato, LDato, TIndice, -1, -1);
                    bool EncAtrib = EntModificar.EncuentraAtributo(atributo.NA);
                    if (EncAtrib == true)
                    {
                        MessageBox.Show("No pueden existir dos atributos con el mismo nombre");
                        fs.Close();
                    }
                    else
                    {
                        //Se convierte el nombre del atributo en arreglo de char
                        atributo.ConvierteChar();
                        EntModificar.AgregaAtributo(atributo); //Se agrega el atributo a la lista de atributos de la entidad
                        EntModificar.DA = EntModificar.LAtributo1[0].DA; //Se asigna la dirección del atributo al campo de dirección de atributo de la entidad
                        atributo.EscribeAtributo(bw);//se escribe los datos del atributo en el archivo
                        fs.Close(); //Se cierra el archivo
                        AgregaAtribDG();//Se muestra la información del atributo en el DatGrid
                    }
                }
                else
                    MessageBox.Show("El nombre del atributo no debe de superar los 30 caracteres");
            }
            else
                MessageBox.Show("Escribe el nombre del nuevo atributo");
            //Método que actualiza la información del archivo
            Actualiza();
            //Método que agrega los nombres de los atributos al combo box
            ComboAtributo();
            //Se limpian los campos que se rellenan con la información del atributo
            ListNombres.Text = "";
            CBDatos.Text = "";
            CBIndice.Text = "";
            NuevoAtrib.Clear();
            txtTDato.Clear();

        }

        //Evento en donde se mostrarán los atributos de la entidad seleccionada del combo box
        private void ListNombres_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se limpia el combo box de atributos
            ListaAtributos.Items.Clear();
            //Se llama al método que llena el combo box de atributos
            ComboAtributo();
        }
        //Se busca el atributo que se va a eliminar 
        private void ElimAtrib_Click(object sender, EventArgs e)
        {
            //Auxiliar del nombre de la entidad seleccionada del combo box
            string aux = ListaAtributos.Text;
            //Ciclo para acceder a a lista de entidades
            foreach (Entidad Ent in LEntidades)
            {
                //Auxiliar para el tamaño de la lista de atributos de cada entidad
                int count = Ent.LAtributo1.Count;
                //Ciclo para acceder a la lista de atributos de una entidad 
                for (int i = 0; i < count; i++)
                {
                    //Condición para encontrar el nombre del atributo seleccionado en el combo box
                    if (Ent.LAtributo1[i].NA == aux)
                    {
                        //Se elimina de la lista
                        Ent.LAtributo1.RemoveAt(i);
                        break;
                    }
                }
                //Se actualiza el campo de dirección de atributo que tiene la entidad 
                if (Ent.LAtributo1.Count > 0)
                    Ent.DA = Ent.LAtributo1[0].DA;
                //Ent.DA = Ent.LAtributo1[Ent.LAtributo1.Count-1].DA;
            }
            Actualiza();
            //Se limpian los campos que fueron llenados para el atributo
            NuevoAtrib.Clear();
            CBDatos.Text = "";
            CBIndice.Text = "";
            txtTDato.Clear();
            ListaAtributos.Text = "";
            ListNombres.Text = "";
            //Se llama al método que hará visualizar la información de los atributos en el DatGrid
            DirSigAtrib();


        }

        //Se modifica los campos seleccionados del atributo
        private void ModifAtrib_Click(object sender, EventArgs e)
        {
            //Auxiliar del nombre del atributo seleccionado del combo box
            string aux = ListaAtributos.Text;
            //Ciclo para acceder a la lista de entidades
            foreach (Entidad Ent in LEntidades)
            {
                //Auxiliar para el tamaño de la lista de atributos de la entidad
                int count = Ent.LAtributo1.Count;
                //Ciclo para acceder a la lista de atributos 
                for (int i = 0; i < count; i++)
                {
                    //Condicional para encontrar el nombre seleccionado del combo box
                    if (Ent.LAtributo1[i].NA == aux)
                    {
                        //Varialbes auxiliares para los campos a modificar
                        string auxNom = Ent.LAtributo1[i].NA;
                        char TDato = Ent.LAtributo1[i].TD;
                        int TIndice = Ent.LAtributo1[i].TI;
                        int LDato = Ent.LAtributo1[i].LD;
                        //Metodo que realiza el cambio de información
                        ValidaCampos(ref TDato, ref TIndice, ref LDato, ref auxNom);

                        //Se guarda la nueva información en los campos correspondientes de la entidad
                        Ent.LAtributo1[i].NA = auxNom;
                        Ent.LAtributo1[i].ConvierteChar();
                        Ent.LAtributo1[i].TD = TDato;
                        Ent.LAtributo1[i].TI = TIndice;
                        Ent.LAtributo1[i].LD = LDato;
                        break;
                    }
                }
            }
            //Se limpian los campos que se llenaron para hacer modificaciones de un atributo
            NuevoAtrib.Clear();
            CBDatos.Text = "";
            CBIndice.Text = "";
            txtTDato.Clear();
            ListaAtributos.Text = "";
            ListNombres.Text = "";
            Actualiza();
            //Se llama al método que hará visualizar la información de los atributos en el DatGrid
            DirSigAtrib();
        }
        //Método que actualiza la información de los campos a modificar
        public void ValidaCampos(ref char TDato, ref int TIndice, ref int LDato, ref string auxNom)
        {
            if (CBDatos.Text != "")
                TDato = Convert.ToChar(CBDatos.Text); //Se guarda el nuevo tipo de dato
            if (CBIndice.Text != "")
                TIndice = Convert.ToInt32(CBIndice.Text);//Se guarda el nuevo tipo de indice 
            if (txtTDato.Text != "")
                LDato = Convert.ToInt32(txtTDato.Text);//Se guarda la nueva logitud de dato
            if (NuevoAtrib.Text != "")
                auxNom = NuevoAtrib.Text;//Se guarda el nuevo nombre de la entidad
        }

        //Llama al form que mostrará la información de los registros de una entidad 
        private void registroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListNombres.Text == "" || ListNombres.Text == " ")
                MessageBox.Show("Selecciona la entidad");
            else
            {
                ModEnt.Enabled = ModifAtrib.Enabled = false;
                //Busca la entidad seleccionada
                EncuentraEntidad();
                Registros reg = new Registros();    //Se crea el nuevo form
                reg.EntAux1 = EntReg;   //Se guarda la entidad que se selecionó
                reg.CreaRegistro(reg.EntAux1);  //Llama al método que crea el archivo 
                //reg.CreaColumnas();         //Crea las columnas dependiendo de los atributos que tiene la entidad
                reg.ShowDialog();
                //Cambia la dirección de datos
                if (reg.LRegistros1.Count > 0)
                {
                    EntReg.DD = reg.LRegistros1[0].DR;
                    for(int i = 0; i < EntReg.LAtributo1.Count; i++)
                    {
                        Atributo a = EntReg.LAtributo1[i];
                        //Checa índice primario
                        if (a.TI == 2)
                            a.DI = reg.Ind1.TamPrinPrim1;
                    }
                    //AgregaAtribDG();
                    //AgregaFila();
                    Actualiza();
                }
            }
        }

        private void CBIndice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChecaIndices();
        }

        private void CBIndice_DrawItem(object sender, DrawItemEventArgs e)
        {
        }
    }
}
