using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    class Arbol
    {
        List<Nodo> LNodos;
        List<Nodo> H;
        List<Nodo> I;
        Nodo Raiz;
        private long tam;
        public Arbol()
        {
            LNodos = new List<Nodo>();
            H = new List<Nodo>();
            I = new List<Nodo>();
            Raiz = new Nodo(-1, 'R');
        }

        public List<Nodo> LNodos1 { get => LNodos; set => LNodos = value; }
        public long Tam { get => tam; set => tam = value; }
        public List<Nodo> H1 { get => H; set => H = value; }
        public List<Nodo> I1 { get => I; set => I = value; }
        public Nodo Raiz1 { get => Raiz; set => Raiz = value; }

        //Método para buscar los niveles
        public bool BuscaNivel(Nodo Aux)
        {
            bool HayNivel = false;
            foreach (Nodo n in LNodos)
            {
                if (n.TipoNodo1 == 'I')
                {
                    if (Aux.Direcciones1[0] == n.DirNodo1)
                    {
                        HayNivel = true;
                        break;
                    }
                }
            }
            return HayNivel;
        }



        //Método para encontrar la raíz
        public long CabArbol()
        {
            long dir = -1;
            if (LNodos.Count == 1)
                dir = LNodos[0].DirNodo1;
            for(int i = 0; i < LNodos.Count; i++)
            {
                Nodo n = LNodos[i];
                if(n.TipoNodo1 == 'R')
                {
                    dir = n.DirNodo1;
                    break;
                }
            }
            return dir;
        }

        //Método para leer la raíz 
        public void LeeArbol(BinaryReader br)
        {
            long DN = br.ReadInt64();
            char t = br.ReadChar();
            Nodo R = new Nodo(DN, t);
            for(int i = 0; i < 4; i++)
            {
                long d = br.ReadInt64();
                int cb = br.ReadInt32();
                R.Direcciones1.Add(d);
                R.Claves1.Add(cb);
            }
            R.Direcciones1.Add(br.ReadInt64());
            LNodos.Add(R);
        }

        //Método para leer los hijos de la raíz
        public void LeeHijos(BinaryReader br)
        {
            long DAux = br.ReadInt64();
            char t = br.ReadChar();
            Nodo Hijo = new Nodo(DAux, t);
            for(int i = 0; i < 4; i++)
            {
                long d = br.ReadInt64();
                int cb = br.ReadInt32();
                Hijo.Direcciones1.Add(d);
                Hijo.Claves1.Add(cb);
            }
            Hijo.Direcciones1.Add(br.ReadInt64());
            LNodos.Add(Hijo);
        }
        //Método para crear nodos
        public void CreaNodo(long dirn, char tipo, long dirR, int cb, BinaryWriter bw)
        {
            Nodo n = new Nodo(dirn, tipo);
            n.InicializaListas();
            for(int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    n.Direcciones1[i] = dirR;
                    n.Claves1[i] = cb;
                }
            }
            LNodos.Add(n);
            H.Add(n);
            n.EscribeNodo(bw);
        }
    }
}
