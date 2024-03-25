using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    public class clsParametro
    {
        public string nome { get; set; }
        public string valor { get; set; }

        public clsParametro(string name, string value)
        {
            nome = name;
            valor = value;
        }
    }
}
