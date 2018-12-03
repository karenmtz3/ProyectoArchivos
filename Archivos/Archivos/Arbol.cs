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
        public Arbol()
        {
            LNodos = new List<Nodo>();
        }

        public List<Nodo> LNodos1 { get => LNodos; set => LNodos = value; }


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
            n.EscribeNodo(bw);
        }
    }
}
