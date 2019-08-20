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
    public partial class fv60Det : System.Web.UI.Page
    {
        string index = "";
        private System.Xml.XmlDocument xmlDoc;
        XElement xmlFact;
        string[] indexs;
        string[] indexs2;
        int maxXML = 10;
        string complementoMsgError = "";
        string fn = "";
        string emisor = "";
        string receptor = "";


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
            List<PEntidades.FV60XVerificar> listFV60 = new List<PEntidades.FV60XVerificar>();
            listFV60 = (List<PEntidades.FV60XVerificar>)Session["lstFacturas2"];
            string tablas = "";
            string consolas = "";
            string clase = "show";
            if (listFV60.Count > 0)     //solo pasa ENCABEZADO, ya que no hay detalles.
            {
                //indexs2 = new string[indexs.Length + 1];
                //int iddetalle = int.Parse(indexs[0]); /*- 1;*/
                //indexs2[0] = iddetalle.ToString();
                //indexs.CopyTo(indexs2, 1);
                //for (int i = 0; i < indexs2.Length; i++)
                //{
                    tablas += "<table class='tblCV' " + clase + ">";
                    tablas += "<tbody>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Sociedad";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].BUKRS;      //tablas += listFV60[int.Parse(indexs2[i])].BUKRS;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Num. documento";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].BELNR;      //tablas += listFV60[int.Parse(indexs2[i])].BELNR;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Ejercicio";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].GJAHR;     //tablas += listFV60[int.Parse(indexs2[i])].GJAHR;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Fecha documento";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].BLDAT;     //tablas += listFV60[int.Parse(indexs2[i])].BLDAT;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Fecha contabilización";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].BUDAT;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Referencia";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].XBLNR;       //tablas += listFV60[iddetalle].XBLNR;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Moneda";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].WAERS;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Tipo de cambio";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].KURSF;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Proveedor";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].LIFNR;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Impte moneda local";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].DMBTR;
                    tablas += "</td>";
                    tablas += "</tr>";

                tablas += "<tr>";
                tablas += "<td>";
                tablas += "Impte moneda docto.";
                tablas += "</td>";
                tablas += "<td>";
                tablas += listFV60[int.Parse(indexs[0])].WRBTR;
                tablas += "</td>";
                tablas += "</tr>";

                tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "IVA";
                    tablas += "</td>";
                    tablas += "<td>";
                    tablas += listFV60[int.Parse(indexs[0])].WMWST;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "XML adjuntos";
                    tablas += "</td>";
                    tablas += "<td class='contadorXML'>";
                    tablas += listFV60[int.Parse(indexs[0])].ZCOUNT;
                    tablas += "</td>";
                    tablas += "</tr>";

                    tablas += "<tr>";
                    tablas += "<td>";
                    tablas += "Encabezado";
                    tablas += "</td>";
                    //tablas += "<td>";   --- como no tiene detalles no necesita flechas de navegacion
                    //tablas += "<img src='../css/images/fl_back.png' class='imgBack pointer' align='left'><img src='../css/images/fl_next.png' class='imgNext pointer' align='right'>";
                    //tablas += "</td>";
                    tablas += "</tr>";
                    tablas += "</tbody>";
                    tablas += "</table>";

                    //consolas += "<label class='consola " + clase + "'>" + listFV60[int.Parse(indexs2[i])].consola + "</label> ";
                    consolas += "<label class='consola " + clase + "'>" + listFV60[int.Parse(indexs[0])].consola + "</label> ";
   
                clase = "hidd";
                //}
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
                //this.lblIdEstatus.Text = " ";

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
                if (extXML == extencionValidaXML && extPDF == extencionValidaPDF)
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
                            //this.lblIdEstatus.Text = lblIdEstatus.Text + " rawByte XML  ";  //mgv
                        }
                        int fileLen;
                        fileLen = File2.PostedFile.ContentLength;
                        rawBytePDF = new Byte[fileLen];
                        File2.PostedFile.InputStream.Read(rawBytePDF, 0, fileLen);
                        //this.lblIdEstatus.Text = lblIdEstatus.Text + " rawByte PDF  ";  //mgv

                        List<PEntidades.FV60XVerificar> listFV60 = new List<PEntidades.FV60XVerificar>();
                        listFV60 = (List<PEntidades.FV60XVerificar>)Session["lstFacturas2"];
                        //List<string[]> listaDiferentesInstancias = (List<string[]>)Session["listaDiferentesInstancias"];

                        bool continuar = false;
                        if (tipoArchivo == "XML")
                        {
                            continuar = validarSAT(ref impRetencion);
                        }
                        if (continuar)
                        {
                            if (tipoArchivo == "XML")
                            {   //mgv - si NO valida entra al envio directo
                              continuar = validarSAP(ref fecha_xml, ref referencia, impRetencion, listFV60[int.Parse(indexs[0])].XBLNR, listFV60[int.Parse(indexs[0])].WAERS, listFV60[int.Parse(indexs[0])].WRBTR, listFV60[int.Parse(indexs[0])].BLDAT, listFV60[int.Parse(indexs[0])].WMWST);
                            }
                            if (String.IsNullOrEmpty(impRetencion))
                            {
                                impRetencion = "0";
                            }
                            if (continuar)
                            {
                                PNegocio.CargarFV60 cf = new PNegocio.CargarFV60();
                                for (int i = 0; i < indexs.Length; i++)
                                {
 //mgv                                   //if ((listFV60[int.Parse(indexs[i])].DescripcionErrorSAP.Contains("SAP : Cargada correctamente") || tipoArchivo == "PDF") &&
 //mgv                                   //    listFV60[int.Parse(indexs[i])].ZCOUNT <= maxXML)
                                        if (listFV60[int.Parse(indexs[i])].ZCOUNT <= maxXML)
                                        {
                                            int contadorres = 0;
                                        //int indexInstanciaCorrespondiente = Gen.Util.CS.Gen.buscarIndexUbicacionInstanciaCorrres(listaDiferentesInstancias, listFV60[int.Parse(indexs[i])].IDINSTANCIA);

                                        try
                                        {
                                            error = "3";
                                            contadorres =
                                            cf.setFV60cargadasNew(listFV60[int.Parse(indexs[i])].BUKRS,
                                            listFV60[int.Parse(indexs[i])].GJAHR,
                                            listFV60[int.Parse(indexs[i])].BELNR,
                                            listFV60[int.Parse(indexs[i])].uuid,
                                            emisor,
                                            receptor,
                                            listFV60[int.Parse(indexs[i])].WRBTR,
                                            listFV60[int.Parse(indexs[i])].WAERS,
                                            listFV60[int.Parse(indexs[i])].KURSF,
                                            listFV60[int.Parse(indexs[i])].BLDAT,
                                            rawByte,
                                            rawBytePDF,
                                            fn
                                            );
                                            //this.lblIdEstatus.Text = lblIdEstatus.Text + "ERR3 ->" + contadorres.ToString();
                                            error = "4";
                                            listFV60[int.Parse(indexs[i])].UrlXML = fn;

                                            complementoMsgError = "XML: " + fn.ToString();
                                            complementoMsgError += "</br>";
                                            complementoMsgError += "UUID: " + listFV60[int.Parse(indexs[i])].uuid;
                                            complementoMsgError += "</br>";
                                            complementoMsgError += listFV60[int.Parse(indexs[i])].DescripcionErrorSAT;
                                            complementoMsgError += "</br>";
                                            if (contadorres != 0)
                                            {
                                                complementoMsgError += listFV60[int.Parse(indexs[i])].DescripcionErrorSAP;
                                                listFV60[int.Parse(indexs[i])].ZCOUNT = contadorres;
                                                listFV60[int.Parse(indexs[i])].consola = listFV60[int.Parse(index)].DescripcionErrorSAP.Replace("SAP : ", "");
                                            }
                                            else
                                            {
                                                listFV60[int.Parse(indexs[i])].consola = "SAP: Error al guardar el " + tipoArchivo;
                                                complementoMsgError += "SAP: Error al guardar el " + tipoArchivo + " " + cf.msg;
                                            }
 //mgv - no se manejan detalles, solo encabezados   if (i == 0)
                                            //{
                                            //    listFV60[int.Parse(indexs2[i])].consola = listFV60[int.Parse(indexs[i])].consola;
                                            //}
                                            complementoMsgError += "</br>";
                                            complementoMsgError += "</br>";

                                            if (listFV60[int.Parse(indexs[i])].msgVarios == "")
                                            {
                                                listFV60[int.Parse(indexs[i])].esPrimerCarga = true;
                                            }
                                            else
                                            {
                                                listFV60[int.Parse(indexs[i])].esPrimerCarga = false;
                                            }
                                            listFV60[int.Parse(indexs[i])].msgVarios += complementoMsgError;
                                        }
                                        catch (Exception z)
                                        {
                                            listFV60[int.Parse(indexs[i])].consola = "Error al momento de adjuntar el archivo al sistema SAP.";
                                            listFV60[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                                            listFV60[int.Parse(indexs[i])].DescripcionErrorSAT = "";
                                            listFV60[int.Parse(indexs[i])].ErrorMostrar = "N/A";
                                        }
                                    }
                                    else if (listFV60[int.Parse(indexs[i])].ZCOUNT > maxXML)
                                    {
                                        listFV60[int.Parse(indexs[i])].consola = "Limite de archivos alcanzado";
                                    }
                                }
                                Session["lstFacturas"] = listFV60;
                                cargarDatosTabla();
                            }
                            else
                            {
                                PNegocio.CargarFV60 cf = new PNegocio.CargarFV60();
                                for (int i = 0; i < indexs.Length; i++)
                                {
                                    if (listFV60[int.Parse(indexs[i])].ZCOUNT <= maxXML)
                                    {
                                        complementoMsgError = "DIFERENCIAS CON EL XML: " + fn.ToString();
                                        complementoMsgError += "</br>";
                                        complementoMsgError += "UUID: " + listFV60[int.Parse(indexs[i])].uuid;
                                        complementoMsgError += "</br>";
                                        complementoMsgError += listFV60[int.Parse(indexs[i])].DescripcionErrorSAT;
                                        complementoMsgError += "</br>";
                                        complementoMsgError += listFV60[int.Parse(indexs[i])].DescripcionErrorSAP;
                                        complementoMsgError += "</br>";
                                        complementoMsgError += "</br>";

                                        if (listFV60[int.Parse(indexs[i])].msgVarios == "")
                                        {
                                            listFV60[int.Parse(indexs[i])].esPrimerCarga = true;
                                        }
                                        else
                                        {
                                            listFV60[int.Parse(indexs[i])].esPrimerCarga = false;
                                        }
                                        listFV60[int.Parse(indexs[i])].msgVarios += complementoMsgError;
                                        listFV60[int.Parse(indexs[i])].consola = listFV60[int.Parse(indexs[i])].DescripcionErrorSAP;
 //mgv - no se manejan detalles, solo encabezados       if (i == 0)
                                        //{
                                        //    listFV60[int.Parse(indexs2[i])].consola = listFV60[int.Parse(indexs[i])].DescripcionErrorSAP;
                                        //}
                                    }
                                }
                                cargarDatosTabla();
                            }
                        }
                        else
                        {
                            PNegocio.CargarFV60 cf = new PNegocio.CargarFV60();

                            for (int i = 0; i < indexs.Length; i++)
                            {
                                if (listFV60[int.Parse(indexs[i])].ZCOUNT <= maxXML)
                                {
                                    listFV60[int.Parse(indexs[i])].consola = listFV60[int.Parse(indexs[i])].DescripcionErrorSAT;
                                    complementoMsgError += "UUID: " + listFV60[int.Parse(indexs[i])].uuid;
                                    complementoMsgError += "</br>";
                                    complementoMsgError = "XML: " + fn.ToString();
                                    complementoMsgError += "</br>";
                                    complementoMsgError += listFV60[int.Parse(indexs[i])].DescripcionErrorSAT;
                                    complementoMsgError += "</br>";
                                    complementoMsgError += listFV60[int.Parse(indexs[i])].DescripcionErrorSAP;
                                    complementoMsgError += "</br>";
                                    complementoMsgError += "</br>";

                                    if (listFV60[int.Parse(indexs[i])].msgVarios == "")
                                    {
                                        listFV60[int.Parse(indexs[i])].esPrimerCarga = true;
                                    }
                                    else
                                    {
                                        listFV60[int.Parse(indexs[i])].esPrimerCarga = false;
                                    }
                                    listFV60[int.Parse(indexs[i])].msgVarios += complementoMsgError;
 //mgv - no se manejan detalles, solo encabezados       if (i == 0)
                                    //{
                                    //    listFV60[int.Parse(indexs2[i])].consola = listFV60[int.Parse(indexs[i])].DescripcionErrorSAT;
                                    //}
                                }
                            }
                            cargarDatosTabla();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.lblConsola.Text = "Error al cargar el archivo verifique que el XML este generado correctamente";
                        Response.Write("Error: " + ex.Message); //Nota: Exception.Message devuelve un mensaje detallado que describe la excepción actual. //Por motivos de seguridad, no se recomienda devolver Exception.Message a los usuarios finales de //entornos de producción. Sería más aconsejable poner un mensaje de error genérico. } } else { Response.Write("Seleccione un archivo que cargar."); 
                    }
                }
                else
                {
                    string mesajeerr = "";
                    List<PEntidades.FV60XVerificar> listFV60 = new List<PEntidades.FV60XVerificar>();
                    listFV60 = (List<PEntidades.FV60XVerificar>)Session["lstFacturas2"];
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
                        listFV60[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                        listFV60[int.Parse(indexs[i])].DescripcionErrorSAT = mesajeerr; //No es XML
                        listFV60[int.Parse(indexs[i])].consola = mesajeerr; //No es XML
//mgv - no se manejan detalles, solo encabezados                           if (i == 0)
                        //{
                        //    listFV60[int.Parse(indexs2[i])].consola = listFV60[int.Parse(indexs[i])].consola;
                        //}
                    }
                    cargarDatosTabla();
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
                List<PEntidades.FV60XVerificar> listFV60 = new List<PEntidades.FV60XVerificar>();
                listFV60 = (List<PEntidades.FV60XVerificar>)Session["lstFacturas2"];
                for (int i = 0; i < indexs.Length; i++)
                {
                    listFV60[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                    listFV60[int.Parse(indexs[i])].DescripcionErrorSAT = mesajeerr; //No es XML
                    listFV60[int.Parse(indexs[i])].consola = mesajeerr; //No es XML
//mgv - no se manejan detalles, solo encabezados               if (i == 0)
                    //{
                    //    listFV60[int.Parse(indexs2[i])].consola = listFV60[int.Parse(indexs[i])].consola;
                    //}
                }
                cargarDatosTabla();
            }
        }

        private void resulFacturaCorrecta()
        {
        }

        private void resulFacturaIncorrecta(string ubicacion, string index)
        {
            List<PEntidades.FV60XVerificar> listFV60 = new List<PEntidades.FV60XVerificar>();
            listFV60 = (List<PEntidades.FV60XVerificar>)Session["lstFacturas2"];

            if (ubicacion.Equals("SAP"))
            {
                listFV60[int.Parse(index)].consola = listFV60[int.Parse(index)].DescripcionErrorSAP;
            }
            else
            {
                listFV60[int.Parse(index)].consola = listFV60[int.Parse(index)].DescripcionErrorSAT;
            }
        }

        private bool validarSAT(ref string impRetencion)
        {
            PNegocio.ConsultaCFDI c = new PNegocio.ConsultaCFDI();
            string resul = c.esCorrectoCFDI(this.xmlDoc.InnerXml);

            List<PEntidades.FV60XVerificar> listFV60 = new List<PEntidades.FV60XVerificar>();
            listFV60 = (List<PEntidades.FV60XVerificar>)Session["lstFacturas2"];
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
                if (listFV60[int.Parse(indexs[i])].ZCOUNT <= maxXML)
                {
                    listFV60[int.Parse(indexs[i])].uuid = uuid;
                    switch (resul.Trim())
                    {
                        case "Vigente":
                            listFV60[int.Parse(indexs[i])].DescripcionErrorSAT = "SAT : Vigente";
                            break;

                        case "Cancelado":
                            listFV60[int.Parse(indexs[i])].DescripcionErrorSAT = "SAT : Cancelado";
                            listFV60[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                            resulFacturaIncorrecta("SAT", indexs[i]);
                            break;

                        case "Sin estructura CFDI":
                            listFV60[int.Parse(indexs[i])].DescripcionErrorSAT = "SAT : Estructura incorrecta";
                            listFV60[int.Parse(indexs[i])].DescripcionErrorSAP = "N/A";
                            resulFacturaIncorrecta("SAT", indexs[i]);
                            break;

                        default:
                            listFV60[int.Parse(indexs[i])].DescripcionErrorSAT = "SAT : " + resul;
                            break;
                    }
                }
                else
                {
                    listFV60[int.Parse(indexs[i])].consola = "Limite de XML adjuntos alcanzado";
                }
 //mgv - no se manejan detalles, solo encabezados     if (i == 0)
                //{
                //    listFV60[int.Parse(indexs2[i])].DescripcionErrorSAT = listFV60[int.Parse(indexs[i])].DescripcionErrorSAT;
                //}
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

        private bool validarSAP(ref string fecha_xml, ref string referencia, string impRetencion, string refer, string xxWAERS, decimal xxWRBTR, string xxBLDAT, decimal xxWMWST)
        {
            bool result = true;
            string idPRoveedor = Session["ProveedorLoged"].ToString();
            List<string[]> listaValidaciones = PNegocio.FactFV60.obtenerListaValidacionesXML(idPRoveedor);

            List<PEntidades.FV60XVerificar> listFV60 = new List<PEntidades.FV60XVerificar>();
            listFV60 = (List<PEntidades.FV60XVerificar>)Session["lstFacturas2"];
            System.Xml.XmlNode ndNodos;
            for (int j = 0; j < indexs.Length; j++)
            {
                index = indexs[j];
                if (listFV60[int.Parse(index)].ZCOUNT <= maxXML)
                {
                    string folio = "";
                    string xmlString = this.xmlDoc.InnerXml.ToString();
                    folio = xmlDoc.LastChild.Attributes["Folio"].Value;
                    ndNodos = xmlDoc.GetElementsByTagName("cfdi:Emisor")[0];
                    emisor = ndNodos.Attributes["Rfc"].Value;
                    string moneda = "";
                    decimal monto = Convert.ToDecimal(xmlDoc.LastChild.Attributes["Total"].Value);
                    ndNodos = xmlDoc.GetElementsByTagName("cfdi:Receptor")[0];
                    receptor = ndNodos.Attributes["Rfc"].Value;
                    //bool validaFV60 = false;
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
                            listFV60[int.Parse(index)].uuid = ndComplemento.Attributes["UUID"].Value;
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
                        advertencia = advertencia + "Las referencias son diferentes: XML: " + referencia + " -Factura: " + refer + "</br>";
                        result = false;
                    }
                    if (listaValidaciones.Count <= 1)
                    {
                        listaValidaciones.Add(new string[] { "Moneda" });         //listFV60[int.Parse(indexs[i])].WAERS,
                        listaValidaciones.Add(new string[] { "RFC Emisor" });
                        listaValidaciones.Add(new string[] { "Importe Total" });  //listFV60[int.Parse(indexs[i])].WRBTR,
                        listaValidaciones.Add(new string[] { "Importe IVA" });    //no se tiene el IVA
                        listaValidaciones.Add(new string[] { "Sub Total" });      //no se tiene el subtotal
                        listaValidaciones.Add(new string[] { "Fecha Factura" });  //listFV60[int.Parse(indexs[i])].BLDAT,
                    }
                    if (listaValidaciones.Count > 1) // si contiene mas validaciones editadas por el administrador
                    {
                        for (int i = 1; i < listaValidaciones.Count; i++)
                        {
                            switch (listaValidaciones[i][0].Trim())
                            {
                                case "Moneda":
                                    try
                                    {
                                        moneda = xmlDoc.LastChild.Attributes["Moneda"].Value.ToUpper();
                                        if (moneda != xxWAERS)
                                        {
                                            advertencia = advertencia + "La moneda es diferente: XML: " + moneda + " -Factura: " + xxWAERS + "</br>";
                                            result = false;
                                            //this.lblIdEstatus.Text = lblIdEstatus.Text + " Diferencia en monedaXML " + moneda + " Fac " + xxWAERS;  //mgv
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        mensajeval = mensajeval + "El archivo XML no cuenta con moneda. </br>";
                                    }
                                    break;
                                case "RFC Emisor":    
                                    string xrfc = Session["rfc"].ToString();
                                    if ( xrfc.Trim()  != emisor)
                                    {
                                        mensajeval = mensajeval + "El RFC emisor es incorrecto. " + xrfc.Trim() + " - " + emisor + "</br>";    //</br>";
                                        result = false;
                                        //this.lblIdEstatus.Text = lblIdEstatus.Text + " Diferencia en emisor " + xrfc.Trim() + " - " + emisor;  //mgv
                                    }
                                    break;
                                //case "Monto":
                                //    if (monto != xxWRBTR)
                                //    {
                                //        mensajeval = mensajeval + "El importe total es incorrecto." + monto + "</br>Factura: " + xxWRBTR;  
                                //        result = false;
                                //    }
                                //    break;
                                case "Importe Total":
                                    if (String.IsNullOrEmpty(impRetencion) == false)
                                    {
                                        importe = Convert.ToDecimal(impRetencion);
                                    }
                                    importe = decimal.Round(Convert.ToDecimal(xmlDoc.LastChild.Attributes["Total"].Value), 2) + importe;
                                    if (importe != (decimal.Round(Convert.ToDecimal(xxWRBTR), 2)))     
                                    {
                                        advertencia = advertencia + "El importe es diferente: XML: " + importe + " -Factura: " + xxWRBTR + "</br>";
 //mgv - para q pase a subir el archivo //result = false;
                                        //this.lblIdEstatus.Text = lblIdEstatus.Text + " Dif.impte XML " + xmlDoc.LastChild.Attributes["Total"].Value + " Fact. " + xxWRBTR;  //mgv
                                    }
                                    break;
                                case "Importe IVA":
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
                                    if (importeiva != decimal.Round(Convert.ToDecimal(xxWMWST), 2))
                                    {
                                        mensajeval = mensajeval + "El iva es incorrecto. " + importeiva + " -Factura: " + xxWMWST + "</br>";
                                        result = false;
                                    }
                                    break;
                                case "Sub Total":
                                    importesub = decimal.Round(Convert.ToDecimal(xmlDoc.LastChild.Attributes["SubTotal"].Value), 2);
                                    if (importesub != (decimal.Round(Convert.ToDecimal(xxWRBTR), 2) - decimal.Round(Convert.ToDecimal(xxWMWST), 2)))
                                    {
                                        mensajeval = mensajeval + "El subtotal es incorrecto. " + importesub + " -Factura: " + (decimal.Round(Convert.ToDecimal(xxWRBTR), 2) - decimal.Round(Convert.ToDecimal(xxWMWST), 2)) + "</br>";
                                        result = false;
                                    }
                                    break;
                                case "Fecha Factura":
                                    if (fecha_xml != xxBLDAT)
                                    {
                                        advertencia = advertencia + "La fecha del docto es diferente: XML: " + fecha_xml + " Factura: " + xxBLDAT + "</br>";
                                        result = false;
                                        //this.lblIdEstatus.Text = lblIdEstatus.Text + " Diferencia FECHAS XML " + fecha_xml + " FAC " + xxBLDAT;    // listFV60[int.Parse(indexs[i])].BLDAT;  //mgv
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    List<string[]> listaDiferentesInstancias = (List<string[]>)Session["listaDiferentesInstancias"];
                    string rrfc = listaDiferentesInstancias[0][5];          //rfc del RECEPTOR que viene en la instancia
                    if (rrfc.Trim() != receptor)                    
                    {
                        mensajeval = mensajeval + "El RFC receptor es incorrecto." + rrfc.Trim() + " - " + receptor + "</br>";
                        result = false;
                    }
                    if (String.IsNullOrEmpty(mensajeval) && boolfolio)
                    {
                        if (String.IsNullOrEmpty(advertencia) == false)
                        {
                            advertencia = "</br>Advertencia: " + advertencia;
                        }
                        listFV60[int.Parse(index)].DescripcionErrorSAP = "SAP : Cargada correctamente" + advertencia;
                        //result = true;  mgv
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(advertencia) == false)
                        {
                            advertencia = "Advertencia: " + advertencia;
                        }
                        listFV60[int.Parse(index)].DescripcionErrorSAP = "SAP : Valores de XML no coinciden:</br>" + mensajeval + "</br>" +advertencia;
                        resulFacturaIncorrecta("SAP", index);
                    }
//mgv - no se manejan detalles, solo encabezados  if (j == 0)
                    //{
                    //    listFV60[int.Parse(indexs2[j])].DescripcionErrorSAP = listFV60[int.Parse(index)].DescripcionErrorSAP;
                    //}
                }
                else
                {
                    listFV60[int.Parse(index)].consola = "Limite de XML adjuntos alcanzado";
                }
            }
            // mgv HARDCODE            result = true;    // mgv HARDCODE para que regrese a pesar de errores
            return result; // si alguno fue incorrecto pasara como false
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
            PNegocio.CargarFV60 cf = new PNegocio.CargarFV60();
            string sqlstring = "select email from usuario where usuarioLog = '" + usuario + "'";
            return cf.otener_correo(sqlstring);
        }
        protected string obtenerMaxXML()
        {
            PNegocio.CargarFV60 cf = new PNegocio.CargarFV60();
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