<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrProveedor.Master" AutoEventWireup="true" CodeBehind="facturasDet.aspx.cs" Inherits="Proveedores.portal.facturas3" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/BusquedaTabla.js"></script>
    <script>
        $(function () {

        var posicionMostrar = 0;
        var tablas = 0;
        $("#facturas").addClass("active");
        $("#ContentPlaceHolder1_btnLLenarTbs").hide();
            
        $(".btnCargar").click(function () {
            animarLogo();
        });

        $("#ContentPlaceHolder1_File1").click(function () {
            $("#ContentPlaceHolder1_lblConsola").text("");
        });

        $("#ContentPlaceHolder1_btnLLenarTbs").hide();
        function animarLogo() {
            $("#imgNoAnimacion").hide();
            $("#imgAnimacion").show();
        }

        ocultaTablas("tblCV", posicionMostrar);
        ocultaTablas("consola", posicionMostrar);
        if ($("#ContentPlaceHolder1_hidIndexs").val() == "") {
            if (sessionStorage.getItem("contadorCarga") != "1") {
                sessionStorage.setItem("contadorCarga", "1");
                $("#ContentPlaceHolder1_hidIndexs").val(sessionStorage.getItem("indexs"));
                $("#ContentPlaceHolder1_btnLLenarTbs").click();
            }
        }
        else {
            sessionStorage.setItem("contadorCarga", "0");
            tablas = $(".tblCV");
        }
        
        $(".imgBack").click(function () {
            if (posicionMostrar > 0) {
                if (posicionMostrar - 1 >= 0) {
                    posicionMostrar = posicionMostrar - 1;
                    ocultaTablas("tblCV", posicionMostrar);
                    ocultaTablas("consola", posicionMostrar);
                } 
            }
        });

        $(".imgNext").click(function () {
            if (posicionMostrar < tablas.length) {
                if (posicionMostrar + 1 < tablas.length) {
                    posicionMostrar = posicionMostrar + 1;
                    ocultaTablas("tblCV", posicionMostrar);
                    ocultaTablas("consola", posicionMostrar);
                }
            }
        });

 
        });
        function mostrarEstatus(idEstatus) {//  1 = Estructura correcta     2 = Estrcuctura incorrecta      3 = SAT:Cancelado   4 = SAT:Vigente     ....
            switch (idEstatus) {
                case '0':
                    break;
                case '1':
                    $("#valEstructura").html("<img src='../css/images/estatus-correcto.png' />");
                    break;
                case '2':
                    $("#valEstructura").html("<img src='../css/images/estatus-estructura_invalida.png' />");
                    break;
                default:
                    break;
            }
            
        }
        $(document).ready(function () {
            if ($(".consola").val() != 'Cargada correctamente') {
                $(".consola").css({ 'font-size': '1.1em' });
            }
            else {
                $(".consola").css({ 'font-size': '4em' });
            }
        });
    </script>

    <style>
        .imgNext:hover, .imgBack:hover, ico-cerrar:hover {
            -webkit-transform: scale(1.2);
            transform: scale(1.2);
        }

        .consola {
            font-family: serif;
            line-height: 0.8;
            /*font-size: auto;*/
        }
        .error {
            font-family: serif;
            line-height: 0.8;
            font-size: 2.1em;
            /*font-size: auto;*/
        }
    </style>
    <br />

    <center>
        <div id="pop-up">
                <div id="msgWrap" style="width: 840px;">
                    <div style="float: left;width: 340px;/*height: 300px;*/padding-right:10px;margin-bottom: 10px;  /*background:#E0E0E0;*/">

                        <asp:Literal ID="ltlTablas" runat="server"></asp:Literal>

                        <div id="divCargadorArchivo">
                            <strong>Archivo XML</strong><input type="file" class="input-file cargador" id="File1" name="File1" runat="server"/><br />
                            <strong>Archivo PDF</strong><input type="file" class="input-file cargador" id="File2" name="File1" runat="server"/>
                            <asp:Button ID="cargararchivo" CssClass="btnCargar btn_ cargador" runat="server" Text="Cargar" OnClick="Button1_Click" Width="15%" />
                        </div>
                    </div>
                
                    <div id="divValidaciones" style="float: left;width: 150px;height:300px;/*background:#F0F0F0;*//*margin-bottom: 10px;*/">
                        <div id="divLogoSF">
                            <img id="imgNoAnimacion"src="../images/ico-sf-1-step.gif">
                            <img id="imgAnimacion" src="../images/ico-sf-2-steps.gif" style="display:none;">
                        </div>
                        <div>
                            <table style="width: 100%;">
                                <tr>
                                    <td><img src="../images/fichero.png"></td>
                                    <td><img src="../images/sat.png"></td>
                                    <td><img src="../images/sap.png"><br /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                
                    <div id="divConsola" style="width: 340px;height: 277px;border: 3px solid #0B2161; background:#CCCCCC;margin-left:510px;padding:10px;color:#333333;">
                        
                        <%--<asp:Label ID="lblConsola" runat="server" Text="" CssClass="consola"></asp:Label>--%>
                        <asp:Label ID="lblConsola" runat="server" Text="" CssClass="error"></asp:Label>
                        <%--<label class="consola">ASK</label>--%> 
                        <br />
                        <asp:Label ID="lblIdEstatus" runat="server" CssClass="lblIdEstatus" Visible="False"></asp:Label>

                    </div>
                </div>
                <a href="facturas.aspx"><div class="ico-cerrar">VOLVER</div></a>
            </div>
        </center>

    <asp:HiddenField ID="hidIndexs" runat="server" />
    <asp:Button ID="btnLLenarTbs" runat="server" Text="LLenar" OnClick="btnLLenarTbs_Click" />
</asp:Content>
