using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    public class IndicePrim
    {
        Atributo atrib;
        List<string> LTipo;
        List<long> LDir;
        public IndicePrim(Atributo a)
        {
            atrib = a;
            LTipo = new List<string>();
            LDir = new List<long>();
        }

        public List<string> LTipo1 { get => LTipo; set => LTipo = value; }
        public List<long> LDir1 { get => LDir; set => LDir = value; }

        //Crea el bloque principal
        public void CreaBPrincipal()
        {
            if (atrib.TD == 'E')
            {
                for (int i = 1; i < 10; i++)
                {
                    long direccion = -1;
                    LTipo.Add(i.ToString());
                    LDir.Add(direccion);
                    //bw.Write(i);
                    //bw.Write(direccion);
                }
            }
            else if(atrib.TD == 'C')
            {
                string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                char [] auxAlf = alfabeto.ToCharArray();
                for(int i = 0; i < auxAlf.Length; i++)
                {
                    long dir = -1;
                    LTipo.Add(auxAlf[i].ToString());
                    LDir.Add(dir);
                }
            }
        }

        public void EscribeBPrincipal(BinaryWriter bw)
        {
            if (atrib.TD == 'E')
            {
                for(int i = 0; i < LTipo.Count; i++)
                {
                    int v = Convert.ToInt32(LTipo[i]);
                    bw.Write(v);
                    bw.Write(LDir[i]);
                }
            }
            else if(atrib.TD == 'C')
            {
                for(int i = 0; i < LTipo.Count; i++)
                {
                    char c = Convert.ToChar(LTipo[i]);
                    bw.Write(c);
                    bw.Write(LDir[i]);
                }
            }
        }
    }
}
