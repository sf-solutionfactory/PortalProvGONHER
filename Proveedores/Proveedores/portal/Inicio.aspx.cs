using PNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.portal
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            bool permiso = false;
            try
            {
                //INICIO Permiso de ver esta pantalla
                string[] resLog = (string[])Session["resLog"];
                PNegocio.Usuario nUsuario = new PNegocio.Usuario();
                int[] idPantallas = nUsuario.getIdPantallasByIdRol(int.Parse(resLog[2]));
                Session["Pantallas"] = idPantallas;
                //int[] idPantallas = (int[])Session["Pantallas"];
                for (int i = 0; i < idPantallas.Length; i++)
                {
                    if (idPantallas[i] == 1 ||
                        idPantallas[i] == 2 ||
                        idPantallas[i] == 4 ||
                        idPantallas[i] == 8 ||
                        idPantallas[i] == 16 ||
                        idPantallas[i] == 32
                        )
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
            ////FIN Permiso de ver esta pantalla
            Session["fecha1"] = ""; // fechas usadas en pantalla PAGOS
            Session["fecha2"] = "";
            Session["FVfec1"] = ""; //mgv fechas de busqueda en la FV60
            Session["FVfec2"] = "";
            Session["M7fec1"] = ""; //mgv fechas de busqueda en la MIR7
            Session["M7fec2"] = "";
            Session["PantallaUsuario"] = "";

            try
            {
                string idProveedor = Session["ProveedorLoged"].ToString();
                string[] resultado = null;

                    resultado = new PNegocio.Administrador.Noticia().consultarNoticiaPorIdProveedor(idProveedor);
                    this.lblTitulo.Text = resultado[0];
                    this.lblArticulo.Text = resultado[1];
                    //if (resultado != null)
                    //{
                        if (resultado[2].ToString().Trim() != "" && resultado[2].ToString().Trim() != null)
                        {
                            this.lblBanner.Text = "<img src='" + resultado[2] + "'/>";
                        }
                        else
                        {
                            this.lblBanner.Text = "";
                        }
                    //}
                    //else
                    //{
                    //    this.lblArticulo.Text = "No existen noticias por el momento, este al pendiente";
                    //    //this.lblBanner.Text = "<img src='../images/adn_logo.png' />";
                    //}

            }
            catch (Exception)
            {
                this.lblArticulo.Text = "No existen noticias por el momento, este al pendiente";
            }
            try
            {
                DatoMaestro dm = new DatoMaestro();
                string userName = HttpContext.Current.User.Identity.Name;
                // mgv. Crear lista para WS y lista para CN
                List<string[]> informacionImportante = new PNegocio.Usuario().getSociedadesByUsuario(userName);      // sociedad_bukrs as bukrs, RFC, lifnr, instancia WS
                List<string[]> listaDiferentesInstancias = Gen.Util.CS.Gen.sintetizaInfoConexiones(informacionImportante);
                Session["listaDiferentesInstancias"] = listaDiferentesInstancias ;
                //List<string[]> informacionImportanteCN = new PNegocio.Usuario().getSociedadesByUsuarioCN(userName); // sociedad_bukrs as bukrs, RFC, lifnr, CN  
                //List<string[]> listaInstanciasCN = Gen.Util.CS.Gen.sintetizaInfoCN(informacionImportanteCN);
                //Session["listaInstanciasCN"] = listaInstanciasCN;
                /// FIN
            }
            catch (Exception)
            {
                //throw;
            }
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