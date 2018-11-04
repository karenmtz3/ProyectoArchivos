using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    class BloquePrincipal
    {
        string valor;
        long dir;

        public BloquePrincipal(string v)
        {
            valor = v;
            dir = -1;
        }

        public long Dir { get => dir; set => dir = value; }
        public string Valor { get => valor; set => valor = value; }
    }
}
