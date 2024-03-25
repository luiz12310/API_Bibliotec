using MySql.Data.MySqlClient;
using prj_36400_36403_36405.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Lógica
{
    internal class clsLogicGerenciarUsuario : ConexaoBanco
    {
        public List<clsModelUsuario> ListaUser { get; set; }
        MySqlDataReader dados = null;

        #region Acessar Sistema

        public bool AcessarSistema(string login, string senha)
        {
            bool retorno = false;
            try
            {
                string nomeProcedure = "AcessarSistema";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vLogin", login));
                parametros.Add(new clsParametro("vSenha", senha));

                dados = ConsultarProc(nomeProcedure, parametros);

                if (dados.HasRows)
                {
                    while (dados.Read())
                    {
                        retorno = true;
                    }
                }

                return retorno;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao tentar entrar no sistema");
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


        public List<clsModelUsuario> BuscarUsuario(string nomeUsuario)
        {
            List<clsModelUsuario> listaUsuarios = new List<clsModelUsuario>();
            try
            {
                string nomeProcedure = "BuscarUsuario";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vPesquisa", nomeUsuario));

                dados = ConsultarProc(nomeProcedure, parametros);

                if (dados.HasRows)
                {
                    while (dados.Read())
                    {
                        clsModelUsuario usuario = new clsModelUsuario(dados[0].ToString(), dados[1].ToString(), bool.Parse(dados[2].ToString()));
                        listaUsuarios.Add(usuario);
                    }
                }

                return listaUsuarios;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao pesquisar usuário");
            }
            finally
            {
                if (dados != null)
                    if (!dados.IsClosed)
                        dados.Close();
                Desconectar();
            }

        }

        #region Listar Usuários

        public List<clsModelUsuario> ListarUsuarios()
        {
            List<clsModelUsuario> listaUsuarios = new List<clsModelUsuario>();

            try
            {
                string nomeProcedure = "ListarUsuarios";
                List<clsParametro> parametros = new List<clsParametro>();

                dados = ConsultarProc(nomeProcedure, parametros);

                if (dados.HasRows)
                {
                    while(dados.Read())
                    {                                             
                        clsModelUsuario usuario = new clsModelUsuario(dados[0].ToString(), dados[1].ToString(), bool.Parse(dados[2].ToString())) ;
                        listaUsuarios.Add(usuario);
                    }
                }

                return listaUsuarios;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao listar usuários");
            }
            finally
            {
                if(dados != null)
                    if(!dados.IsClosed)
                        dados.Close();
                Desconectar();
            }
        }

        #endregion

        #region Listar Livros

        public List<clsModelLivro> ListarLivros(string login)
        {
            try
            {
                string nomeProcedure = "ListarLivros";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vLogin", login.ToString()));
                List<clsModelLivro> listaLivros = new List<clsModelLivro>();

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
                throw new Exception("Erro ao listar livros do usuário");
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