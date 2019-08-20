using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using XML_Test_Reader;

namespace Proveedores.portal
{
    public partial class facturas3 : System.Web.UI.Page
    {
        string index = "";
        private System.Xml.XmlDocument xmlDoc;
        XElement xmlFact;
        string[] indexs;
        string[] indexs2;
        int maxXML = 10;
        string complementoMsgError = "";

        string fn = "";
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
            //String s = Request.QueryString["index"];

            string strIndex = this.hidIndexs.Value;
            if (strIndex != "")
            {
                if (strIndex.Length == 1)
                {
                    indexs = new string[1];
                    indexs[0] = strIndex;
                }
                else
                {
                    indexs = strIndex.Split(',');
                }

            }
            if (strIndex != "")
            {
                maxXML = int.Parse(obtenerMaxXML());
                this.File1.Visible = true;
                this.cargararchivo.Visible = true;
                cargarDatosTabla();
            }
            else
            {
                this.File1.Visible = false;
                this.cargararchivo.Visible = false;
            }

        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("Inicio.aspx");
        }

        public void cargarDatosTabla()
        {
            List<PEntidades.FacturasXVerificar> listFact = new List<PEntidades.FacturasXVerificar>();
            listFact = (List<PEntidades.FacturasXVerificar>)Session["lstFacturas2"];
            string tablas = "";
            string consolas = "";
            string clase = "show";
            if (listFact.Count > 0)
            {
                indexs2 = new string[indexs.Length + 1];                
                int iddetalle = int.Parse(indexs[0]) - 1;
                indexs2[0] = iddetalle.ToString();
                indexs.CopyTo(indexs2, 1);
                for (int i = 0; i < indexs2.Length; i++)
                {
                    //listFact[int.Parse(indexs[i])].consola = "";
                    tablas += "<table class='tblCV' " + clase + ">";
                    tablas += "<tbody>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Referencia";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[iddetalle].XBLNR2;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Proveedor";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].LIFNR;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Sociedad";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].BUKRS;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Centro";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].WERKS;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Fecha MIGO/BASE";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].BUDAT;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Fecha del documento";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].BLDAT;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Importe";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].WRBTR;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Importe IVA";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].IVA;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "IVA";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].MWSKZ;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Retención";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].RETENCION;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Moneda";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].WAERS;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Factura Elec.";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].ELECT;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Saldo";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFact[int.Parse(indexs2[i])].SALDO;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "XML adjuntos";
                    tablas += "</td>";
                    tablas += "<td class='contadorXML'>";
                    tablas += listFact[int.Parse(indexs2[i])].cantidadXML;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    if (i == 0)
                    {
                        tablas += "Encabezado";
                    }
                    else
                    {
                        tablas += "Detalle";
                    }
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += "<img src='../css/images/fl_back.png' class='imgBack pointer' align='left'><img src='../css/images/fl_next.png' class='imgNext pointer' align='right'>";
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "</tbody>";
                    tablas += "</table>";

                    consolas += "<label class='consola " + clase + "'>" + listFact[int.Parse(indexs2[i])].consola + "</label> ";

                    clase = "hidd";
                }
            }

            this.ltlTablas.Text = tablas;
            this.lblConsola.Text = consolas;
        }

        private void cargar()
        {
            if ((File1.PostedFile != null)
                && (File1.PostedFile.ContentLength > 0) && (File2.PostedFile != null)
                && (File2.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(File2.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Files") + "\\" + fn;
                string nombrePdf = Path.GetFileName(SaveLocation);
                string extPDF = Path.GetExtension(SaveLocation).ToLower();
                fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                SaveLocation = Server.MapPath("Files") + "\\" + fn;
                string extXML = Path.GetExtension(SaveLocation).ToLower();
                string tipoArchivo = "";
                string extencionValidaXML = "";
                string extencionValidaPDF = "";
                string error = "";
                string fecha_xml = "";
                string referencia = "";
                string impRetencion = "";
                string rfc = Session["rfc"].ToString();
                if (String.IsNullOrEmpty(rfc) == false)
                {
                    extencionValidaXML = ".xml";
                    tipoArchivo = "XML";
                }
                extencionValidaPDF = ".pdf";
                extencionValidaPDF = extencionValidaPDF.ToUpper();
                extPDF = extPDF.ToUpper();
                extencionValidaXML = extencionValidaXML.ToUpper();
                extXML = extXML.ToUpper();
                if (extXML == extencionValidaXML && extPDF == extencionValidaPDF) /////////////
                {
                    byte[] rawByte = new byte[0];
                    byte[] rawBytePDF = new byte[0];
                    try
                    {
                        if (tipoArchivo == "XML")
                        {
                            this.xmlDoc = new System.Xml.XmlDocument();
                            this.xmlDoc.Load(File1.PostedFile.InputStream);
                            rawByte = Encoding.UTF8.GetBytes(this.xmlDoc.InnerXml.ToString());
                        }
                        //else
                        //{
                        //    int tam = 36; // tamaño del UUID
                        //    error = "1 " + File1.PostedFile.FileName;
                        int fileLen;
                        fileLen = File2.PostedFile.ContentLength;
                        rawBytePDF = new Byte[fileLen];
                        File2.PostedFile.InputStream.Read(rawBytePDF, 0, fileLen);
                        //    File1.PostedFile.InputStream.Read(rawByte, 0, rawByte.Length);
                        //    Random r = new Random();
                        //    int ramdom = r.Next(10000000, 99999999);
                        //    nombrePdf = "_PDF_" + DateTime.Now + DateTime.Now.Millisecond + "_" + ramdom;
                        //    if (nombrePdf.Length >= tam)
                        //    {
                        //        nombrePdf = nombrePdf.Substring(0, tam);    
                        //    }
                        //}

                        List<PEntidades.FacturasXVerificar> listFact = new List<PEntidades.FacturasXVerificar>();
                        listFact = (List<PEntidades.FacturasXVerificar>)Session["lstFacturas2"];

                        List<string[]> listaDiferentesInstancias = (List<string[]>)Session["listaDiferentesInstancias"];

                        bool continuar = false;
                        if (tipoArchivo == "XML")
                        {
                            continuar = validarSAT(ref impRetencion);
                        }
                        //else if (tipoArchivo == "PDF")
                        //{
                        //    continuar = true;
                        //}
                        if (continuar)
                        {
                            //raw = this.xmlDoc.InnerXml.ToString();
                            if (tipoArchivo == "XML")
                            {
                                continuar = validarSAP(ref fecha_xml, ref referencia, impRetencion, listFact[int.Parse(indexs[0])].XBLNR2);
                            }
                            //else if (tipoArchivo == "PDF")
                            //{
                            //    continuar = true;
                            //}
                            if (String.IsNullOrEmpty(impRetencion)) 
                            {
                                impRetencion = "0";
                            }
                            if (continuar)
                            {
                                PNegocio.CargarFactura cf = new PNegocio.CargarFactura();                                
                                for (int i = 0; i < indexs.Length; i++)
                                {
                                    if ((listFact[int.Parse(indexs[i])].DescripcionErrorSAP.Contains("SAP : Cargada correctamente") || tipoArchivo == "PDF") &&
                                        listFact[int.Parse(indexs[i])].cantidadXML <= maxXML)
                                    {
                                        int contadorres = 0;
                                        /// condicionar tipo conexion 
                                        int indexInstanciaCorrespondiente = Gen.Util.CS.Gen.buscarIndexUbicacionInstanciaCorrres(listaDiferentesInstancias, listFact[int.Parse(indexs[i])].IDINSTANCIA);

                                        //if (tipoArchivo == "PDF")
                                        //{
                                        //    error = "2";
                                        //    listFact[int.Parse(indexs[i])].uuid = nombrePdf;
                                        //    listFact[int.Parse(indexs[i])].DescripcionErrorSAP = "SAP : Cargada correctamente";
                                        //    listFact[int.Parse(indexs[i])].DescripcionErrorSAT = "SAT : N/A";
                                        //}
                                        try
                                        {
                                            error = "3";

                                            contadorres =
                                            cf.setFacturascargadasNew(listFact[int.Parse(indexs[i])].BUKRS,
                                            obtener_correo(),
                                            listFact[int.Parse(indexs[i])].EBELN,
                                            listFact[int.Parse(indexs[i])].LIFNR,
                                            listFact[int.Parse(indexs[i])].DescripcionErrorSAP,
                                            listFact[int.Parse(indexs[i])].DescripcionErrorSAT,
                                            "1",
                                            listFact[int.Parse(indexs[i])].TIPO,
                                            listFact[int.Parse(indexs[i])].WERKS,
                                            //listFact[int.Parse(indexs[i])].XBLNR2,referencia anterior
                                            referencia,
                                            fecha_xml,
                                            fn.ToString(),
                                            listaDiferentesInstancias[indexInstanciaCorrespondiente][1].ToString().Trim(),
                                            listaDiferentesInstancias[indexInstanciaCorrespondiente][4].Split(new Char[] { ',' }),
                                            rawByte,
                                            listFact[int.Parse(indexs[i])].uuid,
                                            listFact[int.Parse(indexs[i])].total,
                                            listFact[int.Parse(indexs[i])].posicion,
                                            listFact[int.Parse(indexs[i])].BELNR,
                                            listFact[int.Parse(indexs[i])].BWTAR,
                                            listFact[int.Parse(indexs[i])].KSCHL,
                                            tipoArchivo,
                                            rawBytePDF, nombrePdf, Convert.ToDecimal(impRetencion)
                                            );


                                            error = "4";
                                            listFact[int.Parse(indexs[i])].UrlXML = fn;

                                            complementoMsgError = "XML: " + fn.ToString();
                                            complementoMsgError += "</br>";
                                            complementoMsgError += "UUID: " + listFact[int.Parse(indexs[i])].uuid;
                                            complementoMsgError += "</br>";
                                            complementoMsgError += listFact[int.Parse(indexs[i])].DescripcionErrorSAT;
                                            complementoMsgError += "</br>";
                                            if (contadorres != 0)
                                            {
                                                complementoMsgError += listFact[int.Parse(indexs[i])].DescripcionErrorSAP;
                                                listFact[int.Parse(indexs[i])].cantidadXML = contadorres;
                                                listFact[int.Parse(indexs[i])].consola = listFact[int.Parse(index)].DescripcionErrorSAP.Replace("SAP : ", "");                                                
                                            }
                                            else
                                            {
                                                listFact[int.Parse(indexs[i])].consola = "SAP: Error al guardar el " + tipoArchivo;
                                                complementoMsgError += "SAP: Error al guardar el " + tipoArchivo;                                                
                                            }
                                            if (i == 0)
                                            {
                                                listFact[int.Parse(indexs2[i])].consola = listFact[int.Parse(indexs[i])].consola;
                                            }
                                            complementoMsgError += "</br>";
                                            complementoMsgError += "</br>";

                                            if (listFact[int.Parse(indexs[i])].msgVarios == "")
                                            {
                                                listFact[int.Parse(indexs[i])].esPrimerCarga = true;
                                            }
                                            else
                                            {
                                                listFact[int.Parse(indexs[i])].esPrimerCarga = false;
                                            }
                                            listFact[int.Parse(indexs[i])].msgVarios += complementoMsgError;                                            
                                        }
                                        catch (Exception z)
                                        {
                                            listFact[int.Parse(indexs[i])].consola = "Error al momento de adjuntar el archivo al sistema SAP.";
                                            listFact[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                                            listFact[int.Parse(indexs[i])].DescripcionErrorSAT = "";
                                            listFact[int.Parse(indexs[i])].ErrorMostrar = "N/A";
                                        }

                                        //if (contadorres != 0)
                                        //{



                                        //}
                                        //else
                                        //{
                                        //    complementoMsgError = "XML: " + fn.ToString();
                                        //    complementoMsgError += "</br>";
                                        //    complementoMsgError += "UUID: " + listFact[int.Parse(indexs[i])].uuid;
                                        //    complementoMsgError += "</br>";
                                        //    complementoMsgError += listFact[int.Parse(indexs[i])].DescripcionErrorSAT;
                                        //    complementoMsgError += "</br>";
                                        //    complementoMsgError += listFact[int.Parse(indexs[i])].DescripcionErrorSAP;
                                        //    complementoMsgError += "</br>";
                                        //    listFact[int.Parse(indexs[i])].consola = "Error al guardar el XML";
                                        //    listFact[int.Parse(indexs[i])].msgVarios += complementoMsgError + "Error al guardar el XML" + "</br></br>"; 
                                        //}


                                    }
                                    else if (listFact[int.Parse(indexs[i])].cantidadXML > maxXML)
                                    {
                                        listFact[int.Parse(indexs[i])].consola = "Limite de archivos alcanzado";
                                    }
                                }
                                //File1.PostedFile.SaveAs(SaveLocation);
                                //this.lblConsola.Text = "Cargada correctamente ";

                                Session["lstFacturas"] = listFact;
                                cargarDatosTabla();

                            }
                            else
                            {
                                PNegocio.CargarFactura cf = new PNegocio.CargarFactura();

                                for (int i = 0; i < indexs.Length; i++)
                                {
                                    if (listFact[int.Parse(indexs[i])].cantidadXML <= maxXML)
                                    {
                                        complementoMsgError = "XML: " + fn.ToString();
                                        complementoMsgError += "</br>";
                                        complementoMsgError += "UUID: " + listFact[int.Parse(indexs[i])].uuid;
                                        complementoMsgError += "</br>";
                                        complementoMsgError += listFact[int.Parse(indexs[i])].DescripcionErrorSAT;
                                        complementoMsgError += "</br>";
                                        complementoMsgError += listFact[int.Parse(indexs[i])].DescripcionErrorSAP;
                                        complementoMsgError += "</br>";
                                        complementoMsgError += "</br>";

                                        if (listFact[int.Parse(indexs[i])].msgVarios == "")
                                        {
                                            listFact[int.Parse(indexs[i])].esPrimerCarga = true;
                                        }
                                        else
                                        {
                                            listFact[int.Parse(indexs[i])].esPrimerCarga = false;
                                        }
                                        listFact[int.Parse(indexs[i])].msgVarios += complementoMsgError;

                                        listFact[int.Parse(indexs[i])].consola = listFact[int.Parse(indexs[i])].DescripcionErrorSAP;
                                        if (i == 0)
                                        {
                                            listFact[int.Parse(indexs2[i])].consola = listFact[int.Parse(indexs[i])].DescripcionErrorSAP;
                                        }
                                    }
                                }
                                cargarDatosTabla();
                            }
                        }
                        else
                        {
                            PNegocio.CargarFactura cf = new PNegocio.CargarFactura();

                            for (int i = 0; i < indexs.Length; i++)
                            {
                                if (listFact[int.Parse(indexs[i])].cantidadXML <= maxXML)
                                {
                                    listFact[int.Parse(indexs[i])].consola = listFact[int.Parse(indexs[i])].DescripcionErrorSAT;

                                    complementoMsgError += "UUID: " + listFact[int.Parse(indexs[i])].uuid;
                                    complementoMsgError += "</br>";
                                    complementoMsgError = "XML: " + fn.ToString();
                                    complementoMsgError += "</br>";
                                    complementoMsgError += listFact[int.Parse(indexs[i])].DescripcionErrorSAT;
                                    complementoMsgError += "</br>";
                                    complementoMsgError += listFact[int.Parse(indexs[i])].DescripcionErrorSAP;
                                    complementoMsgError += "</br>";
                                    complementoMsgError += "</br>";

                                    if (listFact[int.Parse(indexs[i])].msgVarios == "")
                                    {
                                        listFact[int.Parse(indexs[i])].esPrimerCarga = true;
                                    }
                                    else
                                    {
                                        listFact[int.Parse(indexs[i])].esPrimerCarga = false;
                                    }
                                    listFact[int.Parse(indexs[i])].msgVarios += complementoMsgError;
                                    if (i == 0)
                                    {
                                        listFact[int.Parse(indexs2[i])].consola = listFact[int.Parse(indexs[i])].DescripcionErrorSAT;
                                    }
                                }
                            }

                            cargarDatosTabla();
                        }

                    }
                    catch (Exception ex)
                    {
                        this.lblConsola.Text = "Error al cargar el archivo verifique que el XML este generado correctamente";
                        //Response.Write("Error: " + ex.Message); //Nota: Exception.Message devuelve un mensaje detallado que describe la excepción actual. //Por motivos de seguridad, no se recomienda devolver Exception.Message a los usuarios finales de //entornos de producción. Sería más aconsejable poner un mensaje de error genérico. } } else { Response.Write("Seleccione un archivo que cargar."); 
                    }
                }
                else
                {
                    string mesajeerr = "";
                    List<PEntidades.FacturasXVerificar> listFact = new List<PEntidades.FacturasXVerificar>();
                    listFact = (List<PEntidades.FacturasXVerificar>)Session["lstFacturas2"];
                    if ((extXML == extencionValidaXML) == false)
                    {
                        mesajeerr += "El formato del campo de achivos XML es incorrecto.</br>";
                    }
                    if ((extPDF == extencionValidaPDF) == false)
                    {
                        mesajeerr += "El formato del campo de achivos PDF es incorrecto.</br>";
                    }

                    for (int i = 0; i < indexs.Length; i++)
                    {

                        listFact[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                        listFact[int.Parse(indexs[i])].DescripcionErrorSAT = mesajeerr; //No es XML
                        listFact[int.Parse(indexs[i])].consola = mesajeerr; //No es XML
                        if (i == 0)
                        {
                            listFact[int.Parse(indexs2[i])].consola = listFact[int.Parse(indexs[i])].consola;
                        }
                    }
                    cargarDatosTabla();
                    //this.lblConsola.Text = "No es un XML";
                }

            }
            else
            {
                string mesajeerr = "";
                if ((File1.PostedFile == null)
                || (File1.PostedFile.ContentLength == 0))
                {
                    mesajeerr += "Ingrese un archivo XML.</br>";
                }
                if ((File2.PostedFile == null)
                || (File2.PostedFile.ContentLength == 0))
                {
                    mesajeerr += "Ingrese un archivo PDF.</br>";
                }
                List<PEntidades.FacturasXVerificar> listFact = new List<PEntidades.FacturasXVerificar>();
                listFact = (List<PEntidades.FacturasXVerificar>)Session["lstFacturas2"];
                for (int i = 0; i < indexs.Length; i++)
                {
                    listFact[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                    listFact[int.Parse(indexs[i])].DescripcionErrorSAT = mesajeerr; //No es XML
                    listFact[int.Parse(indexs[i])].consola = mesajeerr; //No es XML
                    if (i == 0)
                    {
                        listFact[int.Parse(indexs2[i])].consola = listFact[int.Parse(indexs[i])].consola;
                    }
                }
                cargarDatosTabla();
                //this.lblConsola.Text = "No es un XML";
            }

        }

        private void resulFacturaCorrecta()
        {

        }

        private void resulFacturaIncorrecta(string ubicacion, string index)
        {
            List<PEntidades.FacturasXVerificar> listFact = new List<PEntidades.FacturasXVerificar>();
            listFact = (List<PEntidades.FacturasXVerificar>)Session["lstFacturas2"];

            if (ubicacion.Equals("SAP"))
            {
                //this.lblConsola.Text = listFact[int.Parse(index)].DescripcionErrorSAP.ToString();
                listFact[int.Parse(index)].consola = listFact[int.Parse(index)].DescripcionErrorSAP;
            }
            else
            {
                listFact[int.Parse(index)].consola = listFact[int.Parse(index)].DescripcionErrorSAT;
                //this.lblConsola.Text = listFact[int.Parse(index)].DescripcionErrorSAT.ToString();
            }

        }

        private bool validarSAT(ref string impRetencion)
        {
            PNegocio.ConsultaCFDI c = new PNegocio.ConsultaCFDI();
            string resul = c.esCorrectoCFDI(this.xmlDoc.InnerXml);

            List<PEntidades.FacturasXVerificar> listFact = new List<PEntidades.FacturasXVerificar>();
            listFact = (List<PEntidades.FacturasXVerificar>)Session["lstFacturas2"];
            string uuid = "";
            System.Xml.XmlNode ndComplemento;
            ndComplemento = xmlDoc.GetElementsByTagName("cfdi:Complemento")[0];
            if (ndComplemento != null)
            {
                ndComplemento = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital")[0];
                try
                {
                    uuid = ndComplemento.Attributes["UUID"].Value;
                }
                catch (Exception)
                {
                }
            }

            ndComplemento = xmlDoc.GetElementsByTagName("cfdi:Impuestos")[0];
            try
            {
                impRetencion = ndComplemento.Attributes["TotalImpuestosRetenidos"].Value;
            }
            catch (Exception)
            {
                impRetencion = "";
            }

            for (int i = 0; i < indexs.Length; i++)
            {
                if (listFact[int.Parse(indexs[i])].cantidadXML <= maxXML)
                {
                    listFact[int.Parse(indexs[i])].uuid = uuid;
                    switch (resul.Trim())
                    {
                        case "Vigente":
                            listFact[int.Parse(indexs[i])].DescripcionErrorSAT = "SAT : Vigente";
                            break;

                        case "Cancelado":
                            listFact[int.Parse(indexs[i])].DescripcionErrorSAT = "SAT : Cancelado";
                            listFact[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                            resulFacturaIncorrecta("SAT", indexs[i]);
                            break;

                        case "Sin estructura CFDI":
                            listFact[int.Parse(indexs[i])].DescripcionErrorSAT = "SAT : Estructura incorrecta";
                            listFact[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                            resulFacturaIncorrecta("SAT", indexs[i]);
                            break;

                        default:
                            listFact[int.Parse(indexs[i])].DescripcionErrorSAT = "SAT : " + resul;
                            break;
                    }
                }
                else
                {
                    listFact[int.Parse(indexs[i])].consola = "Limite de XML adjuntos alcanzado";
                }
                if (i == 0)
                {
                    listFact[int.Parse(indexs2[i])].DescripcionErrorSAT = listFact[int.Parse(indexs[i])].DescripcionErrorSAT;
                }
            }

            switch (resul.Trim())
            {
                case "Vigente":
                    return true;

                case "Cancelado":
                    return false;

                case "Sin estructura CFDI":
                    return false;

                default:
                    return false;
            }

        }

        private bool validarSAP(ref string fecha_xml, ref string referencia, string impRetencion, string refer)
        {

            bool result = false;

            string idPRoveedor = Session["ProveedorLoged"].ToString();
            List<string[]> listaValidaciones = PNegocio.Facturas.obtenerListaValidacionesXML(idPRoveedor);

            List<PEntidades.FacturasXVerificar> listFact = new List<PEntidades.FacturasXVerificar>();
            listFact = (List<PEntidades.FacturasXVerificar>)Session["lstFacturas2"];
            System.Xml.XmlNode ndNodos;
            for (int j = 0; j < indexs.Length; j++)
            {
                index = indexs[j];
                if (listFact[int.Parse(index)].cantidadXML <= maxXML)
                {
                    string folio = "";
                    string xmlString = this.xmlDoc.InnerXml.ToString();
                    //Comprobante comprobante = null;
                    //comprobante = (Comprobante)Serializer.FromXml(xmlString, typeof(Comprobante));
                    folio = xmlDoc.LastChild.Attributes["Folio"].Value;
                    ndNodos = xmlDoc.GetElementsByTagName("cfdi:Emisor")[0];
                    string emisor = ndNodos.Attributes["Rfc"].Value;
                    string va_mon = "", val_impt = "", val_imps = "", val_iva = "", val_fec = "";
                    string moneda = "";
                    decimal monto = Convert.ToDecimal(xmlDoc.LastChild.Attributes["Total"].Value);
                    ndNodos = xmlDoc.GetElementsByTagName("cfdi:Receptor")[0];
                    string receptor = ndNodos.Attributes["Rfc"].Value;
                    bool validarmir7 = false;
                    decimal importe = 0;
                    decimal importeiva = 0;
                    decimal importesub = 0;
                    decimal importedes = 0;
                    try
                    {
                        importedes = TruncateDecimal(Convert.ToDecimal(xmlDoc.LastChild.Attributes["Descuento"].Value), 2);
                    }
                    catch (Exception)
                    {
                        importedes = 0;
                    }
                    System.Xml.XmlNode ndComplemento;
                    ndComplemento = xmlDoc.GetElementsByTagName("cfdi:Complemento")[0];
                    if (ndComplemento != null)
                    {
                        ndComplemento = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital")[0];
                        try
                        {
                            listFact[int.Parse(index)].uuid = ndComplemento.Attributes["UUID"].Value;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    var total = xmlDoc.LastChild.Attributes["Total"].Value;
                    //----------->
                    string mensajeval = "";
                    string advertencia = "";
                    bool boolfolio = true;
                    fecha_xml = xmlDoc.LastChild.Attributes["Fecha"].Value.Substring(0, 10);
                    try
                    {
                        referencia = xmlDoc.LastChild.Attributes["Serie"].Value.ToUpper() + xmlDoc.LastChild.Attributes["Folio"].Value;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            referencia = xmlDoc.LastChild.Attributes["Serie"].Value.ToUpper();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                referencia = xmlDoc.LastChild.Attributes["Folio"].Value;
                            }
                            catch (Exception)
                            {
                            }
                        }

                    }

                    referencia = referencia.Replace("_", "").Replace("-", "");
                    if (referencia != refer)
                    {
                        advertencia = advertencia + "Las referencias son diferentes: </br> XML: " + referencia + "</br>Factura: " + refer + "</br>";
                    }
                    if (listaValidaciones.Count <= 1)
                    {
                        listaValidaciones.Add(new string[] { "Moneda" });
                        listaValidaciones.Add(new string[] { "RFC Emisor" });
                        listaValidaciones.Add(new string[] { "Importe Total" });
                        listaValidaciones.Add(new string[] { "Importe IVA" });
                        listaValidaciones.Add(new string[] { "Sub Total" });
                        listaValidaciones.Add(new string[] { "Fecha Factura" });
                    }
                    if (listaValidaciones.Count > 1) // si contiene mas validaciones editadas por el administrador
                    {
                        for (int i = 1; i < listaValidaciones.Count; i++)
                        {
                            switch (listaValidaciones[i][0].Trim())
                            {
                                case "Moneda":
                                    validarmir7 = true;
                                    va_mon = "X";
                                    try
                                    {
                                        moneda = xmlDoc.LastChild.Attributes["Moneda"].Value.ToUpper();
                                    }
                                    catch (Exception)
                                    {
                                        mensajeval = mensajeval + "El archivo XML no cuenta con moneda. </br>";
                                    }

                                    break;
                                case "RFC Emisor":
                                    if (listFact[int.Parse(index)].RFCCorrespon.Trim() != emisor)
                                    {
                                        mensajeval = mensajeval + "El RFC emisor es incorrecto. " + listFact[int.Parse(index)].RFCCorrespon.Trim() + " - " + emisor + "</br>";    
                                    }
                                    break;
                                //case "Monto":
                                //    if (listFact[int.Parse(index)].WRBTR.Trim() != decimal.Round(monto, 2).ToString())
                                //    {

                                //    }
                                //    break;
                                case "Importe Total":
                                    val_impt = "X";
                                    validarmir7 = true;
                                    if (String.IsNullOrEmpty(impRetencion) == false)
                                    {
                                        importe = Convert.ToDecimal(impRetencion);
                                    }
                                    importe = decimal.Round(Convert.ToDecimal(xmlDoc.LastChild.Attributes["Total"].Value), 2) + importe;
                                    break;
                                case "Importe IVA":
                                    val_iva = "X";
                                    validarmir7 = true;
                                    try
                                    {
                                        for (int k = 0; k < xmlDoc.GetElementsByTagName("cfdi:Impuestos").Count; k++)
                                        {
                                            ndNodos = xmlDoc.GetElementsByTagName("cfdi:Impuestos")[k];
                                            if (ndNodos.Attributes.Count > 0)
                                            {
                                                importeiva = decimal.Round(Convert.ToDecimal(ndNodos.Attributes["TotalImpuestosTrasladados"].Value), 2);
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        importeiva = 0;
                                    }
                                    break;
                                case "Sub Total":
                                    val_imps = "X";
                                    validarmir7 = true;
                                    importesub = decimal.Round(Convert.ToDecimal(xmlDoc.LastChild.Attributes["SubTotal"].Value), 2);
                                    break;
                                case "Fecha Factura":
                                    val_fec = "X";
                                    validarmir7 = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (validarmir7)
                        {
                            verificarMir7(fecha_xml, listFact[int.Parse(index)].GJAHR.Trim(), importe, importeiva, importesub, moneda, listFact[int.Parse(index)].BELNR.Trim(), ref mensajeval, ref advertencia, val_fec, val_impt, val_imps, val_iva, va_mon, importedes); //by jemo 15/
                        }
                    }

                    if (listFact[int.Parse(index)].RFCSociedad.Trim() != receptor)
                    {
                        mensajeval = mensajeval + "El RFC receptor es incorrecto." + listFact[int.Parse(index)].RFCSociedad.Trim() + " - " + receptor + "</br>";

                    }

                    //if (listFact[int.Parse(index)].RFCSociedad.Trim() != receptor && (String.IsNullOrEmpty(mensajeval) == false))
                    //{
                    //    mensajeval = mensajeval + "El RFC receptor no coincide con el de sociedad.</br>";
                    //}                

                    //if (folio.Equals(listFact[int.Parse(index)].XBLNR) // siempre valida al menos el folio
                    //    )
                    //{
                    //    boolfolio = true;
                    //}
                    //else {
                    //    boolfolio = false;
                    //}

                    if (String.IsNullOrEmpty(mensajeval) && boolfolio)
                    {
                        if (String.IsNullOrEmpty(advertencia) == false)
                        {
                            advertencia = "</br>Advertencia: " + advertencia;
                        }
                        listFact[int.Parse(index)].DescripcionErrorSAP = "SAP : Cargada correctamente" + advertencia;
                        //resulFacturaCorrecta();
                        result = true;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(advertencia) == false)
                        {
                            advertencia = "Advertencia: " + advertencia;
                        }
                        listFact[int.Parse(index)].DescripcionErrorSAP = "SAP : Valores de XML no coinciden:</br>" + mensajeval + advertencia;
                        resulFacturaIncorrecta("SAP", index);
                    }
                    if (j == 0)
                    {
                        listFact[int.Parse(indexs2[j])].DescripcionErrorSAP = listFact[int.Parse(index)].DescripcionErrorSAP;
                    }
                }
                else
                {
                    listFact[int.Parse(index)].consola = "Limite de XML adjuntos alcanzado";
                }

            }

            return result; // si alguno fue correcto pasara como true


        }
        public void verificarMir7(string fechafac, string año, decimal importe, decimal importeIVA, decimal importeSub, string moneda, string numerodoc, ref string mensaje, ref string advertencia, string val_fec, string val_impt, string val_imps, string val_iva, string val_mon, decimal importedesc)
        {
            PNegocio.FacturasNE nFac = new PNegocio.FacturasNE();
            List<string[]> listaDiferentesInstancias = new List<string[]>();
            listaDiferentesInstancias = (List<string[]>)Session["listaDiferentesInstancias"];
            string repuesta =
             nFac.validardatosMir7(listaDiferentesInstancias, fechafac, año, importe, importeIVA, importeSub, moneda, numerodoc, val_fec, val_impt, val_imps, val_iva, val_mon, importedesc);
            if (repuesta.Contains('N'))
            {
                mensaje = mensaje + "Error: no se pudo validar algunos datos en SAP.</br>";
            }
            else
            {
                if (repuesta.Contains('M'))
                {
                    mensaje = mensaje + "Tipo de moneda incorrecta.</br>";
                }
                if (repuesta.Contains('I'))
                {
                    mensaje = mensaje + "Importe total incorrecto: " + importe + "</br>";
                }
                if (repuesta.Contains('V'))
                {
                    mensaje = mensaje + "Importe IVA incorrecto: " + importeIVA + "</br>";
                }
                if (repuesta.Contains('S'))
                {
                    mensaje = mensaje + "Importe subtotal incorrecto: " + importeSub + "</br>";
                }
                if (repuesta.Contains('F'))
                {
                    advertencia = advertencia + "Fecha diferente al sistema</br>";
                }
                if (repuesta.Contains('E'))
                {
                    mensaje = mensaje + "No existe moneda registrada en el sistema.</br>";
                }
            }
        }
        private decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            int tmp = (int)Math.Truncate(step * value);
            return tmp / step;
        }
        protected string obtener_correo()
        {
            string usuario = HttpContext.Current.User.Identity.Name;
            PNegocio.CargarFactura cf = new PNegocio.CargarFactura();
            string sqlstring = "select email from usuario where usuarioLog = '" + usuario + "'";
            return cf.otener_correo(sqlstring);
        }
        protected string obtenerMaxXML()
        {
            PNegocio.CargarFactura cf = new PNegocio.CargarFactura();
            return cf.getMaxXML();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cargar();
        }

        protected void Regresar_Click(object sender, EventArgs e)
        {

        }

        protected void btnLLenarTbs_Click(object sender, EventArgs e)
        {

        }
    }
}