﻿using System;
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
        //private List<object> LObj;
        private List<string> elementos;
        private List<string> LAux;
        private long DirReg;
        private long DirSigReg;
        // private List<int> tam;
        int tam;
        char[] CNombre;

        public Registro(long dirreg, long dirsigreg)
        {
            DirReg = dirreg;
            DirSigReg = dirsigreg;
            elementos = new List<string>();
            tam = 0;
            //LObj = new List<object>();
            //tam = new List<int>();
        }
        /*public void Escribe(BinaryWriter bw)
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
        }*/
        public void llenaAux()
        {
            LAux = new List<string>();
            foreach (string e in elementos)
                LAux.Add(e); 
        }
        public void Escribe(BinaryWriter bw, Entidad entidad)
        {
            llenaAux();
            bw.Write(DirReg);
            foreach(Atributo atrib in entidad.LAtributo1)
            {
                int valor;
                for(int i = 0; i < LAux.Count; i++)
                {
                    if(atrib.TD == 'E')
                    {
                        valor = Convert.ToInt32(LAux[i]);
                        bw.Write(valor);
                        LAux.RemoveAt(i);
                        break;
                    }
                    else if (atrib.TD == 'C')
                    {
                        tam = atrib.LD;
                        ConvierteChar(LAux[i]);
                        bw.Write(CNombre);
                        LAux.RemoveAt(i);
                        break;
                    }
                }
            }
            bw.Write(DirSigReg);
        }
        public void ConvierteChar(string n)
        {
            //int t = tam[0];
            CNombre = new char[tam];
            char[] name = n.ToCharArray();
            for (int i = 0; i < tam; i++)
                CNombre[i] = ' ';
            for (int i = 0; i < tam; i++)
            {
                if (i < name.Length)
                    CNombre[i] = name[i];
                else
                    break;
            }
            //tam.RemoveAt(0);

        }
        //public List<object> LO { get => LObj; set => LObj = value; }
        public long DR { get => DirReg; set => DirReg = value; }
        public long DSR { get => DirSigReg; set => DirSigReg = value; }
        //public List<int> Tam { get => tam; set => tam = value; }
        public List<string> Elementos { get => elementos; set => elementos = value; }
    }
}
