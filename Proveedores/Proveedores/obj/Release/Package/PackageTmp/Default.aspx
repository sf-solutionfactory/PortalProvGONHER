<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrInicio.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proveedores.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="css/passSeguro/stylePassSeguro.css" rel="stylesheet" />
    <script src="css/passSeguro/ValidaPassSeguro.js"></script>

 <div id="pswd_info">
   <h4>La contraseña debe cumplir con los siguientes requerimientos:</h4>
   <ul>
      <li id="letter">Al menos debe tener <strong><%=hidNumeroLetras.Value%> letra/s</strong></li>
      <li id="capital">Al menos debe tener <strong><%=hidNumeroLetrasM.Value%> letra/s en mayúsculas</strong></li>
      <li id="number">Al menos debe tener <strong><%=hidCantidadNumeros.Value%> número/s</strong></li>
      <li id="length">Debe tener <strong><%=hidNumeroCaracteres.Value%> caractere/s</strong> como mínimo</li>
   </ul>
</div>

    <style>
        .lblError {
            color:red;
        }

        /*.btn {
            color: #C82B40;
        }*/
    </style>

    <center>
        <div class="login">
            <div class="divImg">
                <img src="images/candadoCerrado.png" />
            </div>
            <asp:Label ID="lblPortalProveedores" CssClass="titleText" runat="server" Text="Portal de Proveedores"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblDescripUsuario" runat="server" Text="Usuario"></asp:Label>
            <br />
            <asp:TextBox ID="txtUsuario" CssClass="txt" runat="server" MaxLength="50"></asp:TextBox>
            <asp:TextBox ID="txtrepitContra" CssClass="txt modepass" runat="server" Height="22px" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="lblDescContrasena" runat="server" Text="Contraseña"></asp:Label>
            <br />
            <asp:TextBox ID="txtContrasena" CssClass="txt modepass" runat="server" Height="22px" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="lblError" CssClass="lblError" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnAcceder" CssClass="btn" runat="server" OnClick="btnAcceder_Click" Text="Acceder" />
        </div>
    </center>

    <asp:HiddenField ID="hidVerificar" runat="server"  />
    <asp:HiddenField ID="hidNumeroLetras" runat="server" />
    <asp:HiddenField ID="hidNumeroLetrasM" runat="server" />
    <asp:HiddenField ID="hidCantidadNumeros" runat="server" />
    <asp:HiddenField ID="hidNumeroCaracteres" runat="server" />
    <asp:HiddenField ID="hidVerificarPass" runat="server" />

</asp:Content>