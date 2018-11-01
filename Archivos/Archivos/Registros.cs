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
    public partial class Registros : Form
    {
        private Entidad EntAux;
        private string NomArch;
        private string NomArchIdx;
        long TamArch;
        int pos;
        long cabecera;
        long cabeceraInd;

        private List<Registro> LRegistros;
        private Registro reg;

        DataGridViewTextBoxColumn inicio;
        DataGridViewTextBoxColumn fin;

        OpenFileDialog abrir;
        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;

        int cb;

        int posReg;
        int PosSec;
        int posPrim;

        Atributo atrib;

        //IndicePrim IPrimario;
        List<Secundario> ISecundario;
        List<Primario> IPrimario;

        public Entidad EntAux1 { get => EntAux; set => EntAux = value; }
        internal List<Registro> LRegistros1 { get => LRegistros; set => LRegistros = value; }
        public List<Secundario> ISecundario1 { get => ISecundario; set => ISecundario = value; }

        public Registros()
        {
            InitializeComponent();
            LRegistros = new List<Registro>();
            inicio = new DataGridViewTextBoxColumn();
            fin = new DataGridViewTextBoxColumn();
            abrir = new OpenFileDialog();

            ISecundario = new List<Secundario>();

           posPrim = PosSec = -1;
           posReg = 0;

        }
        //Se crea las columnas dinámicamente de acuerdo a los atributos que tiene una entidad
        public void CreaColumnas()
        {
            label2.Text = EntAux.NE;

            inicio.HeaderText = "Dirección de Registro";
            inicio.Width = 80;
            DGRegistros.Columns.Add(inicio);
            //Ciclos que agregará las columnas dependiendo de la cantidad de atributos que tiene la entidad
            foreach (Atributo atrib in EntAux.LAtributo1)
            {
                DataGridViewTextBoxColumn colum = new DataGridViewTextBoxColumn();
                colum.HeaderText = atrib.NA;
                colum.Width = 100;
                DGRegistros.Columns.Add(colum);
            }
            fin.HeaderText = "Dirección de siguiente registro";
            fin.Width = 80;
            DGRegistros.Columns.Add(fin);

            //DataGrid para hacer las capturas de los registros
            foreach (Atributo atrib in EntAux.LAtributo1)
            {
                DataGridViewTextBoxColumn colum = new DataGridViewTextBoxColumn();
                colum.HeaderText = atrib.NA;
                colum.Width = 100;
                DGCaptura.Columns.Add(colum);
            }
        }

        //Pasa la información deL DataGrid de captura al DataGrid de los registros y lo agrega a la lista de registros
        public void MuestraInf()
        {
            int n = DGRegistros.Rows.Add();
            fs = File.Open(NomArch, FileMode.Open, FileAccess.ReadWrite);
            TamArch = fs.Length;
            fs.Seek(TamArch, SeekOrigin.Begin);
            bw = new BinaryWriter(fs);

            reg = new Registro(TamArch, -1);

            DGRegistros.Rows[n].Cells[0].Value = reg.DR;
            DGRegistros.Rows[n].Cells[DGRegistros.ColumnCount - 1].Value = reg.DSR;
            for (int i = 0; i < DGCaptura.ColumnCount; i++)
            {
                string elem = (string)DGCaptura.Rows[0].Cells[i].Value;
                DGRegistros.Rows[n].Cells[i + 1].Value = elem;
                reg.Elementos.Add(elem);
            }
            LRegistros.Add(reg);
            reg.Escribe(bw, EntAux);
            fs.Close();
            Actualizar();
        }
        //Se actualiza la información y se escribe en el archivo de datos
        public void Actualizar()
        {
            DireccionSigReg();
            fs = File.Open(NomArch, FileMode.Open, FileAccess.Write); //Se abre el archivo
            bw = new BinaryWriter(fs); //Se escribe un BinaryWriter

            foreach (Registro r in LRegistros)
            {
                fs.Seek(r.DR, SeekOrigin.Begin);
                r.Escribe(bw, EntAux);
            }
            fs.Close();
            ImprimeDataGrid();
            ImprimeLista();
        }
        //Se actualiza la información que se encuentra en el archivo de índices
        public void ActualizaIndices()
        {
            DireccionSigIndSec();
            fs = File.Open(NomArchIdx, FileMode.Open, FileAccess.Write);
            bw = new BinaryWriter(fs);
            //fs.Seek(0, SeekOrigin.Begin);
            foreach(Secundario sec in ISecundario)
            {
                fs.Seek(sec.DirInd, SeekOrigin.Begin);
                if (EntAux.LAtributo1[PosSec].TD == 'E')
                    sec.EscribeEnteros(bw);
                else
                {
                    //Debug.WriteLine(sec.Nombre);
                    sec.Escribe(bw);
                }
                //Se escribe el sub bloque con las nuevas direcciones
                if (sec.DirB != -1)
                {
                    fs.Seek(sec.DirB, SeekOrigin.Begin);
                    sec.EscribeSub(bw);
                }
            }
            fs.Close();
        }

        public bool ClaveBusqueda()
        {
            for (int i = 0; i < EntAux.LAtributo1.Count; i++)
            {
                Atributo atrib = EntAux.LAtributo1[i];
                if (atrib.TI == 1)
                {
                    cb = i;
                    return true;
                }
            }
            return false;
        }

        public void DireccionSigIndSec()
        {
            for (int i = 0; i < ISecundario.Count - 1; i++)
            {
                ISecundario[i].DirIndS = ISecundario[i + 1].DirInd;
                if (ISecundario[i].SubSec1.Count > 0)
                {
                    for (int j = 0; j < ISecundario[i].SubSec1.Count-1; j++)
                        ISecundario[i].SubSec1[j].DirESig1 = ISecundario[i].SubSec1[j + 1].DirE1;
                }
            }
        }

        //Actualiza la dirección del siguiente registro cuando hay más de un elemento en la lista
        public void DireccionSigReg()
        {
            bool Clave = ClaveBusqueda();
            if (Clave == true)
            {
                LRegistros = LRegistros.OrderBy(o => o.Elementos[cb]).ToList();
                for (int i = 0; i < LRegistros.Count - 1; i++)
                    LRegistros[i].DSR = LRegistros[i + 1].DR;
                LRegistros[LRegistros.Count - 1].DSR = -1;

            }
            else
            {
                for (int i = 0; i < LRegistros.Count - 1; i++)
                    LRegistros[i].DSR = LRegistros[i + 1].DR;
                LRegistros[LRegistros.Count - 1].DSR = -1;
            }

            for (int i = 0; i < LRegistros.Count; i++)
            {
                DGRegistros.Rows[i].Cells[0].Value = LRegistros[i].DR;
                DGRegistros.Rows[i].Cells[DGRegistros.ColumnCount - 1].Value = LRegistros[i].DSR;
            }
        }

        //Crea el archivo en donde se guardarán los registros de la entidad seleccionada
        public void CreaRegistro(Entidad EntAux)
        {
            NomArch = EntAux.NE + ".dat";
            //Se crea un nuevo FileStream con el nombre de la entidad y con extensión 'dat'
            fs = new FileStream(NomArch, FileMode.OpenOrCreate);
            bw = new BinaryWriter(fs); //Se crea un BinaryWriter
            fs.Close(); //Se cierra el archivo
        }

        //Checa que existan atributos con índice primario o secundario
        public void VerificaIndices()
        {
            for (int i = 0; i < EntAux.LAtributo1.Count;i++)
            {
                Atributo atrib = EntAux.LAtributo1[i];
                if (atrib.TI != 0 && atrib.TI != 1)
                {
                    if (atrib.TI == 2)
                        posPrim = i;
                    if (atrib.TI == 3)
                        PosSec = i;
                    CreaIndices(atrib);
                }
            }
            //Debug.WriteLine("El atributo que es índice secundario es " + EntAux.LAtributo1[PosSec].NA);

        }

        //Crea el archivo de índices
        public void CreaIndices(Atributo atributo)
        {
            NomArchIdx = EntAux.NE + ".idx";
            fs = new FileStream(NomArchIdx, FileMode.OpenOrCreate);
            bw = new BinaryWriter(fs);

            if (atributo.TI == 2)
            {
                string aux = "";
                if (atributo.TD == 'E')
                {
                   
                    for (int i = 1; i < 10; i++)
                    {
                        fs.Seek(fs.Length, SeekOrigin.Begin);
                        aux = i.ToString();
                        long dir = -1;
                        Primario prim = new Primario(atributo, aux, fs.Length, dir);
                        IPrimario.Add(prim);
                        prim.EscribeEnteros(bw);
                    }
                }
                else if (atributo.TD == 'C')
                {
                    string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    char[] auxAlf = alfabeto.ToCharArray();
                    for (int i = 0; i < auxAlf.Length; i++)
                    {
                        aux = auxAlf[i].ToString();
                        long dir = -1;
                        Primario prim = new Primario(atributo, aux, fs.Length, dir);
                        IPrimario.Add(prim);
                        prim.EscribeAlfabeto(bw);
                    }
                }
                /*fs.Seek(fs.Length, SeekOrigin.Begin);
                IPrimario = new IndicePrim(atributo);
                IPrimario.CreaBPrincipal();
                MuestraIndiceP(IPrimario);
                IPrimario.EscribeBPrincipal(bw);*/
            }
            if(atributo.TI == 3)
            {
                string aux = "";
                if (atributo.TD == 'E')
                {
                    for (int i = 1; i < 10; i++)
                    {
                        fs.Seek(fs.Length, SeekOrigin.Begin);
                        aux = i.ToString();
                        long dir = -1;
                        Secundario sec = new Secundario(atributo, aux, dir, fs.Length,-1);
                        ISecundario.Add(sec);
                        sec.EscribeEnteros(bw);
                    }
                }
                else if(atributo.TD == 'C')
                {
                    for(int i = 0; i < 50; i++)
                    {
                        fs.Seek(fs.Length, SeekOrigin.Begin);
                        aux = "aux";
                        long dir = -1;
                        Secundario sec = new Secundario(atributo, aux, dir, fs.Length,-1);
                        sec.ConvierteChar();
                        ISecundario.Add(sec);
                        
                        sec.Escribe(bw);
                    }
                    

                }
                Debug.WriteLine(fs.Length);
            }
            fs.Close();
            ActualizaIndices();
            MuestraIndiceS();



        }
        //Método que muestra el bloque principal del índice primario
        public void MuestraIndiceP(Primario prim)
        {
            /*List<string> AuxT = prim.LTipo1;
            List<long> AuxD = prim.LDir1;
            for (int i = 0; i < AuxT.Count; i++)
            {
                int n = DGPrimario.Rows.Add();
                DGPrimario.Rows[n].Cells[0].Value = AuxT[i];
                DGPrimario.Rows[n].Cells[1].Value = AuxD[i];
            }*/
        }

        //Método que muestra el bloque principal del índice secundario
        public void MuestraIndiceS()
        {
            DGSecundario.Rows.Clear();
            for(int i = 0; i< ISecundario.Count; i++)
            {
                int n = DGSecundario.Rows.Add();
                DGSecundario.Rows[n].Cells[0].Value = ISecundario[i].DirInd;
                DGSecundario.Rows[n].Cells[1].Value = ISecundario[i].Nombre;
                DGSecundario.Rows[n].Cells[2].Value = ISecundario[i].DirB;
                DGSecundario.Rows[n].Cells[3].Value = ISecundario[i].DirIndS;

            }
        }

        //Método que muestra los subbloques del índice secundario
        public void MuestraSubSec()
        {
            DGIndices.Rows.Clear();
            foreach(Secundario sec in ISecundario)
            {
                foreach(EspacioSec cajon in sec.SubSec1)
                {
                    int n = DGIndices.Rows.Add();
                    DGIndices.Rows[n].Cells[0].Value = cajon.DirE1;
                    DGIndices.Rows[n].Cells[1].Value = cajon.DirReg1;
                    DGIndices.Rows[n].Cells[2].Value = cajon.DirESig1;
                }

            }
        }

        //Método para ingresar el elemento que es índice secundario en el bloque principal
        public void AgregaElementoSec()
        {
            fs = File.Open(NomArchIdx, FileMode.Open, FileAccess.ReadWrite);
            //fs.Seek(fs.Length, SeekOrigin.Begin);
            bw = new BinaryWriter(fs);

            for (int i = 0; i < ISecundario.Count; i++)
            {
                Secundario sec = ISecundario[i];
                if(sec.Atrib.TD == 'E')
                {
                    //Cuando se agrega por primera vez
                    if (sec.DirB == -1)
                    {
                        //sec.Nombre = LRegistros[posReg].Elementos[PosSec];
                        //Ciclo para crear el sub bloque y agregar los 50 cajones
                        sec.DirB = fs.Length;
                        for (int j = 0; j < 50; j++)
                        {
                            EspacioSec cajoncito = new EspacioSec(fs.Length, -1, -1);
                            sec.AgregaEsp(cajoncito);
                            cajoncito.EscribeCajon(bw);
                        }
                        sec.SubSec1[i].DirReg1 = LRegistros[posReg].DR;

                        //sec.CreaSubBloque();
                        //sec.SubBloque1[i] = LRegistros[posReg].DR;
                       
                        //sec.EscribeSub(bw);
                        i = ISecundario.Count;
                    }
                    else
                    {
                        int a = 0;
                        MessageBox.Show("Ya esta agregado al bloque principal de secundarios");
                        fs.Seek(sec.DirB, SeekOrigin.Begin);
                        while (a < 50)
                        {
                            if (sec.SubSec1[a].DirReg1 == -1)
                            {
                                sec.SubSec1[a].DirReg1 = LRegistros[posReg].DR;
                                a = ISecundario.Count;
                            }
                            else
                                a++;
                        }
                        i = 49;
                    }
                    
                }
                if(sec.Atrib.TD == 'C')
                {
                    //Cuando se agrega por primera vez 
                    if (sec.Nombre == "aux")
                    {
                        sec.DirB = fs.Length;
                        sec.Nombre = LRegistros[posReg].Elementos[PosSec];
                        for (int j = 0; j < 50; j++)
                        {
                            fs.Seek(fs.Length, SeekOrigin.Begin);
                            EspacioSec cajoncito = new EspacioSec(fs.Length, -1, -1);
                            sec.AgregaEsp(cajoncito);
                            cajoncito.EscribeCajon(bw);
                        }
                        sec.SubSec1[0].DirReg1 = LRegistros[posReg].DR;
                        /*sec.Nombre = LRegistros[posReg].Elementos[PosSec];
                        sec.CreaSubBloque();
                        sec.SubBloque1[i] = LRegistros[posReg].DR;
                        sec.DirB = fs.Length;
                        sec.EscribeSub(bw);*/
                       
                        i = ISecundario.Count;
                    }
                    else if (LRegistros[posReg].Elementos[PosSec] == sec.Nombre)
                    {
                        int a = i;
                        MessageBox.Show("Ya esta agregado al bloque principal de secundarios");
                        fs.Seek(sec.DirB, SeekOrigin.Begin);
                        while (a < 50)
                        {
                            if (sec.SubSec1[a].DirReg1 == -1)
                            {
                                sec.SubSec1[a].DirReg1 = LRegistros[posReg].DR;
                                a = ISecundario.Count;
                            }
                            else
                                a++;
                        }
                        i = ISecundario.Count;
                    }
                    

                }
            }
            posReg++;
            fs.Close();

            ActualizaIndices();
            ImprimeIndice();
            MuestraIndiceS();
            MuestraSubSec();
        }

        //Imprime la lista de indices
        public void ImprimeIndice()
        {
            foreach(Secundario s in ISecundario)
            {
                Debug.WriteLine(s.DirInd + " " + s.Nombre + " " + s.DirB + " " + s.DirIndS);
            }
        }
        //Imprime la información de los registros en consola
        public void ImprimeLista()
        {
            foreach(Registro r in LRegistros)
            {
                Debug.Write(r.DR + " ");
                foreach (string obj in r.Elementos)
                    Debug.Write(obj + " ");
                Debug.WriteLine(r.DSR);
            }
        }

        //Método que muestra la información de los registros en el DG cuando se abre un nuevo registro
        public void ImprimeDataGrid()
        {
            DGRegistros.Rows.Clear();
            int aux = DGRegistros.ColumnCount;
            for (int i = 0; i< LRegistros.Count; i++)
            {
                int n = DGRegistros.Rows.Add();
                DGRegistros.Rows[n].Cells[0].Value = LRegistros[i].DR;
                for (int k = 0; k < LRegistros[i].Elementos.Count; k++)
                    DGRegistros.Rows[n].Cells[k+1].Value = LRegistros[i].Elementos[k];
                DGRegistros.Rows[n].Cells[aux-1].Value = LRegistros[i].DSR;
            }
        }

        //Agrega un nuevo registro a la lista
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            MuestraInf();
            DGCaptura.Rows.Clear();
            if(posPrim != -1 || PosSec != -1)
                AgregaElementoSec();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cabecera = EntAux.DD;
            long aux = 0;
            LRegistros.Clear();

            //abrir.Filter = "Registro de Datos(*.dat)|*.dat"; //Filtro de la extensión del archivo
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                //abrir.FileName es el nombre del archivo seleccionado
                fs = new FileStream(abrir.FileName, FileMode.Open); //Se abre el archivo
                fs.Seek(0, SeekOrigin.Begin);
                //Se crea un BinaryReader y un BinaryWriter
                br = new BinaryReader(fs);
                bw = new BinaryWriter(fs);
                aux = cabecera;
                while (aux != -1)
                {
                    int valor;
                    int tam;
                    reg = new Registro(-1, -1);
                    fs.Seek(aux, SeekOrigin.Begin); //Se posiciona al inicio del archivo
                    long DirReg = br.ReadInt64(); //Lee la dirección del registro
                    for (int i = 0; i < EntAux.LAtributo1.Count; i++)
                    {
                        if (EntAux.LAtributo1[i].TD == 'E')
                        {
                            valor = br.ReadInt32();
                            reg.Elementos.Add(valor.ToString());
                        }
                        else if (EntAux.LAtributo1[i].TD == 'C')
                        {
                            string valor2 = "";
                            tam = EntAux.LAtributo1[i].LD;
                            char[] nombre = br.ReadChars(tam);
                            for (int j = 0; j < nombre.Length; j++)
                            {
                                if (char.IsLetter(nombre[j]) || nombre[j] =='_')
                                    valor2 += nombre[j];
                                else
                                    j = tam;
                            }
                            reg.Elementos.Add(valor2);
                        }
                    }
                    long DirSReg = br.ReadInt64(); //Lee la dirección del siguiente registro
                    reg.DR = DirReg;
                    reg.DSR = DirSReg;
                    aux = DirSReg;
                    LRegistros.Add(reg);
                }
                fs.Close();
                //ImprimeLista();
                ImprimeDataGrid();
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < DGCaptura.ColumnCount; j++)
            {
                string aux = DGCaptura.Rows[0].Cells[j].Value.ToString();
                DGRegistros.Rows[pos].Cells[j + 1].Value = aux;
                LRegistros[pos].Elementos[j] = aux;
            }
            DGCaptura.Rows.Clear();
            Actualizar();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string PElem = DGCaptura.Rows[0].Cells[0].Value.ToString();
            for(int i = 0; i < LRegistros.Count; i++)
            {
                string aux = LRegistros[i].Elementos[0];
                if (PElem == aux)
                    LRegistros.RemoveAt(i);
            }
            DGCaptura.Rows.Clear();
            Actualizar();
        }

       
        private void DGRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            for (int i = 0; i < DGCaptura.ColumnCount; i++)
            {
                string aux;
                aux = DGRegistros.Rows[pos].Cells[i + 1].Value.ToString();
                Debug.WriteLine(aux);
                DGCaptura.Rows[0].Cells[i].Value = aux;
             }

        }


        private void AbrirIndice_Click(object sender, EventArgs e)
        {
            long aux = 0;
            
            ISecundario.Clear();
            for (int i = 0; i < EntAux.LAtributo1.Count; i++)
            {
                Atributo a = EntAux.LAtributo1[i];
                if (a.TI == 3)
                {
                    cabeceraInd = a.DI;
                    PosSec = i;
                    atrib = a;
                }
            }

            fs = new FileStream(NomArchIdx, FileMode.Open); //Se abre el archivo
            fs.Seek(0, SeekOrigin.Begin);
            //Se crea un BinaryReader y un BinaryWriter
            br = new BinaryReader(fs);
            bw = new BinaryWriter(fs);
            aux = cabeceraInd;
            while(aux != -1)
            {
                fs.Seek(aux, SeekOrigin.Begin);
                string nomb ="";
                if(atrib.TI == 3)
                {
                    if(atrib.TD == 'E')
                    {

                    }
                    if(atrib.TD == 'C')
                    {
                        char[] Nombre = br.ReadChars(atrib.LD);
                        foreach(char c in Nombre)
                        {
                            if (char.IsLetter(c))
                                nomb += c;
                        }
                        long dirB = br.ReadInt64();

                        /*if(dirB != -1)
                        {
                            long aux2 = dirB;
                            while(aux2 != -1)
                            {
                                fs.Seek(aux2, SeekOrigin.Begin);

                            }
                        }*/
                    }
                }
            }

        }

    }
}
