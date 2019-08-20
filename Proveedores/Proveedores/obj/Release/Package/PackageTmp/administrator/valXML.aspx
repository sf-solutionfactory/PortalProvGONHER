<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="valXML.aspx.cs" Inherits="Proveedores.administrator.valXML" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(function () {
            
            $("#valXML").addClass("active");

            mostrarDialog();

            
            $("table").tablesorter({ debug: true });


            //("#ContentPlaceHolder1_lstBoxNoSelectedProv").
         
        });
    </script>
    <script src="../js/BusquedaTabla.js"></script>

    <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>

    <asp:Button ID="btnNuevo" runat="server" CssClass="btn" OnClick="btnNuevo_Click" Text="Nuevo" ToolTip="Agregar nuevo grupo de validación XML" />
    <br />
    <asp:Literal ID="litTablaGrpValidaciones" runat="server"></asp:Literal>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="tblComun" DataSourceID="SqlDataSource1" OnRowEditing="GridView1_RowEditing" DataKeyNames="ID">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Descripción" HeaderText="Descripción" SortExpression="Descripción" />
            <asp:BoundField DataField="Validaciones" HeaderText="Validaciones" ReadOnly="True" SortExpression="Validaciones" />
            <asp:CommandField AccessibleHeaderText="Editar" ButtonType="Button" EditText="Editar" ShowEditButton="True">
            <ControlStyle CssClass="btn" />
            <HeaderStyle CssClass="icono" />
            </asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblSinRegistros" runat="server" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="XX-Large" Font-Strikeout="False" ForeColor="Silver" Text="Sin datos para mostrar" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Panel ID="pnlNuevo" runat="server">

        <table class="tblFm2">
            <tr><td><strong>Crear nuevos grupos de validación</strong></td></tr>
        </table>
        <br/>
        <table class="tblFm">
            <tr>
                <td>Nombre del grupo</td>
                <td>
                    <asp:TextBox ID="txtNombreGrupo" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Validaciones
                </td>
                <td>
                    <asp:CheckBoxList ID="chbListValidaciones" runat="server" DataSourceID="srcValidacionFactura" DataTextField="descripcion" DataValueField="idValidacionFactura">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                    Proveedores
                </td>
                <td>
               <%-- <table class='filtro'>
                <tr>
                <td>Filtrar...</td>
                <td><input id='searchTerm' type='text' onkeyup='doSearchSelect()' /></td>
                </tr>
                </table>--%>

                    <asp:Panel ID="pnlSelProveedor" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lstBoxNoSelectedProv" runat="server" DataSourceID="srcProveedoresNoSeleccionados" DataTextField="nombre" DataValueField="idProveedor" Height="200px" SelectionMode="Multiple" ToolTip="Proveedores que aun no tiene grupo de validación" Width="120px"></asp:ListBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnAdd" runat="server" Text="&gt;" CssClass="btn" OnClick="btnAdd_Click" />
                                    <br />
                                    <asp:Button ID="btnRemove" runat="server" Text="&lt;" CssClass="btn" OnClick="btnRemove_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnAddTodo" runat="server" CssClass="btn" OnClick="btnAddTodo_Click" Text="&gt;&gt;" />
                                    <br />
                                    <asp:Button ID="btnRemoveTodo" runat="server" CssClass="btn" OnClick="btnRemoveTodo_Click" Text="&lt;&lt;" />
                                </td>
                                <td>
                                    <asp:ListBox ID="lstBoxSelectedProv" runat="server" DataSourceID="srcProveedoreSeleccionados" DataTextField="nombre" DataValueField="idProveedor" Height="200px" SelectionMode="Multiple" ToolTip="Proveedores seleccionados para estar en el grupo de validación" Width="120px"></asp:ListBox>
                                </td>
                            </tr>
                        </table>
                        
                        
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td style="text-align:right">
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn" OnClick="btnEliminar_Click" Text="Eliminar" />
                    &nbsp; <asp:Button ID="btnGuardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" Text="Guardar" />
                    &nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="btn" OnClick="btnCancelar_Click" Text="Cancelar" />

                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:proveedores2ConnectionString %>" SelectCommand="consultarGrupoValidacionesXML" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        <asp:SqlDataSource ID="srcValidacionFactura" runat="server" ConnectionString="<%$ ConnectionStrings:proveedores2ConnectionString %>" SelectCommand="consultarValidacionFactura" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        <asp:SqlDataSource ID="srcProveedoresNoSeleccionados" runat="server" ConnectionString="<%$ ConnectionStrings:proveedores2ConnectionString %>" SelectCommand="consultarProvNoSelToGrpVal" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        <asp:SqlDataSource ID="srcProveedoreSeleccionados" runat="server" ConnectionString="<%$ ConnectionStrings:proveedores2ConnectionString %>" SelectCommand="consultarProvSelToGrpValByIdGrpVal" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="" Name="idGrupoValidacion" SessionField="idGrupoValidacion" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Literal ID="litConsole" runat="server"></asp:Literal>
        <br />
    </asp:Panel>

    <asp:HiddenField ID="hidCerrarSesion" runat="server" />
</asp:Content>
