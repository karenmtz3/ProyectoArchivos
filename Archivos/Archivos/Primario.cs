using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Archivos
{
    public class Primario
    {

        char TipoDato;
        char[] elemento;
        int tam;
        int PosP;
        List<BloquePrincipal> BPrincipal;
        List<BloquePrincipal> SubBloque;
        public Primario(char t)
        {
            TipoDato = t;
            BPrincipal = new List<BloquePrincipal>();
            SubBloque = new List<BloquePrincipal>();
            PosP = 0;
        }

        internal List<BloquePrincipal> BPrincipal1 { get => BPrincipal; set => BPrincipal = value; }
        internal List<BloquePrincipal> SubBloque1 { get => SubBloque; set => SubBloque = value; }
        public char TipoDato1 { get => TipoDato; set => TipoDato = value; }

        //Método que crea y escribe el bloque principal de primario
        public void CreaBloquePrincipal(BinaryWriter bw)
        {
           if(TipoDato == 'E')
            {
                for(int i = 1; i  < 10; i++)
                {
                    string n = i.ToString();
                    BloquePrincipal b = new BloquePrincipal(n);
                    bw.Write(i);
                    bw.Write(b.Dir);
                    BPrincipal.Add(b);
                }
           }
           if(TipoDato == 'C')
            {
                string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                char [] Alf = alfabeto.ToCharArray();
                for(int i = 0; i < Alf.Length; i++)
                {
                    string v = Alf[i].ToString();
                    BloquePrincipal b = new BloquePrincipal(v);
                    bw.Write(Alf[i]);
                    bw.Write(b.Dir);
                    BPrincipal.Add(b);
                }
            } 
        }

        //Método que crea el sub bloque
        public void CreaSubBloque(Registro r, int t, int pos,BinaryWriter bw)
        {
            SubBloque.Clear();
            char[] valor = new char[tam];
            if(TipoDato == 'E')
            {
                for (int i = 0; i < 100; i++)
                {
                    BloquePrincipal b;
                    if (i == 0)
                    {
                        b = new BloquePrincipal(r.Elementos[pos]);
                        b.Dir = r.DR;
                        int n = Convert.ToInt32(r.Elementos[pos]);
                        bw.Write(n);
                        bw.Write(b.Dir);
                    }
                    else
                    {
                        string n = i.ToString();
                        b = new BloquePrincipal(n);
                        bw.Write(0);
                        bw.Write(b.Dir);
                    }
                    SubBloque.Add(b);
                }
            }
            if(TipoDato == 'C')
            {
                for(int i = 0; i < 100; i++)
                {
                    BloquePrincipal b;
                    if(i == 0)
                    {
                        tam = t;
                        ConvierteChar(r.Elementos[pos]);
                        b = new BloquePrincipal(r.Elementos[pos]);
                        b.Dir = r.DR;
                        bw.Write(elemento);
                        bw.Write(b.Dir);
                    }
                    
                    else
                    {
                        string aux = "zzz";
                        ConvierteChar(aux);
                        b = new BloquePrincipal(aux);
                        bw.Write(elemento);
                        bw.Write(b.Dir);
                    }
                    SubBloque.Add(b);
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
            for(int i = 0; i < tam; i++)
            {
                if (i < elem.Length)
                    elemento[i] = elem[i];
                else
                    break;
            }
        }

        //se llena el sub bloque
        public void ActualizaSub(BinaryReader br, int t)
        {
            tam = t;
            SubBloque.Clear();
            if (TipoDato == 'E')
            {
                for (int i = 0; i < 100; i++)
                {
                    int num = br.ReadInt32();
                    long d = br.ReadInt64();
                    BloquePrincipal b = new BloquePrincipal(num.ToString());
                    b.Dir = d;
                    SubBloque.Add(b);
                    if (d != -1)
                        PosP += 1;
                }
            }
            else if (TipoDato == 'C')
            {
                for (int i = 0; i < 100; i++)
                {
                    string v = "";
                    char[] valor = br.ReadChars(tam);
                    long d = br.ReadInt64();
                    foreach (char c in valor)
                        if (char.IsLetter(c))
                            v += c;
                    BloquePrincipal b = new BloquePrincipal(v);
                    b.Dir = d;
                    SubBloque.Add(b);
                    if (d != -1)
                        PosP += 1;
                }
            }
        }

        //Busca el elemento a eliminar
        public void Elimina(string reg)
        {
            if(TipoDato == 'E')
            {
                for(int i = 0; i < SubBloque.Count; i++)
                {
                    BloquePrincipal b = SubBloque[i];
                    if (reg == b.Valor)
                    {
                        b.Dir = -1;
                        Debug.WriteLine("El elemento a eliminar es " + b.Valor);
                        break;
                    }
                }
            }
            else if(TipoDato == 'C')
            {
                for (int i = 0; i < SubBloque.Count; i++)
                {
                    BloquePrincipal b = SubBloque[i];
                    if (reg == b.Valor)
                    {
                        Debug.WriteLine("El elemento a eliminar es " + b.Valor);
                        b.Dir = -1;
                        break;
                    }
                }
            }
        }

        //Se actualiza los valores del sub bloque
        public void ActualizaSubBloque(BinaryWriter bw)
        {
            if(TipoDato == 'E')
            {
                for(int i = 0; i < SubBloque.Count; i++)
                {
                    BloquePrincipal b = SubBloque[i];
                    int n = Convert.ToInt32(b.Valor);
                    bw.Write(n);
                    bw.Write(b.Dir);
                }
            }
            else if(TipoDato == 'C')
            {
                for(int i = 0; i < SubBloque.Count; i++)
                {
                    BloquePrincipal b = SubBloque[i];
                    ConvierteChar(b.Valor);
                    bw.Write(elemento);
                    bw.Write(b.Dir);
                }
            }
        }

        //Se actualizan las direcciones del bloque principal
        public void ActualizaPrincipal(BinaryWriter bw)
        {
            if (TipoDato == 'E')
            {
                for (int i = 0; i < BPrincipal.Count; i++)
                {
                    BloquePrincipal b = BPrincipal[i];
                    int n = Convert.ToInt32(b.Valor);
                    bw.Write(n);
                    bw.Write(b.Dir);
                }
            }
            if (TipoDato == 'C')
            {
                for (int i = 0; i < BPrincipal.Count; i++)
                {
                    BloquePrincipal b = BPrincipal[i];
                    char l = Convert.ToChar(b.Valor);
                    bw.Write(l);
                    bw.Write(b.Dir);
                }
            }
        }
        //método para agregar un nuevo elemento al sub bloque y escribir los nuevos cambios al archivo
        public void AgregaElem(Registro r, int pos, BinaryWriter bw)
        {
            
            SubBloque[PosP].Valor = r.Elementos[pos];
            SubBloque[PosP].Dir = r.DR;
            PosP = 0;
            if(TipoDato == 'E')
            {
                for (int i = 0; i < SubBloque.Count; i++)
                {
                    int n = Convert.ToInt32(SubBloque[i].Valor);
                    bw.Write(n);
                    bw.Write(SubBloque[i].Dir);
                }
            }
            else if(TipoDato == 'C')
            {
                for(int i = 0; i < SubBloque.Count; i++)
                {
                    string v = SubBloque[i].Valor;
                    ConvierteChar(v);
                    bw.Write(elemento);
                    bw.Write(SubBloque[i].Dir);
                }
            }
            
        }

        //leer el bloque principal 
        public void LeePrincipal(BinaryReader br)
        {
            BPrincipal.Clear();
            if(TipoDato == 'E')
            {
                for(int i = 1; i < 10; i++)
                {
                    BloquePrincipal b;
                    int v = br.ReadInt32();
                    long d = br.ReadInt64();
                    b = new BloquePrincipal(v.ToString());
                    b.Dir = d;
                    BPrincipal.Add(b);

                }
            }
            else if(TipoDato == 'C')
            {
                for(int i = 0; i < 26; i++)
                {
                    BloquePrincipal b;
                    char letra = br.ReadChar();
                    long d = br.ReadInt64();
                    b = new BloquePrincipal(letra.ToString());
                    b.Dir = d;
                    BPrincipal.Add(b);

                }
            }
        }
    }
}
