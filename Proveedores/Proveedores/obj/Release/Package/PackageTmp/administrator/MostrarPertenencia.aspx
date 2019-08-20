<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="MostrarPertenencia.aspx.cs" Inherits="Proveedores.administrator.MostrarPertenencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/Orden.css" rel="stylesheet" />
    <%--<link href="../css/EstiloSortTables.css" rel="stylesheet" />--%>

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

            switch ($("#ContentPlaceHolder1_hidVinculador").val()) {
                case "usuario":
                    $("#usuario").addClass("active");
                    break;
                case "AsignarRol":
                    $("#asignarRol").addClass("active");
                    break;
                case "Proveedores":
                    $("#proveedor").addClass("active");
                    break;
                default:
                    break;
            }

            $("#btnBack").click(function () {
                try {
                    var vinculador = $("#ContentPlaceHolder1_hidVinculador").val();
                } catch (e) {
                    vinculador = "Proveedores";
                }
                document.location.href = vinculador + ".aspx";
            });

            $(".tblComun>tbody>tr").click(function () {

                var proveedor = $(this).find("td:nth-child(1)").html();
                var nombre = $(this).find("td:nth-child(2)").html();
                var descripcion = $(this).find("td:nth-child(3)").html();
                var vinculador = $("#ContentPlaceHolder1_hidVinculador").val();// se recibe al llamar esta ASP
                var campo = $("#ContentPlaceHolder1_hidCampo").val(); // se recibe de 'usuario' al llamar esta ASP
                var primerProveedor = $("#ContentPlaceHolder1_hidPrimerProveedor").val();
                switch (vinculador) {
                    case "AsignarRol":
                        document.location.href = "AsignarRol.aspx?rfc=" + $.trim(proveedor) + "&nom=" + $.trim(nombre) + "&desc=" + $.trim(descripcion);
                        break;
                    case "usuario":
                        document.location.href = "usuario.aspx?rfc=" + $.trim(proveedor) + "&nom=" + $.trim(nombre) + "&desc=" + $.trim(descripcion) + "&campo=" + campo + "&primerproveedor=" + $.trim(primerProveedor);
                        break;
                    case "Proveedores":
                        break;
                    default:
                        document.location.href = "Proveedores.aspx";
                        break;
                }
            });
            mostrarDialog();


            $("table").tablesorter({ debug: true });

        });

    </script>

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>

    <img id="btnBack" src="../images/ico-Volver.png" />
    <div class="paraDiseno">

        <asp:Label ID="lblInfoProveedores" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblEleccion" runat="server" Text=""></asp:Label><br />
        <asp:Button ID="btnDescargarE" runat="server" Text="Descargar en formato Excel" OnClick="btnDescargarE_Click" Visible="False" CssClass="btn" />

        <br />
        <div id="contenidoPost"></div>
        <br />

        <table>
            <tr>
                <td><input type="submit" name="letra" class="cls_mpletras" value="0 - 9"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="A"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="B"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="C"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="D"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="E"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="F"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="G"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="H"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="I"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="J"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="K"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="L"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="M"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="N"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="Ñ"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="O"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="P"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="Q"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="R"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="S"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="T"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="U"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="V"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="W"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="X"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="Y"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="Z"/></td>         
                <td><input type="submit" name="letra" class="cls_mpletras" value="Otros"/></td>         
            </tr>
        </table>

        <br />
        <asp:Label ID="lblTablaFiltro" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>

        <br />
        <br />
        <asp:Label ID="lblComplementoFail" runat="server" Text=""></asp:Label>

        <asp:HiddenField ID="hidVinculador" runat="server" />
        <asp:HiddenField ID="hidCampo" runat="server" />
        <asp:HiddenField ID="hidPrimerProveedor" runat="server" />

        <asp:HiddenField ID="hidPantalla" runat="server" Value="Proveedores" />

        <asp:HiddenField ID="hidCerrarSesion" runat="server" />

    </div>

</asp:Content>
