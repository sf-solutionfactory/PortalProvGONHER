using PEntidades;
using PNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.portal
{
    public partial class FactFV60 : System.Web.UI.Page
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
                        if (idPantallas[i] == 32)
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
                string xfec = Session["FVfec1"].ToString();
                if (this.datepicker.Text != "" && this.datepicker2.Text != "")
                {
                    Session["FVfec1"] = this.datepicker.Text;
                    Session["FVfec2"] = this.datepicker2.Text;
                }
                else if (xfec != "")
                {
                    this.datepicker2.Text = String.Format("{0:MM/dd/yyyy}", Session["FVfec2"].ToString());  
                    this.datepicker.Text = String.Format("{0:MM/dd/yyyy}", Session["FVfec1"].ToString());   
                }
                else     //en la carga inicial usar fechas para info de un mes atras - mgv
                {
                    this.datepicker2.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);       //final
                    this.datepicker.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Today.AddMonths(-1));   //inicial DateTime.Today.AddMonths(-1).ToShortDateString();     
                    Session["FVfec1"] = this.datepicker.Text;
                    Session["FVfec2"] = this.datepicker2.Text;
                }
                cargardatos(false);
            }
        }

        [WebMethod]
        public void cargardatos(bool adjuntar)
        {
            PNegocio.FactFV60 fv60 = new PNegocio.FactFV60();
            List<PEntidades.FV60XVerificar> listFV = new List<FV60XVerificar>();

            try
            {
                try
                {
                    if (this.hidActualiza.Value != "actualiza" && Request.Form["actualizar"] != "actualiza")
                    {
                        listFV = (List<PEntidades.FV60XVerificar>)Session["lstFacturas2"];
                    }
                }
                catch (Exception)
                {
                }

                string mensaje = "";
                if (listFV == null || listFV.Count <= 0)
                {
                    string flow = Gen.Util.CS.Gen.convertirFecha_SAP_CN(this.datepicker.Text.Trim());
                    string fhig = Gen.Util.CS.Gen.convertirFecha_SAP_CN(this.datepicker2.Text.Trim());
                    string refLow = this.txtRef2.Text.Trim();
                    string xProv = Session["lifnr"].ToString();              //traer el proveedor que está consultando

                    //if (refLow == "")
                    //{
                    //    //  q valor mover a refLow si va vacio?
                    //}

                    listFV = fv60.exec_connSAP(xProv, refLow, flow, fhig);   //cambiar llamado GENERICO para lista de tablas y valores
                    if (listFV.Count == 0)
                    {
                        string msg = fv60.msg;
                            if (string.IsNullOrEmpty(msg))
                            {
                                this.lblDialog.Text = "No se encontraron resultados";
                                this.lblDialog.Visible = true;
                            }
                            else
                            {
                                this.lblDialog.Text = msg;
                                this.lblDialog.Visible = true;
                            }
                    }
                    Session["lstFacturas2"] = listFV; //----new----- // se guarda en la sesion el resultado
                    /*Pinta la lista en còdigo HTML*/
                }

                PNegocio.ConvertTittles conv = new PNegocio.ConvertTittles();
                if (listFV.Count > 0)
                {
                    this.lblTabla.Text = conv.convertListFV60ToTableInCodeFacturas(listFV);
                    this.btnActualiza.Visible = true;
                }
                else
                {
                    if (String.IsNullOrEmpty(mensaje))
                    {
                        this.lblTabla.Text = fv60.msg;    /// "<br/><br/><br/><br/><h3>Ingrese un dato para mostrar Factura</h3>";
                        //if (status.Length > 0)
                        //{
                        //    for (int i = 0; i < status.Length; i++)
                        //    {
                        //        if (status[i] != "" && status[i] != null)
                        //        {
                        //            this.lblTabla.Text += "<br/><h3>" + status[i] + "</h3>";
                        //        }
                        //    }
                        this.lblTabla.Text += "<br/><h3>" + "Se recomienda intentar utilizando los campos para selección especifica(Referencia y Fecha)" + "</h3>";
                        //}
                        //if (n_instancias <= 0)
                        //{
                        //    this.lblTabla.Text = "<br/><br/><br/><h3>" + "Este usuario no tiene sociedades activas, por lo que no puede obtener datos" + "</h3>";
                        //}
                    }
                    else
                    {
                        this.lblTabla.Text = "<br/><br/><br/><br/><h3>Por el momento no se tiene acceso a las facturas porque están siendo tratadas por el administrador. </br> Actualice o intente más tarde.</h3>";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
                this.lblTabla.Text = "<h3>Ocurrio un error al obtener los datos<h3>";
            }
        }

        [WebMethod]
        public static string desadjuFV60XML(string uuid)  
        {
            string[] uui = uuid.Split(',');       //enla primera parte del parametro va el uuid
            string[] xvals = uui[1].Split('+');   //la segunda conformada x  string bukrs, string gjahr, string belnr  :mgv
            string mensaje = "";
            int cantidad = 0;
            PNegocio.CargarFV60 nFac = new PNegocio.CargarFV60();
            try
            {  
                cantidad = nFac.desvincularFV60(uui[0], xvals[0], xvals[1], xvals[2]);
                if (cantidad > 1)
                {
                    mensaje = "<br> Se desadjuntaron " + cantidad + " XML/s y " + cantidad + " PDF/s. <br>";
                }
                else
                {
                    mensaje = "<br> Se desadjunto " + cantidad + " XML y " + cantidad + " PDF. <br>";
                }
            }
            catch (Exception)
            {
                mensaje = "Error al quitar archivos adjuntos.";
            }
            return mensaje;
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