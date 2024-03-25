using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using prj_36400_36403_36405.Lógica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    internal class clsLogicGerenciarLivro : ConexaoBanco
    {
        MySqlDataReader dados = null;

        public clsLogicGerenciarLivro()
        {

        }

        #region Pesquisar Livro

        public List<clsModelLivro> PesquisarLivro(string nomeLivro)
        {
            List<clsModelLivro> listaLivros = new List<clsModelLivro>();

            try
            {
                string nomeProcedure = "PesquisarLivro";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vPesquisar", nomeLivro.ToString()));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        clsModelLivro livro = new clsModelLivro(dados[0].ToString(), dados[1].ToString(), int.Parse(dados[2].ToString()), int.Parse(dados[3].ToString()));
                        listaLivros.Add(livro);                
                    }
                }

                return listaLivros;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao pesquisar livro");
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

        #region Listar Empréstimos

        public List<clsModelEmprestimo> ListarEmprestimos(string login)
        {
            List<clsModelEmprestimo> listaEmprestimos = new List<clsModelEmprestimo>();

            try
            {
                string nomeProcedure = "ListarEmprestimos";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vLogin", login));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        clsModelEmprestimo emprestimo = new clsModelEmprestimo(int.Parse(dados[0].ToString()), dados[1].ToString(), int.Parse(dados[2].ToString()), int.Parse(dados[3].ToString()), DateTime.Parse(dados[4].ToString()), DateTime.Parse(dados[5].ToString()), DateTime.Parse(dados[6].ToString()));
                        listaEmprestimos.Add(emprestimo);
                    }
                }

                return listaEmprestimos;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao listar empréstimos.");
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

        #region Listar Todos Livros

        public List<clsModelLivro> ListarTodosLivros()
        {
            List<clsModelLivro> listaLivros = new List<clsModelLivro>();

            try
            {
                string nomeProcedure = "ListarTodosLivros";
                List<clsParametro> parametros = new List<clsParametro>();

                dados = ConsultarProc(nomeProcedure, parametros);

                if (dados.HasRows)
                {
                    while (dados.Read())
                    {
                        clsModelLivro livro = new clsModelLivro(dados[0].ToString(), dados[1].ToString(), int.Parse(dados[2].ToString()), int.Parse(dados[3].ToString()));
                        listaLivros.Add(livro);
                    }
                }

                return listaLivros;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao listar livros");
            }
            finally
            {
                if(dados != null)
                    if (!dados.IsClosed)
                        dados.Close();
                Desconectar();                
            }
        }

        #endregion

        #region Listar Disponibilidade

        public string ListarDisponibilidade(int codigoLivro)
        {
            string disponibilidade = "";

            try
            {
                string nomeProcedure = "ListarDisponibilidade";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vCodigoLivro", codigoLivro.ToString()));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        disponibilidade = dados[0].ToString() + "/" + dados[1].ToString();
                    }
                }

                return disponibilidade;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao listar livros disponíveis");
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

        #region Listar Livros Categorias

        public List<clsModelLivro> ListarLivrosCategorias(int codigoCategoria)
        {
            try
            {
                List<clsModelLivro> listaLivros = new List<clsModelLivro>();

                string nomeProcedure = "ListarLivrosCategorias";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vCategoria", codigoCategoria.ToString()));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        clsModelLivro livro = new clsModelLivro(int.Parse(dados[0].ToString()));
                        listaLivros.Add(livro);
                    }
                }

                return listaLivros;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao filtrar livros por categoria");
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
    }
}