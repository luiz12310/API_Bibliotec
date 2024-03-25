using prj_36400_36403_36405.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace API_MOBILE_BIBLIOTEC_C_.Forms
{
    public partial class livros : System.Web.UI.Page
    {
        clsLogicGerenciarLivro gerenciarLivro = new clsLogicGerenciarLivro();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<clsModelLivro> listaLivros = new List<clsModelLivro>();

            if (!String.IsNullOrEmpty(Request["categoria"]))
            {
                listaLivros = gerenciarLivro.ListarLivrosCategorias(int.Parse(Request["categoria"]));
            }
            else
            {
                if (!String.IsNullOrEmpty(Request["nomeLivro"]))
                {
                    listaLivros = gerenciarLivro.PesquisarLivro(Request["nomeLivro"]);
                }
                else
                {
                    listaLivros = gerenciarLivro.ListarTodosLivros();
                }
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string dadosJSON = jss.Serialize(listaLivros);

            Response.Write(dadosJSON);
        }
    }
}