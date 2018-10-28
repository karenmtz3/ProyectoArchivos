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
        long TamArch;
        int pos;
        long cabecera;

        private List<Registro> LRegistros;
        private Registro reg;

        DataGridViewTextBoxColumn inicio;
        DataGridViewTextBoxColumn fin;

        OpenFileDialog abrir;
        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;

        int cb;

        IndicePrim IPrimario;
        IndiceSec ISecundario;

        public Entidad EntAux1 { get => EntAux; set => EntAux = value; }
        internal List<Registro> LRegistros1 { get => LRegistros; set => LRegistros = value; }

        public Registros()
        {
            InitializeComponent();
            LRegistros = new List<Registro>();
            inicio = new DataGridViewTextBoxColumn();
            fin = new DataGridViewTextBoxColumn();
            abrir = new OpenFileDialog();

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
        //Se actualiza la infromación y se escribe en el archivo
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
        // LEntidades = LEntidades.OrderBy(o => o.NE).ToList();
        /*LRegistros = LRegistros.OrderBy(o => o.Elementos[2]).ToList();
        ImprimeLista();*/
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
            foreach (Atributo atrib in EntAux.LAtributo1)
            {
                if (atrib.TI != 0 || atrib.TI != 1)
                    CreaIndices(atrib);
            }
        }

        //Crea el archivo de índices
        public void CreaIndices(Atributo atributo)
        {
            NomArch = EntAux.NE + ".idx";
            fs = new FileStream(NomArch, FileMode.OpenOrCreate);
            bw = new BinaryWriter(fs);
            
            if (atributo.TI == 2)
            {
                fs.Seek(fs.Length, SeekOrigin.Begin);
                IPrimario = new IndicePrim(atributo);
                IPrimario.CreaBPrincipal();
                MuestraIndiceP(IPrimario);
                IPrimario.EscribeBPrincipal(bw);
            }
            if(atributo.TI == 3)
            {
                Debug.WriteLine(fs.Length);
                fs.Seek(fs.Length, SeekOrigin.Begin);
                ISecundario = new IndiceSec(atributo);
                ISecundario.BPrincipalSec();
                MuestraIndicieS(ISecundario);
                ISecundario.EscribeBSec(bw);
                Debug.WriteLine(fs.Length);
            }
            fs.Close();

        }
        //Método que muestra el bloque principal del índice primario
        public void MuestraIndiceP(IndicePrim prim)
        {
            List<string> AuxT = prim.LTipo1;
            List<long> AuxD = prim.LDir1;
            for (int i = 0; i < AuxT.Count; i++)
            {
                int n = DGPrimario.Rows.Add();
                DGPrimario.Rows[n].Cells[0].Value = AuxT[i];
                DGPrimario.Rows[n].Cells[1].Value = AuxD[i];
            }
        }

        //Método que muestra el bloque principal del índice secundario
        public void MuestraIndicieS(IndiceSec sec)
        {
            List<string> AuxInf = sec.LInf1;
            List<long> AuxD = sec.LDir1;
            for(int i = 0; i < AuxInf.Count; i++)
            {
                int n = DGSecundario.Rows.Add();
                DGSecundario.Rows[n].Cells[0].Value = AuxInf[i];
                DGSecundario.Rows[n].Cells[1].Value = AuxD[i]; 
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
                                if (char.IsLetter(nombre[j]))
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
                DGCaptura.Rows[0].Cells[i].Value = aux; ;

             }

        }
    }
}
