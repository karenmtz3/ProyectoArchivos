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
        public Indice(int TipoInd)
        {
            TipoIndice = TipoInd;
        }

        public void CreaIndice(char t, BinaryWriter bw, long tam)
        {
            if(TipoIndice == 1)
            {
                TamPrinPrim = tam;
                prim = new Primario(t);
                prim.CreaBloquePrincipal(bw);
            }
           /* if (TipoIndice == 2)
                sec = new Secundario(t);*/
        }

        public void Abrir(char t, BinaryReader br)
        {
            if(TipoIndice == 1)
            {
                prim = new Primario(t);
                prim.LeePrincipal(br);
            }
        }

        public Primario Prim { get => prim; set => prim = value; }
        public int TipoIndice1 { get => TipoIndice; set => TipoIndice = value; }
        internal Secundario Sec { get => sec; set => sec = value; }
        public long TamPrinPrim1 { get => TamPrinPrim; set => TamPrinPrim = value; }
    }
}
