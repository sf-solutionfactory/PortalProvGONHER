<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrProveedor.Master" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="Proveedores.portal.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/Orden.css" rel="stylesheet" />

    <script src="../js/validarNotNull.js"></script>
    <script src="../js/validarEmail.js"></script>
    <script src="../js/BusquedaTabla.js"></script>
    <%--<script src="../js/validarCalendario.js"></script>--%>
    <script src="../js/EliminarFilaProv.js"></script>
    <script src="../js/AddDeleteTable.js"></script>

    <link href="../css/passSeguro/stylePassSeguro.css" rel="stylesheet" />
    <script src="../css/passSeguro/ValidaPassSeguro.js"></script>

    <div id="pswd_info">
        <h4>La contraseña debería cumplir con los siguientes requerimientos:</h4>
        <ul>
            <li id="letter">Al menos debería tener <strong><%=hidNumeroLetras.Value%> letra/s</strong></li>
            <li id="capital">Al menos debería tener <strong><%=hidNumeroLetrasM.Value%> letra/s en mayúsculas</strong></li>
            <li id="number">Al menos debería tener <strong><%=hidCantidadNumeros.Value%> número/s</strong></li>
            <li id="length">Debería tener <strong><%=hidNumeroCaracteres.Value%> caractere/s</strong> como mínimo</li>
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
    </style>
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

            $("#usuario").addClass("active");
            $(".busquedaProveedor").click(function () {
                document.location.href = "MostrarPertenencia.aspx?vinculador=usuario&campo=Proveedor&primerproveedor=me";
            });

            $('#ContentPlaceHolder1_btnEnviar').click(function () {
                if (validar()) {

                    if (validateEmail('ContentPlaceHolder1_txtIdemailRepetir')) {
                        if (validateEmail('ContentPlaceHolder1_txtIdemail')) {

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

            $('#ltlbtnCancel').click(function () {
                var v = "usuarios.aspx?" + $('#ContentPlaceHolder1_hidComplementoUr').val();
                document.location.href = v;
            });

            $('#obtenerCheckbox').click(function () {
                takeIdSelectedsCheckBox('check');
            });

            $('#ContentPlaceHolder1_btnEnviar').click(function () {
                takeIdSelectedsCheckBox('check');
            });

            $('#ContentPlaceHolder1_btnGuardarCambios').click(function () {
                takeIdSelectedsCheckBox('check');
            });

            $('#ContentPlaceHolder1_lblProveedorSelected').hide();

            mostrarDialog();

        });

    </script>

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>

    <asp:Label ID="lblProveedorSelected" CssClass="silverColor" runat="server" Text="Proveedor..."></asp:Label>

    <div class="paraDiseno">
        <strong>Llena todos los campos para dar de alta nuevos usuarios</strong>
        <br />
        <br />

        <table class="tblFm">

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
            <tr>
                <td>Vigencia permanente</td>
                <td>
                    <asp:CheckBox ID="ckbVigenciaIlimitada" runat="server" CssClass="ckbVigenciaI" />
                </td>
            </tr>
            <tr id="rowInicioV">
                <td>Inicio vigencia</td>
                <td>
                    <asp:TextBox ID="datepicker" runat="server" class="txtValidar"></asp:TextBox>
                </td>
            </tr>
            <tr id="rowFinV">
                <td>Fin vigencia</td>
                <td>
                    <asp:TextBox ID="datepicker2" runat="server" class="txtValidar"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Cambiar password</td>
                <td>
                    <asp:CheckBox ID="ckbCambiarPassNext" runat="server" />
                </td>
            </tr>

        </table>
        <table class="tblFm">

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
                    <asp:Literal ID="ltlTablaSociedades" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnEnviar" runat="server" Text="Guardar" OnClick="btnEnviar_Click" CssClass="btn" />
                    <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar cambios" OnClick="btnGuardarCambios_Click" CssClass="btn" />
                    <asp:Literal ID="ltlbtnCancel" runat="server" Text="<div id='ltlbtnCancel' class='btn'>Cancelar</div>"></asp:Literal>
                </td>
            </tr>
        </table>

    </div>

    <br />
    <br />
    <br />
    <asp:Label ID="lblExplicacionResultados" runat="server" Text=""></asp:Label>
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
    <asp:HiddenField ID="hidVerificarPass" runat="server" />
    <asp:HiddenField ID="hidPantalla" runat="server" Value="usuarioPortalP" />
    <asp:HiddenField ID="hidComplementoUr" runat="server" />
    <asp:HiddenField ID="hidId" runat="server" />
    <asp:HiddenField ID="hidValCheck" runat="server" />
    <asp:HiddenField ID="hidNumeroLetras" runat="server" />
    <asp:HiddenField ID="hidNumeroLetrasM" runat="server" />
    <asp:HiddenField ID="hidCantidadNumeros" runat="server" />
    <asp:HiddenField ID="hidNumeroCaracteres" runat="server" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />

</asp:Content>
