using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class MostrarPantalla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.hidCerrarSesion.Value != "cerrar")
            {
            
            try
            {
                string[] resLog = null;
                resLog = (string[])Session["resLog"];
                //string sesion = Session["idUsuarioProveedor"].ToString().Trim();
                //if (sesion == "Admin")
                if (resLog[2].ToString() == "0")
                {

                }
                else
                {
                    cerrarSesion();
                }
            }
            catch (Exception)
            {
                cerrarSesion();
            }
            this.hidVinculador.Value = Request.QueryString["vinculador"];
            this.HidIsntanciSap.Value = Request.QueryString["instancia"];
            this.HidEndpoint.Value = Request.QueryString["endpoint"];

            this.lblTEst.Text = Request.QueryString["endpoint"];
            mostrarTablarPantallas();
            }
        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("config.aspx");
        }

        private void mostrarTablarPantallas() {
            this.lblResultado.Text = new PNegocio.Administrador.Pantalla().consultarPantalla("70%");
        }

    }
}