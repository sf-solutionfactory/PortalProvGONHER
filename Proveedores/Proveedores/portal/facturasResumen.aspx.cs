using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.portal
{
    public partial class facturasResumen : System.Web.UI.Page
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
            //      Pintar los que si se encontraron
            PNegocio.ConvertTittles ct = new PNegocio.ConvertTittles();
            List<PEntidades.FacturasXVerificar> lstFact = new List<PEntidades.FacturasXVerificar>();
            if (Session["lstResumen"] != null)
            {
                lstFact = (List<PEntidades.FacturasXVerificar>)Session["lstResumen"];
                this.lblTblEncontrados.Text = ct.convertirAHtmlTable(lstFact);
                this.lblNumEnc.Text = "Facturas encontradas " + lstFact.Count;
                Session["lstResumen"] = null;
            } else {
                this.lblNumEnc.Text = "";
            }
            
            //      Pintar los que no se encontraron
            List<string[]> lstNoEnc = null;
            if (Session["lstNoEnc"] != null) {
                lstNoEnc = (List<string[]>)Session["lstNoEnc"];
                this.lblTblNoEncontrados.Text = convertirAHtml(lstNoEnc);
                this.lblNumNoEnc.Text = "Facturas no encontradas " + lstNoEnc.Count;
                Session["lstNoEnc"] = null;
            } else {
                this.lblNumNoEnc.Text = "";
            }

            //      Pitar el número de arcivos procesados
            this.lblNumArchivos.Text = "" + (((lstNoEnc != null) ? lstNoEnc.Count : 0) + ((lstFact != null) ? lstFact.Count : 0));
        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("Inicio.aspx");
        }

        private string convertirAHtml(List<string[]> lstNoEnc)
        {
            string html = "";
            html += "<table class='tblComun'>";
                html += "<thead>";
                html += "<tr>"+
                            "<th>Archivo</th>"+
                            "<th>Folio</th>"+
                        "</tr>";
                html += "</thead>";
                html += "<tbody>";
                for (int i = 0; i < lstNoEnc.Count; i++)
                {
                    html += "<tr>";
                    html += "<td>" + lstNoEnc[i][0] + "</td>";
                    html += "<td>" + lstNoEnc[i][1] + "</td>";
                    html += "</tr>";
                }
                html += "</tbody>";
            html += "</table>";
            return html;
        }
    }
}