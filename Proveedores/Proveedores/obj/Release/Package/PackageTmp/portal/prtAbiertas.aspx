<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrProveedor.Master" AutoEventWireup="true" CodeBehind="prtAbiertas.aspx.cs" Inherits="Proveedores.portal.prtAbiertas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../js/BusquedaTabla.js"></script>

    

    <script>
        $(function () {

            var activaFiltro = true;
            
            $("#prtAbiertas").addClass("active");

            $(".ico-actualizar").click(function () {
                //alert("in")
                $("#ContentPlaceHolder1_hidActualiza").val("actualiza");
                //document.location.href = "facturas.aspx";
            });

            $("#searchTerm").keyup(function () { //$("#searchTerm").keyup(function () {
                if (activaFiltro) {
                    activaFiltro = false;
                    setTimeout(function () {
                        doSearch();
                        activaFiltro = true;
                    }, 1500);
                }
            });

        });
    </script>
    <label class="h1">Partidas abiertas</label>

     <table class="tblComp">
        <tr>
            <td>
                Fecha inicio:<asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
            </td>
            <td>
                Fecha Fin:<asp:TextBox ID="datepicker2" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnActualizaX" runat="server" Text="" CssClass="ico-actualizar" OnClick="Button1_Click" />
                
            </td>
        </tr>
    </table>

    <table class="filtro">
        <tr>
            <td>Filtrar...</td>
            <td><input id="searchTerm" type="text"/></td>
        </tr>
    </table>

    <asp:Label ID="lblTabla" runat="server"></asp:Label>

    <asp:Button ID="btnActualiza" runat="server" Text="" CssClass=".ico-actualizar" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />
    <asp:HiddenField ID="hidActualiza" runat="server" />
</asp:Content>
