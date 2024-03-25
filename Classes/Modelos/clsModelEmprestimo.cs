using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    public class clsModelEmprestimo : ConexaoBanco
    {
        public clsModelUsuario Usuario { get; set; }
        public clsModelExemplar Exemplar { get; set; }
        public clsModelLivro Livro { get; set; }
        public int codigo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataEstimadaDevolucao { get; set; }
        public DateTime DataDevolucao { get; set; }
        MySqlDataReader dados = null;

        public clsModelEmprestimo(int cdEmprestimo, string nmLogin, int codigoExemplar, int codigoLivro, DateTime dtEmprestimo, DateTime dtDevolucaoEstimada, DateTime dtDevolucao)
        {
            codigo = cdEmprestimo;
            Usuario = new clsModelUsuario(nmLogin);
            Exemplar = new clsModelExemplar(codigoExemplar, codigoLivro);
            Livro = new clsModelLivro(codigoLivro);
            DataEmprestimo = dtEmprestimo;
            DataEstimadaDevolucao = dtDevolucaoEstimada;
            DataDevolucao = dtDevolucao;
        }

        public clsModelEmprestimo(string Login, int codigoExemplar, int codigoLivro)
        {
            try
            {
                string nomeProcedure = "PreencherDadosEmprestimo";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vLogin", Login.ToString()));
                parametros.Add(new clsParametro("vCodigoExemplar", codigoExemplar.ToString()));
                parametros.Add(new clsParametro("vCodigoLivro", codigoLivro.ToString()));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        Usuario= new clsModelUsuario(Login);
                        codigo = int.Parse(dados[0].ToString());
                        Exemplar = new clsModelExemplar(codigoExemplar, codigoLivro);
                        DataEmprestimo = DateTime.Parse(dados[1].ToString());
                        string data = "1000-10-10";
                        if (String.IsNullOrEmpty(dados[2].ToString()))
                        {                            
                            DataDevolucao = DateTime.Parse(data.ToString());
                        }
                        else
                        {
                            DataDevolucao = DateTime.Parse(dados[2].ToString());
                        }
                        DataEstimadaDevolucao = DateTime.Parse(dados[3].ToString());

                    }
                }
            }
            catch(Exception)
            {
                throw new Exception("Erro ao preencher empréstimo");
            }
            finally
            {
                if (dados != null)
                    if (!dados.IsClosed)
                        dados.Close();
                Desconectar();
            }
        }
    }
}
