<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="Proveedores.administrator.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/SortTable.css" rel="stylesheet" />
    <link href="../css/Orden.css" rel="stylesheet" />
    <link href="../css/EstiloSortTables.css" rel="stylesheet" />

    <script src="../js/BusquedaTabla.js"></script>
    <script src="../js/AddDeleteTable.js"></script>

    <script>
        $(function () {

            var activaFiltro = true;

            $("#searchTermSort").keyup(function () { //$("#searchTerm").keyup(function () {
                if (activaFiltro) {
                    activaFiltro = false;
                    setTimeout(function () {
                        doSearchSorter()
                        activaFiltro = true;
                    }, 1500);
                }
            });

            //$(function () {
            $("table").tablesorter({ debug: true });
            //});
            //Marcar pestaña
            $("#proveedor").addClass("active");
            //sortable
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
            //bloquear y desbloquear proveedores
            $("#btnBloqDesBloqProveedores").click(function () {
                document.location.href = "MostrarPertenencia.aspx?vinculador=Proveedores&campo=Proveedor&primerproveedor=me";
            });

            $("#ContentPlaceHolder1_btnUnir").click(function () {
                takeIdSelectedsUl('sortable2', "", "", "");
            });;


            $("#btnSubmitCargaAutomatica").click(function () {
                $("#ContentPlaceHolder1_btnCargaAutomaticaProv").submit();
            });

            $("#sortable2").mouseenter(function (e) {
                ulBorrar("sortable2");
            });


            $("#sortable2").mouseleave(function (e) {
                ulPintarPrimero("sortable2");
            });

            $("#sortable1").mouseleave(function (e) {
                ulBorrar("sortable1");
                ulPintarPrimero("sortable2");
            });

            if ($("#sortable1").height() > $("#sortable2").height()) {
                $("#sortable2").css("height", $("#sortable1").height());
            }
            $(".cls_mpletras").click(function () {//para mostrar por orden de letra
                var codeHtml = $("#<%=lblObjeto1.ClientID%>").html();
                codeHtml = codeHtml.replace(/</g, "[");
                codeHtml = codeHtml.replace(/>/g, "]");
                $("#procar").val(codeHtml);
            });
            mostrarDialog();
            //agregarTitle();

            $(".tblComun td:nth-child(2n+1)").addClass("td-impar");
            $(".tblComun td:nth-child(2n)").addClass("td-par");

        });

        //function agregarTitle() {
        //    $(".primerLi").attr('title', 'Sera tomado como el titular del nuevo grupo');
        //}

    </script>

    <style>
        .primerLi {
            border: 5px solid #F78181;
            /*border-color: #FE2E2E;*/
        }

        .idProv {
            margin-right: 3em;
            /*margin: 5em;*/
        }

        /*.img {
            max-width: 60px;
            
        }*/

        /*.tableIco {
            background: none;
            border-style: solid;
            border-width: medium;
            border-color: #FAFAFA;
            float: right;
        }

        .tableIco:hover {
            background: #E6E6E6;
            cursor:pointer;
        }*/

        .datMtrWrap {
            height: 7em;
        }

        .btn {
            margin-left: 60%;
        }
    </style>

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>

    <div class="paraDiseno">

        <table class="tblFm2">
            <tr>
                <td><strong>Estos son los proveedores existentes: </strong></td>
            </tr>
        </table>
        <br />
        <br />
    </div>

    <table class="tblFm">
        <tr>
            <td>
                <asp:Label ID="lblTablaFiltro" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNombreGrupo" runat="server" Text="Nombre del grupo:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNombreGrupo" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnUnir" runat="server" Text="Unir" OnClick="btnUnir_Click" class="btn" />
            </td>
        </tr>
    </table>
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
        <input id="procar" type="hidden" name="procargado" value="" />
<%--    <table class="tblFm">
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>--%>

    <%--<div id="tablaPRoveedores">--%>
    <asp:Label ID="lblTablaProveedores" runat="server" Text=""></asp:Label>
    <%--</div>--%>

    <%--<div id="DatosProveedoresUnir">--%>
    <asp:Label ID="lblObjeto1" runat="server" Text=""></asp:Label>
    <%--</div>--%>
    <br />

    <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
    <br />

    <asp:HiddenField ID="hidRFC1" runat="server" />
    <asp:HiddenField ID="hidRFC2" runat="server" />
    <asp:HiddenField ID="hidVerificar" runat="server" Value="si" />
    <asp:HiddenField ID="hidIdSelected" runat="server" />
    <asp:HiddenField ID="hidComplementoUr" runat="server" />

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />

</asp:Content>
