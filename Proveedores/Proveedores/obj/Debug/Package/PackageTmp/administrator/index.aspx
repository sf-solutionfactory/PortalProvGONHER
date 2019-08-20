<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrInicio.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Proveedores.administrator.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <div class="login">
            <div class="divImg">
                <img src="images/candadoCerrado.png" />
            </div>
            <asp:Login ID="lgn1" runat="server" LoginButtonText="Acceder" OnAuthenticate="lgn1_Authenticate" TitleText="PORTAL DE PROVEEDORES" RenderOuterTable="True" TextLayout="TextOnTop" DisplayRememberMe="False" FailureText="Inténtelo de nuevo." DestinationPageUrl="~/portal/datosMaestros.aspx" OnLoggedIn="lgn1_LoggedIn" VisibleWhenLoggedIn="False">
                    <LoginButtonStyle CssClass="btn" />
                    <TextBoxStyle CssClass="txt" />
                    <TitleTextStyle CssClass="titleText" />
            </asp:Login>
        </div>
    </center>
</asp:Content>
