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
    public partial class usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request["loginUsuario"]) || Request["loginUsuario"] != "null")
            {
                string login = Request["loginUsuario"];

                clsModelUsuario usuario = new clsModelUsuario(login);

                JavaScriptSerializer jss = new JavaScriptSerializer();
                string dadosJSON = jss.Serialize(usuario);

                Response.Write(dadosJSON);
            }

        }
    }
}