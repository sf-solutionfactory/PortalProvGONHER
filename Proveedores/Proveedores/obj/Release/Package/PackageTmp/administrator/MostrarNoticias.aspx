<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="MostrarNoticias.aspx.cs" Inherits="Proveedores.administrator.MostrarNoticias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/Orden.css" rel="stylesheet" />
    <link href="../css/SortTable.css" rel="stylesheet" />
    <%--<link href="../css/SortableGrupoNoticia.css" rel="stylesheet" />--%>
    <link href="../css/EstiloSortTables.css" rel="stylesheet" />
    

    <script src="../js/BusquedaTabla.js"></script>
    <script src="../js/AddDeleteTable.js"></script>

    <link href="../js/SortTable/jquery-ui.css" rel="stylesheet" />
    <link href="../js/SortTable/style.css" rel="stylesheet" />

    <link href="../css/EstiloSortTables.css" rel="stylesheet" />

    <script>
        $(function () {

            var activaFiltro = true;

            $("#searchTermNoticia").keyup(function () { //$("#searchTerm").keyup(function () {
                if (activaFiltro) {
                    activaFiltro = false;
                    setTimeout(function () {
                        doSearchSorterMostrarNoticia();
                        activaFiltro = true;
                    }, 1500);
                }
            });

            $("#grupoNoticia").addClass("active");

            $(".tblComun>tbody>tr").click(function () {
                var n_noticia = $.trim($(this).find("td:nth-child(1)").html());
                var titulo = $.trim($(this).find("td:nth-child(2)").html());
                var vinculador = $.trim($("#ContentPlaceHolder1_hidVinculador").val());
                var nombreGrupo = $.trim($("#ContentPlaceHolder1_hidNombrGrupo").val());
                switch (vinculador) {
                    case "GrupoNoticia":
                        document.location.href = "GrupoNoticia.aspx?n_noticia=" + n_noticia + "&titulo=" + titulo + "&nameGroup=" + nombreGrupo;
                        break;
                        
                    default:
                        alert("default");
                        document.location.href = "javascript: history.back(1);";
                        break;
                }

            });

            //$(function () {
            //    $("table").tablesorter({ debug: true });
            //});

            $("ul.droptrue").sortable({
                connectWith: "ul"
            });

            $("ul.dropfalse").sortable({
                connectWith: "ul",
                dropOnEmpty: true
            });

            $("#sortable1, #sortable2").sortable({
                placeholder: "ui-state-highlight"
            });

            $("#sortable1, #sortable2").disableSelection();

            //$("#sortable1").selectable();

            $("#ContentPlaceHolder1_btnSelectNoticia").click(function () {
                    takeIdSelectedsUl('sortable2', "", "", "");
            });

            if ($("#sortable1").height() > $("#sortable2").height()) {
                $("#sortable2").css("height", $("#sortable1").height());
            }

            $(".idProv").hide();

        });

    </script>

    <style>
        #sortable1, #sortable2  {
            width: 480px;
            min-width: 480px;
            min-height: 20px;
        }

       #sortable1 li, #sortable2 li {
            width: 430px;   
            min-width: 430px;     
       }

        
        #sortable2 {
            min-height: 20px;
        }


         table {
            display : inline-block;
            vertical-align : top;
        }

    </style>

    <div id="gettt">Obtener</div>

    

    <table>
        <tr>
            <td><asp:Label ID="lblTablaFiltro" runat="server" Text=""></asp:Label></td>
            <td><asp:Button ID="btnSelectNoticia" runat="server" Text="Terminar" OnClick="btnSelectNoticia_Click" CssClass="btn btnTerminar"/></td>
        </tr>
    </table>
    
    <br />
    <asp:Label ID="lblConsejoSeleccionar" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="lblConsejo" runat="server" Text=""></asp:Label>
    <br />
    <asp:HiddenField ID="hidVinculador" runat="server" />
    <asp:HiddenField ID="hidNombrGrupo" runat="server" />

    <div id="tablaResultados">
        <%--<h2>Proveedores</h2>--%>
        <br />

        <asp:Label ID="lblTablaFiltroUl" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <div id="scrolltable" class="tablauno">
        <asp:Label ID="lblTablaNoticias" runat="server" Text=""></asp:Label>
        </div>
        <asp:Label ID="lblTablaDos" runat="server" Text=""></asp:Label>

    </div>

    <asp:HiddenField ID="hidIdSelected" runat="server" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />

</asp:Content>
