﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Archivos
{
    public class Entidad
    {
        private string NombreEnt;
        private char[] Nombre = new char[30];
        private long DirEnt;
        private long DirAtrib;
        private long DirDatos;
        private long DirSigEnt;

        public Entidad(string nombre,long DEnt, long DAtrib, long DDatos, long DSEnt)
        {
            DirEnt = DEnt;
            DirAtrib = DAtrib;
            DirDatos = DDatos;
            DirSigEnt = DSEnt;
            /*for (int i = 0; i < 30; i++)
                Nombre[i] = '*';*/
            NombreEnt = nombre;
        }
        public void AgregaEspacio()
        {
            char[] nombre = NombreEnt.ToCharArray();
            for (int i = 0; i < 30; i++)
            {
                if (i < nombre.Length)
                    Nombre[i] = nombre[i];
                else
                    break;
            }
        }

        public void Guardar(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(Nombre);
            binaryWriter.Write(DirEnt);
            binaryWriter.Write(DirAtrib);
            binaryWriter.Write(DirDatos);
            binaryWriter.Write(DirSigEnt);

        }
        public long DE
        {
            set => DirEnt = value;
            get => DirEnt;
        }
        public long DA
        {
            set => DirAtrib = value;
            get => DirAtrib;
        }
        public long DD
        {
            set => DirDatos = value;
            get => DirDatos;
        }
        public long DSE
        {
            set => DirSigEnt = value;
            get => DirSigEnt;
        }
        public string NE { get => NombreEnt; set => NombreEnt = value; }
    }

}
