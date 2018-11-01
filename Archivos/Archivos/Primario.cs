using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    public class Primario
    {
        Atributo atrib;
        char[] Nombre;
        string LTipo;
        long Dir;
        long DirI;
        public Primario(Atributo a, string t, long dir, long di)
        {
            atrib = a;
            LTipo = t;
            Dir = dir;
            DirI = di;

        }

        public string LTipo1 { get => LTipo; set => LTipo = value; }
        public long Dir1 { get => Dir; set => Dir = value; }
        public long DirI1 { get => DirI; set => DirI = value; }


        //Escribe Bloque principal con enteros
        public void EscribeEnteros(BinaryWriter bw)
        {
            int num = Convert.ToInt32(LTipo);
            bw.Write(num);
            bw.Write(Dir);
        }

        //Escribe Bloque Principal con el abecedario
        public void EscribeAlfabeto(BinaryWriter bw)
        {
            char letra = Convert.ToChar(LTipo);
            bw.Write(letra);
            bw.Write(Dir);
        }
    }
}
