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
    public partial class categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsModelCategoria categoria = new clsModelCategoria();
            List<clsModelCategoria> jSON = categoria.ListarCategoria();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string dadosJSON = jss.Serialize(jSON);

            Response.Write(dadosJSON);
        }
    }
}