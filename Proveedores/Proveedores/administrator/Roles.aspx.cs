using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.hidCerrarSesion.Value != "cerrar")
            {
            
            try
            {
                string[] resLog = null;
                resLog = (string[])Session["resLog"];
                //string sesion = Session["idUsuarioProveedor"].ToString().Trim();
                //if (sesion == "Admin")
                if (resLog[2].ToString() == "0")
                {

                }
                else
                {
                    cerrarSesion();
                }
            }
            catch (Exception)
            {
                cerrarSesion();
            }
            //INICIO mostrar mensaje en dialogo
            string dialog = "";
            try
            {
                dialog = Session["textoDialogo"].ToString();
                //if(){

                //}
                this.lblDialog.Text = dialog;
                Session["textoDialogo"] = "";
            }
            catch (Exception)
            {
                this.lblDialog.Text = "";
            }
            //FIN mostrar mensaje en dialogo

            if (!IsPostBack)
            {
                cargarEdit();
                mostrarTablaRoles();
            }
            }
        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("config.aspx");
        }

        private void limpiarCampos() {
            this.txtNombreRol.Text = "";
            this.chkFacturasMIR7.Checked = false;
            this.chkFacturasFV60.Checked =  false;
            this.chkPartidas.Checked = false;
            this.chkPagos.Checked = false;
            this.chkDatosMaestros.Checked = false;
            this.chkUsuarios.Checked = false;
            this.chkRolDefault.Checked = false;

        }

        public void cargarEdit()
        {
            try
            {
                PNegocio.Administrador.Roles objInstancia = new PNegocio.Administrador.Roles();
                string[] arregloPantallas =  null;
                List<string[]> lista = objInstancia.consultarRolPorId(Request.QueryString["toEdit"]);
                if (lista.Count > 1)
                {
                    this.hidIdAnt.Value = lista[1][0];
                    this.txtNombreRol.Text = lista[1][1];

                    arregloPantallas = lista[1][2].Split(new Char[] {','});
                    for (int i = 0;i < arregloPantallas.Length; i++ )
                    {
                        switch (arregloPantallas[i].ToString().Trim())
                        {
                            case "Facturas por cargar":
                                this.chkFacturasMIR7.Checked = true;
                                break;
                            case "Facturas FV60":
                                this.chkFacturasFV60.Checked =  true;
                                break;
                            case "Facturas Liberadas":
                                this.chkPartidas.Checked = true;
                                break;
                            case "Pagos realizados":
                                this.chkPagos.Checked = true;
                                break;
                            case "Datos del proveedor":
                                this.chkDatosMaestros.Checked = true;
                                break;
                        case "Control de usuarios":
                                this.chkUsuarios.Checked =  true;
                                break;
                            default:
                                break;
                        }
                        
                    }

                    if (lista[1][3] == "True")
                    {
                        this.rdbEsActivo.Items.FindByText("Activo").Selected = true;
                        this.rdbEsActivo.Items.FindByText("Inactivo").Selected = false;
                    }
                    else {
                        this.rdbEsActivo.Items.FindByText("Activo").Selected = false;
                        this.rdbEsActivo.Items.FindByText("Inactivo").Selected = true;
                    }
                    if (lista[1][4] == "True")
                    {
                        this.chkRolDefault.Checked = true;
                    }
                    this.btnGuardarRol.Visible = false;
                    this.btnGuardarCambios.Visible = true;
                    this.btnCancel.Visible = true;

                    
                }

            }
            catch (Exception)
            {
                this.btnGuardarCambios.Visible = false;
                this.btnCancel.Visible = false;
            }
        }

        private void mostrarTablaRoles()
        {
            int cont = 0;
            this.lblTablaRoles.Text = new PNegocio.Administrador.Roles().consultarRoles(ref cont).Replace("False", "No").Replace("True", "Si");
            
            if (cont<=5)
            {
                chkRolDefault.Visible = true;
            }
           
            //this.lblTabla.Text = "aqui";
        }

        public void trabajaRol(string desicion)
        {
            string verificar = this.hidVerificar.Value;
            //if (verificar == "si")
            //{ 
            //string[] pantallasSelected = { "0", "0", "0", "0", "0", "0" }; incluir una más para Facturas MIR7(si se cambio factura x FV60)
            string[] pantallasSelected = { "0", "0", "0", "0", "0", "0", "0" };  
            if (chkFacturasMIR7.Checked)
                {
                    pantallasSelected[0] = "1";
                
                }
                if(chkPartidas.Checked){
                    pantallasSelected[1] = "1";
                }
                if (chkPagos.Checked)
                {
                    pantallasSelected[2] = "1";
                
                }
                if (chkDatosMaestros.Checked)
                {
                    pantallasSelected[3] = "1";
                
                }
                if (chkUsuarios.Checked)
                {
                    pantallasSelected[4] = "1";
                }
                if (chkRolDefault.Checked)      //esta pantalla sirve para saber si ES CREACION.  ultimo campo a enviar al procedimiento mgv
                {
                    pantallasSelected[5] = "1";
                }
            if (chkFacturasFV60.Checked)
            {
                pantallasSelected[6] = "1";
            }
            int esActivo = 0;
                if(this.rdbEsActivo.Text == "Activo"){
                    esActivo = 1;
                }
                int contadorActivosFinal = 0;
                for (int i = 0; i < pantallasSelected.Length; i++)
                {
                    if (pantallasSelected[i] == "1")
                    {
                        contadorActivosFinal++;
                    }
                }
                    if(contadorActivosFinal>1){
                        if (desicion == "insertar")
                        {
                            this.lblDialog.Text = new PNegocio.Administrador.Roles().insertarRol(
                                    this.txtNombreRol.Text.Trim(), esActivo.ToString().Trim(),
                                    pantallasSelected[0], pantallasSelected[1], pantallasSelected[2], pantallasSelected[3], pantallasSelected[4], pantallasSelected[6], pantallasSelected[5]
                                    );
                            mostrarTablaRoles();
                        }
                        else if (desicion == "actualizar")
                        {
                            this.lblDialog.Text = new PNegocio.Administrador.Roles().actualizaRol(
                                   this.txtNombreRol.Text.Trim(), esActivo.ToString().Trim(),
                                   pantallasSelected[0], pantallasSelected[1], pantallasSelected[2], pantallasSelected[3], pantallasSelected[4],  pantallasSelected[6], 
                                   this.hidIdAnt.Value, pantallasSelected[5]
                                   );
                            mostrarTablaRoles();
                        }

                        string complemento = "";
                        if (this.lblDialog.Text == "correcto")
                        {
                            if(desicion == "insertar"){
                                complemento = "insertó";
                                limpiarCampos();
                            }
                            else if (desicion == "actualizar")
                            {
                                complemento = "actualizó";
                                this.btnGuardarCambios.Visible = false;
                                this.btnCancel.Visible = false;
                                this.btnGuardarRol.Visible = true;
                                limpiarCampos();
                            }
                            this.lblDialog.Text = " se " + complemento + " correctamente";

                        }
                        
                        else{
                            this.lblDialog.Text = "No se logró realizar la acción, probablemente ya exista esa combinación de pantallas o el nombre";
                        }
                        
                        //Response.Redirect(Request.RawUrl);
                    }
                    else{
                        this.lblDialog.Text = "Debe seleccionar al menos dos vistas, las unitarias ya existen por default";
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

                
                //this.lblResultado.Text = resultado;
           //}
        }

        protected void btnGuardarRol_Click(object sender, EventArgs e)
        {
            if (this.hidVerificar.Value == "si")
            {
                trabajaRol("insertar");
            }
            else {
                this.lblDialog.Text = "No deben de existir campos vacios";
                activarMensageDialog();
            }
            
        }

        private void activarMensageDialog(){
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
    
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            this.hidVerificar.Value = "si";
            trabajaRol("actualizar");
            this.hidVerificar.Value = "no";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Roles.aspx");
        }


        //public void actualizaRol()
        //{
        //    if (this.txtPassCambiar.Text == this.hidPsAnt.Value.Trim()
        //        && this.txtPassword.Text == this.txtRepitePassword.Text
        //        && this.txtUsuario.Text == this.txtRepiteUsuario.Text
        //        )
        //    {
        //        PNegocio.Administrador.Instancia objInstancia = new PNegocio.Administrador.Instancia();
        //        objInstancia.actualizarInstancia(this.hidIdAnt.Value, this.txtDescripcion.Text, this.txtUsuario.Text, this.txtPassword.Text);
        //        this.lblResultado.Text = "Se actualizo correctamente";
        //        mostrarTablaInstancias();
        //    }
        //    else
        //    {
        //        this.lblResultado.Text = "Algunos datos no coinciden";
        //    }
        //}
    }
}