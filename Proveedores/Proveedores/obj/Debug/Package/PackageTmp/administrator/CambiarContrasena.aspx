<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="CambiarContrasena.aspx.cs" Inherits="Proveedores.administrator.CambiarContrasena" %>
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
        .auto-style1 {
            height: 2.4em;
        }
    </style>
    <script>

        $(function () {
            $("#usuario").addClass("active");
            $(".busquedaProveedor").click(function () {
                document.location.href = "MostrarPertenencia.aspx?vinculador=CambiarContrasena&campo=Proveedor&primerproveedor=me";
                //alert(cambiarPassNext);
            });

            $(".validaMostrar").hide();
            //alert($.trim($("#ContentPlaceHolder1_lblProveedorSelected").html()) );
            if ($.trim($("#ContentPlaceHolder1_lblProveedorSelected").html()) != "Proveedor...") {
                $(".validaMostrar").show("slow");
            }

            $('#ltlbtnCancel').click(function () {
                var v = "CambiarContrasena.aspx";
                //alert(v);
                document.location.href = v;
            });

                 $('#ContentPlaceHolder1_btnGuardarCambios').click(function () {
                //alert("DDDDDD");
                takeIdSelectedsCheckBox('check');
            });
            mostrarDialog();
        });

    </script>

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>

    <div class="paraDiseno">
        <table class="tblFm2">
            <tr>
                <td><strong>Cambiar contraseña de usuario</strong></td>
            </tr>
        </table>
        <br />
        <br />

        <div class="validaMostrar">
            <table class="tblFm">
                <tr>
                    <td class="auto-style1">Usuario</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="txtbox focusTxt txtValidar"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>Password actual</td>
                    <td>
                        <asp:TextBox ID="passActual" runat="server" CssClass="focusTxt txtValidar" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Nuevo Password</td>
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
            </table>
            <table class="tblFm">
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar cambios" OnClick="btnGuardarCambios_Click" CssClass="btn" />
                        <asp:Literal ID="ltlbtnCancel" runat="server" Text="<div id='ltlbtnCancel' class='btn'>Cancelar</div>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="validaMostrar">
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
        <asp:HiddenField ID="hidPantalla" runat="server" Value="usuario" />
        <asp:HiddenField ID="hidComplementoUr" runat="server" />
        <asp:HiddenField ID="hidId" runat="server" />
        <asp:HiddenField ID="hidValCheck" runat="server" />
        <asp:HiddenField ID="hidNumeroLetras" runat="server" />
        <asp:HiddenField ID="hidNumeroLetrasM" runat="server" />
        <asp:HiddenField ID="hidCantidadNumeros" runat="server" />
        <asp:HiddenField ID="hidNumeroCaracteres" runat="server" />
        <asp:HiddenField ID="hidCerrarSesion" runat="server" />
    </div>
</asp:Content>

