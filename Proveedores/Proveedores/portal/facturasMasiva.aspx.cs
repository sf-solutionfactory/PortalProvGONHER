using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.portal
{
    public partial class facturasMasiva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //INICIO Permiso de ver esta pantalla
            bool permiso = false;
            try
            {
                int[] idPantallas = (int[])Session["Pantallas"];
                for (int i = 0; i < idPantallas.Length; i++)
                {
                    if (idPantallas[i] == 1)
                    {
                        permiso = true;
                        break;
                    }

                }
                if (permiso == false)
                {
                    cerrarSesion();
                }

            }
            catch (Exception)
            {
                cerrarSesion();
            }
            //FIN Permiso de ver esta pantalla

        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("Inicio.aspx");
        }
    }
}