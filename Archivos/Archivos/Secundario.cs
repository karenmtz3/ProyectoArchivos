using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    public class Secundario
    {
        string NombreAtrib;
        long TamPrin;
        int tam;
        int PosS;
        char TDato;
        char[] elemento;
        List<BloquePrincipal> Principal;
        List<long> SubB;
        public Secundario(char t, string atributo)
        {
            PosS = 0;
            TDato = t;
            NombreAtrib = atributo;
            Principal = new List<BloquePrincipal>();
            SubB = new List<long>();
        }

        //Método que crea y escribe en el archivo el bloque principal
        public void CreaPrincipal(BinaryWriter bw, int t)
        {
            if(TDato == 'E')
            {
                int n = int.MaxValue;
                for (int i = 0; i < 50; i++)
                {
                    string valor = n.ToString();
                    BloquePrincipal b = new BloquePrincipal(valor);
                    bw.Write(n);
                    bw.Write(b.Dir);
                    Principal.Add(b);
                }
            }
            if(TDato == 'C')
            {
                for(int i = 0; i < 50; i++)
                {
                    tam = t;
                    string valor = "";
                    /*for(int j =0; j < tam; j++)
                    {
                        char c = 'z';
                        valor += c;
                    }*/
                    ConvierteChar(valor);
                    BloquePrincipal b = new BloquePrincipal(valor);
                    bw.Write(elemento);
                    bw.Write(b.Dir);
                    Principal.Add(b);
                }
            }

        }

        //Se pasa el string a arreglo de char
        public void ConvierteChar(string n)
        {
            elemento = new char[tam];
            char[] elem = n.ToCharArray();
            for (int i = 0; i < tam; i++)
                elemento[i] = ' ';
            for (int i = 0; i < tam; i++)
            {
                if (i < elem.Length)
                    elemento[i] = elem[i];
                else
                    break;
            }
        }


        //Método que llena el sub bloque
        public void LlenaSub(BinaryReader br)
        {
            SubB.Clear();
            for(int i = 0; i < 50; i++)
            {
                long dir = br.ReadInt64();
                SubB.Add(dir);
                if (dir != -1)
                    PosS+=1;
            }
        }

        //Método que lee el sub-bloque
        public void LeeSub(BinaryReader br)
        {
            SubB.Clear();
            for (int i = 0; i < 50; i++)
            {
                long dir = br.ReadInt64();
                SubB.Add(dir);
            }
        }

        //Método que escribe en el archivo los nuevos cambios del sub-bloque
        public void ActualizaSub(BinaryWriter bw)
        {
            for(int i = 0; i < SubB.Count; i++)
                bw.Write(SubB[i]);
        }

        //Método que escribe en el archivo los nuevos cambios del principal
        public void ActualizaPrincipal(BinaryWriter bw, int t)
        {
            if(TDato == 'E')
            {
                for(int i = 0; i < Principal.Count; i++)
                {
                    BloquePrincipal b = Principal[i];
                    int n = Convert.ToInt32(b.Valor);
                    bw.Write(n);
                    bw.Write(b.Dir);
                }
            }
            if(TDato == 'C')
            {
                tam = t;
                for(int i = 0; i < Principal.Count; i++)
                {
                    BloquePrincipal b = Principal[i];
                    ConvierteChar(b.Valor);
                    bw.Write(elemento);
                    bw.Write(b.Dir);
                }
            }
        }

        //Método para agregar las direcciones de los registros a un sub-bloque
        public void AgregaSub(long DR, BinaryWriter bw)
        {
            SubB[PosS] = DR;
            PosS = 0;
            for(int i = 0;i < SubB.Count;i++)
                bw.Write(SubB[i]);
        }

        //Método que crea y escribe en el archivo el sub bloque de secundario
        public void CreaSubBloque(long DR, BinaryWriter bw)
        {
            SubB.Clear();
            for (int i = 0; i < 50; i++)
            {
                if (i == 0)
                {
                    bw.Write(DR);
                    SubB.Add(DR);
                }
                else
                {
                    long dir = -1;
                    bw.Write(dir);
                    SubB.Add(dir);
                }
            }
        }

        //Método que lee el bloque principal 
        public void LeePricipal(BinaryReader br,int t)
        {
            Principal.Clear();
            if(TDato == 'E')
            {
                for(int i = 0; i < 50; i++)
                {
                    int valor = br.ReadInt32();
                    long dir = br.ReadInt64();
                    BloquePrincipal b = new BloquePrincipal(valor.ToString());
                    b.Dir = dir;
                    Principal.Add(b);
                }
            }
            else if(TDato == 'C')
            {
                for(int i = 0; i < 50; i++)
                {
                    string aux = "";
                    char[] aux2 = br.ReadChars(t);
                    foreach (char n in aux2)
                    {
                        if (char.IsLetter(n))
                            aux += n;
                    }
                    long d = br.ReadInt64();
                    BloquePrincipal b = new BloquePrincipal(aux);
                    b.Dir = d;
                    Principal.Add(b);
                }
            }
        }

        public List<long> SubB1 { get => SubB; set => SubB = value; }
        internal List<BloquePrincipal> Principal1 { get => Principal; set => Principal = value; }
        public string NombreAtrib1 { get => NombreAtrib; set => NombreAtrib = value; }
        public long TamPrin1 { get => TamPrin; set => TamPrin = value; }
    }
}
