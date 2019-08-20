<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="MostrarPantalla.aspx.cs" Inherits="Proveedores.administrator.MostrarPantalla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/EliminarFila.js"></script>

    <script>
        $(function () {
           
            $(".tblComun>tbody>tr").click(function () {

                var id_pantalla = $(this).find("td:nth-child(1)").html();
                var nombre = $(this).find("td:nth-child(2)").html();
                var descripcion = $(this).find("td:nth-child(3)").html();
                var vinculador = $.trim($("#ContentPlaceHolder1_hidVinculador").val());
                var hidInstancia = $.trim($("#ContentPlaceHolder1_HidIsntanciSap").val());
                var hidEndPoint = $.trim($("#ContentPlaceHolder1_HidEndpoint").val());

                switch (vinculador) {
                    case "WebServices":
                        document.location.href = "WebServices.aspx?n_pantalla=" + id_pantalla + "&nombre=" + nombre + "&desc=" + descripcion + "&hidInstancia=" + hidInstancia + "&hidEndPoint=" + hidEndPoint;
                        break;
                    default:
                        document.location.href = "javascript: history.back(1);";
                        break;
                }

            });

           
            $("table").tablesorter({ debug: true });
         
        });

          </script>

    <strong>Para su elección clic sobre el registro </strong> 

    <asp:Label ID="lblResultado" runat="server" Text="Resultado..."></asp:Label>

    <br/>
    <br/>
    <div id="contenidoPost"></div>
    <br/>
    <br/>
    <asp:HiddenField ID="hidVinculador" runat="server" />
    <asp:HiddenField ID="HidIsntanciSap" runat="server" />
    <asp:HiddenField ID="HidEndpoint" runat="server" />

    <br/>
    <asp:Label ID="lblTEst" runat="server" Text="TEst..."></asp:Label>
    <asp:HiddenField ID="hidPantalla" runat="server" Value="MostrarPantalla" />
    <asp:HiddenField ID="hidComplementoUr" runat="server" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />

</asp:Content>
