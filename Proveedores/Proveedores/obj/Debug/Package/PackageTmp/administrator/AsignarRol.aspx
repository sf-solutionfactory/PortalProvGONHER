<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="AsignarRol.aspx.cs" Inherits="Proveedores.administrator.AsignarRol" %>
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

               $("#asignarRol").addClass("active");
                $("#ContentPlaceHolder1_btnAsignarRol").click(function(){
                    validar();
                    $.trim($("#ContentPlaceHolder1_hidUser").val($("#ContentPlaceHolder1_cmbUsuarios").val()));
                    $.trim($("#ContentPlaceHolder1_hidRol").val($("#ContentPlaceHolder1_cmbRol").val()));
                });

                $(".buscar").click(function () {
                    document.location.href = "MostrarPertenencia.aspx?vinculador=AsignarRol&campo=N";
                });
            });

        </script>

    <style>
        .separar {
            width:12em;
        }
        
    </style>

    <div class="paraDiseno">

    <strong>Aquí puedes asigar roles a los usuarios, primeramente seleecione un proveedor para mostrar sus usuarios</strong>

    <br/>
    <br/>
    <table>
        <tr>
            <td class="separar">
                <strong>Proveedor</strong>
            </td>
            <td class="separar">
                <asp:Label ID="lblProveedorSelected" class="silverColor" runat="server" Text="Proveedor..."></asp:Label>
            </td>
            <td>
                <%--<a href='MostrarPertenencia.aspx?vinculador=AsignarRol&campo=N' class="buscar link">busqueda...</a>--%>
                <strong class="buscar link">Busqueda...</strong>
                
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td>
                <%--<asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" />--%>
            </td>
        </tr>
    </table>

        <div class="asignacionUsuario">

    <table>
        <tr>
            <td>
                Usuario
            </td>
            <td>
                Rol
            </td>
        </tr>
        <tr>
            
            <td>
               
                <asp:DropDownList ID="cmbUsuarios" runat="server" OnTextChanged="tomarRol" AutoPostBack="false" class="txtValidar"></asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="cmbRol" runat="server"  class="txtValidar"></asp:DropDownList>
            </td>
            <%--<td>
                <asp:Label ID="" runat="server" Text="Label"></asp:Label>
            </td>--%>
            <td>
            </td>
        </tr>
        <tr><td><asp:Button ID="btnAsignarRol" runat="server" Text="Asignar" OnClick="btnAsignarRol_Click" CssClass="btn" />
</td></tr>
       
    </table>

            <div id="sociedades">
                <table>
                    <tr></tr>

                </table>
                <asp:CheckBoxList ID="chbListaSciedades" runat="server"></asp:CheckBoxList>

            </div>

    </div>


    <br/>
    
    <br/>
    <asp:Label ID="lblUsuario" runat="server" Text=""></asp:Label>

    <br/>
    <div id="contenidoPost"></div>
    <br/>

    </div>


    <asp:Label ID="lblLeyenda" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblTablaFiltro" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblTablaGrupoRoles" runat="server" Text=""></asp:Label>

    <asp:HiddenField ID="hidPantalla" runat="server" Value="AsignarRol" />
    <asp:HiddenField ID="hidComplementoUr" runat="server" Value="" />
    
    <asp:HiddenField ID="hidProv" runat="server" />
    <asp:HiddenField ID="hidUser" runat="server" />
    <asp:HiddenField ID="hidRol" runat="server" />
    <asp:HiddenField ID="hidVerificar" runat="server" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />
</asp:Content>
