using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.portal
{
    public partial class Facturas : System.Web.UI.Page
    {
        public static List<string[]> listaDiferentesInstanciasg;
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
                string xfec = Session["M7fec1"].ToString();
                if (this.datepicker.Text != "" && this.datepicker2.Text != "")
                {
                    Session["M7fec1"] = this.datepicker.Text;
                    Session["M7fec2"] = this.datepicker2.Text;
                }
                else if (xfec != "")
                {
                    this.datepicker2.Text = String.Format("{0:MM/dd/yyyy}", Session["M7fec2"].ToString());
                    this.datepicker.Text = String.Format("{0:MM/dd/yyyy}", Session["M7fec1"].ToString());
                }
                else     //en la carga inicial usar fechas para info de un mes atras - mgv
                {
                    this.datepicker2.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);       //final
                    this.datepicker.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Today.AddMonths(-1));   //inicial DateTime.Today.AddMonths(-1).ToShortDateString();     
                    Session["M7fec1"] = this.datepicker.Text;
                    Session["M7fec2"] = this.datepicker2.Text;
                }
                cargardatos(false);
            }
        }

        [WebMethod]
        public void cargardatos(bool adjuntar)
        {
            PNegocio.FacturasNE nFac = new PNegocio.FacturasNE();//Es el bean que tiene el acceso al web service
            List<PEntidades.FacturasXVerificar> lstFact = new List<PEntidades.FacturasXVerificar>();

            string ordenarOrden = "X";

            try
            {
                int n_instancias = 0;
                try
                {
                    if (this.hidActualiza.Value != "actualiza" && Request.Form["actualizar"] != "actualiza")
                    {
                        lstFact = (List<PEntidades.FacturasXVerificar>)Session["lstFacturas2"];
                    }
                }
                catch (Exception)
                {
                }
                string mensaje = "";
                if (lstFact == null || lstFact.Count <= 0)
                {
                    List<string[]> listaDiferentesInstancias = (List<string[]>)Session["listaDiferentesInstancias"];
                    listaDiferentesInstanciasg = listaDiferentesInstancias;

                    //string ordenarComo = this.rdbMostrarComo.Text.Trim();
                    //string ordenarRef = "";
                    //if (ordenarComo == "Referencia")
                    //{
                    //    ordenarOrden = "";
                    //    ordenarRef = "X";
                    //}

                    //Gen.Util.CS.Gen.convertirFecha(
                    string flow = Gen.Util.CS.Gen.convertirFecha_SAP(this.datepicker.Text.Trim());
                    string fhig = Gen.Util.CS.Gen.convertirFecha_SAP(this.datepicker2.Text.Trim());

                    string monedaHig = this.txtMoneda1.Text.Trim();
                    string monedaLow = this.txtMoneda2.Text.Trim();

                    //string factHig = Gen.Util.CS.Gen.convertirFecha_SAP(this.txtffact1.Text.Trim());
                    //string factLow = Gen.Util.CS.Gen.convertirFecha_SAP(this.txtffact2.Text.Trim());

                    string refhig = this.txtRef1.Text.Trim();
                    string refLow = this.txtRef2.Text.Trim();


                    if (refLow == "")
                    {
                        refLow = refhig;
                    }
                    if (refhig == "")
                    {
                        refhig = refLow;
                    }

                    //"f hig", "f low", "fact hig", "fact low", "moneda hig", "moneda low", objLifnr, ordenarOrden, ordenarRef, "ref hig", "ref low"
                    n_instancias = listaDiferentesInstancias.Count;
                    //if (n_instancias > 0)
                    //{
                    if (flow != "" || refLow != "" || monedaLow != "")
                    {
                        //mgv - probar llamada conexion SAP
                        //lstFact = nFac.getListFacturasNewConSAP(
                        //listaDiferentesInstancias,    ordenarOrden, "",        fhig, flow,
                        //"", "",                       monedaHig, monedaLow,    refhig, refLow, ref mensaje   );
                        //mgv - cambio a conexion SAP

                        lstFact = nFac.getListFacturasNew(
                        listaDiferentesInstancias,
                        ordenarOrden, "",
                        fhig, flow,
                        //txtffact1.Text, txtffact2.Text,
                        "", "",
                        monedaHig, monedaLow,
                        refhig, refLow, ref mensaje
                        );
                    }

                    //}
                    Session["lstFacturas2"] = lstFact; //----new----- // se guarda en la sesion el resultado
                    /*Pinta la lista en còdigo HTML*/
                }

                try
                {
                    PNegocio.ConvertTittles conv = new PNegocio.ConvertTittles();

                    if (lstFact.Count > 0)
                    {

                        this.lblTabla.Text = conv.convertListToTableInCode(lstFact, ordenarOrden);
                        this.btnActualiza.Visible = true;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(mensaje))
                        {
                            string[] status = nFac.status;
                            this.lblTabla.Text = "<br/><br/><br/><br/><h3>Ingrese un dato para mostrar Factura</h3>";
                            if (status.Length > 0)
                            {
                                for (int i = 0; i < status.Length; i++)
                                {
                                    if (status[i] != "" && status[i] != null)
                                    {
                                        this.lblTabla.Text += "<br/><h3>" + status[i] + "</h3>";
                                    }
                                }
                                this.lblTabla.Text += "<br/><h3>" + "Se recomienda intentar utilizando los campos para selección especifica(Referencia, Moneda y Fecha)" + "</h3>";

                            }
                            if (n_instancias <= 0)
                            {
                                this.lblTabla.Text = "<br/><br/><br/><h3>" + "Este usuario no tiene sociedades activas, por lo que no puede obtener datos" + "</h3>";
                            }
                        }
                        else
                        {
                            this.lblTabla.Text = "<br/><br/><br/><br/><h3>Por el momento no se tiene acceso a las facturas porque están siendo tratadas por el administrador. </br> Actualice o intente más tarde.</h3>";
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
                this.lblTabla.Text = "<h3>Ocurrio un error al obtener los datos<h3>";
            }
        }
        [WebMethod]
        public static string desadjuntar(string uuid)
        {
            string[] uui = uuid.Split(',');
            string mensaje = "";
            int cantidad = 0 ;
            PNegocio.CargarFactura nFac = new PNegocio.CargarFactura();
            try
            {
                cantidad = nFac.desvincular(listaDiferentesInstanciasg, uui);
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