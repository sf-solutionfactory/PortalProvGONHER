using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using PNegocio;

namespace Administrador.Proveedores
{
    public partial class MtrAdministrador : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //FIN mostrar mensaje en dialogo

            //try
            //{
            //    DatoMaestro dm = new DatoMaestro();
            //    string userName = HttpContext.Current.User.Identity.Name;
            //    var objProveedor = dm.getDatoMaestro(userName);
            //this.lblUsuario.Text = "Adminitrador";
            //    Session["proveedor"] = objProveedor;
            //}
            //catch (Exception ex)
            //{

            //}
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {

            
            ////se borra la cookie de autenticacion
            //System.Web.Security.FormsAuthentication.SignOut();

            ////se redirecciona al usuario a la pagina de login
            //Response.Redirect(Request.UrlReferrer.ToString()); 
        }

        
    }
}