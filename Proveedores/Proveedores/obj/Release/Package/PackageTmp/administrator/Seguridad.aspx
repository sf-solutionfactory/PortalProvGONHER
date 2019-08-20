<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="Seguridad.aspx.cs" Inherits="Proveedores.administrator.Seguridad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/Orden.css" rel="stylesheet" />
    <script src="../js/EliminarFila.js"></script>

    <script src="../js/BusquedaTabla.js"></script>
    <script src="../js/validarNotNull.js"></script>

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

             $('#ContentPlaceHolder1_btnGCredInac').click(function () {
                 validar();
             });

             mostrarDialog();

             
            $("table").tablesorter({ debug: true });
         

         });

    </script>

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>

    <div class="paraDiseno">

        
    <table class="tblFm tblFm3">
        <tr>
            <td>
                Credencial inaceptada
            </td>
            <td>
                <asp:TextBox ID="txtCredencial" runat="server" CssClass="txtValidar"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td>
                <asp:Button ID="btnGCredInac" runat="server" Text="Guardar" OnClick="btnGCredInac_Click" CssClass=" btn"/>
            </td>
        </tr>
    </table>

        </div>

    <br/><br/><br/>

     <div id="tablaResultados">

    <div>
         <asp:Label ID="lblTablaFiltro" runat="server" Text=""></asp:Label>
        <br />
        <asp:Literal ID="ltlTablaCredInaceptadas" runat="server" ></asp:Literal>
    </div>
         </div>


    <asp:HiddenField ID="hidVerificar" runat="server" />
    <asp:HiddenField ID="hidPantalla" runat="server" Value="Seguridad" />

         

    <asp:HiddenField ID="hidPsAnt" runat="server" />
        <asp:HiddenField ID="hidIdAnt" runat="server" />
        <asp:HiddenField ID="hidComplementoUr" runat="server" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />
</asp:Content>
