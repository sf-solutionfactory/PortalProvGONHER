using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PEntidades;
using System.ComponentModel;

namespace PNegocio
{
    public class ConvertTittles
    {
        public ConvertTittles()
        {
        }
        List<string> list_uuid = new List<string>();

        public DataTable convertListFacXVerif(List<FacturasXVerificar> list)
        {
            DataTable table;
            table = ConvertToDataTable<FacturasXVerificar>(list);
            table.Columns[0].ColumnName = "Purch. Doc";
            table.Columns[1].ColumnName = "Material";
            table.Columns[2].ColumnName = "Item";
            table.Columns[3].ColumnName = "Type";
            table.Columns[4].ColumnName = "Cat";
            table.Columns[5].ColumnName = "PGr";
            table.Columns[6].ColumnName = "Doc. Date";
            table.Columns[7].ColumnName = "Quantity";
            table.Columns[8].ColumnName = "Unit";
            table.Columns[9].ColumnName = "Net price";
            table.Columns[10].ColumnName = "Crcy";
            table.Columns[11].ColumnName = "1";
            table.Columns[12].ColumnName = "OpenTgtQty";
            table.Columns[13].ColumnName = "Still to be del. Qty";
            table.Columns[14].ColumnName = "Still to be del. Val";
            table.Columns[15].ColumnName = "Still to be inv. Qty";
            table.Columns.RemoveAt(1);
            return table;
        }

