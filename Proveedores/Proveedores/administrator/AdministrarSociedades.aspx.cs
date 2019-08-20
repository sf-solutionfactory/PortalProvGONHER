using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class AdministrarSociedades : System.Web.UI.Page
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

            String prov = "";

            try
            {
                prov = Session["idProveedorSeleccionadoSoc"].ToString();
                List<int> listaProveedor = new List<int>();
                mostrarSociedades(cargarSociedades(prov), listaProveedor);
            }
            catch (Exception)
            {
                
                throw;
            }
            }
        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("config.aspx");
        }


        private List<string[]> cargarSociedades(string rfc)
        {
            PNegocio.Administrador.Usuario inst = new PNegocio.Administrador.Usuario();

            List<string[]> resultado = inst.cosultarSociedadesPorprov(rfc, "90%");
            return resultado;


        }

        private void mostrarSociedades(List<string[]> resultado, List<int> listaNumeros)
        {
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                this.ltlTablaSociedades.Text = Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + "90%" + ";", listaEvitar, false, true, false, false, 0, 1);
                Session["TablaSociedades"] = resultado;
            }
            else
            {
                this.ltlTablaSociedades.Text = "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            String prov = "";
            String nombre = "";
            String descripcion = "";
            try
            {
                prov = Session["idProveedorSeleccionadoSoc"].ToString();
                nombre = Session["nombreSeleccionadoSoc"].ToString();
                descripcion = Session["descripcionSeleccionadoSoc"].ToString();
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("usuario.aspx?"+"rfc=" + prov + "&nom=" + nombre + "&desc=" + descripcion + "&campo=Proveedor&primerproveedor=me"); 
            
        } 
    }
}