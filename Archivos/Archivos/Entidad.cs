using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Archivos
{
    class Entidad
    {
        string NombreEnt;
        long DirEnt, DirAtrib, DirDatos, DirSigEnt;

        public Entidad(string nombre, long DEnt, long DAtrib, long DDatos, long DSEnt)
        {
            NombreEnt = nombre;
            DirEnt = DEnt;
            DirAtrib = DAtrib;
            DirDatos = DDatos;
            DirSigEnt = DSEnt;
        }
        public string SGnombre
        {
            set => NombreEnt = value;
            get => NombreEnt;
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

    }

}
