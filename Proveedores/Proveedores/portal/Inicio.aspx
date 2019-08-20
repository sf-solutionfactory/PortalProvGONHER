<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrProveedor.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Proveedores.portal.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(function () {
            
            $("#Inicio").addClass("active");
            //$("table tr td a").button({ icons: { primary: "ui-icon-circle-arrow-n" } });
            //$("table tr td .estatusCorrecto").button({ icons: { primary: "ui-icon-check" } });
            //$("table tr td .estatusIncorrecto").button({ icons: { primary: "ui-icon-closethick" } });
            //$("table tr td .estatusNone").button({ icons: { primary: "ui-icon-notice" } });
            //altoBody = $("#wrap").height();
            //alto = $("header").height();
            //alto += $("#cssmenu").height();
            //alto += $("footer").height();
            //alto += $("h2").height();
            //alto += 20;
            //$("#contenido").html('<h2>¡¡Bienvenido a tu portal de proveedores!!</h2><center><img src="../images/animacionArcaPS.gif" style="width:70%; height:' + (altoBody - alto) + 'px;"/></center>');
            
            calcularAltoArticulo();
            $(window).resize(function () {
                calcularAltoArticulo();
            });
            
            //$("*").css({ "font-size": "" });
            //$(document).css({ "font-size": "" });
            if (document.all) {
                document.body.style.removeProperty('font-size');
            } else {
                document.body.style.removeAttribute('font-size');
            }
            
            function calcularAltoArticulo() {
                var heightMen = 0;
                heightMen -= $('#divHeader').height();
                heightMen -= $('#divFooter').height();
                heightMen -= 8;
                heightMen -= $('.h1').height();
                heightMen -= $('#divBanner').height();

                heightArt = ($("#divFooter").position().top) + (heightMen);
                $("#divArticulo").height(heightArt);
            }
            calcularAltorC2();
            function calcularAltorC2() {
                
            }
        });
    </script>

    <style>
        #divColumna1 #divBanner img {
            width: 700px;
            height: 250px;
            -webkit-box-shadow: 12px 6px 49px -3px rgba(0,0,0,0.75);
            -moz-box-shadow: 12px 6px 49px -3px rgba(0,0,0,0.75);
            box-shadow: 12px 6px 49px -3px rgba(0,0,0,0.75);
        }
        *{
            font-size:inherit;
        }
        #divColumna2, #divFooter, #dialog-confirm, #divHeader{
            font-size:12px;
        }
    </style>
    <%--<script type="text/javascript" src="http://counter5.statcounterfree.com/private/counter.js?c=00c48f5cc61861bbd35c1c5294116ad3"></script>--%>

    <div id="divWrapContenido">
        <div id="divColumna1">
            <div id="divTitulo">
                <label class="h1">
                    <asp:Label ID="lblTitulo" runat="server" Text="" CssClass="titulo"></asp:Label>
                    
                    </label>
                </div>
            <br/>
            <br/>
            <div id="divArticulo">
                <asp:Label ID="lblArticulo" runat="server" Text="" CssClass="articulo"></asp:Label>
                
                </div>
            <div id="divBanner">
                <asp:Label ID="lblBanner" runat="server" Text=""></asp:Label>
                </div>
            </div>

        <div id="divColumna2">
            <div id="divNav1">
                
                </div>
            <div id="divNav2">
                <label class="h2">
                    <%--Oficinas--%> 
                    Corporativas
                    </label>
                <div>
                    <p>
                        Filtros de Occidente, S.A. de C.V.
                        </p>
                    <p>
                        Avenida 8 de Julio #2355 
                        <%--Av. Hacienda del Rosario 117--%>
                        <br/>
                        Zona Industrial Guadalajara,  
                        <%--Real de Bugambilias, León, Gto.--%>
                        <br/>
                        Jalisco
                        </p>
                    <p>
						CP 44940
                        <%--CP 64640 Monterrey, N.L., Méx.--%>
                        </p>
                    <p>
                        <%--Telefono: (+52) 477 718.6219--%>
                        Teléfono: 52 33 39424000 EXT 0
                        </p>
                    <p>
                        <%--sf-solutionfactory.com--%>
                    www.grupogonher.com
                        </p>
                    </div>
                <div>
                    <%--Contacto--%> 
                    </div>
                <div>
                    <p>               
                        </p>
                    <p>                 
                        </p>
                    <p>              
                        </p>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
