<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrProveedor.Master" AutoEventWireup="true" CodeBehind="datosMaestros.aspx.cs" Inherits="Proveedores.datosMaestros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(function () {
            
            $("#datosMaestros").addClass("active");
        });
    </script>

    <style>

        .lblDM, .lblDM_H {
              color: #4D4D4D;
              text-align: left;
              font-weight: bolder;
              font-size: 12px;
        }

        .lblDM_H {
              font-size: 14px;
        }

    </style>


    <%--<label class="h1">
        Mis datos
    </label>--%>
    <br/><br/>

    <asp:Panel ID="pnlDatosMaestros" runat="server"></asp:Panel>
    <br/>
    <asp:Label ID="lblTabla" runat="server"></asp:Label>
    <br/>
    <div style="width:100%;height:50px;">
        <a href='datosMaestros.aspx'>
            <div class="ico-actualizar">
            </div>
        </a>
    </div>

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />

    </asp:Content>
