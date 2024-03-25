using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    internal class Ocorrencia
    {
        public clsModelUsuario Usuario { get; set; }
        public clsModelExemplar Exemplar { get; set; }
        public clsModelLivro Livro { get; set; }
        public string DataEmprestimo { get; set; }
        public string HoraEmprestimo { get; set; }
        public TipoOcorrencia TipoOcorrencia { get; set; }
        public string ocorrencia { get; set;}

        public Ocorrencia(clsModelUsuario usuario, clsModelExemplar exemplar, clsModelLivro livro, string dataEmprestimo, string horaEmprestimo, TipoOcorrencia tipoOcorrencia, string Ocorrencia)
        {
            this.Usuario = usuario;
            this.Exemplar = exemplar;
            this.Livro = livro;
            this.DataEmprestimo = dataEmprestimo;
            this.HoraEmprestimo = horaEmprestimo;
            this.TipoOcorrencia = tipoOcorrencia;
            this.ocorrencia = Ocorrencia;
        }
    }
}
