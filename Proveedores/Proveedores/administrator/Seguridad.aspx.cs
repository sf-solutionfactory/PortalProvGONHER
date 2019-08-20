using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAP;
using SAP.Middleware.Connector;

namespace Proveedores.administrator
{
    public partial class Seguridad : System.Web.UI.Page
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

            //INICIO mostrar mensaje en dialogo
            string dialog = "";
            try
            {
                dialog = Session["textoDialogo"].ToString();
                
                this.lblDialog.Text = dialog;
                Session["textoDialogo"] = "";
            }
            catch (Exception)
            {
                this.lblDialog.Text = "";
            }
            //FIN mostrar mensaje en dialogo
            consultarCredencialesAnaceptables();
            }
        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("config.aspx");
        }

        private void insertarCredInac()
        {
            PNegocio.Administrador.Seguridad objInstancia = new PNegocio.Administrador.Seguridad();
            PNegocio.Encript encript = new PNegocio.Encript();
            string res = objInstancia.insertarCredInacep(encript.Encriptar(encript.Encriptar(this.txtCredencial.Text)));
            this.lblDialog.Text  = "Insertado";
            consultarCredencialesAnaceptables();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

        }

        protected void btnGCredInac_Click(object sender, EventArgs e)
        {
            if(this.hidVerificar.Value == "si"){
                insertarCredInac();
            }
            
        }

        private void consultarCredencialesAnaceptables(){
            this.ltlTablaCredInaceptadas.Text = new PNegocio.Administrador.Seguridad().consultarCredenciales();
            if (this.ltlTablaCredInaceptadas.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltro();
            }
        }

        //  llamada a SAP desde código.
        //protected void conSAp()
        //{
        //    string res = "";
        //    PNegocio.FactFV60 fv60 = new PNegocio.FactFV60();
        //    //res = fv60.exec_connSAP();
        //    fv60.exec_connSAP();
        //}

        //protected void Button1_Click(object sender, EventArgs xe)
        //{
        //    conSAp();
        //}
        // llamada fin
    }
}