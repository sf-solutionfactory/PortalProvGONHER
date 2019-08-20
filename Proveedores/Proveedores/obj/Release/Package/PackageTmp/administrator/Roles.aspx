<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="Proveedores.administrator.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/Orden.css" rel="stylesheet" />

    <script src="../js/validarNotNull.js"></script>
    <script src="../js/BusquedaTabla.js"></script>
    <script src="../js/EliminarFila.js"></script>

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
            //$("#Roles").setMenuItemFocused();
            $("#Roles").addClass("active");
            $('#ContentPlaceHolder1_btnGuardarRol').click(function () {
                validar();
                //validarCheckBox();
            });

            $("table").tablesorter({ debug: true });


            mostrarDialog();

        });
    </script>

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>



    <div class="paraDiseno">

        <table class="tblFm2">
            <tr>
                <td><strong>Aquí puedes crear nuevos roles:</strong></td>
            </tr>
        </table>
        <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
            <br/><br/>
    <asp:Panel ID="Panel1" runat="server">
        <table class="tblFm">
            <tr>
                <td>Nombre del rol</td>
                <td>
                    <table>
                        <tr><td> <asp:TextBox ID="txtNombreRol" runat="server" class="txtValidar"></asp:TextBox></td><td><asp:RadioButtonList ID="rdbEsActivo" runat="server">
                            <asp:ListItem Selected="True">Activo</asp:ListItem>
                            <asp:ListItem>Inactivo</asp:ListItem>
                        </asp:RadioButtonList></td></tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                 Pantallas   
                </td>
                <td>
                    <table>
                    <tr><td><asp:CheckBox ID="chkFacturas" runat="server" Text="Facturas" CssClass="chk" name="chkVerificar"/></td></tr>
                    <tr><td><asp:CheckBox ID="chkPartidas" runat="server" Text="Facturas abiertas" CssClass="chk" name="chkVerificar"/></td></tr>
                    <tr><td><asp:CheckBox ID="chkPagos" runat="server" Text="Pagos" CssClass="chk" name="chkVerificar"/></td></tr>
                    <tr><td><asp:CheckBox ID="chkDatosMaestros" Text="Datos maestros" CssClass="chk" runat="server" name="chkVerificar" /></td></tr>
                    <tr><td><asp:CheckBox ID="chkUsuarios" Text="Usuarios" CssClass="chk" runat="server" name="chkVerificar" /></td></tr>
                    <tr><td><asp:CheckBox ID="chkRolDefault" Text="Rol Default" CssClass="chk" runat="server" name="chkRolDefault" /></td></tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td>
                    <table>
                        <tr>
                            <td><asp:Button ID="btnGuardarRol" runat="server" Text="Guardar" OnClick="btnGuardarRol_Click" CssClass="btn" /></td>
                        </tr>
                        <tr>
                            <td><asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar cambios" OnClick="btnGuardarCambios_Click" CssClass="btn" /></td>
                            <td><asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancelar" OnClick="btnCancel_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>

    </asp:Panel>
        <br/>

        <table class="tblFm2">
            <tr>
                <td><strong>Estos son los roles existentes: </strong></td>
            </tr>
        </table>
        <table class="tblFm">
            <tr>
                <td>Filtrar...</td>
                <td><input id="searchTerm" type="text"/></td>
            </tr>
        </table>
   
    <asp:Label ID="lblTablaRoles" runat="server" Text="lblTablaRoles"></asp:Label>

    <br/><br/>
         

    
</div>
    <br/>
    <asp:HiddenField ID="hidVerificar" runat="server" />
    <asp:HiddenField ID="hidPantalla" runat="server" Value="Roles" />

    <asp:HiddenField ID="hidIdAnt" runat="server" />
    <asp:HiddenField ID="hidComplementoUr" runat="server" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />

</asp:Content>
