<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="DesvincularGrupoProv.aspx.cs" Inherits="Proveedores.administrator.DesvincularGrupoProv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/Orden.css" rel="stylesheet" />

    <script src="../js/EliminarFila.js"></script>
    <script src="../js/BusquedaTabla.js"></script>

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

            $("#proveedor").addClass("active");
            mostrarDialog();

            
            $("table").tablesorter({ debug: true });
       
        });


    </script>

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>

    <div class="paraDiseno">
        <strong>Elija como desea desvincular</strong> 
        <br/><br/><br/>
        <asp:DropDownList ID="chkModoDesvincular" runat="server" AutoPostBack="True" OnTextChanged="MostrarInformacion">
        <asp:ListItem Selected="True">Por grupo</asp:ListItem>
        <asp:ListItem>Por proveedor</asp:ListItem>
        </asp:DropDownList>

        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn" />

     <table>
        <tr>
            <td><asp:Label ID="lblDescribeNombre" runat="server" Text=""></asp:Label></td>
            <td><asp:Label ID="lblNombreGrupo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDescribeTituloGrupo" runat="server" Text=""></asp:Label></td>
            <td><asp:Label ID="lblTItuloGrupoNoticia" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
        
    </div>

    <br/>

    <div id="tablaResultados">
    <asp:Label ID="lblTextoExplicacion" runat="server" Text=""></asp:Label>
    <br/>
    <asp:Label ID="lblTablaFiltro" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblMostrarTabla" runat="server" Text=""></asp:Label>

    <asp:HiddenField ID="hidVerificar" runat="server" />
    <asp:HiddenField ID="hidPantalla" runat="server" Value="DesvincularGrupoProv" />

    <asp:HiddenField ID="hidComplementoUr" runat="server" />

        <asp:HiddenField ID="hidCerrarSesion" runat="server" />
    </div>

</asp:Content>
