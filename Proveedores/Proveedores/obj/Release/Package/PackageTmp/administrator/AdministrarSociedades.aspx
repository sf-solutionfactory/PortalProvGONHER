<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="AdministrarSociedades.aspx.cs" Inherits="Proveedores.administrator.AdministrarSociedades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/Orden.css" rel="stylesheet" />
    <script src="../js/EliminarFila.js"></script>

    <script>
        $(function () {
            $("table").tablesorter({ debug: true });
        });
    </script> 


    <div id="tablaResultados">

        <asp:Literal ID="ltlTablaSociedades" runat="server"></asp:Literal>

        <br/>

        <asp:Button ID="btnTerminar" runat="server" Text="Terminar" OnClick="btnTerminar_Click" CssClass="btn"/>

    </div>

        <asp:HiddenField ID="hidVerificar" runat="server" />
        <asp:HiddenField ID="hidPantalla" runat="server" Value="AdministrarSoc" />
        <asp:HiddenField ID="hidComplementoUr" runat="server" />
        <asp:HiddenField ID="hidId" runat="server" />
        <asp:HiddenField ID="hidValCheck" runat="server" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />

</asp:Content>