        public string convertListPAbiertasToTableInCode(List<PAbiertasYPago> lstPAbiertas, int index, string cont,string idTabla) // [index para ubicar la fila a la que se le aplicara el estilo de muestra],[cont decide el modo de expandir; si es "n" es solo a una fila y va a expandir, si es "yes" es solo a una fila y va a contraer, si es et expande todo, si es ct contrae todo, en este caso los index son negativos para que no afecte a ninguna fila en especial]
        {
            string html = "";
            html += "<table class='tblComun' id='" + idTabla + "'>"; // tableToOrder para filtrar
            html += "   <thead>" +
                        "       <tr>" +
                        "         <th>Clearing Document</th>" +
                        "         <th>Nº documento</th>" +
                        "         <th>Tipo de documento</th>" +
                        "         <th>Fecha de pago</th>" +
                        "         <th>Monto</th>" +
                        "         <th>Moneda</th>" +
                        "         <th>Nº asignacion</th>" +
                        "         <th>Factura</th>" +
                        "         <th>Cuenta</th>" +
                        "         <th>Proveedor</th>" +
                        "         <th>Texto</th>" +
                        "         <th>Doc. Compras</th>" +
                        "         <th class='icono'></th>" +
                        "       </tr>" +
                        "   </thead>";
            html += "   <tbody>";

            // KZ es el encabezado, el que contiene los desplegables
            // RE son los desplegables
            for (int i = 0; i < lstPAbiertas.Count; i++)
            {
                string clase = "";
                string contenido = "<div class='ico-expandir'></div>"; // por default se pone la opcion a expandir
                string link = "<a href='pagos.aspx?index=" + lstPAbiertas[i].indice + "&cont=n'>";
                string endlink = "</a>";
                if (lstPAbiertas[i].indice == index)
                {
                    clase = "kz";
                    contenido = "<div class='ico-contraer'></div>";
                    if (cont == "yes") // de acuerdo a lo que se recibe se configura la contra
                    {
                        link = "<a href='pagos.aspx?index=" + i + "&cont=n'>";
                    }
                    else
                    {
                        link = "<a href='pagos.aspx?index=" + i + "&cont=yes'>";
                    }
                }
                if (cont == "et")
                {
                    clase = "kz";
                }
                if (lstPAbiertas[i].BLART1 == "KZ" || lstPAbiertas[i].BLART1 == "ZT" || lstPAbiertas[i].BLART1 == "KA") //#F5DA81
                {                    
                    if (cont == "yes")
                    {
                        contenido = "<div class='ico-expandir'></div>";
                    }
                    if (cont == "et")
                    {
                        contenido = "<div class='ico-contraer'></div>";
                    }

                    //html += "<div id='contenedorPagos'>"; // para dividir cada KZ con sus respectivos RE
                    html += "<tr class='" + clase + "' " + ((clase=="kz") ? "" : "") + ">" + // clase para agregar el color correspondiente
                               "    <td>" + lstPAbiertas[i].AUGBL1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].BELNR1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].BLART1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].BLDAT1 + "</td>" +
                               "    <td class='columna-numerica'>" + lstPAbiertas[i].DMSHB1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].HWAER1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].ZUONR1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].XBLNR + "</td>" +
                               "    <td>" + lstPAbiertas[i].KONTO + "</td>" +
                               "    <td>" + lstPAbiertas[i].NAME1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].SGTXT + "</td>" +
                               "    <td>" + lstPAbiertas[i].EBELN + "</td>" +
                               "    <td class='icono nonecolor'>" + link + contenido + endlink + "</td>" + // contenido solo es para agregar el icono correspondiente
                               "</tr>";
                    if (lstPAbiertas[i].indice == index || cont == "et") // en caso de que sea el index a expandir o que la se reciba la peticion de expandir todo
                    {
                        i++;
                        while (lstPAbiertas.Count > i && lstPAbiertas[i].BLART1 == "RE" && cont != "yes") //#F3F781
                        {
                            html += "<tr class='re'>" +
                                   "    <td>" + lstPAbiertas[i].AUGBL1 + "</td>" +
                                   "    <td>" + lstPAbiertas[i].BELNR1 + "</td>" +
                                   "    <td>" + lstPAbiertas[i].BLART1 + "</td>" +
                                   "    <td>" + lstPAbiertas[i].BLDAT1 + "</td>" +
                                   "    <td class='columna-numerica'>" + lstPAbiertas[i].DMSHB1 + "</td>" +
                                   "    <td>" + lstPAbiertas[i].HWAER1 + "</td>" +
                                   "    <td>" + lstPAbiertas[i].ZUONR1 + "</td>" +
                                   "    <td>" + lstPAbiertas[i].XBLNR + "</td>" +
                                   "    <td>" + lstPAbiertas[i].KONTO + "</td>" +
                                   "    <td>" + lstPAbiertas[i].NAME1 + "</td>" +
                                   "    <td>" + lstPAbiertas[i].SGTXT + "</td>" +
                                   "    <td>" + lstPAbiertas[i].EBELN + "</td>" +
                                   "    <td style='background:#FFFFFF;'></td>" +
                                   "</tr>";
                            i++;
                        }
                        i--;
                        //html += "</div>"; // cierra contenedorPagos
                    }
                    else {
                        //html += "</div>";  // cierra contenedorPagos
                    }
                }
            }
            html += "   </tbody>" + "</table>";
            return html;
        }

        public string convertListPAbiertasToTableInCodeFacturas(List<PAbiertasYPago> lstPAbiertas)
        {
            string html = "";
            html += "<table class='tblComun sortTable' id='tableToOrder'>";
            html += "   <thead>" +
                        "       <tr>" +
                        //"         <th class='" + "tHide" + "'>hide</th>" +
                        "         <th>Clearing Document</th>" +
                        "         <th>Nº documento</th>" +
                        "         <th>Tipo de documento</th>" +
                        //"         <th>Fecha de pago</th>" +
                        "         <th>Monto</th>" +
                        "         <th>Moneda</th>" +
                        "         <th>Nº asignacion</th>" +
                        "         <th>Factura</th>" +
                        "         <th>Proveedor</th>" +
                        "         <th>Doc. Compras</th>" +
                        "         <th>Fecha base. Base</th>" +
                        //"         <th>F. Vencimiento</th>" +
                        "       </tr>" +
                        "   </thead>";
            html += "   <tbody>";

            for (int i = 0; i < lstPAbiertas.Count; i++)
            {
                html += "<tr>" +
                               "    <td>" + lstPAbiertas[i].AUGBL1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].BELNR1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].BLART1 + "</td>" +
                               //"    <td>" + lstPAbiertas[i].BLDAT1 + "</td>" +
                               "    <td class='columna-numerica'>" + lstPAbiertas[i].DMSHB1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].HWAER1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].ZUONR1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].XBLNR + "</td>" +
                               "    <td>" + lstPAbiertas[i].NAME1 + "</td>" +
                               "    <td>" + lstPAbiertas[i].EBELN + "</td>" +
                               "    <td>" + lstPAbiertas[i].F_BASE + "</td>" +
                               //"    <td>" + lstPAbiertas[i].F_VENCIM + "</td>" +
                               "</tr>"; 
            }
            html += "   </tbody>" +  "</table>";
            return html;
        }

        //mgv
        public string convertListFV60ToTableInCodeFacturas(List<FV60XVerificar> lstFv60)
        {
            List<PEntidades.HeaderList> nombres = new List<HeaderList>();
            HeaderList head;
            nombres.Add(head = new HeaderList("Referencia "));
            nombres.Add(head = new HeaderList("Sociedad "));
            nombres.Add(head = new HeaderList("Docto contable "));
            nombres.Add(head = new HeaderList("Ejercicio"));
            nombres.Add(head = new HeaderList("Fecha doc "));
            nombres.Add(head = new HeaderList("Fecha contable "));
            nombres.Add(head = new HeaderList("Moneda "));
            nombres.Add(head = new HeaderList("Tipo de Cambio "));
            nombres.Add(head = new HeaderList("Cta prov"));
            nombres.Add(head = new HeaderList("Impte mon local"));
            nombres.Add(head = new HeaderList("Impte mon docto "));
            nombres.Add(head = new HeaderList("IVA"));
            nombres.Add(head = new HeaderList("Adjuntos"));

            string codeBeginTable = "";
            codeBeginTable += "<table class='tblComun tblFact'id='tableToOrder'>";

            string codetableCreateHeader = "<thead><tr>";
            for (int i = 0; i < nombres.Count; i++)
            {
                codetableCreateHeader += "<th>" + nombres[i].Header + "</th>";
            }
            codetableCreateHeader += "<th></th>";
            codetableCreateHeader += "<th>Est.</th>";
            codetableCreateHeader += "<th>Des.</th>";
            codetableCreateHeader += "<th>Descripción</th>";
            codetableCreateHeader += "<th>Det.</th>";
            codetableCreateHeader += "</tr></thead>";
            codetableCreateHeader += "<th class='icono'></th>";
            codetableCreateHeader += "</tr></thead>";

            string codeFillTable = "<tbody>";
            string clase = "";
            int id = 1;
            int contadorEs = 0;
            string cantidadXML = "";

            for (int i = 0; i < lstFv60.Count; i++)
            {
                string claseFila = "";
 // mgv              if (lstFv60[i].TIPOLINEA.ToUpper() == "D")
 //               {
 //                   clase = "class = 'd D" + id + "'";
 //                   cantidadXML = lstFv60[i].ZCOUNT.ToString();
 //               }
 //               else if (lstFv60[i].TIPOLINEA.ToUpper() == "E")
 //               {
 //                 cantidadXML = "";
                    cantidadXML = lstFv60[i].ZCOUNT.ToString();   // como no hay linea detalle
                    id++;
                    clase = "class = 'hidd D" + id + "'";
                    contadorEs++;
 //mgv               }

                codeFillTable += "<tr " + clase + " id='D" + id + "'>" +
                "<td>" + lstFv60[i].XBLNR + "</td>";
                clase = ""; // clase se regresa a vacio

                codeFillTable += "<td class='bukrs' class='columna-numerica'>" + lstFv60[i].BUKRS + "</td>" +
                                "<td class='belnr' class='columna-numerica'>" + lstFv60[i].BELNR + "</td>" +
                                "<td class='gjahr' class='columna-numerica'>" + lstFv60[i].GJAHR + "</td>" +
                                "<td class='columna-numerica'>" + lstFv60[i].BLDAT + "</td>" +
                                "<td class='columna-numerica'>" + lstFv60[i].BUDAT + "</td>" +
                                //"<td>" + lstFv60[i].XBLNR + "</td>" +
                                "<td>" + lstFv60[i].WAERS + "</td>" +
                                "<td class='columna-numerica'>" + lstFv60[i].KURSF + "</td>" +
                                "<td class='columna-numerica'>" + lstFv60[i].LIFNR + "</td>" +
                                "<td class='columna-numerica'>" + lstFv60[i].DMBTR + "</td>" +
                                "<td class='columna-numerica'>" + lstFv60[i].WRBTR + "</td>" +
                                "<td class='columna-numerica'>" + lstFv60[i].WMWST + "</td>" + 
                                "<td class='columna-numerica'>" + lstFv60[i].ZCOUNT + "</td>";
                //--- mgv Junio 28
                lstFv60[i].DescripcionErrorSAT = lstFv60[i].DescripcionErrorSAT.ToUpper().Trim();
                lstFv60[i].DescripcionErrorSAP = lstFv60[i].DescripcionErrorSAP.ToUpper().Trim();
                lstFv60[i].InsidenciaPersonal = lstFv60[i].InsidenciaPersonal.ToUpper().Trim();

                lstFv60[i].ErrorCompleto = "";
                if (lstFv60[i].DescripcionErrorSAT == "")
                {
                    lstFv60[i].ErrorMostrar = "N/A";
                    lstFv60[i].ErrorCompleto += " <br/> ";
                }
                else
                {
                    lstFv60[i].ErrorMostrar = lstFv60[i].DescripcionErrorSAT;
                    lstFv60[i].ErrorCompleto = lstFv60[i].msgVarios + " <br/> ";
                }

                if (lstFv60[i].DescripcionErrorSAP == "" || lstFv60[i].DescripcionErrorSAP == "N/A")
                {
                    lstFv60[i].ErrorCompleto += "" + " <br/> ";

                }
                else
                {
                    lstFv60[i].ErrorMostrar = lstFv60[i].DescripcionErrorSAP;
                    lstFv60[i].ErrorCompleto = lstFv60[i].msgVarios + " <br/> ";
                }

                if (lstFv60[i].DescripcionErrorSAP.Equals("SAP : Cargada correctamente"))
                {
                    lstFv60[i].ErrorMostrar = lstFv60[i].DescripcionErrorSAP;
                    codeFillTable += "<td></td>";
                }
                else
                {
                    claseFila = "hrfCargadorXml";
                    if (lstFv60[i].TIPOLINEA.ToUpper() == "E")
                    {
                        claseFila = "fv"; // fondo verde  claseFila = "fa";
                    }
                    else if (lstFv60[i].ZCOUNT >= 10)
                    {
                        claseFila = "hrfCantidadMax";
                    }
                    codeFillTable += "<td class='icono' title='Para cargar archivo correpondiente al la entrada " + lstFv60[i].XBLNR + "'><div class='" + claseFila + "' indx = '" + i + "'></div></td>";
                }
                if (lstFv60[i].msgVarios != "" && lstFv60[i].esPrimerCarga == false)
                {
                    lstFv60[i].ErrorCompleto = lstFv60[i].msgVarios;
                    lstFv60[i].ErrorMostrar = "Varios archivos"; // Varios XML
                }

                if (lstFv60[i].InsidenciaPersonal == "")
                {
                    lstFv60[i].ErrorCompleto += "" + " <br/> ";
                }
                else
                {
                    lstFv60[i].ErrorMostrar = "Incidencia Manual";
                    lstFv60[i].ErrorCompleto += "INCIDENCIA MANUAL : " + lstFv60[i].InsidenciaPersonal.Replace("\\N", " <br/> ").ToString();
                }
                switch (lstFv60[i].ErrorMostrar)
                {
                    case "Varios XML":
                        codeFillTable += "<td class='icono'><div class='variosXML' title='" + lstFv60[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;
                    case "Varios archivos":
                        codeFillTable += "<td class='icono'><div class='variosXML' title='" + lstFv60[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;
                    case "N/A":
                        claseFila = "estatus-sin-accion";
                        //if (lstFv60[i].TIPOLINEA.ToUpper() == "E")
                        //{
                        //    claseFila = "fv"; // fondo azul         
                        //}
                        codeFillTable += "<td class='icono'><div class='" + claseFila + "' title='" + lstFv60[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;
                    case "SAT : CANCELADO":
                        codeFillTable += "<td class='icono'><div class='estatus-cancelado' title='" + lstFv60[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    case "SAT : ESTRUCTURA INCORRECTA":
                        codeFillTable += "<td class='icono'><div class='estatus-estructura_invalida' title='" + lstFv60[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    case "NO ES XML":
                        codeFillTable += "<td class='icono'><div class='' title='" + lstFv60[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    case "FORMATO DE ARCHIVO NO VALIDO":
                        codeFillTable += "<td class='icono'><div class='' title='" + lstFv60[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    case "SAP : CARGADA CORRECTAMENTE":
                        codeFillTable += "<td class='icono'><div class='estatus-correcto' title='" + lstFv60[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    case "N/AIncidenciaP"://INCIDENCIA PERSONAL
                        codeFillTable += "<td class='icono'><button class='' title='" + "No hay incidencias" + "'>" + "" + "</button></td>";
                        break;

                    case "":
                        codeFillTable += "<td class='icono'><div class='' title='" + lstFv60[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    default:
                        string titulo = "Error de incidencia";
                        claseFila = "estatus-no_coincide";
                        if (lstFv60[i].TIPOLINEA.ToUpper() == "E")
                        {
                            titulo = "";
                            claseFila = "";
                            lstFv60[i].ErrorMostrar = "";
                        }
                        codeFillTable += "<td class='icono'><div class='" + claseFila + "' title='" + titulo + "'>" + "" + "</div></td>";
                        break;
                }

                // --- mgv Junio 28
                //codeFillTable += "<td class='icono' title='Para cargar archivo correpondiente al la entrada " + lstFv60[i].XBLNR + "'><div class='" + claseFila + "' indx = '" + i + "'></div></td>";
                //codeFillTable += "<td class='icono'><div class='fv' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                //codeFillTable += "<td class='icono'><div class='fv' indx = '" + i + "'>" + "" + "</div></td>";

                // Lalo
                if (lstFv60[i].TIPOLINEA.ToUpper() == "E")
                {
                    int pos = i; //como no existen detalles se queda en la misma posicion.... int pos = i + 1;
                    string htmldesa = "";
                    int poci = 1;
//mgv                    while (poci < lstFv60.Count)
//mgv                    {

 //mgv: xq todas    if (lstFv60[pos].TIPOLINEA.ToUpper() != "E")
 //son linea E         {
                            if (lstFv60[pos].ZCOUNT > 0)
                            {
                                string desadjuntar = "";
                                string msm = lstFv60[pos].msgVarios;
                                popdesvincularXML(ref msm, ref desadjuntar);
                                list_uuid.Clear();
                                htmldesa += "<p>Posicion " + pos + " </p>" + desadjuntar;
 //mgv                          pos++;
                                poci++;
                            }
                            else
                            {
                               poci++;
//mgv                                pos++;
                             }
                        //                       }
                        //                       else
                        //                       {
                        //                           break;
                        //mgv                       }
//mgv                    }
                    if (htmldesa == "")
                    {
                        codeFillTable += "<td class='iconodes'><div class='' title='Desajuntar archivos'></div></td>";
                    }
                    else
                    {
                        // codeFillTable += "<td class='iconodes'><div class='desadjuFV60XML' title='Desadjuntar archivos' msm='" + htmldesa + "'></div></td>";
                        string VanValores = lstFv60[pos].BUKRS + "+" + lstFv60[pos].GJAHR + "+" + lstFv60[pos].BELNR + "+";
                        codeFillTable += "<td class='iconodes'><div class='desadjuFV60XML' title='Desadjuntar archivos' msm='" + htmldesa + "' value='" + VanValores + "'></div></td>";

                    }
                }
                else
                {
                    codeFillTable += "<td class='icono nonecolor'><div class='' title='Desajuntar archivos'></div></td>";
                }

                claseFila = "factDescripcion";
                if (lstFv60[i].TIPOLINEA.ToUpper() == "E")
                {
                    claseFila = "fondoGris";
                    lstFv60[i].ErrorMostrar = "";
                }
                codeFillTable += "<td class='" + claseFila + "'>" + lstFv60[i].ErrorMostrar + "</td>";
                claseFila = "estatus-detalle";
                if (lstFv60[i].TIPOLINEA.ToUpper() == "E")
                {
                    claseFila = "fondoGris";
                }
                codeFillTable += "<td class='icono " + claseFila + "'><div class='" + claseFila + "' msg='" + lstFv60[i].ErrorCompleto + "'></div></td>";

                //string icono = "ico-contraer";
                //if (lstFv60[i].TIPOLINEA.ToUpper() == "E")
                //{
                //    icono = "ico-expandir";
                //    codeFillTable += "<td class='icono nonecolor " + claseFila + "'><div class='" + icono + "'></div></td>";
                //}
                //else if (lstFv60[i].TIPOLINEA.ToUpper() == "E")
                //{
                //    codeFillTable += "<td style='background:#FFFFFF;'></td>";
                //}
                codeFillTable += "</tr>";
            }
            string codeEndTable = "</tbody></table>";
            string completeTable = "" + codeBeginTable + codetableCreateHeader + codeFillTable + codeEndTable;
            return completeTable;
        }

        public string convertListToTableInCode(List<FacturasXVerificar> listFact, string ordenarOrden)//Lista de facturas
        {
            List<PEntidades.HeaderList> nombres = new List<HeaderList>();
            HeaderList head;
            
            nombres.Add(head = new HeaderList("Factura-Remisión"));
            if (ordenarOrden == "X")
            {
                nombres.Add(head = new HeaderList("Orden de compra"));   
            }
            //nombres.Add(head = new HeaderList("Costos plan"));
            nombres.Add(head = new HeaderList("Sociedad"));
            nombres.Add(head = new HeaderList("Centro"));
            nombres.Add(head = new HeaderList("Fecha Doc."));
            nombres.Add(head = new HeaderList("Fecha de recepción"));
            nombres.Add(head = new HeaderList("Importe"));
            nombres.Add(head = new HeaderList("Material"));
            nombres.Add(head = new HeaderList("Numero Material"));
            //nombres.Add(head = new HeaderList("Imp. IVA"));
            //nombres.Add(head = new HeaderList("Retención"));
            nombres.Add(head = new HeaderList("Moneda"));
            nombres.Add(head = new HeaderList("Total"));
            nombres.Add(head = new HeaderList("Adjuntos"));

            string codeBeginTable = "";
            codeBeginTable += "<table class='tblComun tblFact sortTable tblGroup' id='tableToOrder'>";

            string codetableCreateHeader = "<thead><tr>";
            for (int i = 0; i < nombres.Count; i++)
            {
                codetableCreateHeader += "<th>" + nombres[i].Header + "</th>";
            }
            codetableCreateHeader += "<th></th>";
            codetableCreateHeader += "<th>Est.</th>";
            codetableCreateHeader += "<th>Des.</th>";
            codetableCreateHeader += "<th>Descripción</th>";
            codetableCreateHeader += "<th>Det.</th>";
            codetableCreateHeader += "<th class='icono'></th>";
            codetableCreateHeader += "</tr></thead>";

            string codeFillTable = "<tbody>";
            string clase = "";
            int id = 1;
            int contadorEs = 0;
            string cantidadXML = "";
            //string idS = "";
            //try
            //{
                for (int i = 0; i < listFact.Count; i++)
                {
                    string claseFila = "";
                    if (listFact[i].tipoLinea.ToUpper() == "D")
                    {
                        clase = "class = 'd D" + id + "'";
                        cantidadXML = listFact[i].cantidadXML.ToString();
                    }
                    else if (listFact[i].tipoLinea.ToUpper() == "E")
                    {
                        cantidadXML = "";
                        id++;
                        clase = "class = 'hidd D" + id + "'";
                        contadorEs++;

                    }
                    codeFillTable += "<tr " + clase + " id='D" + id + "'>" +
                                    "<td>" + listFact[i].XBLNR2 + "</td>";
                    clase = ""; // clase se regresa a vacio
                    if (ordenarOrden == "X")
                    {
                        codeFillTable += "<td>" + listFact[i].EBELN + "</td>";
                    }
                    //if (listFact[i].TIPO == "X")
                    //{
                    //    codeFillTable += "<td>" + "<center><img src='../images/tipoOk.png' class='tipo'/></center>" + "</td>";
                    //}
                    //else 
                    //{
                    //    codeFillTable += "<td>" + "<center><img src='../images/tipoX.png' class='tipo'/></center>" + "</td>"; 
                    //}
                    codeFillTable += "<td>" + listFact[i].BUKRS + "</td>" +
                                      "<td>" + listFact[i].WERKS + "</td>" +
                                      "<td class='columna-numerica'>" + listFact[i].BUDAT + "</td>" +
                                      "<td class='columna-numerica'>" + listFact[i].BLDAT + "</td>" +
                                      "<td class='columna-numerica'>" + listFact[i].WRBTR + "</td>" +
                                      "<td class='columna-numerica'>" + listFact[i].descMaterial + "</td>" +
                                      "<td class='columna-numerica'>" + listFact[i].MATNR + "</td>" +
                                      //"<td class='columna-numerica'>" + listFact[i].MWSKZ + "</td>" +
                                      //"<td class='columna-numerica'>" + listFact[i].RETENCION + "</td>" +
                                      "<td >" + listFact[i].WAERS + "</td>" +
                                      "<td class='columna-numerica'>" + listFact[i].SALDO + "</td>" +
                                      "<td class='columna-numerica'>" + cantidadXML + "</td>";
                    listFact[i].DescripcionErrorSAT = listFact[i].DescripcionErrorSAT.ToUpper().Trim();
                    listFact[i].DescripcionErrorSAP = listFact[i].DescripcionErrorSAP.ToUpper().Trim();
                    listFact[i].InsidenciaPersonal = listFact[i].InsidenciaPersonal.ToUpper().Trim();

                    listFact[i].ErrorCompleto = "";
                    if (listFact[i].DescripcionErrorSAT == "")
                    {
                        listFact[i].ErrorMostrar = "N/A";
                        listFact[i].ErrorCompleto += " <br/> ";
                    }
                    else
                    {
                        listFact[i].ErrorMostrar = listFact[i].DescripcionErrorSAT;
                        //listFact[i].ErrorCompleto += listFact[i].DescripcionErrorSAT + " <br/> ";
                        //listFact[i].ErrorCompleto += listFact[i].msgVarios + " <br/> ";
                        listFact[i].ErrorCompleto = listFact[i].msgVarios + " <br/> ";
                    }

                    if (listFact[i].DescripcionErrorSAP == "" || listFact[i].DescripcionErrorSAP == "N/A")
                    {
                        listFact[i].ErrorCompleto += "" + " <br/> ";

                    }
                    else
                    {
                        listFact[i].ErrorMostrar = listFact[i].DescripcionErrorSAP;
                        //listFact[i].ErrorCompleto += listFact[i].ErrorMostrar + " <br/> ";
                        //listFact[i].ErrorCompleto += listFact[i].msgVarios + " <br/> ";
                        listFact[i].ErrorCompleto = listFact[i].msgVarios + " <br/> ";
                    }

                    if (listFact[i].DescripcionErrorSAP.Equals("SAP : Cargada correctamente"))
                    {
                        listFact[i].ErrorMostrar = listFact[i].DescripcionErrorSAP;
                        codeFillTable += "<td></td>";
                    }
                    else
                    {
                        claseFila = "hrfCargadorXml";
                        if (listFact[i].tipoLinea.ToUpper() == "E")
                        {
                            claseFila = "fa"; // fondo verde
                        }
                        else if (listFact[i].cantidadXML >= 10)
                        {
                            claseFila = "hrfCantidadMax";
                        }
                        //codeFillTable += "<td class='icono'><a href='facturasDet.aspx?index=" + i + "' title='Para cargar XML correpondiente al la entrada " + listFact[i].XBLNR + "' ><div class='"+claseFila+"'></div></a></td>";
                        codeFillTable += "<td class='icono' title='Para cargar archivo correpondiente al la entrada " + listFact[i].XBLNR2 + "'><div class='" + claseFila + "' indx = '" + i + "'></div></td>";
                    }
                    if (listFact[i].msgVarios != "" && listFact[i].esPrimerCarga == false)
                    {
                        listFact[i].ErrorCompleto = listFact[i].msgVarios;
                        listFact[i].ErrorMostrar = "Varios archivos"; // Varios XML
                    }

                    if (listFact[i].InsidenciaPersonal == "")
                    {
                        listFact[i].ErrorCompleto += "" + " <br/> ";
                    }
                    else
                    {
                        listFact[i].ErrorMostrar = "Incidencia Manual";
                        listFact[i].ErrorCompleto += "INCIDENCIA MANUAL : " + listFact[i].InsidenciaPersonal.Replace("\\N", " <br/> ").ToString();
                    }
                    switch (listFact[i].ErrorMostrar)
                    {
                        case "Varios XML":
                            codeFillTable += "<td class='icono'><div class='variosXML' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                            break;
                        case "Varios archivos":
                            codeFillTable += "<td class='icono'><div class='variosXML' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                            break;
                        case "N/A":
                            claseFila = "estatus-sin-accion";
                            if (listFact[i].tipoLinea.ToUpper() == "E")
                            {
                                claseFila = "fv"; // fondo azul         
                            }
                            codeFillTable += "<td class='icono'><div class='" + claseFila + "' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                            break;
                        case "SAT : CANCELADO":
                            codeFillTable += "<td class='icono'><div class='estatus-cancelado' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                            break;

                        case "SAT : ESTRUCTURA INCORRECTA":
                            codeFillTable += "<td class='icono'><div class='estatus-estructura_invalida' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                            break;

                        case "NO ES XML":
                            codeFillTable += "<td class='icono'><div class='' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                            break;

                        case "FORMATO DE ARCHIVO NO VALIDO":
                            codeFillTable += "<td class='icono'><div class='' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                            break;

                        case "SAP : CARGADA CORRECTAMENTE":
                            codeFillTable += "<td class='icono'><div class='estatus-correcto' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                            break;

                        case "N/AIncidenciaP"://INCIDENCIA PERSONAL
                            codeFillTable += "<td class='icono'><button class='' title='" + "No hay incidencias" + "'>" + "" + "</button></td>";
                            break;

                        case "":
                            codeFillTable += "<td class='icono'><div class='' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                            break;

                        default:
                            string titulo = "Error de incidencia";
                            claseFila = "estatus-no_coincide";
                            if (listFact[i].tipoLinea.ToUpper() == "E")
                            {
                                titulo = "";
                                claseFila = "";
                                listFact[i].ErrorMostrar = "";
                            }
                            codeFillTable += "<td class='icono'><div class='" + claseFila + "' title='" + titulo + "'>" + "" + "</div></td>";
                            break;
                    }
                    // Lalo
                    if (listFact[i].tipoLinea.ToUpper() == "E")
                    {
                    int pos = i + 1;
                    string htmldesa = "";
                    int poci = 1;
                    while (pos < listFact.Count)
                    {

                        if (listFact[pos].tipoLinea.ToUpper() != "E")
                        {
                            if (listFact[pos].cantidadXML > 0)
                            {
                                string desadjuntar = "";
                                string msm = listFact[pos].msgVarios;
                                popdesvincularXML(ref msm, ref desadjuntar);
                                list_uuid.Clear();
                                htmldesa += "<p>Pocision " + poci + " </p>" + desadjuntar;
                                pos++;
                                poci++;
                            }
                            else
                            {
                                poci++;
                                pos++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (htmldesa == "")
                    {
                        codeFillTable += "<td class='iconodes'><div class='' title='Desajuntar archivos'></div></td>";
                    }
                    else
                    {
                        codeFillTable += "<td class='iconodes'><div class='desadjuntarXML' title='Desadjuntar archivos' msm='" + htmldesa + "'></div></td>"; ;
                    }
                }
                    else
                    {
                        codeFillTable += "<td class='icono nonecolor'><div class='' title='Desajuntar archivos'></div></td>";
                    }

                    claseFila = "factDescripcion";
                    if (listFact[i].tipoLinea.ToUpper() == "E")
                    {
                        claseFila = "fondoGris";
                        listFact[i].ErrorMostrar = "";
                    }
                    codeFillTable += "<td class='" + claseFila + "'>" + listFact[i].ErrorMostrar + "</td>";
                    claseFila = "estatus-detalle";
                    if (listFact[i].tipoLinea.ToUpper() == "E")
                    {
                        claseFila = "fondoGris";
                    }
                    codeFillTable += "<td class='icono " + claseFila + "'><div class='" + claseFila + "' msg='" + listFact[i].ErrorCompleto + "'></div></td>";

                    string icono = "ico-contraer";
                    if (listFact[i].tipoLinea.ToUpper() == "E")
                    {
                        icono = "ico-expandir";
                        codeFillTable += "<td class='icono nonecolor " + claseFila + "'><div class='" + icono + "'></div></td>";
                    }
                    else if (listFact[i].tipoLinea.ToUpper() == "E")
                    {
                        codeFillTable += "<td style='background:#FFFFFF;'></td>";
                    }


                    codeFillTable += "</tr>";
                }
            //}
            //catch (Exception e)
            //{

            //    throw;
            //}
            
            string codeEndTable = "</tbody></table>";
            string completeTable = "" + codeBeginTable + codetableCreateHeader + codeFillTable + codeEndTable;
            return completeTable;
        }

        /**            * Autor original : Eric Alejandro             * */
        public string convertirAHtmlTable(List<FacturasXVerificar> listFact)//Lista de facturas
        {

            List<PEntidades.HeaderList> nombres = new List<HeaderList>();
            HeaderList head;
            nombres.Add(head = new HeaderList("Factura"));
            nombres.Add(head = new HeaderList("Sociedad"));
            nombres.Add(head = new HeaderList("Centro"));
            nombres.Add(head = new HeaderList("Fecha de recepción"));
            nombres.Add(head = new HeaderList("Fecha Doc."));
            nombres.Add(head = new HeaderList("Importe"));
            nombres.Add(head = new HeaderList("Imp. IVA"));
            nombres.Add(head = new HeaderList("IVA"));
            nombres.Add(head = new HeaderList("Retención"));
            nombres.Add(head = new HeaderList("Moneda"));
            nombres.Add(head = new HeaderList("F. Elect."));
            nombres.Add(head = new HeaderList("Saldo"));

            string codeBeginTable = "";
            codeBeginTable += "<table class='tblComun tblFact'>";

            string codetableCreateHeader = "<thead><tr>";
            for (int i = 0; i < nombres.Count; i++)
            {
                codetableCreateHeader += "<th>" + nombres[i].Header + "</th>";
            }
            codetableCreateHeader += "<th>Est.</th>";
            codetableCreateHeader += "<th>Descripción</th>";
            codetableCreateHeader += "<th>Det.</th>";
            codetableCreateHeader += "</tr></thead>";

            string codeFillTable = "<tbody>";
            for (int i = 0; i < listFact.Count; i++)
            {

                codeFillTable += "<tr>" +
                                "<td>" + listFact[i].XBLNR2 + "</td>" +
                                "<td>" + listFact[i].BUKRS + "</td>" +
                                "<td>" + listFact[i].WERKS + "</td>" +
                                "<td class='columna-numerica'>" + listFact[i].BUDAT + "</td>" +
                                "<td class='columna-numerica'>" + listFact[i].BLDAT + "</td>" +
                                "<td class='columna-numerica'>" + listFact[i].WRBTR + "</td>" +
                                "<td class='columna-numerica'>" + listFact[i].IVA + "</td>" +
                                "<td class='columna-numerica'>" + listFact[i].MWSKZ + "</td>" +
                                "<td class='columna-numerica'>" + listFact[i].RETENCION + "</td>" +
                                "<td >" + listFact[i].WAERS + "</td>" +
                                "<td class='columna-numerica'>" + listFact[i].ELECT + "</td>" +
                                "<td class='columna-numerica'>" + listFact[i].SALDO + "</td>";

                listFact[i].DescripcionErrorSAT = listFact[i].DescripcionErrorSAT.ToUpper().Trim();
                listFact[i].DescripcionErrorSAP = listFact[i].DescripcionErrorSAP.ToUpper().Trim();
                listFact[i].InsidenciaPersonal = listFact[i].InsidenciaPersonal.ToUpper().Trim();

                if (listFact[i].DescripcionErrorSAT == "")
                {
                    listFact[i].ErrorMostrar = "N/A";
                    listFact[i].ErrorCompleto += "\n";
                }
                else
                {
                    listFact[i].ErrorMostrar = listFact[i].DescripcionErrorSAT;
                    listFact[i].ErrorCompleto += listFact[i].DescripcionErrorSAT + "\n";
                }

                if (listFact[i].DescripcionErrorSAP == "")
                {
                    listFact[i].ErrorCompleto += "" + "\n";

                }
                else
                {
                    listFact[i].ErrorMostrar = listFact[i].DescripcionErrorSAP;
                    listFact[i].ErrorCompleto += listFact[i].ErrorMostrar + "\n";
                }


                if (listFact[i].DescripcionErrorSAP.Equals("SAP : Cargada correctamente"))
                {
                    listFact[i].ErrorMostrar = listFact[i].DescripcionErrorSAP;
                    codeFillTable += "<td></td>";
                }
                else
                {
                    //codeFillTable += "<td class='icono'><a href='facturasDet.aspx?index=" + i + "' title='Para cargar XML correpondiente al la entrada " + listFact[i].XBLNR + "' ><div class='hrfCargadorXml'></div></a></td>";
                }

                if (listFact[i].InsidenciaPersonal == "")
                {
                    listFact[i].ErrorCompleto += "" + "\n";
                }
                else
                {
                    listFact[i].ErrorMostrar = "Incidencia Manual";
                    listFact[i].ErrorCompleto += "INCIDENCIA MANUAL : " + listFact[i].InsidenciaPersonal.Replace("\\N", "\n").ToString();
                }

                switch (listFact[i].ErrorMostrar)
                {
                    case "N/A":
                        codeFillTable += "<td class='icono'><div class='estatus-sin-accion' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;
                    case "SAT : CANCELADO":
                        codeFillTable += "<td class='icono'><div class='estatus-cancelado' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    case "SAT : ESTRUCTURA INCORRECTA":
                        codeFillTable += "<td class='icono'><div class='estatus-estructura_invalida' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    case "NO ES XML":
                        codeFillTable += "<td class='icono'><div class='' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    case "SAP : CARGADA CORRECTAMENTE":
                        codeFillTable += "<td class='icono'><div class='estatus-correcto' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    case "N/AIncidenciaP"://INCIDENCIA PERSONAL
                        codeFillTable += "<td class='icono'><button class='' title='" + "No hay incidencias" + "'>" + "" + "</button></td>";
                        break;

                    case "":
                        codeFillTable += "<td class='icono'><div class='' title='" + listFact[i].ErrorMostrar + "'>" + "" + "</div></td>";
                        break;

                    default:
                        codeFillTable += "<td class='icono'><div class='estatus-no_coincide' title='" + "Error de incidencia" + "'>" + "" + "</div></td>";
                        break;
                }

                codeFillTable += "<td class='factDescripcion'>" + listFact[i].ErrorMostrar + "</td>";
                //codeFillTable += "<td class='icono'><div class='det'><img src='../images/detalle.png'></div></td>";
                codeFillTable += "<td class='icono'><div class='estatus-detalle' msg='" + listFact[i].ErrorCompleto + "'></div></td>";
                codeFillTable += "</tr>";
            }
            string codeEndTable = "</tbody></table>";
            string completeTable = "" + codeBeginTable + codetableCreateHeader + codeFillTable + codeEndTable;
            return completeTable;
        }

        public DataTable convertListPartAbiertasYPagos(List<PAbiertasYPago> list)
        {
            DataTable table;
            table = ConvertToDataTable<PAbiertasYPago>(list);
            table.Columns[0].ColumnName = "Assignment Number";
            table.Columns[1].ColumnName = "Acount. Doc. Number";
            table.Columns[2].ColumnName = "Doc. type";
            table.Columns[3].ColumnName = "Doc.date";
            table.Columns[4].ColumnName = "Amount";
            table.Columns[5].ColumnName = "1";

            return table;

        }

        private void popdesvincularXML(ref string msn, ref string codehtml)
        {
            string uuid = "";
            string msm = msn;
            int first, last;
            if (msm.Contains("<br> Nombre") == false)//recien adjuntado
            {
                if (codehtml.Contains("<table>") == false)
                {
                    codehtml += "<table><tr class=\"row_des_enca\"><td style=\"padding:5px\">Detalle de adjunto</td><td style=\"padding:5px\">Seleccion</td></tr>";
                }
                if (msm.Contains("SAP: Error al guardar el") == false && msm.Contains("Valores de XML no coinciden") == false)
                {
                    while (msm.Length > 0)
                    {
                        first = msm.IndexOf("UUID: ");
                        if (first > -1)
                        {
                            first += 6;
                            last = msm.IndexOf("</br>SAT");
                            uuid = msm.Substring(first, last - first);
                            if (searchUUID(uuid) == false)
                            {
                                codehtml += "<tr class=\"row_des_res\"><td style=\"padding:5px\">" + msm.Substring(0, last) + "</td><td style=\"padding:5px\"><input id=\"" + uuid.Trim() + "\" type=\"checkbox\" class=\"chkuuid\"/></td></tr>";
                            }
                            msm = msm.Remove(0, msm.IndexOf("</br></br>") + 10);
                        }
                        else
                        {
                            msm = "";
                        }
                    }
                    if (codehtml.Contains("</table>") == false)
                    {
                        codehtml += "</table>";
                    }
                }
                else
                {
                    msm = "";
                    if (codehtml.Contains("</table>") == false)
                    {
                        codehtml += "</table>";
                    }
                }
                msn = msm;
            }else
            {
                if(msm.Contains("UUID") || msm.Contains("SAT") || msm.Contains("SAP"))//adjuntados anteriormente
                {
                    if (codehtml.Contains("<table>") == false)
                    {
                        codehtml += "<table><tr class=\"row_des_enca\"><td style=\"padding:5px\">Detalle de adjunto</td><td style=\"padding:5px\">Seleccion</td></tr>";
                    }
                    while (msm.Length > 0)
                    {
                        first = msm.IndexOf("UUID: ");
                        if (first > -1)
                        {
                            first += 6;
                            last = msm.IndexOf("<br> Nombre");
                            if (last == -1)
                            {
                                msm = msm.Remove(0, msm.IndexOf("<br> <br>") + 9);
                                popdesvincularXML(ref msm, ref codehtml);
                            }
                            else
                            {
                                uuid = msm.Substring(first, last - first);
                                list_uuid.Add(uuid);
                                last = msm.IndexOf(".xml", System.StringComparison.OrdinalIgnoreCase) + 4; //ignora las mayusculas y minusculas
                                first -= 6;
                                codehtml += "<tr class=\"row_des_det\"><td style=\"padding:5px\" >" + msm.Substring(first, last - first) + "</td><td style=\"padding:5px\" ><input id=\"" + uuid.Trim() + "\" type=\"checkbox\" class=\"chkuuid\"/></td></tr>";
                                msm = msm.Remove(0, last + 3);
                            }
                            
                        }
                        else
                        {
                            msm = "";
                            if (codehtml.Contains("</table>") == false)
                            {
                                codehtml += "</table>";
                            }
                        }
                    }
                }
                else
                {
                    codehtml = msm;
                }
                msn = msm;
            } 
        }

        private bool searchUUID(string uuid)
        {
            bool existe = false;
            int inde = list_uuid.FindIndex(s => s.Contains(uuid));
            if (inde>-1)
            {
                existe = true;
            }
            else
            {
                list_uuid.Add(uuid);
            }
            return existe;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

    }
}
