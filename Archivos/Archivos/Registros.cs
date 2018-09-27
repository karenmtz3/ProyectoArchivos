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

        

        private List<Registro> LRegistros;
        private Registro reg;

        long TamArch;

        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;


        public Entidad EntAux1 { get => EntAux; set => EntAux = value; }

        public Registros()
        {
            InitializeComponent();
            LRegistros = new List<Registro>();

        }
        public void CreaColumnas()
        {
            label2.Text = EntAux.NE;

            //Ciclos que agregará las columnas dependiendo de la cantidad de atributos que tiene la entidad
            foreach (Atributo atrib in EntAux.LAtributo1)
            {
                DataGridViewTextBoxColumn colum = new DataGridViewTextBoxColumn();
                colum.HeaderText = atrib.NA;
                colum.Width = 100;
                DGRegistros.Columns.Add(colum);
                
            }
            //DataGrid para hacer las capturas de los registros
            foreach (Atributo atrib in EntAux.LAtributo1)
            {
                DataGridViewTextBoxColumn colum = new DataGridViewTextBoxColumn();
                colum.HeaderText = atrib.NA;
                colum.Width = 100;
                DGCaptura.Columns.Add(colum);
            }
        }

        public void MuestraDatos()
        {
            int n = DGRegistros.Rows.Add();
            fs = File.Open(NomArch, FileMode.Open, FileAccess.ReadWrite);
            TamArch = fs.Length;
            fs.Seek(TamArch, SeekOrigin.Begin);
            bw = new BinaryWriter(fs);


            reg = new Registro(TamArch, -1);
            Debug.Write("DR: " + reg.DR + " ");
            Debug.WriteLine("DSR: " + reg.DSR);

            for (int i = 0; i < DGCaptura.ColumnCount; i++)
            {
                int ent;
                string cad;
                foreach (Atributo a in EntAux.LAtributo1)
                {
                    if (a.NA == DGCaptura.Columns[i].HeaderText)
                    {
                        if (a.TD == 'E')
                        {
                            ent = Convert.ToInt32(DGCaptura.Rows[0].Cells[i].Value);
                            DGRegistros.Rows[n].Cells[i].Value = ent;
                            reg.LO.Add(ent);
                            break;
                        }
                        else if (a.TD == 'C')
                        {
                            cad = (string)DGCaptura.Rows[0].Cells[i].Value;
                            DGRegistros.Rows[n].Cells[i].Value = cad;
                            reg.Tam = a.LD;
                            reg.LO.Add(cad);
                            break;
                        }
                    }
                }
            }
            LRegistros.Add(reg);
            reg.Escribe(bw);
            fs.Close();
        }
        public void imprime()
        {
            foreach (Registro r in LRegistros)
                foreach (object obj in r.LO)
                    Debug.WriteLine(obj.ToString());
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

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            MuestraDatos();
            DGCaptura.Rows.Clear();
        }
    }
}
