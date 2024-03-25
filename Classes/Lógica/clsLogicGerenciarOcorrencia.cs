using MySql.Data.MySqlClient;
using prj_36400_36403_36405.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Lógica
{
    internal class clsLogicGerenciarOcorrencia : ConexaoBanco
    {
        MySqlDataReader dados = null;
        //public void registrar(Emprestimo emprestimo, TipoOcorrencia tipoOcorrencia, string dsOcorrencia)
        //{
        //    Ocorrencia ocorrencia = new Ocorrencia(emprestimo.Usuario, emprestimo.Exemplar, emprestimo.Livro, emprestimo.DataEmprestimo, emprestimo.HoraEmprestimo, tipoOcorrencia, dsOcorrencia);

        //    ConexaoBanco Banco = new ConexaoBanco("localhost", "root", "root", "bibliotec");
        //    Banco.Conectar();

        //    string comando = $"update usuario set ic_bloqueado = true and dt_bloqueio = '{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}' and hr_bloqueio = '{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}' where nm_login = '{ocorrencia.Usuario.LoginUsuario}'";
        //    Banco.Inserir(comando);

        //    comando = "insert into ocorrencia values " +
        //        $"('{ocorrencia.Usuario.LoginUsuario}', {ocorrencia.Exemplar.Codigo}, {ocorrencia.Exemplar.CodigoLivro}, " +
        //        $"'{ocorrencia.DataEmprestimo}', '{ocorrencia.HoraEmprestimo}', {ocorrencia.TipoOcorrencia.Codigo}, '{ocorrencia.ocorrencia}')";

        //    Banco.Inserir(comando);


        //    Banco.Desconectar();
        //}

        #region Preencher CMB Ocorrências

        public List<string> PreencherOcorrencias()
        {
            List<string> listaNomes = new List<string>();
            string nomeOcorrencia = "";
            
            try
            {
                string nomeProcedure = "PreencherOcorrencias";
                List<clsParametro> parametros = new List<clsParametro>();

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        nomeOcorrencia = dados[0].ToString();
                        listaNomes.Add(nomeOcorrencia);
                    }
                }

                return listaNomes;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao carregar ocorrências");
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

        #region Enviar Ocorrência

        public void EnviarOcorrencia(string login, string descricaoOcorrencia, string tipoOcorrencia, int codigoExemplar, int codigoLivro, int codigoEmprestimo)
        {
            try
            {
                string nomeProcedure = "EnviarOcorrencia";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vLogin", login));
                parametros.Add(new clsParametro("vDescricao", descricaoOcorrencia));
                parametros.Add(new clsParametro("vNomeTipoOcorrencia", tipoOcorrencia));
                parametros.Add(new clsParametro("vCodigoExemplar", codigoExemplar.ToString()));
                parametros.Add(new clsParametro("vCodigoLivro", codigoLivro.ToString()));
                parametros.Add(new clsParametro("vCodigoEmprestimo", codigoEmprestimo.ToString()));

                InserirProc(nomeProcedure, parametros);
            }
            catch(Exception)
            {
                throw new Exception("Erro ao enviar ocorrência");
            }
            finally
            {
                Desconectar();
            }
        }       

        #endregion
    }
}
