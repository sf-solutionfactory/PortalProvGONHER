<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="facturasResumen.aspx.cs" Inherits="Proveedores.portal.facturasResumen" %>
<form id="form1" runat="server">
    <style>
        h2 {   
            font-size:20px;
            color:#4D4D4D;
            text-indent:5px;
        }

        h3 {
            font-size:20px;
            color:#666666;
            text-indent:20px;
        }
    </style>
    <script src="../js/Estilo.js"></script>
    <h2>
        Total de archivos procesados: <asp:label runat="server" ID="lblNumArchivos"  style="font-size:20px;"></asp:label>
    </h2>
    <h3>
        <asp:label runat="server" ID="lblNumEnc" style="font-size:19px;"></asp:label>

    </h3>
    <asp:Label ID="lblTblEncontrados" runat="server"></asp:Label>
    <h3>
        <asp:label runat="server" ID="lblNumNoEnc" style="font-size:19px;"></asp:label>
    </h3>
    <asp:label runat="server" ID="lblTblNoEncontrados"></asp:label>
    <center>
        <a href="facturas.aspx"><div class="ico-cerrar" style="color:#666666;">VOLVER</div></a>
    </center>
</form>