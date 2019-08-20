<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrProveedor.Master" AutoEventWireup="true" CodeBehind="pagos.aspx.cs" Inherits="Proveedores.pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/BusquedaTabla.js"></script>

    <script>
        $(function () {

            var activaFiltro = true;

            $("#pagos").addClass("active");
            $(".btn").button();

            $("#ContentPlaceHolder1_hidActualiza").val("");
            $(".ico-actualizar").click(function () {
                //alert("in")
                $("#ContentPlaceHolder1_hidActualiza").val("actualiza");
            });

            $("#ContentPlaceHolder1_hidHeader").val("");

            $("th").click(function () {
                $("#ContentPlaceHolder1_hidHeader").val($(this).html());
                $("#ContentPlaceHolder1_btnToOrder").click();
                
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
    <label class="h1">
        Pagos
    </label>
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
            <td>
                <asp:Label ID="lblExpandirTodo" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblContraerTodo" runat="server" Text="Label"></asp:Label>
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

    <%--<div style="width:100%;height:50px;">
        <a href='pagos.aspx'>
            <div class="ico-actualizar">

            </div>
        </a>
    </div>--%>
    <asp:Button ID="btnActualiza" runat="server" Text="" CssClass="ico-actualizar" />


    <asp:HiddenField ID="hidFiltro" runat="server" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />
    <asp:HiddenField ID="hidActualiza" runat="server" />

    <asp:Button ID="btnToOrder" runat="server" Text="" />
    <asp:HiddenField ID="hidHeader" runat="server" />
    <asp:HiddenField ID="modoOrdenar" runat="server" Value="asc" />

</asp:Content>
