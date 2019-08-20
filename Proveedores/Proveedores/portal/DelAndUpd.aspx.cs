using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.portal
{
    public partial class DelAndUpd : System.Web.UI.Page
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
                    if (idPantallas[i] == 16)
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

            string identificador = Request.Form["identificador"].ToString();
            string pantalla = Request.Form["pantalla"].ToString();
            string desicion = Request.Form["desicion"].ToString();
            string complemento = Request.Form["complemento"].ToString();

            string resultado = "";
            string pag = "";
            string sqlString = "";
            int numSet = 1;

            string resParaSession = "";

            if (desicion == "Eliminar")
            {
                char[] delimiterChars = { ',' };
                switch (pantalla)
                {
                    default:
                        break;
                }
                resParaSession = "Eliminado";
            }
            else
            {
                if (desicion == "Desactivar")
                {
                    numSet = 0;
                    resParaSession = "Desactivado";
                }
                if (desicion == "Activar")
                {
                    numSet = 1;
                    resParaSession = "Activado";

                }
                switch (pantalla)
                {
                    case "usuarioPortalP":
                        sqlString = "update usuario set esBloq = " + numSet + " where usuarioLog = '" + identificador + "';";
                        pag = "usuarios.aspx";
                        break;
                    default:
                        break;
                }
            }

            resultado = new PNegocio.Administrador.Eliminar().ejecutarQueryWhitTran(sqlString);

            if (resultado != "0" && resultado != "")
            {
                Session["textoDialogo"] = resParaSession;

            }
            else
            {
                Session["textoDialogo"] = "La ultima operación no se concretó correctamente, posiblemente ocurrio un error o por el momento es una acción no permitida";
            }

            Response.Write(pag);

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