using MySql.Data.MySqlClient;
using prj_36400_36403_36405.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405
{
    public class ConexaoBanco
    {
        private string linhaConexao { get; set; }
        MySqlConnection conexao = null;
        MySqlCommand cSQL = null;

        public ConexaoBanco(string host, string user, string senha, string db)
        {
            linhaConexao = "SERVER=localhost;UID=root;PASSWORD=root;DATABASE=BiblioTec";
        }

        public ConexaoBanco()
        {
            linhaConexao = "SERVER=localhost;UID=root;PASSWORD=root;DATABASE=BiblioTec";
        }

        public void Conectar()
        {
            try
            {
                conexao = new MySqlConnection(linhaConexao);
                conexao.Open();
            }
            catch(Exception)
            {
                throw new Exception("Não foi possível conectar do Banco de Dados");
            }
        }

        public void Desconectar()
        {
            try
            {
                if (conexao != null)
                {
                    if (conexao.State == System.Data.ConnectionState.Open)
                        conexao.Close();
                }
            }
            catch(Exception)
            {
                throw new Exception("Não foi possível se desconectar do Banco de Dados");
            }
        }

        public MySqlDataReader Consultar(string comando)
        {
            try
            {
                cSQL = new MySqlCommand(comando, conexao);
                return cSQL.ExecuteReader();
            }
            catch
            {
                throw new Exception("Ocorreu um erro na Consulta.");
            }
        }

        public void Inserir(string comando)
        {
            try
            {
                cSQL = new MySqlCommand(comando, conexao);
                cSQL.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Ocorreu um erro na Inserção.");
            }
        }

        public MySqlDataReader ConsultarProc(string nomeProcedure, List<clsParametro> parametros)
        {
            try
            {
                Conectar();

                cSQL = new MySqlCommand(nomeProcedure, conexao);
                cSQL.CommandType = System.Data.CommandType.StoredProcedure;
                cSQL.Parameters.Clear();

                for(int i = 0; parametros.Count > i; i++)
                {
                    cSQL.Parameters.AddWithValue(parametros[i].nome, parametros[i].valor.ToString());
                }

                return cSQL.ExecuteReader();
            }
            catch(Exception)
            {
                throw new Exception("Erro ao Consultar comando");
            }
        }

        public void InserirProc(string nomeProcedure, List<clsParametro> parametros)
        {
            try
            {
                Conectar();

                cSQL = new MySqlCommand(nomeProcedure, conexao);
                cSQL.CommandType = System.Data.CommandType.StoredProcedure;
                cSQL.Parameters.Clear();

                for (int i = 0; parametros.Count > i; i++)
                {
                    cSQL.Parameters.AddWithValue(parametros[i].nome, parametros[i].valor.ToString());
                }

                cSQL.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao executar comando");
            }
        }
    }
}
