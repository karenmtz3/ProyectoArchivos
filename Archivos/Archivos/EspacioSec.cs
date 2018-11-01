using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{

    public class EspacioSec
    {
        long DirE;
        long DirReg;
        long DirESig;
        public EspacioSec(long DE, long DR, long DES)
        {
            DirE = DE;
            DirReg = DR;
            DirESig = DES;
        }

        public long DirE1 { get => DirE; set => DirE = value; }
        public long DirReg1 { get => DirReg; set => DirReg = value; }
        public long DirESig1 { get => DirESig; set => DirESig = value; }

        //Método que escribe el cajon en el archivo
        public void EscribeCajon(BinaryWriter bw)
        {
            bw.Write(DirE);
            bw.Write(DirReg);
            bw.Write(DirESig);
        }
    }
}
