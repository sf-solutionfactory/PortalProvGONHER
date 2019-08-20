using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class Noticia : System.Web.UI.Page
    {
        string codeHtmlContenido;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnCancelar.Visible = false;
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
                mostrarTablarNoticias();
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

        public void cargarEdit()
        {
            try
            {
                if (Request.QueryString["toEdit"] != "" && Request.QueryString["toEdit"] != null)
                {


                    PNegocio.Administrador.Noticia objInstancia = new PNegocio.Administrador.Noticia();

                    string sqlString = "select idnoticia as Nº_noticia, Titulo, cuerpo,fechaInicio, fechaFin,urlImagen, tipoNoticia  from noticia where idNoticia = " + Request.QueryString["toEdit"] + ";";
                    List<string[]> lista = objInstancia.consultarNoticiaPorId(sqlString);
                    if (lista.Count > 1)
                    {
                        this.txtTitulo.Text = lista[1][1];
                        this.hidContenido.Value = lista[1][2];
                        this.datepicker.Text = Gen.Util.CS.Gen.convertirFechaBDaFormatoJq(lista[1][3].ToString());
                        this.datepicker2.Text = Gen.Util.CS.Gen.convertirFechaBDaFormatoJq(lista[1][4].ToString());
                        this.txtURLImagen.Text = lista[1][5];
                        this.hidIdAnt.Value = lista[1][0];

                        if( lista[1][6] == "False"){
                            this.rdbTipoNoticia.Items.FindByText("General").Selected = false;
                            this.rdbTipoNoticia.Items.FindByText("Asignable").Selected = true;
                        }

                        this.btnGuardar.Visible = false;
                        this.btnCancelar.Visible = true;
                    }
                }
                else {
                    this.btnModificar.Visible = false;
                }

            }
            catch (Exception)
            {
                this.btnModificar.Visible = false;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string verificar = this.hidVerificar.Value;
            if (verificar == "si")
            {
                if (validarImagen(this.txtURLImagen.Text.Trim()))
                {
                    insertar();
                }
                else
                {
                    if (this.txtURLImagen.Text.Trim() == null || this.txtURLImagen.Text.Trim() == "")
                    {
                        insertar();
                    }
                    else
                    {
                        this.lblDialog.Text = "El formato de la imagen no es un formato valido, puede ser: .jpg., .png, .gif, .bmp, .tif, .gif";
                    }

                }
            }
            else if (verificar == "noCalendario")
            {
                this.lblDialog.Text = "Las fechas no cumplen con el formato adecuado";
            }
            else {
                this.lblDialog.Text = "Existen campos vacios";  
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

        }

        public void insertar() {
            int tipoNoticia = 1;
            if (this.rdbTipoNoticia.Text.Trim() != "General")
            {
                tipoNoticia = 0;
            }
            codeHtmlContenido = hidContenido.Value;
            codeHtmlContenido = codeHtmlContenido.Replace("[", "<");
            codeHtmlContenido = codeHtmlContenido.Replace("]", ">");
            string res =
                     new PNegocio.Administrador.Noticia().insertarNoticia(
                                   this.txtTitulo.Text, codeHtmlContenido,
                                   Gen.Util.CS.Gen.convertirFecha(this.datepicker.Text), Gen.Util.CS.Gen.convertirFecha(this.datepicker2.Text),
                                   this.txtURLImagen.Text.Trim(), tipoNoticia
                                   );
            if (res == "correcto")
            {
                if (tipoNoticia == 0)
                {
                    enviarCorreoGeneral();     
                }
                Session["textoDialogo"] = "Insertado correctamente";
                Response.Redirect("Noticia.aspx");
                
            }
            else
            {
                this.lblDialog.Text = res;
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

            mostrarTablarNoticias();
        }

        private void enviarCorreoGeneral() {
            //List<string> listaEmail = new List<string>();

            //listaEmail.Add("emails");
            //bool enviadoEmail = PNegocio.EnviarEmail.SendMail(listaEmail);
        }

        public bool validarImagen(string ruta)
        {
            ruta = ruta.ToLower();
            if (ruta.EndsWith(".jpg", StringComparison.Ordinal) ||
                ruta.EndsWith(".png", StringComparison.Ordinal) ||
                ruta.EndsWith(".gif", StringComparison.Ordinal) ||
                ruta.EndsWith(".bmp", StringComparison.Ordinal) ||
                ruta.EndsWith(".tif", StringComparison.Ordinal) ||
                ruta.EndsWith(".gif", StringComparison.Ordinal)
                )
            {
                return true;
            }
            return false;
        }

        private void mostrarTablarNoticias()
        {
            this.lblTablaNoticias.Text = new PNegocio.Administrador.Noticia().consultarNoticias("90%");
            if (this.lblTablaNoticias.Text != "<div>No se encontraron resultados para mostrar en la tabla</div>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltro();
                this.lblDescribeResultados.Text = "<strong>Estas son las noticias existentes:</strong>";
            }
            else {
                this.lblTablaNoticias.Text = "<strong>No se encontraron resultados para mostrar en la tabla</strong>";   
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string verificar = this.hidVerificar.Value;
            if (verificar == "si")
            {
                if (validarImagen(this.txtURLImagen.Text.Trim()))
                {
                    modificarNoticia();
                }
                else
                {
                    if (this.txtURLImagen.Text.Trim() == null || this.txtURLImagen.Text.Trim() == "")
                    {
                        modificarNoticia();
                    }
                    else
                    {
                        this.lblDialog.Text = "El formato de la imagen no es un formato valido, puede ser: .jpg., .png, .gif, .bmp, .tif, .gif";
                    }

                }
            }
            else if (verificar == "noCalendario")
            {
                this.lblDialog.Text = "Las fechas no cumplen con el formato adecuado";
            }
            else
            {
                this.lblDialog.Text = "Existen campos vacios";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
            
        }

        public void modificarNoticia() {
            int tipoNoticia = 1;
            if (this.rdbTipoNoticia.Text.Trim() != "General")
            {
                tipoNoticia = 0;
            }
            PNegocio.Administrador.Noticia objInstancia = new PNegocio.Administrador.Noticia();
            codeHtmlContenido = hidContenido.Value;
            codeHtmlContenido = codeHtmlContenido.Replace("[", "<");
            codeHtmlContenido = codeHtmlContenido.Replace("]", ">");
            string res = "";
            res = objInstancia.modificarNoticia(this.hidIdAnt.Value, this.txtTitulo.Text.Trim(), codeHtmlContenido.Trim(), Gen.Util.CS.Gen.convertirFecha(this.datepicker.Text.Trim()), Gen.Util.CS.Gen.convertirFecha(this.datepicker2.Text.Trim()), this.txtURLImagen.Text, tipoNoticia);
            if (res == "correcto")
            {
                Session["textoDialogo"] = "Modificado correctamente";
                Response.Redirect("Noticia.aspx");
                
            }
            else {
                this.lblDialog.Text = "No fue posible realizar el cambio, intente nuevamente";
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Noticia.aspx");
        }

    }
}