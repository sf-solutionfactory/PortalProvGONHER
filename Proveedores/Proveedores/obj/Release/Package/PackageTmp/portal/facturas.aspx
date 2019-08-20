<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrProveedor.Master" AutoEventWireup="true" CodeBehind="facturas.aspx.cs" Inherits="Proveedores.portal.Facturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/BusquedaTabla.js"></script>
    <script>
        $(function () {

            var mostrarFlechas;
            var numeros = [];
            var entrada = [];
            var activaFiltro = true;      
            
            $("#facturas").addClass("active");
            $("#btnCrgMasiva").hide();

            workDetalles("ocultar");

            $(".ico-actualizar").click(function () {
                $("#ContentPlaceHolder1_hidActualiza").val("actualiza");
                //document.location.href = "facturas.aspx";
            });

            $("tr td.icono.nonecolor").click(function () {
                var padre = $(this).parent("tr");
                var oID = $(padre).attr("id");
                doSearchGroup(oID, numeros);
            });

            $(".ico-expandir_Todo").click(function () {
                workDetalles("mostrar");
            });
            $(".ico-contraer_Todo").click(function () {
                workDetalles("ocultar");
            });

            //$(".hrfCargadorXml").mouseover(
            $(document).ready(
                function () {
                    var padre = $(this).parent("a");
                    padre = $(padre).parent("td");
                    padre = $(padre).parent("tr");
                    var oID = $(padre).attr("id");

                    //var celdaVerde = $("." + oID).eq(0).find("");
                    ////fondoGris
                    //$("." + oID).eq(0).addClass("fl_verde");
                    $(".fv").addClass("fl_verde");
                    //$(".fa").addClass("fl_azul");
                });

            //$(".hrfCargadorXml").mouseout(function () {
                //if (numeros.length <= 0) {
            //        $(".fv").removeClass("fl_verde");
            //        $(".fa").removeClass("fl_azul");
            //    }
            //});

            $("#searchTerm").keyup(function () { //$("#searchTerm").keyup(function () {
                if (activaFiltro) {
                    activaFiltro = false;
                    setTimeout(function () {
                        workDetalles("mostrar");
                        doSearch();
                        activaFiltro = true;
                    }, 1500);                  
                }
            });
            var dialog;
            $(".desadjuntarXML").click(function () {//evento de desajuntar archivois
                var mensaje = $(this).attr('msm');
                
                mensaje = $.parseHTML(mensaje);
                if (mensaje != "" && mensaje != null) {
                    $("#ContentPlaceHolder1_lblDialog").text("");
                    $("#ContentPlaceHolder1_lblDialog").append(mensaje);
                    if ($.trim($("#ContentPlaceHolder1_lblDialog").text()) != "") {
                        dialog = $("#ContentPlaceHolder1_lblDialog").dialog({
                            autoOpen: false,
                            height: 350,
                            width: 400,
                            modal: true,
                            buttons: {
                                "Desadjuntar": function () {
                                    obtenerUUID()
                                    cerrardialog()
                                    
                                },
                                Cancel: function () {
                                    dialog.dialog("close");
                                }
                            },
                            close: function () {
                               
                            }
                        });
                        dialog.dialog("open");
                        //return true;
                    }

                }
            });
            function cerrardialog() {                
                dialog.dialog("close");
            }
            //////////////-------->

            $(".hrfCargadorXml").click(function () {
                if ($(this).hasClass('hrfCargadorXmlClicked')) {
                    var padre = $(this).parent("td");
                    padre = $(padre).parent("tr");
                    var oID = $(padre).attr("id");
                    var pos1 = -1
                    pos1 = jQuery.inArray(oID, entrada)
                    if (pos1 != -1) {
                        entrada.splice($.inArray(oID, entrada), 1);
                    }

                    $(this).removeClass("hrfCargadorXmlClicked");
                    var num = $(this).attr("indx");
                    var pos = -1;
                    pos = jQuery.inArray(num, numeros);
                    if (pos != -1) {
                        numeros.splice($.inArray(num, numeros), 1);
                    }
                }
                else {
                    var padre = $(this).parent("td");
                    padre = $(padre).parent("tr");
                    var oID = $(padre).attr("id");
                    var pos1 = 0
                    if (entrada.length >0) {
                        pos1 = jQuery.inArray(oID, entrada)
                    }
                    if (pos1 > -1) {
                        $(this).addClass("hrfCargadorXmlClicked");
                        var xc = $(this).attr("indx");
                        numeros[numeros.length] = $(this).attr("indx");
                        entrada[entrada.length] = oID;
                    }
                    else {
                        //alert("No se permite seleccionar diferentes partidas de diferentes entradas.");
                        $('#<%=this.lblDialog.ClientID%>').val("No se permite seleccionar diferentes partidas de diferentes entradas.");
                        mostrarDialog();
                    }
                   
                    //var xl = $(this).attr("href");
                    //numeros[numeros.length] = $(this).attr("indx");
                    
                }
                if (numeros.length > 0) {
                    $(".fa").addClass("fl_azul");
                } else {
                    $(".fa").removeClass("fl_azul");
                }
            });

            /////////////----------<
            /////////////---------->

            $(".fv").click(function () {
                //var padre = $('div').parent(".iconodes");
                //var hijo = padre.children(".desadjuntarXML");
                //if (hijo.hasClass("desadjuntarXML") === false) {//para indicar si contiene archivos ya adjuntos si tiene no dejara adjuntar mas

                    if ($(this).hasClass('fl_verde')) {
                    var cadena = "";
                    var padre = $(this).parent("td");
                    padre = $(padre).parent("tr");
                    var oID = $(padre).attr("id");
                    var pos1 = 0;
                    if (numeros.length == 0) {
                        //var padre = $(this).parent("div");
                        for (var i = 1; i < $("." + oID).size() ; i++) {
                            var hijo = $("." + oID).eq(i).children(".icono");                            
                            hijo = $(hijo).children("div");
                            var xc = $(hijo).attr("indx");
                            numeros[numeros.length] = xc;
                        }
                    }
                    if (entrada.length > 0) {
                        pos1 = jQuery.inArray(oID, entrada)
                    }
                    if (pos1>-1) {
                        for (var i = 0; i < numeros.length; i++) {
                            cadena = cadena + numeros[i];
                            if (numeros.length - 1 != i) {
                                cadena = cadena + ',';
                            }
                        }
                        sessionStorage.setItem("indexs", cadena);
                        window.location = "facturasDet.aspx";
                    }
                    else {
                        //alert("No tiene permitido enviar entrada con partidas no correspondiente");
                        $('#<%=this.lblDialog.ClientID%>').text("No tiene permitido enviar entrada con partidas no correspondiente");
                        mostrarDialog();
                    }
                }
                <%--}
                else {
                    $('#<%=this.lblDialog.ClientID%>').text("El pedido ya contiene un archivos adjuntados.");
                        mostrarDialog();
                }--%>
                
            });
            $(".fa").click(function () {
                if ($(this).hasClass('fl_azul')) {
                    numeros = [];
                    entrada = [];
                    //$(".fv").removeClass("fl_verde");
                    $(".fa").removeClass("fl_azul");
                    $(".hrfCargadorXml").removeClass("hrfCargadorXmlClicked");
                }
                
            });

            $("#btnActualizaX").click(function () {
                $("#imgLoaging").show();
            });

            

            $("#imgLoaging").hide();
            /////////////----------<
        
        });

    </script>

    <style>
        .hrfCargadorXml:hover {
            background: url('../css/images/cargar_xml_2.png') repeat scroll 0% 0% transparent;
            /*background: url('../css/images/fl_der.png') repeat scroll 0% 0% transparent;*/
        }

        img.tipo {
            width:20px;
        }

        .vistaPor {
            width:150px;
        }

        .img-der{ 
            position:absolute;
            z-index:1;
            background-color:#FFFFFF;
            top:100px;
            left:300px;
            width:300px;
            height:12px;
        }

        /*h4 { 
            font-family: fantasy; 
            font-size: 4em;
        }*/

    </style>

    <label class="h1">
        Facturas
    </label>
    <br/><br/>
     <a id="btnCrgMasiva" style="float:right;text-decoration:none;color:#4D4D4D;text-align:center;" class="btn" href="facturasMasiva.aspx">
        Carga masiva
    </a>
    <br/>

    <%--<label class="lblShow">
        mostrar detalles
    </label>--%>

    <%--<div><img src="../css/images/fl-der.png" class="img-der"/></div> --%>

    <table class="filtro">
            <table>
                <theader>
                    <th class="vistaPor">
                        Referencia: 
                    </th>
                    <th>
                        Moneda:
                    </th>
                    <%-- <th>
                        Fecha factura: 
                    </th>--%>
                    <th>
                        Fecha: 
                    </th>
                </theader>
                <tr>
                    <%--<td>
                        <asp:RadioButtonList ID="rdbMostrarComo" runat="server">
                             <asp:ListItem Selected="True">Orden</asp:ListItem>
                             <asp:ListItem>Referencia</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>--%>
                    <td class="referecia">
                        <asp:TextBox ID="txtRef1" runat="server"></asp:TextBox><br/><br/>
                        <asp:TextBox ID="txtRef2" runat="server"></asp:TextBox>
                    </td>
                    <td class="moneda">
                        <asp:TextBox ID="txtMoneda1" runat="server"></asp:TextBox><br/><br/>
                        <asp:TextBox ID="txtMoneda2" runat="server"></asp:TextBox>
                    </td>
                    <%-- <td class="fechafact">
                        <asp:TextBox ID="txtffact1" CssClass="datepicker" runat="server"></asp:TextBox><br/><br/>
                        <asp:TextBox ID="txtffact2" CssClass="datepicker2" runat="server"></asp:TextBox>
                    </td>--%>
                    <td class="fechacompra">
                        <asp:TextBox ID="datepicker" runat="server"></asp:TextBox><br/><br/>
                        <asp:TextBox ID="datepicker2" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnActualizaX" CssClass="ico-actualizar" runat="server" Text="" />
                    </td>
                    <td>
                        <table class="tblComp">
                            <tr>
                                <td>
                                    <div class="ico-expandir_Todo" title="Expandir todo"></div>
                                </td>
                                <td>
                                    <div class="ico-contraer_Todo" title="Contraer todo"></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>
        
        <%--</tr>--%>
        <tr>
            <td><br/>Filtrar...</td>
            <%--<td><input id="searchTerm" type="text" onkeyup="doSearch()" /></td>--%>
            <td><input id="searchTerm" type="text"/></td>
        </tr>
    </table>

        <br/>
        <img id="imgLoaging" src="../images/loadingDots.gif" />
        <br/>

    <asp:Label ID="lblTabla" runat="server"></asp:Label>
        <asp:Button ID="btnActualiza" CssClass="ico-actualizar" runat="server" Text="" />
    <br/>
    <br/>
    <br/>

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />
    <asp:HiddenField ID="hidActualiza" runat="server" />

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>
    <asp:Label ID="lblDialog2" runat="server" title="Informe" Text=""></asp:Label>

</asp:Content>
