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
    public partial class facturasCarga : System.Web.UI.Page
    {
        private string index = "";//Posición en la lista de facturas
        private System.Xml.XmlDocument xmlDoc;
        //private System.Xml.Linq xmlDocLinq;
        private string filName = "";
        private HttpPostedFile file = null;
        private List<PEntidades.FacturasXVerificar> listFact = null;
        private List<PEntidades.FacturasXVerificar> lstResumen = null;
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
            this.listFact = new List<PEntidades.FacturasXVerificar>();
            this.listFact = (List<PEntidades.FacturasXVerificar>)Session["lstFacturas2"];

            //Para hacer el resumen de la carga
            if (Session["lstResumen"] != null) {
                this.lstResumen = (List<PEntidades.FacturasXVerificar>)Session["lstResumen"];
            } else {
                this.lstResumen = new List<PEntidades.FacturasXVerificar>();
            }
            
            //      Para hacer el resumen de la lista que no se encuentran
            if (Session["lstNoEnc"] != null)
            {
                this.lstNoEnc = (List<string[]>)Session["lstNoEnc"];
            } else {
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
                if (ext == ".xml")//Cuando si es XML
                {
                    try
                    {
                         HttpPostedFile file2 = file;
                        this.xmlDoc = new System.Xml.XmlDocument();
                        this.xmlDoc.Load(this.file.InputStream);
                        //string d = System.Text.Encoding.UTF8.GetString(this.file.InputStream.ToString()); 
                          //byte[] filed = null;
                          //using (var binaryReader = new BinaryReader(file2.InputStream))
                          //{
                          //    filed = binaryReader.ReadBytes(file.ContentLength);  
                          //}
                          //string e = System.Text.Encoding.UTF8.GetString(filed);
                          //string ot = this.xmlDoc.ToString();

                        //string endpoint = "";
                        //string[] userPass = null;

                        this.index = buscarIndexByXBLNR(this.xmlDoc);//Para saber de que item en lista vamos a hacer la validación con SAP y SAT
                        List<string[]> listaDiferentesInstancias = (List<string[]>)Session["listaDiferentesInstancias"];
                        if (this.index != "")//Cuando si se encuentra la factura
                        {
                            string raw = "";
                            
                            if (validarSAT(this.xmlDoc))//Cuando es válido en SAT
                            {
                                if (validarSAP())//Cuando es válido en SAP
                                {
                                    //this.file.SaveAs(absoluteURL);
                                    PNegocio.CargarFactura cf = new PNegocio.CargarFactura();
                                    raw = this.xmlDoc.InnerXml;
                                    //cf.setFacturascargadas(datProveedor.Liftnr, this.listFact[int.Parse(index)].XBLNR, "1", this.filName.ToString(), this.listFact[int.Parse(index)].DescripcionErrorSAP.ToString(), this.listFact[int.Parse(index)].DescripcionErrorSAT.ToString(), this.listFact[int.Parse(index)].InsidenciaPersonal, endpoint,userPass);
                                    int indexInstanciaCorrespondiente = Gen.Util.CS.Gen.buscarIndexUbicacionInstanciaCorrres(listaDiferentesInstancias, listFact[int.Parse(index)].IDINSTANCIA);

                                   // cf.setFacturascargadasNew(listFact[int.Parse(index)].BUKRS, listFact[int.Parse(index)].EBELN, listFact[int.Parse(index)].LIFNR, listFact[int.Parse(index)].DescripcionErrorSAP, listFact[int.Parse(index)].DescripcionErrorSAT, "1", listFact[int.Parse(index)].TIPO, listFact[int.Parse(index)].WERKS, listFact[int.Parse(index)].XBLNR, filName.ToString(),
                                   //listaDiferentesInstancias[indexInstanciaCorrespondiente][1].ToString().Trim(),
                                   //listaDiferentesInstancias[indexInstanciaCorrespondiente][4].Split(new Char[] { ',' }),
                                   //raw,
                                   // listFact[int.Parse(index)].uuid,
                                   // listFact[int.Parse(index)].total,
                                   // listFact[int.Parse(index)].posicion,
                                   // listFact[int.Parse(index)].BELNR,
                                   // listFact[int.Parse(index)].BWTAR,
                                   // listFact[int.Parse(index)].KSCHL
                                   //);
                                    
                                    //this.file.SaveAs(absoluteURL);
                                    this.listFact[int.Parse(index)].UrlXML = this.filName;
                                    Session["lstFacturas"] = this.listFact;
                                }
                                else//Cuando no es válido en SAP
                                {
                                    PNegocio.CargarFactura cf = new PNegocio.CargarFactura();
                                    //cf.setFacturascargadas(datProveedor.Liftnr, this.listFact[int.Parse(index)].XBLNR, "3", this.filName.ToString(), this.listFact[int.Parse(index)].DescripcionErrorSAP.ToString(), this.listFact[int.Parse(index)].DescripcionErrorSAT.ToString(), this.listFact[int.Parse(index)].InsidenciaPersonal, endpoint, userPass);
                                    int indexInstanciaCorrespondiente = Gen.Util.CS.Gen.buscarIndexUbicacionInstanciaCorrres(listaDiferentesInstancias, listFact[int.Parse(index)].IDINSTANCIA);

                                    //cf.setFacturascargadasNew(listFact[int.Parse(index)].BUKRS, listFact[int.Parse(index)].EBELN, listFact[int.Parse(index)].LIFNR, listFact[int.Parse(index)].DescripcionErrorSAP, listFact[int.Parse(index)].DescripcionErrorSAT, "3", listFact[int.Parse(index)].TIPO, listFact[int.Parse(index)].WERKS, listFact[int.Parse(index)].XBLNR, filName.ToString(),
                                    //    listaDiferentesInstancias[indexInstanciaCorrespondiente][1].ToString().Trim(),
                                    //    listaDiferentesInstancias[indexInstanciaCorrespondiente][4].Split(new Char[] { ',' }),
                                    //    raw,
                                    //    listFact[int.Parse(index)].uuid,
                                    //    listFact[int.Parse(index)].total,
                                    //    listFact[int.Parse(index)].posicion,
                                    //    listFact[int.Parse(index)].BELNR,
                                    //    listFact[int.Parse(index)].BWTAR,
                                    //    listFact[int.Parse(index)].KSCHL
                                    //    );
                                }
                            }
                            else //Cuando no es válido en SAT
                            {
                                PNegocio.CargarFactura cf = new PNegocio.CargarFactura();
                                //cf.setFacturascargadas(datProveedor.Liftnr, this.listFact[int.Parse(index)].XBLNR, "2", this.filName.ToString(), "", this.listFact[int.Parse(index)].DescripcionErrorSAT, this.listFact[int.Parse(index)].InsidenciaPersonal, endpoint, userPass);
                                int indexInstanciaCorrespondiente = Gen.Util.CS.Gen.buscarIndexUbicacionInstanciaCorrres(listaDiferentesInstancias, listFact[int.Parse(index)].IDINSTANCIA);

                                //cf.setFacturascargadasNew(listFact[int.Parse(index)].BUKRS, listFact[int.Parse(index)].EBELN, listFact[int.Parse(index)].LIFNR, listFact[int.Parse(index)].DescripcionErrorSAP, listFact[int.Parse(index)].DescripcionErrorSAT, "3", listFact[int.Parse(index)].TIPO, listFact[int.Parse(index)].WERKS, listFact[int.Parse(index)].XBLNR, filName.ToString(),
                                //    listaDiferentesInstancias[indexInstanciaCorrespondiente][1].ToString().Trim(),
                                //    listaDiferentesInstancias[indexInstanciaCorrespondiente][4].Split(new Char[] { ',' }),
                                //    raw,
                                //    listFact[int.Parse(index)].uuid,
                                //    listFact[int.Parse(index)].total,
                                //    listFact[int.Parse(index)].posicion,
                                //    listFact[int.Parse(index)].BELNR,
                                //    listFact[int.Parse(index)].BWTAR,
                                //    listFact[int.Parse(index)].KSCHL
                                //    );
                            }
                            //      Para llenar la lista con las facturas que si se encontraron.
                            this.lstResumen.Add(this.listFact[int.Parse(index)]);
                            Session["lstResumen"] = this.lstResumen;
                        }
                        else//Cuando no se encuentra la factura en la lista
                        {
                            string[] noEnc = new string[2];
                            noEnc[0] = this.filName;
                            noEnc[1] = this.xmlDoc.GetElementsByTagName("cfdi:Comprobante")[0].Attributes["folio"].Value;
                            this.lstNoEnc.Add(noEnc);
                            Session["lstNoEnc"] = this.lstNoEnc;
                            //Session["prueba"] += "Archivo:" + this.filName + ", Folio factura:" + this.xmlDoc.GetElementsByTagName("cfdi:Comprobante")[0].Attributes["folio"].Value + " -|-";
                            //var v = Session["prueba"];
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }
                }
                else//Cuando no es la extención .xml
                {
                    this.listFact[int.Parse(index)].DescripcionErrorSAP = "N/A";
                    this.listFact[int.Parse(index)].DescripcionErrorSAT = "No es XML";
                }
            }
            else//Cuando el Request.Files[0] biene vacio
            {
                this.listFact[int.Parse(index)].DescripcionErrorSAP = "N/A";
                this.listFact[int.Parse(index)].DescripcionErrorSAT = "No es XML";
            }
        }

        private string buscarIndexByXBLNR(System.Xml.XmlDocument xmlDocument)
        {
            string numPos = "";

            System.Xml.XmlNode nodo = xmlDocument.GetElementsByTagName("cfdi:Comprobante")[0];
            string folio = nodo.Attributes["folio"].Value;
            for (int i = 0; i < this.listFact.Count; i++)
            {
                if (this.listFact[i].XBLNR2 == folio)
                {
                    numPos = "" + i;
                    break;
                }
            }
            return numPos;
        }

        private void resulFacturaCorrecta()
        {
                //this.lbljquery.Text = "<script>" +
                //               "var myVar = setInterval(function () { animarCorrecto() }, 2000);" +
                //               "</script>";


                //Response.Write("<script>" +
                //               "//var myVar = setInterval(function () { animarCorrecto() }, 2000);" +
                //               "alert('animar correcto');" + 
                //               "</script>");
        }

        private bool validarSAT(System.Xml.XmlDocument xmlDoc)
        {
            PNegocio.ConsultaCFDI c = new PNegocio.ConsultaCFDI();
            string resul = c.esCorrectoCFDI(xmlDoc.InnerXml); 

            switch (resul.Trim())
            {
                case "Vigente":
                    this.listFact[int.Parse(index)].DescripcionErrorSAT = "SAT : Vigente";
                    return true;

                case "Cancelado":
                    this.listFact[int.Parse(index)].DescripcionErrorSAT = "SAT : Cancelado";
                    this.listFact[int.Parse(index)].DescripcionErrorSAP = "N/A";
                    //resulFacturaIncorrecta("SAT");
                    return false;

                case "Sin estructura CFDI":
                    this.listFact[int.Parse(index)].DescripcionErrorSAT = "SAT : Estructura incorrecta";
                    this.listFact[int.Parse(index)].DescripcionErrorSAP = "N/A";
                    //resulFacturaIncorrecta("SAT");
                    return false;

                default:
                    this.listFact[int.Parse(index)].DescripcionErrorSAT = "SAT AAAA : " + resul;
                    return false;
            }
        }

        private bool validarSAP()
        {
            
            bool result = false;
            
            Comprobante comprobante = null;
            comprobante = (Comprobante)Serializer.FromXml(xmlDoc.InnerXml, typeof(Comprobante));
            //comprobante = (Comprobante)Serializer.FromXml(xmlString, typeof(Comprobante));

            var folio = comprobante.folio;
            var emisor = comprobante._Emisor.rfc;
            var moneda = comprobante.Moneda;
            var monto = comprobante.total;
            var receptor = comprobante.Receptor.rfc;

            string idPRoveedor = Session["ProveedorLoged"].ToString();
            List<string[]> listaValidaciones = PNegocio.Facturas.obtenerListaValidacionesXML(idPRoveedor);

            //System.Xml.XmlNode nodo = xmlDocument.GetElementsByTagName("cfdi:Comprobante")[0];
            //folio = nodo.Attributes["folio"].Value;

            //Session["RFCProveedorLoged"] 

            bool opcionesfactura = true;
            bool boolfolio = true;
            if (listaValidaciones.Count > 1) // si contiene mas validaciones editadas por el administrador
            {
                for (int i = 1; i < listaValidaciones.Count; i++)
                {
                    switch (listaValidaciones[i][0].Trim())
                    {
                        case "Moneda":
                            if (listFact[int.Parse(index)].WAERS.Trim() == moneda)
                            {
                                opcionesfactura = true;
                            }
                            else
                            {
                                opcionesfactura = false;
                            }
                            break;
                        case "RFC Receptor":
                            if (listFact[int.Parse(index)].RFCCorrespon.Trim() == receptor)
                            {
                                opcionesfactura = true;
                            }
                            else
                            {
                                opcionesfactura = false;
                            }
                            break;
                        //case "RFC Emisor":
                        //    if (listFact[int.Parse(index)].WAERS == emisor)
                        //    {
                        //        facturaCorecta = true;
                        //    }
                        //    else
                        //    {
                        //        facturaCorecta = false;
                        //    }
                        //    break;
                        case "Monto":
                            if (listFact[int.Parse(index)].WRBTR.Trim() == monto.ToString())
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

            if (folio.Equals(this.listFact[int.Parse(index)].XBLNR2))
            {
                boolfolio = true;
            }
            else
            {
                boolfolio = false;
            }


            if (opcionesfactura && boolfolio)
            {
                this.listFact[int.Parse(index)].DescripcionErrorSAP = "SAP : Cargada correctamente";
                resulFacturaCorrecta();
                result = true;

            }
            else
            {
                this.listFact[int.Parse(index)].DescripcionErrorSAP = "SAP : Valores de XML no coinciden";
                //resulFacturaIncorrecta("SAP");
            }
            return result;
        }
    }
}