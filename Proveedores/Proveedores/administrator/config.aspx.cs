using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace Proveedores.administrator
{
    public partial class config : System.Web.UI.Page
    {
        string estatus;
        string umbral;
        string formaRegistro;
        string email;
        string contrasena;
        string confirmaContrasena;
        string asunto;
        string contenido;
        string numPassRec;
        string maxIntentos;
        string intervaloTiempoBloq;
        string caducidadPass;
        string bloqSociedad;
        

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
                else {
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

            List<string[]> datosCOnfiguracion;
            string verBotones = "";
            try
            {
                verBotones = Request.QueryString["dir"].ToString();
            }
            catch (Exception)
            {

            }
            if (String.IsNullOrEmpty( hidloadconfpass.Value) )
            {
                cargarConfigPass();
            }
            
            if (!IsPostBack && verBotones == "verAnt")
            {
                datosCOnfiguracion = PNegocio.Administrador.Configuracion.consultarConfiguracion("anterior");
                this.lblVernterior.Visible = false;
                this.lblAdvertencia.Text = "Esto solo es una visualización de los datos anteriores, si presiona el botón se restablecerá la configuración completa tomando en cuenta las credenciales utilizadas ";
                this.lblInfoGuardar.Text = "Puede realizar cambios sobre los datos cargados y guardar:  ";
                this.lblLeyendaConfigAnterior.Visible = false;
                this.lblLeyendaConfigAnterior2.Visible = true;
                this.btnRecuperarAnterior.Visible = true;
                this.btnCancel.Visible = true;
                cargarDatosEstatusActivo(datosCOnfiguracion);
            }
            else {
                if (!IsPostBack)
                {
                    datosCOnfiguracion = PNegocio.Administrador.Configuracion.consultarConfiguracion("activo");
                    //this.lblAdvertencia.Text = "";
                    this.lblVernterior.Text = "Ver anterior";
                    this.lblLeyendaConfigAnterior2.Visible = false;
                    this.lblLeyendaConfigAnterior.Visible = true;
                    this.btnRecuperarAnterior.Visible = false;
                    this.btnCancel.Visible = false;
                    cargarDatosEstatusActivo(datosCOnfiguracion);
                }

                if (verBotones == "verAnt")
                {
                    
                }
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

        protected void btnEnviarConfig_Click(object sender, EventArgs e)
        {
            string  d = this.txtEmail.Text;
            if (this.hidVerificar.Value == "noEmail")
            {
                this.lblDialog.Text = "El email no cumple con las características necesarias";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

            }
            else if(this.hidVerificar.Value == "no"){
                this.lblDialog.Text = "Existen campos sin datos";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

            }
            else if (this.hidVerificar.Value == "noNumeros")
            {
                this.lblDialog.Text = "Existen campos invalidos, debe introducir solo numeros en algunos campos, verifiquelo";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

            }
            else if (this.hidVerificar.Value == "si")
            {
                guardarConfigPass();
                if (this.hidTipoCorreo.Value.Trim() == "configurado")
                {
                    if (
                        this.txtSMTP.Text.Trim() != "" && this.txtSMTP.Text.Trim() != null &&
                        this.txtPuerto.Text != "" && this.txtPuerto.Text.Trim() != null // &&
                        //this.txtCorreoServidorCorreo.Text != "" && this.txtCorreoServidorCorreo.Text.Trim() != null &&
                        //this.txtContraServidorCorreo.Text != "" && this.txtContraServidorCorreo.Text.Trim() != null &&
                        //this.txtRepiteContraServidorCorreo.Text != "" && this.txtRepiteContraServidorCorreo.Text.Trim() != null
                        )
                    {
                        ejecutaprcedureUpdateConfig();
                    }
                    else {
                        this.lblDialog.Text = "Sí desea utilizar la configuración especial de correo debe llenar todos los campos";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

                    }


                }
                else if (this.hidTipoCorreo.Value.Trim() == "normal")
                {
                    ejecutaprcedureUpdateConfig();
                }
                
            }
            
        }

        protected void btnRecuperarAnterior_Click(object sender, EventArgs e)
        {
                ejecutaprocedureBackupCPnfig();
        }

        public void ejecutaprocedureBackupCPnfig() {
            try 
	        {	        
		        
            if(PNegocio.Administrador.Configuracion.backupCOnfig() == "ok" ){
                Session["textoDialogo"] = "Se recuperó la configuración correctamente";
                Response.Redirect("~/administrator/config.aspx", false);
            }
            else{
                this.lblDialog.Text = "No fue posible realizar el cambio";
            }
	        }
	        catch (Exception)
	        {
                this.lblDialog.Text = "No fue posible conectarse a la BD";
	        }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

            
            

        }

        public void ejecutaprcedureUpdateConfig()
        {

            bool SSL = true; // 
            int puerto = 587;
            string smtp = "";

            estatus = this.rdbPortal.Text.Trim();
            umbral = this.txtUmbral.Text.Trim();
            formaRegistro = "Propio";

            string sufijoEmail = this.dpdSufijoEmail.Text.Trim();
            string stringSQLComplemento = "";
            string identificador = "";

            if (this.hidTipoCorreo.Value.Trim() == "normal")
            {
                email = this.txtEmail.Text.Trim() + sufijoEmail;
                contrasena = txtPassword.Text.Trim();
                confirmaContrasena = txtConfirmPassword.Text.Trim();

                switch(sufijoEmail){
                    case "@hotmail.com":
                        smtp = "smtp.live.com";
                        break;
                    case "@gmail.com":
                        smtp = "smtp.gmail.com";
                        break;
                    case "@outlook.com":
                        smtp = "smtp.live.com";
                        break;
                    case "@yahoo.com":
                        smtp = "smtp.mail.yahoo.com";
                        break;
                }

                identificador = sufijoEmail;
                
            }

            else if (this.hidTipoCorreo.Value.Trim() == "configurado")
            {
                email = this.txtCorreoServidorCorreo.Text.Trim();
                contrasena = txtContraServidorCorreo.Text.Trim();
                confirmaContrasena = txtRepiteContraServidorCorreo.Text.Trim();

                SSL = false;
                if (this.chkSSL.Checked)
                {
                    SSL = true;
                }
                 puerto = int.Parse(this.txtPuerto.Text.Trim());
                 smtp = this.txtSMTP.Text.Trim();

                 identificador = "configurado";
            }

            string SSLString = "1";

            if (SSL) {
                SSLString = "1";
            }
            if (!SSL)
            {
                SSLString = "0";
            }

            stringSQLComplemento = "update email set SMTPAdd = '" + smtp + "',puerto = " + puerto + ", SSLOpt = " + SSLString + " where sufijo = '" + identificador + "'";

            asunto = txtAsunto.Text.Trim();
            contenido = this.txtAreaContenido.Text.Trim();
            numPassRec = txtNumPassRec.Text.Trim();
            maxIntentos = txtMaxIntentos.Text.Trim();
            intervaloTiempoBloq = txtIntervalo.Text.Trim();
            caducidadPass = txtCaducidadPass.Text.Trim();
            bloqSociedad = this.dpdBloqSociedad.Text.Trim();

            if (estatus.Equals("Activado"))
            {
                estatus = "1";
            }
            else
            {
                estatus = "0";
            }
            
            if (bloqSociedad.Equals("1 = N"))
            {
                bloqSociedad = "1";
            }
            else
            {
                bloqSociedad = "0";
            }
            if (formaRegistro.Equals("Propio"))
            {
                formaRegistro = "1";
            }
            else
            {
                formaRegistro = "0";
            }

            string tiempoBloqAdmin = this.txtTiempoBloqAdmin.Text.Trim();
            if (tiempoBloqAdmin == "")
            {
                tiempoBloqAdmin = "0";
            }

            if (contrasena == confirmaContrasena)
            {
                PNegocio.Administrador.Configuracion conf = new PNegocio.Administrador.Configuracion();

                PNegocio.Encript encript = new PNegocio.Encript();
                string passwd = encript.Encriptar(encript.Encriptar(contrasena));
                conf.guardarConfiguracion(estatus, asunto, contenido, email, passwd, formaRegistro, 
                    umbral, bloqSociedad, numPassRec, intervaloTiempoBloq, maxIntentos, "1", caducidadPass,
                    identificador, stringSQLComplemento, tiempoBloqAdmin, this.txtMaxXML.Text);
                string mensaje = "";
                mensaje = "La configuración fue guardada correctamente";
                List<string> listaEmail = new List<string>();
                listaEmail.Add(email);
                bool enviadoEmail = PNegocio.EnviarEmail.SendMail(listaEmail, null, null, "Administrador", null);
                if (enviadoEmail)
                {
                    mensaje += ", sus datos de correo electrónico fueron configurados correctamente, verifique la bandeja de entrada del correo que utilizo en la configuración para ver el resultado de su configuración";
                }
                else {
                    mensaje += ", sin embargo no fue posible enviar el resultado a la bandeja de entrada del correo que introdujo en la configuración, verifique que los datos sean correctos";
 
                }
                Session["textoDialogo"] = mensaje;
                Response.Redirect("config.aspx");
            }
            else
            {
                this.lblDialog.Text = "Las contraseñas no coinciden";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

        }

        public void cargarDatosEstatusActivo(List<string[]> datosCOnfiguracion)
        {
            quitarSelectDropsDown();

            if (datosCOnfiguracion[1][0].ToString() == "True")
            {
                this.rdbPortal.Items.FindByText("Activado").Selected = true;
            }
            else {
                this.rdbPortal.Items.FindByText("Desactivado").Selected = true;
            }

            this.txtAsunto.Text = datosCOnfiguracion[1][1].ToString();
            this.txtAreaContenido.Text = datosCOnfiguracion[1][2].ToString();
            if (datosCOnfiguracion[1][3].ToString().Trim() != "" && datosCOnfiguracion[1][3].ToString().Trim() != null)
            {
                if (datosCOnfiguracion[1][12].ToString().Trim() == "configurado" || datosCOnfiguracion[1][12].ToString().Trim() == "configuradoAnterior")
                {
                    this.hidTipoCorreo.Value = "configurado";
                    List<string[]> list = PNegocio.Administrador.Configuracion.consultarConfiguracionEmail(datosCOnfiguracion[1][12].ToString().Trim()); // SMTPAdd,puerto,SSLOpt
                    this.txtSMTP.Text = list[1][0];
                    this.txtPuerto.Text = list[1][1];
                    this.chkSSL.Checked = false;
                    if(list[1][2].ToString().Trim() == "True"){
                        this.chkSSL.Checked = true;
                    }

                    this.txtCorreoServidorCorreo.Text = datosCOnfiguracion[1][3].ToString().Trim();
                    //string[] emailSinSufijo= datosCOnfiguracion[1][3].ToString().Split(new Char[] { '@' });
                    //this.txtCorreoServidorCorreo.Text = this.txtEmail.Text = emailSinSufijo[0];
                   
                    
                }
                else {
                    string[] emailSinSufijo= datosCOnfiguracion[1][3].ToString().Split(new Char[] { '@' });
                    this.txtEmail.Text = emailSinSufijo[0];
                    string txtSufijo = emailSinSufijo[1].ToString().Trim();
                    txtSufijo = "@" + txtSufijo;
                    this.dpdSufijoEmail.Items.FindByText(txtSufijo).Selected = true;
                    //this.dpdSufijoEmail.Text = txtSufijo;
                }
                
            }
            this.txtUmbral.Text = datosCOnfiguracion[1][5].ToString();

            if (datosCOnfiguracion[1][6].ToString() == "True")
            {
                this.dpdBloqSociedad.Items.FindByText("1 = N").Selected = true;
            }
            else {
                this.dpdBloqSociedad.Items.FindByText("1 = 1").Selected = true;
            }


            this.txtNumPassRec.Text = datosCOnfiguracion[1][7].ToString();
            this.txtIntervalo.Text = datosCOnfiguracion[1][8].ToString();
            this.txtMaxIntentos.Text = datosCOnfiguracion[1][9].ToString();
            this.txtCaducidadPass.Text = datosCOnfiguracion[1][11].ToString();

            string configPor = datosCOnfiguracion[1][10].ToString();

            this.txtTiempoBloqAdmin.Text = datosCOnfiguracion[1][13].ToString();

            this.txtMaxXML.Text = datosCOnfiguracion[1][14].ToString(); 

        }

        private void quitarSelectDropsDown() {
            this.dpdBloqSociedad.SelectedItem.Selected = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("config.aspx");
        }
        /// <summary>
        /// Guarda la configuracion de la contra seña en un txt.
        /// </summary>
        private void guardarConfigPass()
        {
            string[] lines = { txtNumeroLetras.Text, txtNumeroLetrasM.Text, 
                                 txtCantidadNumeros.Text, txtNumeroCaracteres.Text };
            System.IO.File.WriteAllLines(@"C:\temp\config.txt", lines);
            hidloadconfpass.Value = null;
        }
        /// <summary>
        /// Carga la configuracion que tendra para ingresar la contraseña.
        /// </summary>
        private void cargarConfigPass()
        {
            if (File.Exists(@"C:\temp\config.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\temp\config.txt");
                txtNumeroLetras.Text = lines[0];
                txtNumeroLetrasM.Text = lines[1];
                txtCantidadNumeros.Text = lines[2];
                txtNumeroCaracteres.Text = lines[3];
            }
            else
            {
                txtNumeroLetras.Text = "1";
                txtNumeroLetrasM.Text = "1";
                txtCantidadNumeros.Text = "1";
                txtNumeroCaracteres.Text = "8";
            }
            hidloadconfpass.Value = "false";
        }

    }
}