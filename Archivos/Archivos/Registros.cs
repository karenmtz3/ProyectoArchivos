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

        bool AbrirArch;

        private List<Registro> LRegistros;
        private Registro reg;

        DataGridViewTextBoxColumn inicio;
        DataGridViewTextBoxColumn fin;

        OpenFileDialog abrir;
        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;

        int cb;

        int PosSec;
        int posPrim;

        Indice Ind;

        public Entidad EntAux1 { get => EntAux; set => EntAux = value; }
        internal List<Registro> LRegistros1 { get => LRegistros; set => LRegistros = value; }
        internal Indice Ind1 { get => Ind; set => Ind = value; }

        public Registros()
        {
            InitializeComponent();
            LRegistros = new List<Registro>();
            inicio = new DataGridViewTextBoxColumn();
            fin = new DataGridViewTextBoxColumn();
            abrir = new OpenFileDialog();

            AbrirArch = false;

           posPrim = PosSec = -1;

        }
        #region Registros
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
            for(int i = 0; i < EntAux.LAtributo1.Count; i++)
            {
                Atributo atrib = EntAux.LAtributo1[i];
                DataGridViewTextBoxColumn colum = new DataGridViewTextBoxColumn();
                colum.HeaderText = atrib.NA;
                colum.Width = 100;
                DGCaptura.Columns.Add(colum);
                //Se verifica para crear el archivo de índices
                if (atrib.TI == 2)
                {
                    CreaArchIndice(atrib.TD, 1);
                    posPrim = i;
                }
                else if(atrib.TI == 3)
                {
                    CreaArchIndice(atrib.TD, 2);
                    PosSec = i;
                }
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
            SubPrimario();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cabecera = EntAux.DD;
            long aux = 0;
            AbrirArch = true;
            CreaColumnas();
            LRegistros.Clear();

            fs = new FileStream(NomArch, FileMode.Open); //Se abre el archivo
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
            string elimInd = DGCaptura.Rows[0].Cells[posPrim].Value.ToString();
            Debug.WriteLine(elimInd);
            BuscaEnPrincipal(elimInd);
            for (int i = 0; i < LRegistros.Count; i++)
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
                //Debug.WriteLine(aux);
                DGCaptura.Rows[0].Cells[i].Value = aux;
             }

        }
        #endregion
        #region índices
     

        //Método que crea el archivo de índices
        public void CreaArchIndice(char t,int TipoIndice)
        {
            NomArchIdx = EntAux.NE + ".idx";
            if (AbrirArch)
                AbrirIdx();
            else
            {
                
                //Se crea un nuevo FileStream con el nombre de la entidad y con extensión 'idx'
                fs = new FileStream(NomArchIdx, FileMode.OpenOrCreate);
                bw = new BinaryWriter(fs); //Se crea un BinaryWriter
                Ind = new Indice(TipoIndice);

                if (t == 'E')
                {

                    Ind.CreaIndice('E', bw, fs.Length);
                }
                else if (t == 'C')
                {
                    Ind.CreaIndice('C', bw, fs.Length);
                }
                MuestraPrimario();
                fs.Close(); //Se cierra el archivo
            }
        }

        public void AbrirIdx()
        {
            for (int i = 0; i < EntAux.LAtributo1.Count; i++)
            {
                Atributo a = EntAux.LAtributo1[i];
                if (a.TI == 2)//Si es índice primario 
                {
                    Ind = new Indice(1);
                    posPrim = i;
                    long AuxDir = EntAux.LAtributo1[posPrim].DI;
                    fs = new FileStream(NomArchIdx, FileMode.Open);
                    fs.Seek(AuxDir, SeekOrigin.Begin);
                    br = new BinaryReader(fs);
                    if (a.TD == 'E')
                    {
                        Ind.Abrir(a.TD, br);
                    }
                    else if (a.TD == 'C')
                    {
                        Ind.Abrir(a.TD, br);
                    }
                    fs.Close();
                    MuestraPrimario();
                    break;
                }
                /* if (a.TI == 3) //Si es índice secundario
                 {
                     Ind = new Indice(2);
                     PosSec = i;
                 }*/
            }
        }


        #region IndicePrimario
        //Método que muestra el bloque principal del índice primario
        public void MuestraPrimario()
        {
            DGPrimario.Rows.Clear();
            foreach(BloquePrincipal b in Ind.Prim.BPrincipal1)
            {
                int n = DGPrimario.Rows.Add();
                DGPrimario.Rows[n].Cells[0].Value = b.Valor;
                DGPrimario.Rows[n].Cells[1].Value = b.Dir;
            }
        }

        //Método que muestra el sub bloque de primario
        public void MuestraSubPrim()
        {
            DGSubPrim.Rows.Clear();
            Ind.Prim.SubBloque1 = Ind.Prim.SubBloque1.OrderBy(o => o.Valor).ToList();
            foreach(BloquePrincipal b in Ind.Prim.SubBloque1)
            {
                if (b.Dir != -1)
                {
                    int n = DGSubPrim.Rows.Add();
                    DGSubPrim.Rows[n].Cells[0].Value = b.Valor;
                    DGSubPrim.Rows[n].Cells[1].Value = b.Dir;
                }
            }
        }

        //Se busca el espacio de donde pertenece el valor a eliminar
        public void BuscaEnPrincipal(string nombre)
        {
            fs = new FileStream(NomArchIdx, FileMode.Open, FileAccess.Read);
            br = new BinaryReader(fs);
            char[] r = nombre.ToCharArray();
            for(int i = 0; i < Ind.Prim.BPrincipal1.Count;i++)
            {
                BloquePrincipal b = Ind.Prim.BPrincipal1[i];
                char letra = Convert.ToChar(b.Valor);
                if (char.ToUpper(r[0]) == letra)
                {
                    fs.Seek(b.Dir, SeekOrigin.Begin);
                   
                    int tam = EntAux.LAtributo1[posPrim].LD;
                    Ind.Prim.ActualizaSub(br, tam);
                    fs.Close();

                    Ind.Prim.Elimina(nombre);
                    fs = new FileStream(NomArchIdx, FileMode.Open, FileAccess.Write);
                    fs.Seek(b.Dir, SeekOrigin.Begin);
                    bw = new BinaryWriter(fs);
                    Ind.Prim.ActualizaSubBloque(bw);
                    fs.Close();
                }
            }
            MuestraSubPrim();
        }
        
        //Se crean los sub bloques del primario para agregar un elemento
        public void SubPrimario()
        {
            fs = new FileStream(NomArchIdx, FileMode.Open, FileAccess.Write);
            string auxR = reg.Elementos[posPrim];
            char[] R = auxR.ToCharArray();
            for(int i = 0; i < Ind.Prim.BPrincipal1.Count; i++)
            {
                BloquePrincipal b = Ind.Prim.BPrincipal1[i];
                char letra = Convert.ToChar(b.Valor);
                if(char.ToUpper(R[0]) == letra)
                {
                    //Condición para crear el sub bloque 
                    if(b.Dir == -1)
                    {
                        //Se actualiza el bloque principal
                        b.Dir = fs.Length;
                        fs.Seek(Ind.TamPrinPrim1, SeekOrigin.Begin);
                        bw = new BinaryWriter(fs);
                        Ind.Prim.ActualizaPrincipal(bw);

                        //Se escribe el sub bloque al final del archivo
                        fs.Seek(fs.Length, SeekOrigin.Begin);
                        bw = new BinaryWriter(fs);
                        int tam = EntAux.LAtributo1[posPrim].LD;
                        Ind.Prim.CreaSubBloque(reg,tam,posPrim,bw);
                        fs.Close();
                        i = Ind.Prim.BPrincipal1.Count;
                    }
                    else
                    {
                        AgregaElemASubPrim(i);
                    }
                }
            }
            MuestraPrimario();
            MuestraSubPrim();

        }

        //Método para agregar más elementos al sub-bloque
        public void AgregaElemASubPrim(int posI)
        {
            fs.Close();
            fs = new FileStream(NomArchIdx, FileMode.Open, FileAccess.Read);
            br = new BinaryReader(fs);
            fs.Seek(Ind.Prim.BPrincipal1[posI].Dir, SeekOrigin.Begin);
            int tam = EntAux.LAtributo1[posPrim].LD;
            Ind.Prim.ActualizaSub(br,tam);
            fs.Close();

            fs = new FileStream(NomArchIdx, FileMode.Open, FileAccess.Write);
            fs.Seek(Ind.Prim.BPrincipal1[posI].Dir, SeekOrigin.Begin);
            bw = new BinaryWriter(fs);
            Ind.Prim.AgregaElem(reg, posPrim,bw);
            fs.Close();
            MuestraSubPrim();

           
        }
        //Si se selecciona una celda con dirección de sub bloque se visualizará la información en el DGSubPrim
        private void DGPrimario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long aux = Convert.ToInt64(DGPrimario[1, e.RowIndex].Value);
            if (aux != -1)
            {
                fs = new FileStream(NomArchIdx, FileMode.Open, FileAccess.Read);
                fs.Seek(aux, SeekOrigin.Begin);
                br = new BinaryReader(fs);
                int tam = EntAux.LAtributo1[posPrim].LD;
                Ind.Prim.ActualizaSub(br,tam);
                fs.Close();
                MuestraSubPrim();
               
            }
            else
                MessageBox.Show("No existe sub bloque");

        }
        #endregion

        #region IndiceSecundario

        #endregion



        #endregion

        private void AbrirIndice_Click(object sender, EventArgs e)
        {
           /* long aux = 0;
            
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
                   // }
               // }
           // }

        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaColumnas();
        }
    }
}
