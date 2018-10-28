using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    public class IndiceSec
    {
        Atributo atrib;
        char[] Inf;

        List<string> LInf;
        List<long> LDir;
        public IndiceSec(Atributo a)
        {
            atrib = a;
            LInf = new List<string>();
            LDir = new List<long>();
        }

        public List<string> LInf1 { get => LInf; set => LInf = value; }
        public List<long> LDir1 { get => LDir; set => LDir = value; }

        //Se crea el bloque principal
        public void BPrincipalSec()
        {
            for(int i = 0; i < 50; i++)
            {
                string aux = "aux";
                long direccion = -1;
                LInf.Add(aux);
                LDir.Add(direccion);
            }
        }

        public void EscribeBSec(BinaryWriter bw)
        {
            for(int i = 0; i < LInf.Count; i++)
            {
                ConvierteChar(LInf[i]);
                bw.Write(Inf);
                bw.Write(LDir[i]);
            }
        }

        public void ConvierteChar(string n)
        {
            Inf = new char[atrib.LD];
            char [] auxn = n.ToCharArray();
            for (int i = 0; i < atrib.LD; i++)
                Inf[i] = ' ';
            for (int i = 0; i < atrib.LD; i++)
            {
                if (i < auxn.Length)
                    Inf[i] = auxn[i];
                else
                    break;
            }

        }
    }
}
