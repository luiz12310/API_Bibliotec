using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    public class clsModelTipoUsuario : ConexaoBanco
    {
        public int codigo { get; set; }
        public string nome { get; set; }

        MySqlDataReader dados = null;

        public clsModelTipoUsuario(int codigoTipoUser)
        {
            try
            {
                string nomeProcedure = "PreencherDadosTipoUsuario";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vCodigoTipoUser", codigoTipoUser.ToString()));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        codigo = codigoTipoUser;
                        nome = dados[0].ToString();
                    }
                }
            }
            catch(Exception)
            {
                throw new Exception("Erro ao preencher tipo do usuário");
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
