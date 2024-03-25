using prj_36400_36403_36405.Lógica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace API_MOBILE_BIBLIOTEC_C_.Forms
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
            {
            clsLogicGerenciarUsuario gerenciarUser = new clsLogicGerenciarUsuario();

            try
            {
                if(!String.IsNullOrEmpty(Request["login"]) && !String.IsNullOrEmpty(Request["password"]))
                {
                    string login = Request["login"];
                    string senha = Request["password"];

                    string retorno = gerenciarUser.AcessarSistema(login, senha).ToString();
                    
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    string dadosJSON = jss.Serialize(retorno);

                    Response.Write(dadosJSON);
                }
            }
            catch(Exception)
            {
                throw new Exception("Erro no backend");
            }

        }
    }
}