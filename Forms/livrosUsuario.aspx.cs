using prj_36400_36403_36405.Lógica;
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
    public partial class livrosUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request["loginUsuario"]))
            {
                string login = Request["loginUsuario"];

                clsLogicGerenciarUsuario gerenciarFunc = new clsLogicGerenciarUsuario();
                List<clsModelLivro> jSON = gerenciarFunc.ListarLivros(login);

                JavaScriptSerializer jss = new JavaScriptSerializer();
                string dadosJSON = jss.Serialize(jSON);

                Response.Write(dadosJSON);
            }

            if (!String.IsNullOrEmpty(Request["livro"]))
            {
                string codigoLivro = Request["livro"];

                clsModelLivro livro = new clsModelLivro(int.Parse(codigoLivro));

                JavaScriptSerializer jss = new JavaScriptSerializer();
                string dadosJSON = jss.Serialize(livro);

                Response.Write(dadosJSON);
            }
        }
    }
}