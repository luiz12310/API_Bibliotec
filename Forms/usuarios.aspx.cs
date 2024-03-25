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
    public partial class usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsLogicGerenciarUsuario gerenciarUsuario = new clsLogicGerenciarUsuario();
            List<clsModelUsuario> jSON = null;

            if (String.IsNullOrEmpty(Request["nomeUsuario"]))
            {
                jSON = gerenciarUsuario.ListarUsuarios();
            }
            else
            {
                string pesquisaNome = Request["nomeUsuario"];
                jSON = gerenciarUsuario.BuscarUsuario(pesquisaNome);
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string dadosJSON = jss.Serialize(jSON);

            Response.Write(dadosJSON);
        }
    }
}