<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrProveedor.Master" AutoEventWireup="true" CodeBehind="factFV60.aspx.cs" Inherits="Proveedores.portal.FactFV60" %>
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

            $("#factFV60").addClass("active");
            $("#btnCrgMasiva").hide();
            workDetalles("ocultar");

            $(".ico-actualizar").click(function () {
                $("#ContentPlaceHolder1_hidActualiza").val("actualiza");
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

            $(document).ready(
                function () {
                    var padre = $(this).parent("a");
                    padre = $(padre).parent("td");
                    padre = $(padre).parent("tr");
                    var oID = $(padre).attr("id");
                    $(".fv").addClass("fl_verde");
                });

            $("#searchTerm").keyup(function () { 
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
            $("table tbody tr td div.desadjuFV60XML").click(function () {      //evento de desajuntar archivos
                var mensaje = $(this).attr('msm');

                mensaje = $.parseHTML(mensaje);
                var Xxvalor = $(this).attr('value');   //mgv: manda los datos que SAP necesita para desadjuntar los archivos
                //alert("---posicion--- " + Xxvalor);
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
                                    obtenerPARAMS(Xxvalor)
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
                    }
                }
            });

            function cerrardialog() {                
                dialog.dialog("close");
            }

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
                }
                if (numeros.length > 0) {
                    $(".fa").addClass("fl_azul");
                } else {
                    $(".fa").removeClass("fl_azul");
                }
            });

            $(".fv").click(function () {
                    if ($(this).hasClass('fl_verde')) {
                    var cadena = "";
                    var padre = $(this).parent("td");
                    padre = $(padre).parent("tr");
                    var oID = $(padre).attr("id");
                        var pos1 = 0;
                    if (numeros.length == 0) {
                        for (var i = 0; i < $("." + oID).size() ; i++) {
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
                        window.location = "fv60Det.aspx";
                    }
                    else {
                        $('#<%=this.lblDialog.ClientID%>').text("No tiene permitido enviar entrada con partidas no correspondiente");
                        mostrarDialog();
                    }
                }
            });
            $(".fa").click(function () {
                if ($(this).hasClass('fl_azul')) {
                    numeros = [];
                    entrada = [];
                    $(".fa").removeClass("fl_azul");
                    $(".hrfCargadorXml").removeClass("hrfCargadorXmlClicked");
                }
            });

            $("#btnActualizaX").click(function () {
                $("#imgLoaging").show();
            });

            $("#imgLoaging").hide();
        });

    </script>

    <style>
        .hrfCargadorXml:hover {
            background: url('../css/images/cargar_xml_2.png') repeat scroll 0% 0% transparent;
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

    </style>

    <label class="h1">
        FV60
    </label>
    <br/><br/>
     <a id="btnCrgMasiva" style="float:right;text-decoration:none;color:#4D4D4D;text-align:center;" class="btn" href="facturasMasiva.aspx">
        Carga masiva
    </a>
    <br/>

     <table class="filtro">
            <table>
                <theader>
                    <th class="vistaPor">
                        Referencia: 
                    </th>
                    <th>
<%--                        Moneda:--%>
                    </th>
                    <th>
                        Fecha inicial: 
                    </th>
                </theader>
                <tr>
                    <td class="referecia">
                        <asp:TextBox ID="txtRef2" runat="server"></asp:TextBox><br/><br/>
                    </td>
                    <td class="moneda">
                        <asp:TextBox ID="txtMoneda2" runat="server" Visible="False"></asp:TextBox><br/><br/>
                        <asp:TextBox ID="txtMoneda1" runat="server" Visible="False"></asp:TextBox>
                    </td>
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
        
        <tr>
            <td><br/>Filtrar...</td>
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
