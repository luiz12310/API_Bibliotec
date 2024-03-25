using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_36400_36403_36405.Modelos
{
    public class clsModelLivro : ConexaoBanco
    {
        public int Codigo { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public int AnoEdicao { get; set; }
        public string Sinopse { get; set; }
        public clsModelEditora Editora { get; set; } 
        public List<clsModelAutor> Autor { get; set; }
        public List<clsModelCategoria> Categoria { get; set; }
        public string Disponibilidade { get; set; }

        public string Capa { get; set; }

        MySqlDataReader dados = null;
        clsLogicGerenciarLivro gerenciarLivro = new clsLogicGerenciarLivro();

        public clsModelLivro(int codigo, string isbn, string titulo, int ano, string sinopse, clsModelEditora editora, List<clsModelAutor> autor)
        {
            this.Codigo = codigo;
            this.ISBN = isbn;
            this.Titulo = titulo;
            this.AnoEdicao = ano;
            this.Sinopse = sinopse;
            this.Editora = editora;
            this.Autor = autor;
        }

        public clsModelLivro(string nomeLivro, string listAutores, int codigoEditora, int codigoLivro)
        {
            Titulo = nomeLivro;
            Codigo = codigoLivro;
            Editora = new clsModelEditora(codigoEditora);
            //ListaAutores = /*listAutores*/;
        }

        public clsModelLivro(int codigoLivro)
        {
            try
            {
                string nomeProcedure = "PreencherDadosLivro";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vCodigoLivro", codigoLivro.ToString()));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        Codigo = codigoLivro;
                        Titulo = dados[0].ToString();
                        ISBN = dados[1].ToString();
                        AnoEdicao = int.Parse(dados[2].ToString());
                        Sinopse = dados[3].ToString();
                        Editora = new clsModelEditora(int.Parse(dados[4].ToString()));

                        char[] cha = { ',' };
                        string[] arrayNumeros = dados[5].ToString().Split(cha); 
                         
                        Categoria = new List<clsModelCategoria>();

                        for(int i = 0; i < arrayNumeros.Length; i++)
                        {
                            clsModelCategoria categoria = new clsModelCategoria(int.Parse(arrayNumeros[i]));
                            Categoria.Add(categoria);
                        }

                        string[] arrayAutores = dados[6].ToString().Split(cha);

                        Autor = new List<clsModelAutor>();

                        for(int i = 0; i < arrayAutores.Length; i++)
                        {
                            clsModelAutor autor = new clsModelAutor(int.Parse(arrayAutores[i]));
                            Autor.Add(autor); 
                        }

                        Disponibilidade = gerenciarLivro.ListarDisponibilidade(codigoLivro);
                    }
                }
            }
            catch(Exception)
            {
                throw new Exception("Erro ao preencher dados do livro");
            }
            finally
            {
                if (dados != null)
                    if (!dados.IsClosed)
                        dados.Close();
                Desconectar();
            }
        }

        public clsModelLivro()
        {

        }

        // Métodos

        #region Detalher Livro

        public void DetalharLivro(int codigoLivro)
        {
            try
            {
                string nomeProcedure = "DetalharLivro";
                List<clsParametro> parametros = new List<clsParametro>();
                parametros.Add(new clsParametro("vCodigoLivro", codigoLivro.ToString()));

                dados = ConsultarProc(nomeProcedure, parametros);

                if(dados.HasRows)
                {
                    while(dados.Read())
                    {
                        Titulo = dados[0].ToString();
                        ISBN = dados[1].ToString();
                        Editora = new clsModelEditora(int.Parse(dados[2].ToString()));
                        AnoEdicao = int.Parse(dados[3].ToString());
                        Disponibilidade = dados[4].ToString() + "/" + dados[5].ToString();
                        //listaCategorias = dados[6].ToString();
                        //ListaAutores = dados[7].ToString();
                        Sinopse = dados[8].ToString();

                        if (!String.IsNullOrEmpty(dados[9].ToString()))
                        {
                            byte[] foto = null;
                            foto = (byte[])dados[9];
                            string base64String = Convert.ToBase64String(foto, 0, foto.Length);
                            Capa = base64String;
                        }
                    }
                }
            }
            catch(Exception)
            {
                throw new Exception("Erro ao detalhar livro");
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