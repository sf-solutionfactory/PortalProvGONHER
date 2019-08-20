﻿<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="usuario.aspx.cs" Inherits="Proveedores.administrator.usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/Orden.css" rel="stylesheet" />

    <script src="../js/validarNotNull.js"></script>
    <script src="../js/validarEmail.js"></script>
    <script src="../js/BusquedaTabla.js"></script>
    <script src="../js/validarCalendario.js"></script>
    <script src="../js/EliminarFila.js"></script>
    <script src="../js/AddDeleteTable.js"></script>

    <script>
        $(function () {
            var activaFiltro = true;

            $("#searchTerm").keyup(function () { //$("#searchTerm").keyup(function () {
                if (activaFiltro) {
                    activaFiltro = false;
                    setTimeout(function () {
                        doSearch();
                        activaFiltro = true;
                    }, 1500);
                }
            });

            $("table").tablesorter({ debug: true });
        });
    </script>

    <link href="../css/passSeguro/stylePassSeguro.css" rel="stylesheet" />
    <script src="../css/passSeguro/ValidaPassSeguro.js"></script>

    <div id="pswd_info">
        <h4>La contraseña debería cumplir con los siguientes requerimientos:</h4>
        <ul>
            <li id="letter">Al menos debería tener <strong><%--<tr>
            <td>Tipo usuario</td>
            <td>
                <asp:DropDownList ID="cmbTipoUsuario" runat="server" class="focusTxt txtValidar"></asp:DropDownList></td>
        </tr>--%>letra/s</strong></li>
            <li id="capital">Al menos debería tener <strong><%-- <tr>
            <td>Creado por</td>
            <td>
                <asp:Label ID="lblCreadoPor" runat="server" Text=""></asp:Label></td>
        </tr>--%>letra/s en mayúsculas</strong></li>
            <li id="number">Al menos debería tener <strong><%--<asp:Button ID="btnCancel" runat="server" Text="Cancelar" />--%>número/s</strong></li>
            <li id="length">Debería tener <strong><%--<div id="btnCancelZ">cancelar</div>--%>caractere/s</strong> como mínimo</li>
        </ul>
    </div>

    <style>
        #ltlbtnCancel {
            width: 6em;
            max-width: 50%;
        }

        #ltlbtnCancel, #ContentPlaceHolder1_btnGuardarCambios {
            display: inline-block;
            vertical-align: top;
        }

        #ContentPlaceHolder1_cmbRol {
            height: 12px;
            min-height: 25px;
        }
        .auto-style1 {
            height: 2.4em;
        }
    </style>
    <script>

        $(function () {

            $("#usuario").addClass("active");
            $(".busquedaProveedor").click(function () {
                //$("#ContentPlaceHolder1_btnProvs").click();
                $("#ContentPlaceHolder1_hidEsAdmin").val("0");
                document.location.href = "MostrarPertenencia.aspx?vinculador=usuario&campo=Proveedor&primerproveedor=me";
            });

            $(".busquedaAdministrador").click(function () {
                $(".validaMostrar").show("slow");
                $("#ContentPlaceHolder1_hidEsAdmin").val("1");
                $("#ContentPlaceHolder1_btnAdmin").click();
            });

            $('#ContentPlaceHolder1_btnGuardarCambios').click(function () {
                if (validarCalendario("#ContentPlaceHolder1_datepicker")) {
                    if (validarCalendario("#ContentPlaceHolder1_datepicker2")) {

                    }
                }
            });

            $('#ContentPlaceHolder1_btnEnviar').click(function () {
                if ($('.ckbVigencia').is(':checked')) {
                    alert('Seleccionado');
                }
                if (validar()) {
                    //var resultEmail1 = validateEmail('ContentPlaceHolder1_txtIdemailRepetir');
                    //alert(resultEmail1);
                    if (validateEmail('ContentPlaceHolder1_txtIdemailRepetir')) {
                        if (validateEmail('ContentPlaceHolder1_txtIdemail')) {
                            //if (validar()) {

                            if (validarCalendario("#ContentPlaceHolder1_datepicker")) {
                                if (validarCalendario("#ContentPlaceHolder1_datepicker2")) {

                                }
                            }
                            //}
                        }
                    }
                }
            });

            $("#<%=ckbVigenciaIlimitada.ClientID%>").on('change', function () {
                if ($(this).is(':checked')) {
                    $("#rowInicioV").hide();
                    $("#rowFinV").hide();
                    $("#<%=datepicker.ClientID%>").val("01/01/2010");
                    $("#<%=datepicker2.ClientID%>").val("12/31/2099");
                }
                else {
                    $("#rowInicioV").show();
                    $("#rowFinV").show();
                    $("#<%=datepicker.ClientID%>").val("");
                    $("#<%=datepicker2.ClientID%>").val("");
                }
            });

            if ($("#<%=ckbVigenciaIlimitada.ClientID%>").attr('checked')) {
                $("#rowInicioV").hide();
                $("#rowFinV").hide();
            } else {
                $("#rowInicioV").show();
                $("#rowFinV").show();
            }

            $(".validaMostrar").hide();
            //alert($.trim($("#ContentPlaceHolder1_lblProveedorSelected").html()) );
            if ($.trim($("#ContentPlaceHolder1_lblProveedorSelected").html()) != "Proveedor...") {
                $(".validaMostrar").show("slow");
            }
            else {
                if ($("#ContentPlaceHolder1_hidEsAdmin").val() == "1") {
                    $(".validaMostrar").show("slow");
                }
            }
            $("#ContentPlaceHolder1_btnAdmin").hide();
            $("#ContentPlaceHolder1_btnProvs").hide();

            //alert($("#ContentPlaceHolder1_hidEsAdmin").val());
            
            if ($("#ContentPlaceHolder1_hidEsAdmin").val() == "1") {
                     $("table.btn-valida").hide();
                     $("tr.btn-valida").hide();
                }
            //alert($("#ContentPlaceHolder1_hidEsAdmin").val());

            //$(".focusTxt").focus(function () {
            //    var user1 = "";
            //    user1 = $("#ContentPlaceHolder1_lblProveedorSelected").html();
            //    if ($.trim(user1) == "Proveedor...") {
            //        $("#ContentPlaceHolder1_lblProveedorSelected").css("color", "#FF0000");
            //        $("#ContentPlaceHolder1_lblDialog").text("llenar proveedores");
            //        mostrarDialog();
            //        //alert("llenar proveedores");
            //        this.blur();
            //    }
            //});

            $('#ltlbtnCancel').click(function () {
                var v = "usuario.aspx?" + $('#ContentPlaceHolder1_hidComplementoUr').val();
                //alert(v);
                document.location.href = v;
            });

            $('#obtenerCheckbox').click(function () {
                //alert("DDDDDD");
                takeIdSelectedsCheckBox('check');
            });

            $('#ContentPlaceHolder1_btnEnviar').click(function () {
                //alert("DDDDDD");
                takeIdSelectedsCheckBox('check');
            });

            $('#ContentPlaceHolder1_btnGuardarCambios').click(function () {
                //alert("DDDDDD");
                takeIdSelectedsCheckBox('check');
            });

            mostrarDialog();

        });

    </script>
    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>
    <%--<div id="obtenerCheckbox">chechbox</div>--%><%--<div class ="resUsuariosTabla">--%><%--<div id="contenidoPost"></div>--%>

    <div class="paraDiseno">

        <table class="tblFm2">
            <tr>
                <td><strong>Llena todos los campos para dar de alta nuevos usuarios </strong></td>
            </tr>
        </table>
        <br />
        <br />

        <table class="tblFm">
            <tr class="usr-tr-prov">
                <td>Proveedor
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <strong class="busquedaProveedor link">
                                </strong>
                                <asp:Button ID="btnProvs" CssClass="silverColor" runat="server" Text="Proveedor.." OnClick="btnProvs_Click" />
                                <asp:Label ID="lblProveedorSelected" CssClass="silverColor" runat="server" Text="Proveedor..."></asp:Label></td>
                            <td><strong class="busquedaProveedor link">Busqueda...</strong></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="usr-tr-adm">
                <td>Administrador
                </td>
                <td>
                    <table>
                        <tr>
                            <td class="auto-style1">
                                <asp:Button ID="btnAdmin" CssClass="silverColor" runat="server" Text="Administrador..." OnClick="btnAdmin_Click" />
                                <asp:Label ID="lblAdminSelected" CssClass="silverColor" runat="server" Text="Administrador..."></asp:Label></td>
                            <td class="auto-style1"><strong class="busquedaAdministrador link">Busqueda...</strong></td>

                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div class="validaMostrar">

            <table class="tblFm">
                <%--</div>--%>
                <tr>
                    <td>Usuario</td>
                    <td>
                        <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="txtbox focusTxt txtValidar"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Nombre</td>
                    <td>
                        <asp:TextBox ID="txtIdNombre" runat="server" CssClass="focusTxt txtValidar"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Apellidos</td>
                    <td>
                        <asp:TextBox ID="txtIdApellidos" runat="server" CssClass="focusTxt txtValidar"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Password
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="txtbox focusTxt txtValidar" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Repetir password</td>
                    <td>
                        <asp:TextBox ID="txtPasswordRepetir" runat="server" CssClass="txtbox focusTxt txtValidar" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr class="btn-valida">
                    <td>Vigencia permanente</td>
                    <td>
                        <asp:CheckBox ID="ckbVigenciaIlimitada" runat="server" CssClass="ckbVigenciaI" />
                    </td>
                </tr>
                <tr class="btn-valida" id="rowInicioV">
                    <td>Inicio vigencia</td>
                    <td>
                        <asp:TextBox ID="datepicker" runat="server" class="txtValidar"></asp:TextBox>
                    </td>
                </tr>
                <tr class="btn-valida" id="rowFinV">
                    <td>Fin vigencia</td>
                    <td>
                        <asp:TextBox ID="datepicker2" runat="server" class="txtValidar"></asp:TextBox>
                    </td>
                </tr>
                <tr class="btn-valida">
                    <td>Cambiar password</td>
                    <td>
                        <asp:CheckBox ID="ckbCambiarPassNext" runat="server" />
                    </td>
                </tr>

                <%--<tr>
            <td>Clase usuario</td>
            <td>
                <asp:DropDownList ID="cmbClaseUsuario" runat="server" class="focusTxt txtValidar"></asp:DropDownList>
            </td>
        </tr>--%>
            </table>
            <table class="tblFm btn-valida">
                <%--<tr>
            <td>Tipo usuario</td>
            <td>
                <asp:DropDownList ID="cmbTipoUsuario" runat="server" class="focusTxt txtValidar"></asp:DropDownList></td>
        </tr>--%>

                <%-- <tr>
            <td>Creado por</td>
            <td>
                <asp:Label ID="lblCreadoPor" runat="server" Text=""></asp:Label></td>
        </tr>--%>
                <tr>
                    <td>Email
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdemail" runat="server" class="focusTxt txtValidar"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Repetir email</td>
                    <td>
                        <asp:TextBox ID="txtIdemailRepetir" runat="server" class="focusTxt txtValidar"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Rol a asignar</td>
                    <td>
                        <asp:DropDownList ID="cmbRol" runat="server" CssClass="txtValidar" AutoPostBack="false"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Sociedades</td>
                    <td>
                        <asp:Panel ID="pnlSociedades" runat="server"></asp:Panel>
                        <asp:Button ID="btnAdminSoc" runat="server" Text="Administrar sociedades ..." CssClass="btn" OnClick="btnAdminSoc_Click" />
                        <asp:Literal ID="ltlTablaSociedades" runat="server"></asp:Literal>
                    </td>
                </tr>
                </table>
                <table class="tblFm">
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnEnviar" runat="server" Text="Guardar" OnClick="btnEnviar_Click" CssClass="btn" />
                        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar cambios" OnClick="btnGuardarCambios_Click" CssClass="btn" />
                        <%--<asp:Button ID="btnCancel" runat="server" Text="Cancelar" />--%>
                        <asp:Literal ID="ltlbtnCancel" runat="server" Text="<div id='ltlbtnCancel' class='btn'>Cancelar</div>"></asp:Literal>
                        <%--<div id="btnCancelZ">cancelar</div>--%>
                    </td>
                </tr>
            </table>

        </div>
    </div>

    
    <%--<div id="obtenerCheckbox">chechbox</div>--%>

    <div class="validaMostrar">
        <%--<div class ="resUsuariosTabla">--%>
        <br />
        <%--<div id="contenidoPost"></div>--%>
        <br />
        <br />
        <asp:Label ID="lblExplicacionResultados" runat="server"></asp:Label>
        <label id="lblnormal" runat="server" ></label>
        <br />
        <br />
        <asp:Label ID="lblTablaFiltro" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblTablaUsuarios" runat="server" Text=""></asp:Label>
        <br />
        <asp:HiddenField ID="hidProveedor" runat="server" />
        <asp:HiddenField ID="hidVerificar" runat="server" />
        <asp:HiddenField ID="hidEsAdmin" runat="server" />
        <asp:HiddenField ID="hidVerificarPass" runat="server" />
        <asp:HiddenField ID="hidPantalla" runat="server" Value="usuario" />
        <asp:HiddenField ID="hidComplementoUr" runat="server" />
        <asp:HiddenField ID="hidId" runat="server" />
        <asp:HiddenField ID="hidValCheck" runat="server" />
        <asp:HiddenField ID="hidNumeroLetras" runat="server" />
        <asp:HiddenField ID="hidNumeroLetrasM" runat="server" />
        <asp:HiddenField ID="hidCantidadNumeros" runat="server" />
        <asp:HiddenField ID="hidNumeroCaracteres" runat="server" />
        <asp:HiddenField ID="hidCerrarSesion" runat="server" />
        <%--</div>--%>
    </div>

</asp:Content>
