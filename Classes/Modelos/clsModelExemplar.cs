using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    public class clsModelExemplar
    {
        public int Codigo { get; set; }
        public int CodigoLivro { get; set; }

        public clsModelExemplar()
        {

        }

        public clsModelExemplar(int codigoExemplar, int codigoLivro)
        {
            Codigo = codigoExemplar;
            CodigoLivro = codigoLivro;
        }
    }
}
