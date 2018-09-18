using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    public class Atributo
    {
        private char[] nombre = new char[30];
        private string NombreAtrib;
        private long DirAtrib, DirIndice, DSAtrib;
        private char TipoDato;
        private int LongDato, TipoIndice;

        public Atributo(string nombre, long DA, char TD, int LD, int TI,long DI, long DSA)
        {
            NombreAtrib = nombre;
            DirAtrib = DA;
            DirIndice = DI;
            DSAtrib = DSA;
            TipoDato = TD;
            LongDato = LD;
            TipoIndice = TI;
        }
        public void ConvierteChar()
        {
            char[] n = NombreAtrib.ToCharArray();
            for (int i = 0; i < 30; i++)
            {
                if (i < n.Length)
                    nombre[i] = n[i];
                else
                    break;
            }
        }
        public void EscribeAtributo(BinaryWriter bw)
        {
            bw.Write(nombre);
            bw.Write(DirAtrib);
            bw.Write(DirIndice);
            bw.Write(DSAtrib);
            bw.Write(TipoDato);
            bw.Write(LongDato);
            bw.Write(TipoIndice);

        }

        public string NA
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
