using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    class Atributo
    {
        int tam = 30;
        string[] NombreAtrib;
        long DirAtrib, DirIndice, DSAtrib;
        char TipoDato;
        int LongDato, TipoIndice;

        public Atributo(string[] nombre, long DA, long DI, long DSA, char TD, int LD, int TI)
        {
            NombreAtrib = nombre;
            DirAtrib = DA;
            DirIndice = DI;
            DSAtrib = DSA;
            TipoDato = TD;
            LongDato = LD;
            TipoIndice = TI;
        }

        public string[] NA
        {
            set => NombreAtrib = value;
            get => NombreAtrib;
        }
        public long DA
        {
            set => DirAtrib = value;
            get => DirAtrib;
        }
        public long DI
        {
            set => DirIndice = value;
            get => DirIndice;
        }
        public long DSA
        {
            set => DSAtrib = value;
            get => DSAtrib;
        }
        public char TD
        {
            set => TipoDato = value;
            get => TipoDato;
        }
        public int LD
        {
            set => LongDato = value;
            get => LongDato;
        }
        public int TI
        {
            set => TipoIndice = value;
            get => TipoIndice;
        }
    }
}
