using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class instanciaCN : System.Web.UI.Page
    {
        string id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.hidCerrarSesion.Value != "cerrar")
            {
                try
                {
                    string[] resLog = null;
                    resLog = (string[])Session["resLog"];
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
                string dialog = "";    //INICIO mostrar mensaje en dialogo
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
                    mostrarTablaInstancias();
                }
            }
        }

        private string obtenRFCxSociedadCN(string xname, string appsh, string xsapr, string sysn, string xuser, string pasw, string cliente, string sociedad)
        {
            string RFC = "";
            RFC = PNegocio.Administrador.InstanciaCN.RFCxSociedad(xname,  appsh,  xsapr,  sysn,  xuser,  pasw,  cliente, sociedad);
            return RFC;
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
                    PNegocio.Administrador.InstanciaCN objInstancia = new PNegocio.Administrador.InstanciaCN();

                    string sqlString = "SELECT * FROM RfcConfigParams where idRfc = " + Request.QueryString["toEdit"] + ";";
                    List<string[]> lista = objInstancia.consultarInstanciaCNPorId(sqlString);
                    if (lista.Count > 1)
                    {
                        this.hidIdAnt.Value = id = lista[1][0].Trim();
                        this.txtName.Text = lista[1][1].Trim();
                        this.txtAppSH.Text = lista[1][2].Trim();
                        this.txtSAProuter.Text = lista[1][3].Trim();
                        this.txtSystemNumber.Text = lista[1][4].Trim();
                        this.txtUser.Text = lista[1][5].Trim();
                        this.txtPassword.Text = lista[1][6].Trim();
                        this.txtClient.Text = lista[1][7].Trim();
                        this.txtMiSociedad.Text = lista[1][14].Trim();
                        this.btnEjecutaInstanciaCN.Visible = false;
                    }
                }
                else
                {
                    this.btnCancelEdit.Visible = false;
                    this.btnEditaInstanciaCN.Visible = false;
                }
            }
            catch (Exception)
            {
                this.btnCancelEdit.Visible = false;
                this.btnEditaInstanciaCN.Visible = false;
            }
        }

        private void mostrarTablaInstancias()
        {
            this.lblTabla.Text = new PNegocio.Administrador.InstanciaCN().consultarInstanciaCN();
            if (this.lblTabla.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltro();
                this.lblExplicacionInstancias.Text = "<strong>Estas son los conectores a SAP que tenemos en el registro:</strong>";
            }
        }

        protected void btnEjecutaInstancia_Click(object sender, EventArgs e)
        {
            //string verificar = this.hidVerificar.Value;
            //if (verificar == "si")
            //{
            try
            {
                int sociedad = int.Parse(this.txtMiSociedad.Text.Trim());
                tryInsertInstancia();
            }
            catch (Exception)
            {
                this.lblDialog.Text = "La sociedad solo permite numeros";
            }
            //}
            //else
            //{
            //    this.lblDialog.Text = "Existen campos vacios";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
            //}
        }

        private void tryInsertInstancia()
        {
            PNegocio.Administrador.InstanciaCN objInstancia = new PNegocio.Administrador.InstanciaCN();
            string xname = this.txtName.Text.Trim();
            string appsh = this.txtAppSH.Text.Trim();
            string sapR = this.txtSAProuter.Text.Trim();
            string sysNu = this.txtSystemNumber.Text.Trim();
            string xusr = this.txtUser.Text.Trim();
            string passW = this.txtPassword.Text.Trim();
            string xcte = this.txtClient.Text.Trim();

            try
            {
                string mensaje = "";
                switch (objInstancia.guardarInstanciaCN(xname, appsh, sapR, sysNu, xusr, passW, xcte))
                {
                    case "existente":
                        //this.lblResultado.Text = "Ya existe esa descripcion";
                        mensaje = "Ya existe la descripción o el endpoint";
                        break;
                    case "insertado":
                        mensaje = "Insertado";
                        try
                        {
                            string RFC = obtenRFCxSociedadCN(xname, appsh, sapR, sysNu, xusr, passW, xcte, this.txtMiSociedad.Text.Trim());
                            if (RFC != "" && RFC != null)
                            {
                                PNegocio.Administrador.InstanciaCN instanciaCN = new PNegocio.Administrador.InstanciaCN();
                                string sqlString = "update RfcConfigParams set RFC = '" + RFC + "', sociedad = '" + this.txtMiSociedad.Text.Trim() + "'  where Rfc_AppServerHost like '" + appsh + "' and Rfc_SAPRouter = '" + sapR + "'; select @@ROWCOUNT";
                                List<string[]> res = null;
                                res = instanciaCN.insertarRFCxCN(sqlString);
                            }
                            else
                            {
                                mensaje = "La conexión fue insertada pero no regresó respuesta alguna, verifique que: <br/>  1.- Sus datos sean correctos <br/> 2.- Que la conexión este en funcionamiento <br/> 3.- Que la sociedad sea la que le pertenece de lo contrario no podremos conocer su RFC vacio";
                            }
                        }
                        catch (Exception)
                        {
                            mensaje = "La conexión fue  insertada pero no regresó respuesta alguna, verifique que: <br/>  1.- Sus datos sean correctos <br/> 2.- Que la conexión este en funcionamiento <br/> 3.- Que la sociedad sea la que le pertenece de lo contrario no podremos conocer su RFC ssd";
                        }
                        mostrarTablaInstancias();
                        break;
                    case "error":
                        //this.lblResultado.Text = "Hubo un error en la insercion";
                        mensaje = "Error en la inserción";
                        //Response.Redirect(Request.RawUrl);
                        break;
                }
                this.lblDialog.Text = mensaje;
            }
            catch (Exception)
            {
                this.lblDialog.Text = "No se encontró la conexión a la base de datos, intente nuevamente";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
        }

        protected void btnEditaInstancia_Click(object sender, EventArgs e)
        {
            //string verificar = this.hidVerificar.Value.Trim();
            //if (verificar == "si")
            //{
            try
            {
                int sociedad = int.Parse(this.txtMiSociedad.Text.Trim());
            actualizaInstancia();
            }
            catch (Exception)
            {
                this.lblDialog.Text = "La sociedad solo permite numeros";
            }
            //}
            //else
            //{
            //    this.lblDialog.Text = "Existen campos vacios";
            //    activarMensageDialog();
            //}
        }

        public void actualizaInstancia()
        {
            {
                PNegocio.Administrador.InstanciaCN objInstancia = new PNegocio.Administrador.InstanciaCN();
                string res;
                res = objInstancia.actualizarInstanciaCN(this.hidIdAnt.Value, this.txtName.Text, this.txtAppSH.Text, this.txtSAProuter.Text,
                 this.txtSystemNumber.Text, this.txtUser.Text, this.txtPassword.Text, this.txtClient.Text, this.txtMiSociedad.Text );
                if (res == "actualizado")
                {
                    Session["textoDialogo"] = "Actualizado correctamente";
                    try
                    {
                        string RFC = obtenRFCxSociedadCN(this.txtName.Text, this.txtAppSH.Text, this.txtSAProuter.Text, this.txtSystemNumber.Text, this.txtUser.Text, this.txtPassword.Text, this.txtClient.Text, this.txtMiSociedad.Text.Trim());
                        if (RFC != "" && RFC != null)
                        {
                            PNegocio.Administrador.InstanciaCN instanciaCN = new PNegocio.Administrador.InstanciaCN();
                            string sqlString = "update RfcConfigParams set RFC = '" + RFC + "', sociedad = '" + this.txtMiSociedad.Text.Trim() + "'  where Rfc_AppServerHost like '" + this.txtAppSH.Text + "' and Rfc_SAPRouter = '" + this.txtSAProuter.Text + "'; select @@ROWCOUNT";
                            List<string[]> xres = null;
                            xres = instanciaCN.insertarRFCxCN(sqlString);
                        }
                        else
                        {
                            Session["textoDialogo"] = "La conexión fue insertada pero no regresó respuesta alguna, verifique que: <br/>  1.- Sus datos sean correctos <br/> 2.- Que la conexión este en funcionamiento <br/> 3.- Que la sociedad sea la que le pertenece de lo contrario no podremos conocer su RFC vacio";
                        }
                    }
                    catch (Exception)
                    {
                        Session["textoDialogo"] = "La conexión fue  insertada pero no regresó respuesta alguna, verifique que: <br/>  1.- Sus datos sean correctos <br/> 2.- Que la conexión este en funcionamiento <br/> 3.- Que la sociedad sea la que le pertenece de lo contrario no podremos conocer su RFC ssd";
                    }
                    Response.Redirect("instanciaCN.aspx");
                    mostrarTablaInstancias();
                }
                else
                {
                    switch (res)
                    {
                        case "existente":
                            this.lblDialog.Text = "El server o SAP router ya están registrados";
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
            }

            activarMensageDialog();
            //Response.Redirect("instancia.aspx");
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("instanciaCN.aspx");
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