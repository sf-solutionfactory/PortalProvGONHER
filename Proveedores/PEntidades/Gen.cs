using System;
using System.Collections.Generic;

namespace Gen.Util.CS
{
    public class Gen
    {
        /** Método que convierte un SQLReader a una Lista de arreglos de string List<string[]>
          */
        public static List<string[]> SQLReaderToListArray(System.Data.SqlClient.SqlDataReader datRdr)
        {
            int tamCol = datRdr.FieldCount;
            string[] tupla = new string[tamCol];
            List<string[]> lstRes = new List<string[]>();

            System.Data.DataTable schemaTable = datRdr.GetSchemaTable();
            System.Data.DataRow datRow;
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                datRow = schemaTable.Rows[i];
                tupla[i] = "" + datRow[0];
            }
            lstRes.Add(tupla);

            while (datRdr.Read())
            {
                tupla = new string[tamCol];
                for (int i = 0; i < tamCol; i++)
                {
                    tupla[i] = datRdr[i].ToString();
                }
                lstRes.Add(tupla);
            }
            return lstRes;
        }

        public static string convertToHtmlTable(List<string[]> lista)
        {
            return Gen.convertToHtmlTable(lista, "", "");
        }

        public static string convertToHtmlTable(List<string[]> lista, string htmlId, string cssClass) // se utiliza solo para mostrar tabñas de la parte del usuario
        {
            string html = "";
            int tamcol = lista[0].Length;
            html += "<table id='" + htmlId + "' class='" + cssClass + "'>" +
                    "<thead>";
            html += "<tr>";
            for (int i = 0; i < tamcol; i++)
            {
                html += "<th>" + lista[0][i] + "</th>";
            }
            html += "</tr></thead>";
            html += "<tbody>";
            for (int i = 1; i < lista.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < tamcol; j++)
                {
                    html += "<td>" + lista[i][j] + "</td>";
                }
                html += "</tr>";
            }
            html += "</tbody>";
            html += "</table>";
            return html;
        }

        public static string convertToHtmlTableSort(List<string[]> lista, string htmlId, string cssClass)
        {
            string html = "";
            int tamcol = lista[0].Length;
            //html += "<table>";
            //html += "<tr><td>";

            html += "<ul id='" + htmlId + "' class='" + cssClass + "'>";
            for (int i = 1; i < lista.Count; i++)
            {
                html += "<li class='ui-state-default' class='moveIt'>";
                for (int j = 0; j < tamcol; j++)
                {
                    if (j == 0)
                    {
                        html += "<div class='idProv'>" + lista[i][j].Trim() + "</div>";
                        html += " <br>";
                    }
                    else
                    {
                        html += "<div class='DivSort'>" + lista[i][j].Trim() + "</div>";
                        html += " <br>";
                    }
                }
                html += "</li>";
            }
            html += "</ul>";
            //html += "</td>";
            //html += "</tr>";
            //html += "</table>";
            return html;
        }

        public static string convertToHtmlTableSort2(List<string[]> lista, string htmlId, string cssClass, int ocultarIzquierda)
        {
            string html = "";
            int tamcol = lista[0].Length;
            //html += "<table>";
            //html += "<tr><td>";

            html += "<ul id='" + htmlId + "' class='" + cssClass + "'>";
            for (int i = 1; i < lista.Count; i++)
            {
                html += "<li class='ui-state-default' class='moveIt'>";
                for (int j = 0; j < tamcol; j++)
                {
                    if (j == 0)
                    {
                        html += "<div class='idProv'>" + lista[i][j].Trim() + "</div>";
                        html += " <br>";
                    }
                    else
                    {
                        html += "<div class='DivSort'>" + lista[i][j].Trim() + "</div>";
                        html += " <br>";
                    }
                }
                html += "</li>";
            }
            html += "</ul>";
            //html += "</td>";
            //html += "</tr>";
            //html += "</table>";
            return html;
        }

        public static string convertToHtmlTableSortGrupoNoticia(List<string[]> lista, string htmlId, string cssClass, int evitarDerecha)
        {
            //evitarDerecha = 1;
            string html = "";
            int tamcol = lista[0].Length;
            html += "<table>";
            //html += "<tr><td><label class='tituloSortNum'>" + lista[0][0].Trim() + "</label><label class='tituloSort'>" + lista[0][1].Trim() + "</label></td></tr>";
            html += "<tr><td>";

            html += "<ul id='" + htmlId + "' class='" + cssClass + "'>";
            for (int i = 1; i < lista.Count; i++)
            {
                html += "<li class='ui-state-default' class='moveIt'>";
                for (int j = (0 + 0); j < tamcol-(evitarDerecha); j++)
                {
                    if (j == 0)
                    {
                        html += "<label class='idProv tHide' >" + lista[i][j].Trim() + "</label>";
                        html += " <br>";

                    }
                    else
                    {
                        if (j == 3 || j == 4)
                        { // cuando sean fechas,(en este caso de la tabla de noticias)
                            if (j == 3)
                            {
                                //html += "<center>";
                                html += "<label class='DivSort dateli'>" + convertirFechaBDaFormatoJq(lista[i][j].Trim()) + "</label>";
                            }
                            else
                            {
                                html += "<label class='DivSort dateli'>" + convertirFechaBDaFormatoJq(lista[i][j].Trim()) + "</label>";
                                //html += "</center>";
                                html += " <br>";
                                
                            }
                        }
                        else {
                            html += "<label class='DivSort'>" + lista[i][j].Trim() + "</label>";
                            html += " <br>";
                        }
                    }
                }
                html += "</li>";
            }
            html += "</ul>";
            html += "</td>";
            html += "</tr>";
            html += "</table>";
            return html;
        }

        public static string convertToHtmlTableDelete(List<string[]> lista, string htmlId, string cssClass, List<int> listaEvitar, bool modificable, bool activable, bool desechable, bool detalles, int colEvitarDer, int colEvitarIzq)
        {
            //int colEvitarIzq = 1;
            string html = "";
            int tamcol = lista[0].Length - colEvitarDer;
            html += "<table id='" + htmlId + "' class='" + cssClass + "'>" +
                            "<thead>";
            html += "<tr>";
            for (int i = 0; i < tamcol; i++)
            {
                if (i < colEvitarIzq)
                {
                    html += "<th class='tHide'>" + lista[0][i] + "</th>";
                }
                else
                {
                    html += "<th>" + lista[0][i] + "</th>";
                }

            }
            if (activable || modificable || desechable || detalles)
            {
                html += "<th>Modificaciones</th>"; // agregar titulo de cambios para modificar, Activar/desactivar
            }

            html += "</tr></thead>";
            html += "<tbody>";
            for (int i = 1; i < lista.Count; i++)
            {
                string enviar = "";
                string estatus = "";
                string ComplementoBoton = "Activar";
                html += "<tr>";
                for (int j = 0; j < tamcol; j++)
                {
                    if (tamcol - 1 == j) // esto es para tomar el ultimo registro, se pone el menos uno para tomar la ultima columna que es la que trae el estatus
                    {
                        estatus = lista[i][j];
                        if (lista[i][j] == "True")
                        {
                            html += "<td>" + "Activo" + "</td>";
                        }
                        else
                        {
                            if (activable) // si el activable quiere decir que trae informacion de bloqueo(esbloq)
                            {
                                html += "<td>" + "Inactivo" + "</td>";
                            }
                            else
                            {
                                html += "<td>" + lista[i][j] + "</td>"; // en caso de que no contenga el estatus se debe de mostrar el contenido
                            }
                        }
                    }
                    else
                    {
                        if (j < colEvitarIzq)
                        {
                            html += "<td class='tHide'>" + lista[i][j] + "</td>";
                        }
                        else
                        {
                            html += "<td>" + lista[i][j] + "</td>";
                        }

                    }
                }

                if (estatus == "True")
                {
                    ComplementoBoton = "Desactivar";
                }

                enviar = lista[i][0].ToString();


                if (!Gen.isIn(listaEvitar, enviar))
                {
                    if (activable || modificable || desechable || detalles)
                    {
                        html += "<td>";
                        html += "<center>";
                        if (activable)// si Activar
                        {
                            html += "<label class='btn plusBtn'onclick='workThis(this);'>" + ComplementoBoton + "</label>";
                        }
                        if (modificable) // editar
                        {
                            html += "<label class='btn plusBtn' onclick='workThis(this);'>Modificar</label>";
                        }
                        if (desechable) // eliminar
                        {
                            html += "<label class='btn plusBtn' onclick='workThis(this);'>Eliminar</label>";
                            if (detalles) // eliminar
                            {
                                html += "<label class='btn plusBtn' onclick='workThis(this);'>Ver más</label>";
                            }
                        }
                        html += "</center>";
                        html += "</td>";
                    }
                }
                else// si se encuentra en la lista sera imposible editar
                {
                    html += "<td>Imposible cambiar</td>";
                }
                html += "</tr>";
            }
            html += "</tbody>";
            html += "</table>";
            return html;
        }

        public static string convertToHtmlTableDelete2(List<string[]> lista, string htmlId, string cssClass, List<int> listaEvitar, bool modificable, bool activable, bool desechable, bool detalles, int colEvitarDer, int colEvitarIzq, bool checkBox,  List<int> ListcheckBox)
        { 
            
            //int colEvitarIzq = 1;
            string html = "";
            int tamcol = lista[0].Length - colEvitarDer;
            html += "<table id='" + htmlId + "' class='" + cssClass + "'>" +
                     "<thead>";
            html += "<tr>";

            //bool checkBox = false;
            if(ListcheckBox.Count > 0){
                checkBox = true;  
            }

            ////
            if (checkBox == true) {
                //html += "<th class='tHide'>idCheck</th>";  
                html += "<th>Check</th>";  
            }
            ////

            for (int i = 0; i < tamcol; i++)
            {
                if (i < colEvitarIzq)
                {
                    html += "<th class='tHide'>" + lista[0][i] + "</th>"; // se agrega la clase de ocultar a la izquierda si es que se tiene considerado
                }
                else
                {
                    html += "<th>" + lista[0][i] + "</th>";
                }
            }
            if (activable || modificable || desechable || detalles)
            {
                html += "<th>Modificaciones</th>"; // agregar titulo de cambios para modificar, Activar/desactivar
            }

            html += "</tr></thead>";
            html += "<tbody>";
            for (int i = 1; i < lista.Count; i++)
            {
                string enviar = "";
                string estatus = "";
                string ComplementoBoton = "Activar";

                
                html += "<tr>";
                /////
                if (checkBox == true)
                {
                    //html += "<td class='tHide'>"+i+"</td>";
                    string isCheck = ""; 
                    for(int t = 0; t < ListcheckBox.Count; t++){
                        if (ListcheckBox[t] == i) {
                            isCheck = "checked"; 
                        }
                    }
                        //
                    html += "<td><input type='checkbox' class='chk' value='" + i + "' "+isCheck+"/></td>";
                }
                ////
                for (int j = 0; j < tamcol; j++)
                {
                    if (tamcol - 1 == j) // esto es para tomar el ultimo registro, se pone el menos uno para tomar la ultima columna que es la que trae el estatus
                    {
                        estatus = lista[i][j];
                        if (lista[i][j] == "True")
                        {
                            html += "<td>" + "Activo" + "</td>";
                        }
                        else
                        {
                            if (activable) // si el activable quiere decir que trae informacion de bloqueo(esbloq)
                            {
                                html += "<td>" + "Inactivo" + "</td>";
                            }
                            else
                            {
                                html += "<td>" + lista[i][j] + "</td>"; // en caso de que no contenga el estatus se debe de mostrar el contenido
                            }
                        }
                    }
                    else
                    {
                        if (j < colEvitarIzq)
                        {
                            html += "<td class='tHide'>" + lista[i][j] + "</td>";
                        }
                        else
                        {
                            html += "<td>" + lista[i][j] + "</td>";
                        }

                    }
                }

                if (estatus == "True")
                {
                    ComplementoBoton = "Desactivar";
                }

                enviar = lista[i][0].ToString();


                if (!Gen.isIn(listaEvitar, enviar))
                {
                    if (activable || modificable || desechable || detalles)
                    {
                        html += "<td>";
                        html += "<center>";
                        if (activable)// si Activar
                        {
                            html += "<label class='btn plusBtn'onclick='workThis(this);'>" + ComplementoBoton + "</label>";
                        }
                        if (modificable) // editar
                        {
                            html += "<label class='btn plusBtn' onclick='workThis(this);'>Modificar</label>";
                        }
                        if (desechable) // eliminar
                        {
                            html += "<label class='btn plusBtn' onclick='workThis(this);'>Eliminar</label>";
                        if (detalles) // eliminar
                        {
                            html += "<label class='btn plusBtn' onclick='workThis(this);'>Ver más</label>";
                        }
                        }
                        html += "</center>";
                        html += "</td>";
                    }
                }
                else// si se encuentra en la lista sera imposible editar
                {
                    html += "<td>Imposible cambiar</td>";
                }
                html += "</tr>";
            }
            html += "</tbody>";
            html += "</table>";
            return html;
        }

        public static bool isIn(List<int> listaEvitar, string id) {
            bool res = false;
            for(int i=0; i<listaEvitar.Count; i++ ){
                if (listaEvitar[i].ToString() == id)
                {
                    listaEvitar.Remove(i);
                    res = true;
                    break;
                }
            }
            return res;
        }

        public static string convertirFechaBDaFormatoJq(string fecha) {
            char[] delimiterChars = new char[1];
            delimiterChars[0] = ' ';
            string[] fechaS;
            fechaS = fecha.ToString().Split(delimiterChars);
            delimiterChars[0] = '/';
            fechaS = fechaS[0].ToString().Split(delimiterChars);
            fechaS[0] = fechaS[1] + "/" + fechaS[0] + "/" + fechaS[2];
            return fechaS[0].ToString();
        }

        public static string convertirFechaDesdeSesion(string date)
        {
            string[] split = date.Split(new Char[] { '/' });
            date = split[2] + "-" + split[0] + "-" + split[1];
            return date;
        }

        public static string convertirFecha(string date) // para SQL
        {
            string[] split = date.Split(new Char[] { '/' });
            if (split.Length==3)
            {
                //date = split[2] + "-" + split[1] + "-" + split[0];  // local
                date = split[2] + "-" + split[0] + "-" + split[1];  // ADN
                //año - dia - mes
            }
            return date;
        }

        public static string convertirFecha_SAP_CN(string date) // para pasar a CONECTOR SAP 
        {
            string[] split = date.Split(new Char[] { '/' });
            if (split.Length == 3)
            {
                date = split[2] + split[0] + split[1];         // yyyyMMdd
            }
            return date;
        }
        public static string convertirFecha_SAP(string date) // para pasar a sap
        {
            string[] split = date.Split(new Char[] { '/' });
            if (split.Length == 3)
            {
                date = split[2] + "-" + split[1] + "-" + split[0];
            }
            return date;
        }

        public static List<string[]> sintetizaInfoConexiones(List<string[]> informacionImportante)
        {
            List<string[]> listaDiferentesInstancias = new List<string[]>();
            PNegocio.Encript encript = new PNegocio.Encript();

            if (informacionImportante.Count > 1)
            {
                string[] datosEndpoint = new string[7]; // idInstancia, endpoint
                datosEndpoint[0] = informacionImportante[1][3];   //instancia
                datosEndpoint[1] = informacionImportante[1][4];   //endpointAdd
                datosEndpoint[2] = informacionImportante[1][0];   //bukrs
                datosEndpoint[3] = informacionImportante[1][5];   //lifnr
                //string desencript = encript.Desencriptar(encript.Desencriptar(informacionImportante[1][7].Trim()));
                //usuario, pass
                datosEndpoint[4] = informacionImportante[1][6].Trim() + "," + informacionImportante[1][7].Trim();
                //datosEndpoint[4] = informacionImportante[1][6].Trim() + "," + encript.Desencriptar(encript.Desencriptar(informacionImportante[1][7].Trim()));
                datosEndpoint[5] = informacionImportante[1][8].Trim();  // RFC
                datosEndpoint[6] = informacionImportante[1][9].Trim();  // descripcion
                listaDiferentesInstancias.Add(datosEndpoint);

                for (int i = 2; i < informacionImportante.Count; i++)
                {
                    if (informacionImportante[i][3] != listaDiferentesInstancias[listaDiferentesInstancias.Count - 1][0] ||
                        informacionImportante[i][4] != listaDiferentesInstancias[listaDiferentesInstancias.Count - 1][1]
                        )
                    { // primera vez que se encuentra
                        datosEndpoint = new string[7];
                        //instancias = "";
                        datosEndpoint[0] = informacionImportante[i][3];
                        datosEndpoint[1] = informacionImportante[i][4];
                        datosEndpoint[2] = informacionImportante[i][0];
                        datosEndpoint[3] = informacionImportante[i][5];
                        datosEndpoint[4] = informacionImportante[i][6].Trim() + "," + informacionImportante[i][7].Trim();
                        //datosEndpoint[4] = informacionImportante[i][6].Trim() + "," + encript.Desencriptar(encript.Desencriptar(informacionImportante[1][7].Trim()));
                        datosEndpoint[5] = informacionImportante[i][8].Trim();
                        datosEndpoint[6] = informacionImportante[i][9].Trim();
                        listaDiferentesInstancias.Add(datosEndpoint);
                    }
                    else // se concatenan los datos que nose repiten
                    {
                        datosEndpoint[2] += "," + informacionImportante[i][0];
                        datosEndpoint[3] += "," + informacionImportante[i][5];
                    }
                }
            }
            return listaDiferentesInstancias; 
        }

        public static List<string[]> sintetizaInfoCN(List<string[]> informacionImportante)
        {
            List<string[]> listaInstanciasCN = new List<string[]>();

            if (informacionImportante.Count > 1)
            {
      //SELECT 0 bukrs, 1.RFC, 2.lifnr, 3.instancia, 4.endpointAdd, 5.lifnr, 6.usuario, 7.pass, 8.RFC, 9.descripcion

                //SELECT 0bukrs, 1.RFC, 2.lifnr, 3.instancia, 4.Rfc_AppServerHost, 5.Rfc_SAPRouter, 6.Rfc_SystemNumber
                // 7.Rfc_Client, 8.Rfc_Language, 9.Rfc_PoolSize, 10.Rfc_PeakConnectionsLimit, 11.Rfc_PoolIdleTimeout,
                //12.lifnr, 13.Rfc_User, 14.Rfc_Password, 15r.RFC, 16.Rfc_Name

                string[] datosCN = new string[11]; // idrfc, conexion SAP
                datosCN[0] = informacionImportante[1][3];   //instancia
                datosCN[1] = informacionImportante[1][4];   //Rfc_AppServerHost
                datosCN[2] = informacionImportante[1][0];   //bukrs
                datosCN[3] = informacionImportante[1][2];   //lifnr
                datosCN[4] = informacionImportante[1][13].Trim() + "," + informacionImportante[1][14].Trim();  //usuario, pass
                datosCN[5] = informacionImportante[1][15].Trim();  // r.RFC
                datosCN[6] = informacionImportante[1][16].Trim();  // name
                datosCN[7] = informacionImportante[1][5].Trim();  // Rfc_SAPRouter
                datosCN[8] = informacionImportante[1][6].Trim();  // Rfc_SystemNumber
                datosCN[9] = informacionImportante[1][7].Trim();  // Rfc_Client
                datosCN[10] = informacionImportante[1][8].Trim();  // Rfc_Language
                datosCN[11] = informacionImportante[1][9].Trim();  // Rfc_PoolSize
                datosCN[12] = informacionImportante[1][10].Trim();  // Rfc_PeakConnectionsLimit
                datosCN[13] = informacionImportante[1][11].Trim();  // Rfc_PoolIdleTimeout
                listaInstanciasCN.Add(datosCN);

                for (int i = 2; i < informacionImportante.Count; i++)
                {
                    if (informacionImportante[i][3] != listaInstanciasCN[listaInstanciasCN.Count - 1][0] ||
                        informacionImportante[i][4] != listaInstanciasCN[listaInstanciasCN.Count - 1][1] ||
                        informacionImportante[i][7] != listaInstanciasCN[listaInstanciasCN.Count - 1][5]
                        )
                    { // primera vez que se encuentra
                        datosCN = new string[11];
                        datosCN[0] = informacionImportante[1][3];   //instancia
                        datosCN[1] = informacionImportante[1][4];   //Rfc_AppServerHost
                        datosCN[2] = informacionImportante[1][0];   //bukrs
                        datosCN[3] = informacionImportante[1][2];   //lifnr
                        datosCN[4] = informacionImportante[1][13].Trim() + "," + informacionImportante[1][14].Trim();  //usuario, pass
                        datosCN[5] = informacionImportante[1][15].Trim();   // r.RFC
                        datosCN[6] = informacionImportante[1][16].Trim();   // name
                        datosCN[7] = informacionImportante[1][5].Trim();    // Rfc_SAPRouter
                        datosCN[8] = informacionImportante[1][6].Trim();    // Rfc_SystemNumber
                        datosCN[9] = informacionImportante[1][7].Trim();    // Rfc_Client
                        datosCN[10] = informacionImportante[1][8].Trim();   // Rfc_Language
                        datosCN[11] = informacionImportante[1][9].Trim();   // Rfc_PoolSize
                        datosCN[12] = informacionImportante[1][10].Trim();  // Rfc_PeakConnectionsLimit
                        datosCN[13] = informacionImportante[1][11].Trim();  // Rfc_PoolIdleTimeout
                        listaInstanciasCN.Add(datosCN);
                    }
                    else // se concatenan los datos que nose repiten
                    {
                        datosCN[2] += "," + informacionImportante[i][0];   //bukrs
                        datosCN[3] += "," + informacionImportante[i][2];   //lifnr
                    }
                }
            }
            return listaInstanciasCN;
        }

        public static int buscarIndexUbicacionInstanciaCorrres(List<string[]> listaDiferentesInstancias, int numeroBuscar)
        {
            int buscado = 0;
            for (int i = 0; i < listaDiferentesInstancias.Count; i ++ )
            {
                if (int.Parse(listaDiferentesInstancias[i][0]) == numeroBuscar)
                {
                    buscado = i;
                    break;
                }
            }
            return buscado;
        }

        public static int buscarIndexUbicacionCNcorr(List<string[]> listaInstanciasCN, int numeroBuscar)
        {
            int buscado = 0;
            for (int i = 0; i < listaInstanciasCN.Count; i++)
            {
                if (int.Parse(listaInstanciasCN[i][0]) == numeroBuscar)
                {
                    buscado = i;
                    break;
                }
            }
            return buscado;
        }
    }
}