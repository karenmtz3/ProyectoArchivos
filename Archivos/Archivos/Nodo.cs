using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    public class Nodo
    {
        private long DirNodo;
        private char TipoNodo;
        private List<long> Direcciones;
        private List<int> Claves;

        public Nodo(long DN, char T)
        {
            DirNodo = DN;
            TipoNodo = T;
            Direcciones = new List<long>();
            Claves = new List<int>();
            //LCajones = new List<CajonNodo>();
        }

        //Método que inicializa las listas de direcciones y de claves
        public void InicializaListas()
        {
            for (int i = 0; i < 5; i++)
                Direcciones.Add(-1);
            for (int i = 0; i < 4; i++)
                Claves.Add(int.MaxValue);
        }
        //Método para escribir el nodo en el archivo
        public void EscribeNodo(BinaryWriter bw)
        {
            bw.Write(DirNodo);
            bw.Write(TipoNodo);
            //Escribe en el archivo direcciones y claves
            for (int i = 0; i < 4; i++)
            {
                bw.Write(Direcciones[i]);
                bw.Write(Claves[i]);
            }
            bw.Write(Direcciones[4]);
        }

        public char TipoNodo1 { get => TipoNodo; set => TipoNodo = value; }
        public long DirNodo1 { get => DirNodo; set => DirNodo = value; }
        public List<long> Direcciones1 { get => Direcciones; set => Direcciones = value; }
        public List<int> Claves1 { get => Claves; set => Claves = value; }
    }
}
