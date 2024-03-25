using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    public class clsModelAutor : ConexaoBanco
    {
        public string Nome { get; set; }
        public int Codigo { get; set; }
        MySqlDataReader dados = null;

        public clsModelAutor(string nome, int codigo)
        {
            this.Nome = nome;
            this.Codigo = codigo;
        }

        public clsModelAutor(int codigo)
        {
            try
            {
                string nomeProcedure = "PreencherAutor";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vCodigo", codigo.ToString()));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        Codigo = codigo;
                        Nome = dados[0].ToString();
                    }
                }
            }
            catch(Exception)
            {
                throw new Exception("Erro ao preencher autor");
            }
            finally
            {
                if (dados != null)
                    if (!dados.IsClosed)
                        dados.Close();
                Desconectar();
            }

            this.Codigo = codigo;
        }
    }
}
