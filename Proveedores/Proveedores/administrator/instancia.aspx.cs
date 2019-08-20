using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class instancia : System.Web.UI.Page
    {
        //string PassAnterior;
        string id;
        
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
           
            //if (dialog != null && dialog != "")
            //{
                
            //}
            //this.btnCancelEdit.Visible = false;
            //this.btnEditaInstancia.Visible = false;

            if(!IsPostBack){
                cargarEdit();
                mostrarTablaInstancias();
            }

            }
        }


        private string obneterRFCPorSociedad(string endpoint , string[] userPsss, string sociedad)
        {
            string RFC = "" ;
            RFC = PNegocio.Administrador.Instancia.RFCPorSociedad(endpoint, userPsss, sociedad);
            return RFC;

        }


        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("config.aspx");
        }

        public void cargarEdit(){
            try
            {
                if (Request.QueryString["toEdit"] != "" && Request.QueryString["toEdit"] != null)
                {


                    PNegocio.Administrador.Instancia objInstancia = new PNegocio.Administrador.Instancia();

                    string sqlString = "SELECT idInstancia as ID,usuario as Usuario , descripcion as Descripción, pass, esBloq as estatus, endpointAdd as EndpointAddres, RFC, sociedad FROM instancia where idInstancia = " + Request.QueryString["toEdit"] + ";";
                    List<string[]> lista = objInstancia.consultarInstanciaPorId(sqlString);
                    if (lista.Count > 1)
                    {
                        this.txtDescripcion.Text = lista[1][2].Trim();
                        this.txtUsuario.Text = lista[1][1].Trim();
                        this.txtRepiteUsuario.Text = lista[1][1].Trim();
                        this.hidPsAnt.Value = lista[1][3].Trim();
                        this.hidIdAnt.Value = id = lista[1][0].Trim();
                        this.txtEndpoint.Text = lista[1][5].Trim();
                        this.txtMiSociedad.Text = lista[1][7].Trim();
                        this.btnEjecutaInstancia.Visible = false;
                    }
                }
                else {
                    this.btnCancelEdit.Visible = false;
                    this.btnEditaInstancia.Visible = false;
                    //this.lblTextoCambiarPass.Visible = false;
                    //this.lblTextoCambiarPass2.Visible = false;
                    //this.txtPassCambiar.Visible = false;
                }

            }
            catch (Exception)
            {
                this.btnCancelEdit.Visible = false;
                this.btnEditaInstancia.Visible = false;
                //this.lblTextoCambiarPass.Visible = false;
                //this.lblTextoCambiarPass2.Visible = false;
                //this.txtPassCambiar.Visible = false;
            }
        }

        private void mostrarTablaInstancias()
        {
            
            this.lblTabla.Text = new PNegocio.Administrador.Instancia().consultarInstancia();
            if (this.lblTabla.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltro();
                this.lblExplicacionInstancias.Text = "<strong>Estas son las instancias de SAP que tenemos en el registro:</strong>";
            }
            else {
                //this.lblExplicacionInstancias.Text = "";
            }
            //this.lblTabla.Text = "aqui";
        }

        protected void btnEjecutaInstancia_Click(object sender, EventArgs e)
        {
            string verificar = this.hidVerificar.Value;
            if (verificar == "si")
            {
                try
                {
                    int sociedad = int.Parse(this.txtMiSociedad.Text.Trim());
                    tryInsertInstancia();
                    
                }
                catch (Exception)
                {
                    this.lblDialog.Text = "La sociedad solo permite numeros";   
                }
                
            }
            else
            {
                //this.lblResultado.Text = "Existen campos vacios";
                //Session["textoDialogo"] = "Existen campos vacios";
                this.lblDialog.Text = "Existen campos vacios";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
            }
        }

        private void tryInsertInstancia()
        {
            
                PNegocio.Administrador.Instancia objInstancia = new PNegocio.Administrador.Instancia();

                string user = this.txtUsuario.Text.Trim();
                string descripcion = this.txtDescripcion.Text.Trim();
                string password = this.txtPassword.Text.Trim();
                string endpoint = this.txtEndpoint.Text.Trim();
                if (this.txtPassword.Text.Trim() == this.txtRepitePassword.Text.Trim() &&
                    this.txtUsuario.Text.Trim() == this.txtRepiteUsuario.Text.Trim()
                    )
                {
                    try
                    {
                       
                        string mensaje = "";
                        PNegocio.Encript encript = new PNegocio.Encript();
                        switch (objInstancia.guardarInstancia(descripcion, user, encript.Encriptar(encript.Encriptar(password)),endpoint))
                        {
                                
                            case "existente":
                                //this.lblResultado.Text = "Ya existe esa descripcion";
                                mensaje = "Ya existe la descripción o el endpoint";                                
                                break;
                            case "insertado":
                                mensaje = "Insertado";
                                string[] userPass = new string[2];
                                userPass[0] = user;
                                userPass[1] = encript.Encriptar(encript.Encriptar(password));
                                try
                                {

                                    string RFC = obneterRFCPorSociedad(endpoint, userPass, this.txtMiSociedad.Text.Trim());
                                    if (RFC != "" && RFC != null)
                                    {
                                        PNegocio.Administrador.Instancia instancia = new PNegocio.Administrador.Instancia();
                                        string sqlString = "update instancia set RFC = '" + RFC + "', sociedad = '" + this.txtMiSociedad.Text.Trim() + "'  where descripcion like '" + descripcion + "' and usuario = '" + userPass[0] + "' and pass = '" + userPass[1] + "'; select @@ROWCOUNT";
                                        List<string[]> res = null;
                                        res = instancia.insertarRFCPorInstancia(sqlString);
                                    }
                                    else {
                                        mensaje = "La instancia fue  insertada pero no regresó respuesta alguna, verifique que: <br/>  1.- Sus datos sean correctos <br/> 2.- Que la instancia este en funcionamiento <br/> 3.- Que la sociedad sea la que le pertenece de lo contrario no podremos conocer su RFC vacio";       
                                        
                                    }
                                    
                                }
                                catch (Exception)
                                {
                                    mensaje = "La instancia fue  insertada pero no regresó respuesta alguna, verifique que: <br/>  1.- Sus datos sean correctos <br/> 2.- Que la instancia este en funcionamiento <br/> 3.- Que la sociedad sea la que le pertenece de lo contrario no podremos conocer su RFC ssd";       

                                }
                                

                                mostrarTablaInstancias();
                                //Session["textoDialogo"] = "insertado";
                                //mensaje = "Insertado correctamente";
                                //this.lblExplicacionInstancias.Text = "";
                                //if (this.lblTabla.Text == "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
                                //{
                                //    this.lblTabla.Text = "";
                                //}
                                //this.lblResultado.Text = "se inserto correctamente, recargue para ver los cambios <a href='instancia.aspx'>recargar</a>";
                                
                                //Response.Redirect(Request.RawUrl);

                                //string script = @"<script type='text/javascript'>alert('" + "insertado correctamente" + "');</script>";
                                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                                break;
                            case "error":
                                //this.lblResultado.Text = "Hubo un error en la insercion";
                                mensaje = "Error en la inserción";
                                //Response.Redirect(Request.RawUrl);
                                break;
                        }
                        //Session["textoDialogo"] = mensaje;
                        this.lblDialog.Text = mensaje;
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
                        //string script = @"<script type='text/javascript'>alert('" + mensaje + "');</script>";
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    }

                    catch (Exception)
                    {
                        //this.lblResultado.Text = "No habia conexion";
                        //Session["textoDialogo"] = "No se encontro la conexion a la base de datos, intente nuevamente";
                        this.lblDialog.Text = "No se encontró la conexión a la base de datos, intente nuevamente";
                        
                    }
                }
                else
                {
                    //this.lblResultado.Text = "Usuario o contraseña no coinciden";
                    //Session["textoDialogo"] = "Usuario o contraseña no coinciden";
                    this.lblDialog.Text = "Contraseña o usuario no coinciden";
                    
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
        }

        protected void btnEditaInstancia_Click(object sender, EventArgs e)
        {
            string verificar = this.hidVerificar.Value.Trim() ;
            
            if (verificar == "si")
            {
                try
                {
                    int sociedad = int.Parse(this.txtMiSociedad.Text.Trim());
                    actualizaInstancia();

                }
                catch (Exception)
                {
                    this.lblDialog.Text = "La sociedad solo permite numeros";
                }
                
            }
            else {
                this.lblDialog.Text = "Existen campos vacios";
                activarMensageDialog();
            }
           
            
        }

        public void actualizaInstancia() {
            if (
                //this.txtPassCambiar.Text == this.hidPsAnt.Value.Trim()
                 this.txtPassword.Text == this.txtRepitePassword.Text
                && this.txtUsuario.Text == this.txtRepiteUsuario.Text
                )
            {
                PNegocio.Encript encrypt = new PNegocio.Encript();
                PNegocio.Administrador.Instancia objInstancia = new PNegocio.Administrador.Instancia();
                string res;
                res = objInstancia.actualizarInstancia(this.hidIdAnt.Value, this.txtDescripcion.Text,
                this.txtUsuario.Text, encrypt.Encriptar(encrypt.Encriptar(this.txtPassword.Text.Trim())), this.txtEndpoint.Text.Trim());
                if (res == "actualizado")
                {
                    Session["textoDialogo"] = "Actualizado correctamente";
                    try
                    {
                        if (this.txtEndpoint.Text.Trim() != null && this.txtEndpoint.Text.Trim() != "")
                        {
                            
                            string[] userPass = new string[2];
                            userPass[0] = this.txtUsuario.Text.Trim();
                            userPass[1] = encrypt.Encriptar(encrypt.Encriptar(this.txtPassword.Text.Trim()));
                            string RFC = obneterRFCPorSociedad(this.txtEndpoint.Text.Trim(), userPass, this.txtMiSociedad.Text.Trim());
                            if (RFC != "" && RFC != null)
                            {
                                PNegocio.Administrador.Instancia instancia = new PNegocio.Administrador.Instancia();
                                string sqlString = "update instancia set RFC = '" + RFC + "', sociedad = '" + this.txtMiSociedad.Text.Trim() + "' where endpointAdd like '" + this.txtEndpoint.Text.Trim() + "' and usuario = '" + userPass[0] + "' and pass = '" + userPass[1] + "'; select @@ROWCOUNT ";
                                List<string[]> result = null;
                                result = instancia.insertarRFCPorInstancia(sqlString);   
                            }
                            else {
                                Session["textoDialogo"] = "La instancia fue  actualizada pero no regresó respuesta alguna, verifique que: <br/>  1.- Sus datos sean correctos <br/> 2.- Que la instancia este en funcionamiento <br/> 3.- Que la sociedad sea la que le pertenece de lo contrario no podremos conocer su RFC";       
                                        
                            }
                                 
                        }
                    }
                    catch (Exception)
                    {
                        Session["textoDialogo"] = "La instancia fue  actualizada pero no regresó respuesta alguna, verifique que: <br/>  1.- Sus datos sean correctos <br/> 2.- Que la instancia este en funcionamiento <br/> 3.- Que la sociedad sea la que le pertenece de lo contrario no podremos conocer su RFC";       

                    }
                    //this.lblDialog.Text = "Actualizado correctamente";
                    Response.Redirect("instancia.aspx");
                    //mostrarTablaInstancias();
                }
                else
                {
                    switch (res)
                    {
                        case "existente":
                            this.lblDialog.Text = "La descripción o el edpoint ya están registrados";
                            break;
                        default:
                            this.lblDialog.Text = "Ocurrió algún error, intente de nuevo";
                            break;
                    }
                    //this.lblDialog.Text = "Algunos datos no coinciden";
                    mostrarTablaInstancias();
                    //Session["textoDialogo"] = "Algunos datos no coinciden";
                    //Response.Redirect("instancia.aspx");
                    //this.lblResultado.Text = "Algunos datos no coinciden";
                }

                //Response.Redirect("instancia.aspx");
                //this.lblResultado.Text = "Se actualizo correctamente, clic aqui para <a href='instancia.aspx'>refrescar</a>";
                //mostrarTablaInstancias();
            }
            else {
                this.lblDialog.Text = "Contraseña o usuario no coinciden";
            }
            activarMensageDialog();
            //Response.Redirect("instancia.aspx");
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("instancia.aspx");
        }

        //protected void btnEditaInstancia(object sender, EventArgs e)
        //{
        //    Response.Redirect("instancia.aspx");
        //}
        private void activarMensageDialog()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

        }
        
    }
}