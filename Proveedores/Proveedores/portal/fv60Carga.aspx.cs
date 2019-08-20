using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using XML_Test_Reader;

namespace Proveedores.portal
{
    public partial class fv60Carga : System.Web.UI.Page
    {
        private string index = "";                    //Posición en la lista
        private System.Xml.XmlDocument xmlDoc;
        private string filName = "";
        private HttpPostedFile file = null;
        private List<PEntidades.FV60XVerificar> listFV60 = null;
        private List<PEntidades.FV60XVerificar> lstResumen = null;
        private List<string[]> lstNoEnc = null;
        Comprobante comxml;
        XElement xmlFact;

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
            this.listFV60 = new List<PEntidades.FV60XVerificar>();
            this.listFV60 = (List<PEntidades.FV60XVerificar>)Session["lstFacturas2"];

            //Para hacer el resumen de la carga
            if (Session["lstResumen"] != null)
            {
                this.lstResumen = (List<PEntidades.FV60XVerificar>)Session["lstResumen"];
            }
            else
            {
                this.lstResumen = new List<PEntidades.FV60XVerificar>();
            }

            //      Para hacer el resumen de la lista que no se encuentran
            if (Session["lstNoEnc"] != null)
            {
                this.lstNoEnc = (List<string[]>)Session["lstNoEnc"];
            }
            else
            {
                this.lstNoEnc = new List<string[]>();
            }

            this.file = Request.Files[0];
            cargar();
        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("Inicio.aspx");
        }

