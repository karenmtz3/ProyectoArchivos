using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    class Indice
    {
        Primario prim;
        Secundario sec;
        int TipoIndice;
        long TamPrinPrim;

        List<Secundario> secundarios;
        List<int> pos;
        public Indice(int TipoInd)
        {
            TipoIndice = TipoInd;
            secundarios = new List<Secundario>();
            pos = new List<int>();
        }

        //Metodo para inicializar la instancia de primario
        public void CreaIndice(char t, BinaryWriter bw, long tam)
        {
            if(TipoIndice == 1)
            {
                TamPrinPrim = tam;
                prim = new Primario(t);
                prim.CreaBloquePrincipal(bw);
            }
        }

        //Método que lee agrega secundarios a la lista de secundarios
        public void AgregaSec(char t, long tam, int LD, string atributo)
        { // t es el tipo de dato y atributo es el nombre del atributo
            sec = new Secundario(t, atributo);
            sec.TamPrin1 = tam;
            secundarios.Add(sec);
        }

        //Método para inicializar la instancia de secundario
        public void CreaSec(char t, BinaryWriter bw, long tam, int LD, string atributo)
        {
            sec = new Secundario(t,atributo);
            sec.TamPrin1 = tam;
            secundarios.Add(sec);
            sec.CreaPrincipal(bw,LD);

        }

        //Llama al método que se encarga de leer el bloque principal
        public void Abrir(char t, BinaryReader br)
        {
            if(TipoIndice == 1)
            {
                prim = new Primario(t);
                prim.LeePrincipal(br);
            }
        }
        //Método que encuntra la posición del atributo al que se le creará el bloque principal o se agregará una dirección
        public int EncuentraSec(string NA)
        {
            int pos = -1;
            for (int i = 0; i < secundarios.Count; i++)
                if (secundarios[i].NombreAtrib1 == NA)
                {
                    pos = i;
                    break;
                }
            return pos;
        }

        public Primario Prim { get => prim; set => prim = value; }
        public int TipoIndice1 { get => TipoIndice; set => TipoIndice = value; }
        internal Secundario Sec { get => sec; set => sec = value; }
        public long TamPrinPrim1 { get => TamPrinPrim; set => TamPrinPrim = value; }
        public List<int> Pos { get => pos; set => pos = value; }
        internal List<Secundario> Secundarios { get => secundarios; set => secundarios = value; }
    }
}
