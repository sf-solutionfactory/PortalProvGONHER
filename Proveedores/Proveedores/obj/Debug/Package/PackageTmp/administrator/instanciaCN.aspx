<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="instanciaCN.aspx.cs" Inherits="Proveedores.administrator.instanciaCN" %>
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

            $("table").tablesorter({ debug: true });
          
            $("#opener").click(function () {
                mostrarDialog();
            });

            $("#Div1").click(function () {
                if (mostrar) {
                    alert($("#ContentPlaceHolder1_lblDialog").text());
                    mostrar = false;

                }
                
            });

            $("#instanciaCN").addClass("active");

            $('#ContentPlaceHolder1_btnEjecutaInstancia').click(function () {
                validar();
            });

            $('#ContentPlaceHolder1_btnEditaInstancia').click(function () {
                validar();
            });

            mostrarDialog();

        });

    </script>

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>

    <style>
        
        div.ui-dialog {
            min-height: 100px;
        }

        .ui-dialog .ui-dialog-title {
            color: #58ACFA;
            font-size: 14px;
        }

        .ui-dialog .ui-dialog-content {
            color: #6E6E6E;
            font-weight: bold;
            font-size: 12px;
        }
    </style>

    <div class="paraDiseno">

        <table class="tblFm2">
            <tr>
                <td><strong>Llena los campos para dar de alta la conexión a SAP</strong></td>
            </tr>
        </table>

        <table class="tblFm">
            <tr>
                <td>Name</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" class="txtValidar" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><span lang="EN-US">AppServerHost</span></td>
                <td>
                    <asp:TextBox ID="txtAppSH" runat="server" class="txtValidar" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><span lang="EN-US">SAPRouter</span></td>
                <td>
                    <asp:TextBox ID="txtSAProuter" runat="server" class="txtValidar" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><span lang="EN-US">SystemNumber</span></td>
                <td>
                    <asp:TextBox ID="txtSystemNumber" runat="server" class="txtValidar" MaxLength="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><span lang="EN-US">User</span>
                </td>
                <td>
                    <asp:TextBox ID="txtUser" runat="server" class="txtValidar" MaxLength="20"></asp:TextBox>
                </td>
            </tr>           
            <tr>
                <td><span lang="EN-US">Password</span></td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" class="txtValidar" MaxLength="10" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><span lang="EN-US">Client</span></td>
                 <td>
                    <asp:TextBox ID="txtClient" runat="server" MaxLength="20" class="txtValidar"></asp:TextBox>
                 </td>
            </tr>
           <tr>
                <td>Mi sociedad                </td>
                 <td>
                    <asp:TextBox ID="txtMiSociedad" runat="server" MaxLength="4" class="txtValidar"></asp:TextBox>
                 </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnEjecutaInstanciaCN" runat="server" Text="Guardar" OnClick="btnEjecutaInstancia_Click" CssClass="btn" />
                    <asp:Button ID="btnEditaInstanciaCN" runat="server" Text="Guardar cambios" CssClass="btn" OnClick="btnEditaInstancia_Click" />
                    <asp:Button ID="btnCancelEdit" runat="server" Text="Cancelar" OnClick="btnCancelEdit_Click" CssClass="btn" />
                </td>
            </tr>
        </table>
    </div>

    <div id="tablaResultados">  

        <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
        <br /><br />
        <table class="tblFm2">
            <tr>
                <td><asp:Label ID="lblExplicacionInstancias" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>

        <table class="tblFm">
            <tr>
              <td> <asp:Label ID="lblTablaFiltro" runat="server" Text=""></asp:Label> </td>
            </tr>
        </table>
        
        <br />
        <br />
        <asp:Label ID="lblTabla" runat="server" CssClass="lblTable"></asp:Label>
        <br />
        <br />
        <br />

        <asp:HiddenField ID="hidVerificar" runat="server" />
        <asp:HiddenField ID="hidPantalla" runat="server" Value="InstanciaCN" />
        <asp:HiddenField ID="hidIdAnt" runat="server" />
        <asp:HiddenField ID="hidComplementoUr" runat="server" />
        <asp:HiddenField ID="hidCerrarSesion" runat="server" />
    </div>
</asp:Content>
