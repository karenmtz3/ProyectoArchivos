using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    class CajonNodo
    {
        private long DR;
        private int CB;

        public CajonNodo(int cb)
        {
            CB = cb;
            DR = -1;
        }

        public long DR1 { get => DR; set => DR = value; }
        public int CB1 { get => CB; set => CB = value; }
    }
}
