﻿using System;
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
        private long TamArch;
        private int pos;
        private long cabecera;

        private bool AbrirArch;

        private List<Registro> LRegistros;
        private Registro reg;

        DataGridViewTextBoxColumn inicio;
        DataGridViewTextBoxColumn fin;

        OpenFileDialog abrir;
        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;

        private int cb;

        //índices secundarios
        List<Atributo> AtribSec;
        Secundario ElemSec;
        int PosSec;
        bool ExisteSec;

        bool ExisteIndice;

        private int posPrim;

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

            ExisteIndice = ExisteSec = false;
            AbrirArch = false;

            AtribSec = new List<Atributo>();

            //PosSec = - 1;
           posPrim = -1;

        }
        #region Registros
        //Se crea las columnas dinámicamente de acuerdo a los atributos que tiene una entidad
        public void CreaColumnas()
        {
            label2.Text = EntAux.NE;
            //bool P = false, S = false;
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
                if (atrib.TI == 2 || atrib.TI == 3)
                {
                    //P = true;
                    CreaArchIndice(atrib.TD, 1);
                    posPrim = i;
                    //i = EntAux.LAtributo1.Count;
                }
                /*else if(atrib.TI == 5)
                {
                    AtribSec.Add(atrib);
                    if (AbrirArch)
                        ExisteSec = true;
                    //S = true;
                    //CreaArchIndice(atrib.TD, 2);
                    //PosSec = i;
                }*/
            }
            if (AtribSec.Count > 0 && ExisteSec == false)
                CreaArchIndice(AtribSec[0].TD, 2);
            //if (ExisteSec == true)
               // CreaArchIndice(AtribSec[0].TD, 2);
            
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
            for(int i = 0; i < DGCaptura.ColumnCount; i++)
            {
                Atributo a = EntAux.LAtributo1[i];
                if(a.TI == 2) //Si es índice primario crea el bloque principal
                    SubPrimario();
                else if(a.TI == 3) //Si hay índices secudarios, agrega el valor al bloque principal y la dirección al sub-bloque
                {
                    string v = (string)DGCaptura.Rows[0].Cells[i].Value;
                    AgregaPrinSec(a.NA, v,a.LD);
                }
            }
            DGCaptura.Rows.Clear();
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
            //Se guarda el registro a modificar
            Registro r = LRegistros[pos];
            string IndPrim = "";
            if(posPrim != -1)// || PosSec !=-1
                //Se guarda el elemento que es indice primario
                IndPrim = LRegistros[pos].Elementos[posPrim];
            for (int j = 0; j < DGCaptura.ColumnCount; j++)
            {
                //Se captura el valor que está renglón 0 columna j
                string aux = DGCaptura.Rows[0].Cells[j].Value.ToString();
                //se pasan los nuevos valores en las posiciones correspondientes en el DG de registros
                DGRegistros.Rows[pos].Cells[j + 1].Value = aux;
                //Se pasan los nuevos valores a la lista de registros
                LRegistros[pos].Elementos[j] = aux;
            }
            DGCaptura.Rows.Clear();
            Actualizar();

            if(posPrim !=-1)
            {
                reg = LRegistros[pos];
                string nuevo = LRegistros[pos].Elementos[posPrim];
                if(IndPrim != nuevo)
                {
                    BuscaEnPrincipal(IndPrim);
                    SubPrimario();
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string PElem = DGCaptura.Rows[0].Cells[0].Value.ToString();
            string elimPrim = "";
            if (posPrim != -1)
            {
                elimPrim = DGCaptura.Rows[0].Cells[posPrim].Value.ToString();
                Debug.WriteLine(elimPrim);
                BuscaEnPrincipal(elimPrim);
            }
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
            {
                if (ExisteIndice == false)
                {
                    Ind = new Indice(1);
                    ExisteIndice = true;
                    AbrirIdx();
                }
            }
            else
            {
                if(ExisteIndice == true)
                    //Se crea un nuevo FileStream con el nombre de la entidad y con extensión 'idx'
                    fs = new FileStream(NomArchIdx, FileMode.OpenOrCreate);
                else
                {
                    Ind = new Indice(1);
                    fs = new FileStream(NomArchIdx, FileMode.OpenOrCreate);
                    ExisteIndice = true;

                }
                bw = new BinaryWriter(fs); //Se crea un BinaryWriter


                if (TipoIndice == 2)//para índice secundarios
                {

                    for (int i = 0; i < AtribSec.Count; i++)
                    {
                        Atributo a = AtribSec[i];
                        Debug.WriteLine(fs.Length);
                        fs.Seek(fs.Length, SeekOrigin.Begin);
                        Ind.CreaSec(a.TD, bw, fs.Length, a.LD, a.NA);
                    }
                    Debug.WriteLine(fs.Length);
                }
                else if (TipoIndice == 1)//para índice primario
                {
                    Ind = new Indice(TipoIndice);
                    if (t == 'E')
                        Ind.CreaIndice('E', bw, fs.Length);
                    else if (t == 'C')
                        Ind.CreaIndice('C', bw, fs.Length);

                    MuestraPrimario();
                }
                fs.Close(); //Se cierra el archivo
            }
        }

        public void AbrirIdx()
        {
            AtribSec.Clear();
            for (int i = 0; i < EntAux.LAtributo1.Count; i++)
            {
                Atributo a = EntAux.LAtributo1[i];
                if (a.TI == 2)//Si es índice primario 
                {
                   // Ind = new Indice(1);
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
                    MuestraPrimario();
                    //break;
                }
                 if (a.TI == 3) //Si es índice secundario
                 {
                    AtribSec.Add(a);
                    ExisteSec = true;
                 }
            }
            fs.Close();
            if (ExisteSec == true)
            {
                //Ind = new Indice(2);
                foreach(Atributo a in AtribSec)
                {
                    Ind.AgregaSec(a.TD, a.DI, a.LD, a.NA);
                }
                LlenaCB();
                ExisteSec = false;
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
        //Para índice primario
        public void BuscaEnPrincipal(string nombre)
        {
            fs = new FileStream(NomArchIdx, FileMode.Open, FileAccess.Read);
            br = new BinaryReader(fs);
            char[] r = nombre.ToCharArray();

            if(Ind.Prim.TipoDato1 == 'E')
            {
                for(int i = 0; i < Ind.Prim.BPrincipal1.Count; i++)
                {
                    BloquePrincipal b = Ind.Prim.BPrincipal1[i];
                    char num = Convert.ToChar(b.Valor);
                    if(r[0] == num)
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
                        i = Ind.Prim.BPrincipal1.Count;
                    }
                }
            }
            else if (Ind.Prim.TipoDato1 == 'C')
            {
                for (int i = 0; i < Ind.Prim.BPrincipal1.Count; i++)
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
                        i = Ind.Prim.BPrincipal1.Count;
                    }
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
            if (Ind.Prim.TipoDato1 == 'E')
            {
                for (int i = 0; i < Ind.Prim.BPrincipal1.Count; i++)
                {
                    BloquePrincipal b = Ind.Prim.BPrincipal1[i];
                    char numero = Convert.ToChar(b.Valor);
                    if (R[0] == numero)
                    {
                        //Condición para crear el sub bloque 
                        if (b.Dir == -1)
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
                            Ind.Prim.CreaSubBloque(reg, tam, posPrim, bw);
                            fs.Close();
                            i = Ind.Prim.BPrincipal1.Count;
                        }
                        else
                            AgregaElemASubPrim(i);
                    }
                }
            }
            else if (Ind.Prim.TipoDato1 == 'C')
            {
                for (int i = 0; i < Ind.Prim.BPrincipal1.Count; i++)
                {
                    BloquePrincipal b = Ind.Prim.BPrincipal1[i];
                    char letra = Convert.ToChar(b.Valor);
                    if (char.ToUpper(R[0]) == letra)
                    {
                        //Condición para crear el sub bloque 
                        if (b.Dir == -1)
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
                            Ind.Prim.CreaSubBloque(reg, tam, posPrim, bw);
                            fs.Close();
                            i = Ind.Prim.BPrincipal1.Count;
                        }
                        else
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
        //Método que llena el combo box de los atributos que son índices secundarios
        public void LlenaCB()
        {
            //Se limpia el combo box de los atributos 
            CBAtribSec.Items.Clear();
            //Ciclo para agregar las entidades al combo box
            foreach (Atributo atrib in AtribSec)
                CBAtribSec.Items.Add(atrib.NA);
        }


        //Método para agregar elementos al bloque principal 
        public void AgregaPrinSec(string NA, string valor, int tam)
        {
            fs = File.Open(NomArchIdx, FileMode.Open, FileAccess.ReadWrite);
            br = new BinaryReader(fs);
            int PS = Ind.EncuentraSec(NA);
            Secundario sec = Ind.Secundarios[PS];
            fs.Seek(sec.TamPrin1, SeekOrigin.Begin);
            sec.LeePricipal(br, tam);
            for (int i = 0; i < sec.Principal1.Count; i++)
            {
                BloquePrincipal b = sec.Principal1[i];
                Debug.WriteLine(b.Valor);
                string aux2 = int.MaxValue.ToString();
                if(b.Valor == "" || b.Valor == aux2) // si lo que esta en el bloque es vacío se agrega el elemento
                {
                    //Se asignan los nuevos valores y se actualiza el bloque principal
                    b.Valor = valor;
                    b.Dir = fs.Length;
                    fs.Seek(sec.TamPrin1, SeekOrigin.Begin);
                    bw = new BinaryWriter(fs);
                    sec.ActualizaPrincipal(bw,tam);

                    //una vez actualizado se crea el sub-bloque de direcciones
                    fs.Seek(fs.Length, SeekOrigin.Begin);
                    bw = new BinaryWriter(fs);
                    sec.CreaSubBloque(reg.DR,bw);
                    fs.Close();
                    i = sec.Principal1.Count;
                }
                else if(b.Valor == valor) //Si se encuentra el elemento en el bloque principal se agrega la dirección al sub-bloque
                {
                    long P = sec.Principal1[i].Dir;
                    AgregaDirSec(sec,P, reg.DR);
                    i = sec.Principal1.Count;
                    fs.Close();
                }
            }
            //fs.Close();
        }

        //Método que agrega direcciónes al sub-bloque
        public void AgregaDirSec(Secundario s,long dir, long DR)
        {
            fs.Seek(dir, SeekOrigin.Begin);
            br = new BinaryReader(fs);
            s.LlenaSub(br); //Se leer el sub-bloque

            fs.Seek(dir, SeekOrigin.Begin);
            bw = new BinaryWriter(fs);
            s.AgregaSub(DR, bw);

        }
        //Método que busca el atributo que se selecciono en el combo box
        public void BuscaSecundario()
        {
            //Ciclo para recorrer la lista de entidades
            for (int i = 0; i < AtribSec.Count; i++)
            {
                //Se busca el nombre de la entidad a modificar en la lista de entidades
                if (CBAtribSec.Text == AtribSec[i].NA)
                {
                    PosSec = i;
                    //AtributoS = AtribSec[i];
                }
            }
        }

        //Método para visualizar el bloque principal de secundarios
        public void MuestraPrinSec()
        {
            DGSecundario.Rows.Clear();
            ElemSec.Principal1 = ElemSec.Principal1.OrderBy(o => o.Valor).ToList();
            foreach (BloquePrincipal b in ElemSec.Principal1)
            {
                if (b.Dir != -1)
                {
                    int n = DGSecundario.Rows.Add();
                    DGSecundario.Rows[n].Cells[0].Value = b.Valor;
                    DGSecundario.Rows[n].Cells[1].Value = b.Dir;
                }
            }
        }

        //Método para visualizar el sub-bloque de secundarios
        public void MuestraSubSec()
        {
            DGSubSec.Rows.Clear();
            foreach (long dir in ElemSec.SubB1)
            {
                if (dir != -1)
                {
                    int n = DGSubSec.Rows.Add();
                    DGSubSec.Rows[n].Cells[0].Value = dir;
                }
            }
        }
        #endregion

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaColumnas();
            LlenaCB();
        }
        
        public int LongDato(string CBSec)
        {
            int LD = 0;
            foreach (Atributo a in EntAux.LAtributo1)
            {
                if (a.NA == CBSec)
                {
                    LD = a.LD;
                    break;
                }
            }
            return LD;
        }

        private void CBAtribSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscaSecundario();
            DGSubSec.Rows.Clear();
            int tam = LongDato(CBAtribSec.Text);
            ElemSec = Ind.Secundarios[PosSec];
            Debug.WriteLine(ElemSec.NombreAtrib1);
            fs = new FileStream(NomArchIdx, FileMode.Open, FileAccess.ReadWrite);
            fs.Seek(ElemSec.TamPrin1, SeekOrigin.Begin);
            br = new BinaryReader(fs);
            ElemSec.LeePricipal(br, tam);
            fs.Close();
            MuestraPrinSec();
        }

        private void DGSecundario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long aux = Convert.ToInt64(DGSecundario[1, e.RowIndex].Value);
            if (aux != -1)
            {
                fs = new FileStream(NomArchIdx, FileMode.Open, FileAccess.Read);
                fs.Seek(aux, SeekOrigin.Begin);
                br = new BinaryReader(fs);
                BuscaSecundario();
                ElemSec = Ind.Secundarios[PosSec];
                ElemSec.LeeSub(br);
                fs.Close();
                MuestraSubSec();

            }
            else
                MessageBox.Show("No existe sub bloque");
        }
    }
}
