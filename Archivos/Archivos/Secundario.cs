using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Archivos
{
    public class Secundario
    {
        Atributo atrib;
        List<EspacioSec> SubSec;
        List<long> SubBloque;
        char[] Inf;
        string nombre;  //nombre del registro que es índice secundario
        long dirB;      //dirección del sub bloque 
        long dirInd;    //dirección del espacio 
        long dirIndS;   //dirección del siguiente espacio
        public Secundario(Atributo a, string valor, long direccion, long dind, long dIS)
        {
            atrib = a;
            nombre = valor;
            dirB = direccion;
            dirInd = dind;
            dirIndS = dIS;
            SubBloque = new List<long>();

           
            SubSec = new List<EspacioSec>();
        }

        public void AgregaEsp(EspacioSec cajoncito)
        {
            SubSec.Add(cajoncito);  
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public long DirB { get => dirB; set => dirB = value; }
        public Atributo Atrib { get => atrib; set => atrib = value; }
        public List<long> SubBloque1 { get => SubBloque; set => SubBloque = value; }
        public long DirInd { get => dirInd; set => dirInd = value; }
        public long DirIndS { get => dirIndS; set => dirIndS = value; }
        public List<EspacioSec> SubSec1 { get => SubSec; set => SubSec = value; }


        //Escribe bloque principal con enteros
        public void EscribeEnteros(BinaryWriter bw)
        {
            bw.Write(dirInd);   //dirección del bloquesito
            int num = Convert.ToInt32(nombre);
            bw.Write(num);         //valor que se va a escribir
            bw.Write(dirB);        //dirección del sub bloque 
            bw.Write(dirIndS);     //dirección del siguiente
        }

        public void Escribe(BinaryWriter bw)
        {
            ConvierteChar();
            bw.Write(dirInd);
            bw.Write(Inf);
            bw.Write(dirB);
            bw.Write(dirIndS);
        }

        public void ConvierteChar()
        {
            Inf = new char[atrib.LD];
            char [] auxn = nombre.ToCharArray();
            for (int i = 0; i < atrib.LD; i++)
                Inf[i] = ' ';
            for(int i = 0; i < atrib.LD; i++)
            {
                if (i < auxn.Length)
                    Inf[i] = auxn[i];
                else
                    break;
            }
        }

        //Crea el sub bloque que tiene las direcciones al registro
        public void CreaSubBloque()
        {
            
            for(int i = 0; i < 50; i++)
            {
                long dir = -1;
                SubBloque.Add(dir);
            }
        }

        //Escribe el sub bloque en el archivo
        public void EscribeSub(BinaryWriter bw)
        {
            for (int i = 0; i < SubBloque.Count; i++)
                bw.Write(SubBloque[i]);
        }

    }
}
