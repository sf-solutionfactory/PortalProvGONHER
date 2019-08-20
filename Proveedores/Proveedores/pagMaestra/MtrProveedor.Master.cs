using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using PNegocio;
namespace Proveedores
{
    public partial class MtrProveedor : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DatoMaestro dm = new DatoMaestro();
                string userName = HttpContext.Current.User.Identity.Name;
                this.lblUsuario.Text = userName;
                //List<string[]> endpoints = new List<string[]>();

                //endpoints = new PNegocio.getEndpoints().consultarEndpoints(Session["lifnr"].ToString(), Session["ProveedorLoged"].ToString());
                //for (int i = 0; i < endpoints.Count; i++)
                //{

                //try
                //{
                //    string[] res = new string[2];
                //    res = endpoints[0];
                //    string[] userPass = new string[2];
                //    userPass[0] = res[2];
                //    userPass[1] = res[3];
                //    var objProveedor = dm.getDatoMaestro(Session["lifnr"].ToString(), res[0], userPass);
                //    Session["proveedor"] = objProveedor;
                //    this.lblUsuario.Text = objProveedor.NAME1;
                //    // esto se pone mientras se cambia el metodo de pintar los datos maestros
                //}
                //catch (Exception)
                //{

                //}

                this.lblMenu.Text = generarMenu();

            }
            catch (Exception)
            {
                
            }
        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            //System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            //Response.Redirect("Inicio.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            Session.RemoveAll();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect(Request.UrlReferrer.ToString());
        }



        private string generarMenu()
        {
            string rtnMenu = "";

            //string[] resLog = (string[])Session["resLog"];
            //PNegocio.Usuario nUsuario = new PNegocio.Usuario();
            //int[] idPantallas = nUsuario.getIdPantallasByIdRol(int.Parse(resLog[2]));
            //Session["Pantallas"] = idPantallas;
            int[] idPantallas = (int[])Session["Pantallas"];
            //for (int i = 0; i < idPantallas.Length; i++)
            for (int i = 0; i < idPantallas.Length; i++)
            {
                switch (idPantallas[i])
                {
                    case 0:
                        rtnMenu += "<li id='Inicio' ><a href='Inicio.aspx' ><span>Inicio</span></a></li>";
                        break;
                    case 1:
                        rtnMenu += "<li id='facturas' ><a href='facturas.aspx' ><span>Facturas MIR7</span></a></li>";
                        break;
                    case 2:
                        rtnMenu += "<li id='prtAbiertas' class='last'><a href='prtAbiertas.aspx' ><span>Facturas Liberadas</span></a></li>";
                        break;
                    case 4:
                        rtnMenu += "<li id='pagos' class='last'><a href='pagos.aspx'><span>Pagos</span></a></li>";
                        break;
                    case 8:
                        rtnMenu += "<li id='datosMaestros' ><a href='datosMaestros.aspx'><span>Mis Datos</span></a></li>";
                        break;
                    case 16:
                        rtnMenu += "<li id='usuarios' ><a href='usuarios.aspx'><span>Usuarios</span></a></li>";
                        break;
                    case 32:
                        rtnMenu += "<li id='factFV60' ><a href='factFV60.aspx'><span>Facturas FV60</span></a></li>";
                        break;
                }
            }
            //Session["objUsuario"] = objUsuario;//Lo mando por que se borraba de la sesión
            Session["menu"] = rtnMenu;
            return rtnMenu;
        }
    }
}
