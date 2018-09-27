using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;


namespace Archivos
{
    class Registro
    {
        private List<object> LObj;
        private long DirReg;
        private long DirSigReg;
        private int tam;

        char[] CNombre;

        public Registro(long dirreg, long dirsigreg)
        {
            DirReg = dirreg;
            DirSigReg = dirsigreg;
            LObj = new List<object>();
        }
        public void Escribe(BinaryWriter bw)
        {
            bw.Write(DirReg);
            for (int i = 0; i < LObj.Count; i++)
            {
                if (LObj[i].GetType() == typeof(int))
                {
                    //Debug.WriteLine("El elemento " + LObj[i] + " es de tipo int");
                    int e = (int)LObj[i];
                    bw.Write(e);
                }
                else if (LObj[i].GetType() == typeof(string))
                {
                    string n = (string)LObj[i];
                    ConvierteChar(n);
                    bw.Write(CNombre);
                   // Debug.WriteLine("El elemento " + LObj[i] + " es de tipo string");
                }

            }
            bw.Write(DirSigReg);
        }
        public void ConvierteChar(string n)
        {
            CNombre = new char[tam];
            char[] name = n.ToCharArray();
            for (int i = 0; i < tam; i++)
                CNombre[i] = ' ';
            for (int i = 0; i < 30; i++)
            {
                if (i < name.Length)
                    CNombre[i] = name[i];
                else
                    break;
            }

        }
        public List<object> LO { get => LObj; set => LObj = value; }
        public long DR { get => DirReg; set => DirReg = value; }
        public long DSR { get => DirSigReg; set => DirSigReg = value; }
        public int Tam { get => tam; set => tam = value; }
    }
}
