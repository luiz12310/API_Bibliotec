using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    public class clsModelEditora : ConexaoBanco
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        MySqlDataReader dados = null;

        public clsModelEditora(int codigo, string nome)
        {
            this.Codigo = codigo;
            this.Nome = nome;
        }

        public clsModelEditora(int codigo)
        {
            try
            {
                string nomeProcedure = "PreencherDadosEditora";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vCodigoEditora", codigo.ToString()));

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
            catch
            {
                throw new Exception("Erro ao preencher dados da editora");
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