        private void cargar()
        {
            PEntidades.Proveedor datProveedor = (PEntidades.Proveedor)Session["proveedor"];
            if ((this.file != null) && (this.file.ContentLength > 0))
            {
                this.filName = System.IO.Path.GetFileName(this.file.FileName);
                string absoluteURL = Server.MapPath("Files") + "\\" + this.filName;
                string ext = System.IO.Path.GetExtension(absoluteURL).ToLower();
                if (ext == ".xml")                    //Cuando si es XML
                {
                    try
                    {
                        HttpPostedFile file2 = file;
                        this.xmlDoc = new System.Xml.XmlDocument();
                        this.xmlDoc.Load(this.file.InputStream);
                        this.index = buscarIndexByXBLNR(this.xmlDoc);             //Para saber de que item en lista vamos a hacer la validación con SAP y SAT
                        List<string[]> listaDiferentesInstancias = (List<string[]>)Session["listaDiferentesInstancias"];
                        if (this.index != "")                                     //Cuando si se encuentra la factura
                        {
                            string raw = "";
                            if (validarSAT(this.xmlDoc))                          //Cuando es válido en SAT
                            {
                                if (validarSAP())                                 //Cuando es válido en SAP
                                {
                                    PNegocio.CargarFV60 cf = new PNegocio.CargarFV60();
                                    raw = this.xmlDoc.InnerXml;
                                    this.listFV60[int.Parse(index)].UrlXML = this.filName;
                                    Session["lstFacturas"] = this.listFV60;
                                }
                                else             //Cuando no es válido en SAP
                                {
                                    PNegocio.CargarFV60 cf = new PNegocio.CargarFV60();
                                }
                            }
                            else                //Cuando no es válido en SAT
                            {
                                PNegocio.CargarFV60 cf = new PNegocio.CargarFV60();
                            }
                            //      Para llenar la lista con las facturas que si se encontraron.
                            this.lstResumen.Add(this.listFV60[int.Parse(index)]);
                            Session["lstResumen"] = this.lstResumen;
                        }
                        else//Cuando no se encuentra la factura en la lista
                        {
                            string[] noEnc = new string[2];
                            noEnc[0] = this.filName;
                            noEnc[1] = this.xmlDoc.GetElementsByTagName("cfdi:Comprobante")[0].Attributes["folio"].Value;
                            this.lstNoEnc.Add(noEnc);
                            Session["lstNoEnc"] = this.lstNoEnc;
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }
                }
                else//Cuando no es la extención .xml
                {
                    this.listFV60[int.Parse(index)].DescripcionErrorSAP = "N/A";
                    this.listFV60[int.Parse(index)].DescripcionErrorSAT = "No es XML";
                }
            }
            else//Cuando el Request.Files[0] biene vacio
            {
                this.listFV60[int.Parse(index)].DescripcionErrorSAP = "N/A";
                this.listFV60[int.Parse(index)].DescripcionErrorSAT = "No es XML";
            }
        }

        private string buscarIndexByXBLNR(System.Xml.XmlDocument xmlDocument)
        {
            string numPos = "";

            System.Xml.XmlNode nodo = xmlDocument.GetElementsByTagName("cfdi:Comprobante")[0];
            string folio = nodo.Attributes["folio"].Value;
            for (int i = 0; i < this.listFV60.Count; i++)
            {
                if (this.listFV60[i].XBLNR == folio)
                {
                    numPos = "" + i;
                    break;
                }
            }
            return numPos;
        }

        private bool validarSAT(System.Xml.XmlDocument xmlDoc)
        {
            PNegocio.ConsultaCFDI c = new PNegocio.ConsultaCFDI();
            string resul = c.esCorrectoCFDI(xmlDoc.InnerXml);
            switch (resul.Trim())
            {
                case "Vigente":
                    this.listFV60[int.Parse(index)].DescripcionErrorSAT = "SAT : Vigente";
                    return true;
                case "Cancelado":
                    this.listFV60[int.Parse(index)].DescripcionErrorSAT = "SAT : Cancelado";
                    this.listFV60[int.Parse(index)].DescripcionErrorSAP = "N/A";                    //resulFacturaIncorrecta("SAT");
                    return false;
                case "Sin estructura CFDI":
                    this.listFV60[int.Parse(index)].DescripcionErrorSAT = "SAT : Estructura incorrecta";
                    this.listFV60[int.Parse(index)].DescripcionErrorSAP = "N/A";                    //resulFacturaIncorrecta("SAT");
                    return false;
                default:
                    this.listFV60[int.Parse(index)].DescripcionErrorSAT = "SAT AAAA : " + resul;
                    return false;
            }
        }

        private bool validarSAP()
        {
            bool result = false;
            Comprobante comprobante = null;
            comprobante = (Comprobante)Serializer.FromXml(xmlDoc.InnerXml, typeof(Comprobante));

            var folio = comprobante.folio;
            var emisor = comprobante._Emisor.rfc;
            var moneda = comprobante.Moneda;
            var monto = comprobante.total;
            var receptor = comprobante.Receptor.rfc;
            string idPRoveedor = Session["ProveedorLoged"].ToString();
            List<string[]> listaValidaciones = PNegocio.FactFV60.obtenerListaValidacionesXML(idPRoveedor);

            bool opcionesfactura = true;
            bool boolfolio = true;
            if (listaValidaciones.Count > 1)                            //si contiene mas validaciones editadas por el administrador
            {
                for (int i = 1; i < listaValidaciones.Count; i++)
                {
                    switch (listaValidaciones[i][0].Trim())
                    {
                        case "Moneda":
                            if (listFV60[int.Parse(index)].WAERS.Trim() == moneda)
                            {
                                opcionesfactura = true;
                            }
                            else
                            {
                                opcionesfactura = false;
                            }
                            break;
                        case "Monto":
                            if (listFV60[int.Parse(index)].WRBTR  == decimal.Parse(monto.ToString()))
                            {
                                opcionesfactura = true;
                            }
                            else
                            {
                                opcionesfactura = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            if (folio.Equals(this.listFV60[int.Parse(index)].XBLNR))
            {
                boolfolio = true;
            }
            else
            {
                boolfolio = false;
            }

            if (opcionesfactura && boolfolio)
            {
                this.listFV60[int.Parse(index)].DescripcionErrorSAP = "SAP : Cargada correctamente";
                result = true;
            }
            else
            {
                this.listFV60[int.Parse(index)].DescripcionErrorSAP = "SAP : Valores de XML no coinciden";            //resulFacturaIncorrecta("SAP");
            }
            return result;
        }
    }
}