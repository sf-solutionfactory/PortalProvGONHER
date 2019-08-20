<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrAdministrador.Master" AutoEventWireup="true" CodeBehind="config.aspx.cs" Inherits="Proveedores.administrator.config" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="Configuración global">
        <%--<script src="../js/validarNotNull.js"></script>--%>
        <link href="../css/Orden.css" rel="stylesheet" />
        <script src="../js/validarEmail.js"></script>
        <script>
            $(function () {
                $("#config").addClass("active");

                $('#ContentPlaceHolder1_btnEnviarConfig').click(function () {
                    var resultEmail1 = false;

                    if ($('#ContentPlaceHolder1_hidTipoCorreo').val() == "configurado") {

                        if ($('#ContentPlaceHolder1_txtEmail').val() != null
                            && $('#ContentPlaceHolder1_txtEmail').val() != ""
                            ) {
                            resultEmail1 = validateEmail('ContentPlaceHolder1_txtCorreoServidorCorreo');
                        }
                        else {
                            resultEmail1 = true;
                        }
                    
                        if (resultEmail1) {
                            validar();
                        }
                    }
                });

                $('#ContentPlaceHolder1_lblVernterior').click(function () {
                    document.location.href = "config.aspx?dir=verAnt&btn=ant";

                });

                if ($('#ContentPlaceHolder1_hidTipoCorreo').val() == "configurado") {
                    $('#divConfigEspecialCorreo').show();
                    $('.tablaEmailNormal').hide();
                    $('#ContentPlaceHolder1_hidTipoCorreo').val("configurado");
                }
                else {
                    $('#divConfigEspecialCorreo').hide();
                    $('.tablaEmailNormal').show();
                    $('#ContentPlaceHolder1_hidTipoCorreo').val("normal");
                }

                //$('#divConfigEspecialCorreo').hide();

                $('.btnConfigCorreo').click(function () {
                    mostrarCorreoEspecial();
                });

                $('.btnCancel').click(function () {
                    ocultarCorreoEspecial();
                });

                $('#ContentPlaceHolder1_btnEnviarConfig').click(function () {
                    revalidaNumeros();
                });

                

                mostrarDialog();

            });

            

            function ocultarCorreoEspecial() {
                $('#divConfigEspecialCorreo').hide("slow");
                $('.tablaEmailNormal').show("slow");
                $('#ContentPlaceHolder1_hidTipoCorreo').val("normal");
            }

            function mostrarCorreoEspecial() {
                $('#divConfigEspecialCorreo').show("slow");
                $('.tablaEmailNormal').hide("slow");
                $('#ContentPlaceHolder1_hidTipoCorreo').val("configurado");
            }

            


        </script>

        <style>
            #ContentPlaceHolder1_lblVernterior {
                min-width:100px;
            }

            .btnConfigCorreo, .btnCancel {
                width: 160px;
            }

            #ContentPlaceHolder1_dpdTipoCuenta,
             #ContentPlaceHolder1_dpdSufijoEmail,
            #ContentPlaceHolder1_dpdBloqSociedad
             {
                min-height: 15px;
            }

            #divConfigEspecialCorreo {
                border:dashed;
            }

            #ContentPlaceHolder1_dpdSufijoEmail, #ContentPlaceHolder1_txtEmail {
                min-width: 150px;
                width:150px;
            }
            


        </style>

        <asp:Label ID="lblDialog" runat="server" title="Informe" Text=""></asp:Label>

        <%--<div class="paraDiseno">--%>
            <table class="tblFm tblFm3">
                <tr>
                    <td>Estatus del portal
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdbPortal" runat="server">
                            <asp:ListItem>Activado</asp:ListItem>
                            <asp:ListItem>Desactivado</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>Umbral de usuarios por proveedor
                    </td>
                    <td>
                        <asp:TextBox ID="txtUmbral" runat="server" onkeypress="return soloNumeros2(event)" CssClass="soloNumeros2" value="5" MaxLength="2"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td>Bloqueo de sociedad</td>
                    <td>
                        <asp:DropDownList ID="dpdBloqSociedad" runat="server">
                            <asp:ListItem>1 = N</asp:ListItem>
                            <asp:ListItem>1 = 1</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
               
            </table>
        <%--</div>--%>
    </asp:Panel>
    <br />

    <asp:Panel ID="Panel2" runat="server" GroupingText="Correo">
        <%--<div class="paraDiseno">--%>
        <table class="tblFm tblFm3 tablaEmailNormal" >
            <tr>
                <td>Email</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" class="txtValidar"></asp:TextBox>
                    <asp:DropDownList ID="dpdSufijoEmail" runat="server">
                        <asp:ListItem>@gmail.com</asp:ListItem>
                        <asp:ListItem>@yahoo.com</asp:ListItem>
                        <asp:ListItem>@hotmail.com</asp:ListItem>
                        <asp:ListItem>@outlook.com</asp:ListItem>
                    </asp:DropDownList>
                   </td>
            </tr>
            <tr>
                <td>Contraseña
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="txtValidar"></asp:TextBox>
                    <br />

                </td>
            </tr>
            <tr>
                <td>Confirmar contraseña
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" class="txtValidar"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <div class="btn btnConfigCorreo">Configuración especial...</div>
                </td>
            </tr>
        </table>
        
          <table id="divConfigEspecialCorreo" class="tblFm tblFm3">
                <tr>
                    <td><strong>Tipo de servidor</strong></td>
                    <td></td>
                </tr>
                <tr>
                    <td>SMTP</td>
                    <td>
                        <asp:TextBox ID="txtSMTP" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Puerto</td>
                    <td>
                        <asp:TextBox ID="txtPuerto" runat="server" class="soloNumeros2" onkeypress="return soloNumeros2(event)" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>SSL</td>
                    <td>
                        <asp:CheckBox ID="chkSSL" runat="server" Text="SSL" />
                    </td>
                </tr>
                <tr>
                    <td><strong>Información de inicio de sesión</strong></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Correo</td>
                    <td>
                        <asp:TextBox ID="txtCorreoServidorCorreo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Contraseña</td>
                    <td>
                        <asp:TextBox ID="txtContraServidorCorreo" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
              <tr>
                    <td>Repite contraseña</td>
                    <td>
                        <asp:TextBox ID="txtRepiteContraServidorCorreo" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <div class="btn btnCancel">
                            Cancelar
                        </div>
                    </td>
                </tr>
         </table>
                
    <table class="tblFm tblFm3">
            <tr>
                <td>Asunto</td>
                <td>
                    <asp:TextBox ID="txtAsunto" runat="server" class="txtValidar"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Contenido</td>
                <td>
                    <asp:TextBox ID="txtAreaContenido" runat="server" Columns="50" Rows="5" TextMode="multiline" class="txtValidar" />
                </td>
            </tr>
        </table>
    <%--</div>--%>
    </asp:Panel>
    <br />
    <asp:Panel ID="Panel3" runat="server" GroupingText="Seguridad">
        <%--<div class="paraDiseno">--%>
        <table class="tblFm tblFm3">
            <tr>
                <td>Nº passwords por recordar</td>
                <td>
                    <asp:TextBox ID="txtNumPassRec" runat="server" onkeypress="return soloNumeros2(event)" value="3" class="txtValidar soloNumeros2" MaxLength="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Máximo de intentos</td>
                <td>
                    <asp:TextBox ID="txtMaxIntentos" runat="server" onkeypress="return soloNumeros2(event)" value="3" class="txtValidar soloNumeros2" MaxLength="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Intervalo de tiempo para bloqueo(m)</td>
                <td>
                    <asp:TextBox ID="txtIntervalo" runat="server" class="txtValidar soloNumeros2" MaxLength="2" onkeypress="return soloNumeros2(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Caducidad de password(d)</td>
                <td>
                    <asp:TextBox ID="txtCaducidadPass" runat="server" class="txtValidar soloNumeros2" MaxLength="3" onkeypress="return soloNumeros2(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Tiempo de bloqueo de Administrador(m)</td>
                <td>
                    <asp:TextBox ID="txtTiempoBloqAdmin" runat="server" class="txtValidar soloNumeros2" MaxLength="4" onkeypress="return soloNumeros2(event)"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel6" runat="server" GroupingText="Contenido Password">
        <table class="tblFm tblFm3">
            <tr>
                <td>Numero de letras</td>
                <td>
                    <asp:TextBox ID="txtNumeroLetras" runat="server" class="txtValidar soloNumeros2" MaxLength="3"  onkeypress="return soloNumeros2(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Numero de letras en Mayuscúla</td>
                <td>
                    <asp:TextBox ID="txtNumeroLetrasM" runat="server" class="txtValidar soloNumeros2" MaxLength="3"  onkeypress="return soloNumeros2(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Cantidad de números</td>
                <td>
                    <asp:TextBox ID="txtCantidadNumeros" runat="server" class="txtValidar soloNumeros2" MaxLength="3"  onkeypress="return soloNumeros2(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Numero de caracteres</td>
                <td>
                    <asp:TextBox ID="txtNumeroCaracteres" runat="server" class="txtValidar soloNumeros2" MaxLength="3"  onkeypress="return soloNumeros2(event)"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel5" runat="server" GroupingText="XML:">
        <table class="tblFm tblFm3">
            <tr>
                <td>Maximo de XML permitidos para adjuntar</td>
                <td>
                    <asp:TextBox ID="txtMaxXML" runat="server" class="txtValidar soloNumeros2" MaxLength="3"  onkeypress="return soloNumeros2(event)"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    <asp:Panel ID="Panel4" runat="server" GroupingText="Opciones:">
        
    <br />
    <br />

        <style>
            table.opciones.primerTd {
                width:40%;
            }

            .primerTd {
                width:40%;
                /*padding-rigt:3em;*/    
            }

            .segundoTd {
                padding-left:3em;           
            }

            #ContentPlaceHolder1_dpdBloqSociedad {
                height:12px;
                min-height:25px;
            }
            
        </style>

    <table class="opciones tblFm">
        <thead>
            <tr>
                <th>
                    <table class="tblFm2">
                        <tr>
                            <td><strong>Seleccione:</strong></td>
                        </tr>
                    </table>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
               <td class="primerTd">
                   <asp:Label ID="lblInfoGuardar" runat="server" Text=""></asp:Label>
               </td>
               <td class="segundoTd">
                   <asp:Button ID="btnEnviarConfig" runat="server" OnClick="btnEnviarConfig_Click" Text="Guardar" CssClass="btn" /> 
               </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAdvertencia" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblLeyendaConfigAnterior" runat="server" Text="Para ver la configuración anterior presione este botón "></asp:Label>
                    <asp:Label ID="lblLeyendaConfigAnterior2" runat="server" Text="Para recuperar la configuración anterior presione este botón: "></asp:Label>
                </td>
                <td class="segundoTd">
                    <br />
                    <asp:Label ID="lblVernterior" runat="server" CssClass="btn" Text=""></asp:Label>
                    <br />
                    <asp:Button ID="btnRecuperarAnterior" runat="server" CssClass="btn" OnClick="btnRecuperarAnterior_Click" Text="Restablecer" />
                    <br/>
                    <br/>
                    
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="segundoTd"><asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancelar" CssClass="btn" /></td>
            </tr>
        </tbody>
    </table>

    </asp:Panel>

    
    <br />

    <asp:HiddenField ID="hidVerificar" runat="server" Value="si" />

    <br />
    <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
    <asp:HiddenField ID="hidTipoCorreo" runat="server" />

    <asp:HiddenField ID="hidloadconfpass" runat="server" />
    <asp:HiddenField ID="hidCerrarSesion" runat="server" />

</asp:Content>
