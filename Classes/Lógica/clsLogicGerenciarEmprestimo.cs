using MySql.Data.MySqlClient;
using prj_36400_36403_36405.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Lógica
{
    public class clsLogicGerenciarEmprestimo : ConexaoBanco
    {
        #region Calcular Quantidade Empréstimos

        public int ContarQuantidadeEmprestimos(string login)
        {
            MySqlDataReader dados = null;
            int contagem = 0;

            if(login == null)
            {
                login = "";
            }

            try
            {
                string nomeProcedure = "ContarQuantidadeEmprestimos";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vLogin", login));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        contagem++;
                    }
                }

                return contagem;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao contar quantidade de empréstimos do cliente");
            }
            finally
            {
                if (dados != null)
                    if (!dados.IsClosed)
                        dados.Close();
                Desconectar();
            }            
        }

        #endregion

        #region Realizar Empréstimo

        public void RealizarEmprestimo(string login, int codigoLivro)
        {
            try
            {
                string nomeProcedure = "RealizarEmprestimo";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vLogin", login));
                parametros.Add(new clsParametro("vCodigoLivro", codigoLivro.ToString()));

                InserirProc(nomeProcedure, parametros);
            }
            catch(Exception)
            {
                throw new Exception("Erro ao realizar empréstimo");
            }
            finally
            {
                Desconectar();
            }
        }

        #endregion

        #region Devolver Livro

        public void DevolverLivro(string login, int codigoLivro, int codigoExemplar)
        {
            try
            {
                string nomeProcedure = "DevolverLivro";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vLogin", login));
                parametros.Add(new clsParametro("vCodigoLivro", codigoLivro.ToString()));
                parametros.Add(new clsParametro("vCodigoExemplar", codigoExemplar.ToString()));

                InserirProc(nomeProcedure, parametros);
            }
            catch(Exception)
            {
                throw new Exception("Erro ao tentar devolver livro.");
            }
            finally
            {
                Desconectar();
            }
        }

        #endregion
    }
}