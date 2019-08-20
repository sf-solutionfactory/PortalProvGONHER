using PEntidades;
using PNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.portal
{
    public partial class prtAbiertas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.hidCerrarSesion.Value != "cerrar")
            {
                this.btnActualiza.BorderStyle = BorderStyle.None;
                this.btnActualiza.Visible = false;
                this.btnActualizaX.BorderStyle = BorderStyle.None;
                //INICIO Permiso de ver esta pantalla
            bool permiso = false;
            try
            {
                int[] idPantallas = (int[])Session["Pantallas"];
                for (int i = 0; i < idPantallas.Length; i++)
                {
                    if (idPantallas[i] == 2)
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

            PartidasAbiertas res = new PartidasAbiertas();
            
            string fecha1 = this.datepicker.Text;
            string fecha2 = this.datepicker2.Text;

            try
            {
                int n_instancias = 0;
                List<string[]> listaDiferentesInstancias = (List<string[]>)Session["listaDiferentesInstancias"];
                n_instancias = listaDiferentesInstancias.Count;

                List<PAbiertasYPago> lstPAbiertas = res.getfacturasAbiertas(fecha1, fecha2, listaDiferentesInstancias);

                ConvertTittles conv = new ConvertTittles();

                if (lstPAbiertas.Count > 0)
                {
                    this.lblTabla.Text = conv.convertListPAbiertasToTableInCodeFacturas(lstPAbiertas);   
                }
                else
                {
                    string[] status = res.status;
                    this.lblTabla.Text = "<br/><br/><h3>No se encontraron datos<h3>";
                    if (status.Length > 0)
                    {
                        for (int i = 0; i < status.Length; i++)
                        {
                            if (status[i] != "" && status[i] != null)
                            {
                                this.lblTabla.Text += "<br/><h3>" + status[i] + "</h3>";
                            }
                        }
                        this.lblTabla.Text += "<br/><h3>" + "Se recomienda intentar utilizando los campos de fecha" + "</h3>";

                        this.btnActualiza.Visible = true;
                    }
                    if (n_instancias <= 0)
                    {
                        this.lblTabla.Text = "<br/><br/><br/><h3>" + "Este usuario no tiene sociedades activas, por lo que no puede obtener datos" + "</h3>";
                    }
                }
                //if (this.lblTabla.Text  != "")
                //{
                //    this.btnActualiza.Visible = true;
                //}
            }
            catch (Exception)
            {
                this.lblTabla.Text = "<h3>Ocurrio un error al obtener los datos<h3>";
            }         
                
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //btnbuscar = true;
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