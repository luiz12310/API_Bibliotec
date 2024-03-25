using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    public class clsModelUsuario : ConexaoBanco
    {
        public string login { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        public bool bloqueado { get; set; }
        public DateTime dtBloqueio { get; set; }
        public clsModelTipoUsuario tipoUsuario { get; set; }
        MySqlDataReader dados = null;

        public clsModelUsuario(string Login)
        {
            try
            {
                string nomeProcedure = "PreencherDadosUsuario";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vLogin", Login));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        login = Login;
                        nome = dados[0].ToString();
                        senha = dados[1].ToString();
                        bloqueado = bool.Parse(dados[2].ToString());
                        tipoUsuario = new clsModelTipoUsuario(int.Parse(dados[3].ToString()));
                        string data = "1000-10-10";
                        if (String.IsNullOrEmpty(dados[4].ToString()))
                        {
                            dtBloqueio = DateTime.Parse(data);
                        }
                        else
                        {
                            dtBloqueio = DateTime.Parse(dados[4].ToString());
                        }
                        //qtDiasRestantesOcorrencia = int.Parse(dados[5].ToString());
                        //if(bloqueado)
                        //{
                        //    ultimaDataBlock = DateTime.Parse(dados[6].ToString());
                        //}
                    }
                }
            }
            catch(Exception)
            {
                throw new Exception("Erro ao preencher dados do usuário");
            }
            finally
            {
                if (dados != null)
                    if (!dados.IsClosed)
                        dados.Close();
                Desconectar();
            }
        }

        public clsModelUsuario()
        {

        }

        public clsModelUsuario(string nomeUsuario, string loginUsuario, bool bloqueadoUsuario)
        {
            nome = nomeUsuario;
            login = loginUsuario;
            bloqueado = bloqueadoUsuario;
        }

        #region Listar livros e usuário

        public List<clsModelLivro> ListarLivrosUsuario()
        {
            try
            {
                string nomeProcedure = "ListarLivrosUsuario";
                List<clsParametro> parametros = new List<clsParametro>();
                List<clsModelLivro> listaLivros = new List<clsModelLivro>();

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        clsModelLivro livro = new clsModelLivro();
                        listaLivros.Add(livro);
                    }
                }

                return listaLivros;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao carregar dados do usuário");
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
