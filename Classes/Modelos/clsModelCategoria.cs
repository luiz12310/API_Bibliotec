using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    public class clsModelCategoria : ConexaoBanco
    {
        public string Nome { get; set; }
        public int Codigo { get; set; }
        MySqlDataReader dados = null;

        public clsModelCategoria(string nome, int codigo)
        {
            this.Nome = nome;
            this.Codigo = codigo;
        }

        public clsModelCategoria(int codigo)
        {
            try
            {
                string nomeProcedure = "PreencherCategoria";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vCodigo", codigo.ToString()));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while (dados.Read())
                    {
                        Codigo = codigo;
                        Nome = dados[0].ToString();
                    }
                }
            }
            catch(Exception)
            {
                throw new Exception("Erro ao preencher categoria");
            }
            finally
            {
                if (dados != null)
                    if (!dados.IsClosed)
                        dados.Close();
                Desconectar();
            }
        }

        public clsModelCategoria()
        {
            
        }

        #region Listar Categorias

        public List<clsModelCategoria> ListarCategoria()
        {
            try
            {
                string nomeProcedure = "ListarCategoria";
                List<clsParametro> parametros = new List<clsParametro>();

                List<clsModelCategoria> listaCategorias = new List<clsModelCategoria>();

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        clsModelCategoria categoria = new clsModelCategoria(dados[0].ToString(), int.Parse(dados[1].ToString()));
                        listaCategorias.Add(categoria);
                    }
                }

                return listaCategorias;
            }
            catch(Exception)
            {
                throw new Exception("Erro ao listar categorias");
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
