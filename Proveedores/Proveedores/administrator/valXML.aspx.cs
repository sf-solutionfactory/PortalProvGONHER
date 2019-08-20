using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class valXML : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //this.litTablaGrpValidaciones.Text = this.convertToHtmlTable(new PNegocio.Administrador.ValidacionXML().getGrupoValidacionXML(),"tblGrupoValidaciones","tblComun");
        }

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
                    this.pnlNuevo.Visible = false;
                    this.rdbPantalla.Items.FindByValue("1").Selected = true;   //escoger al inicio mostrar validaciones para MIR7
                    Session["idPants"] = 1;
                    //this.rdbPantalla.Items.FindByValue("32").Selected = true;   //escoger al inicio mostrar validaciones para FV60
                    //Session["idPants"] = 32;
                }
                try
                {
                    this.GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    this.lblSinRegistros.Visible = false;
                }
                catch
                {
                    this.lblSinRegistros.Visible = true;
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

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("idGrupoValidacion");
                this.txtNombreGrupo.Text = "";
                foreach (ListItem item in this.chbListValidaciones.Items)
                {
                    item.Selected = false;
                }
                this.btnGuardar.Visible = true;
                this.pnlNuevo.Visible = true;

                this.lstBoxSelectedProv.Items.Clear();
                this.lstBoxNoSelectedProv.DataBind();
                this.btnEliminar.Visible = false;
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            List<int> idValidaciones = new List<int>();
            foreach (ListItem validacion in this.chbListValidaciones.Items)
            {
                if (validacion.Selected)
                {
                    idValidaciones.Add(int.Parse(validacion.Value));
                }
            }

            List<int> idProveedores = new List<int>();
            foreach (ListItem proveedor in this.lstBoxSelectedProv.Items)
            {
                idProveedores.Add(int.Parse(proveedor.Value));
            }

            int idGrupoValidacion = 0;
            if (Session["idGrupoValidacion"] != null)
            { //Cuando si tiene un numero, entonces la operación va a hacer para guardar cambios
                idGrupoValidacion = int.Parse(Session["idGrupoValidacion"].ToString());
            }

            string mensajeJS = "";
            if (idValidaciones.Count != 0)
            {
                if (this.txtNombreGrupo.Text.Trim().Length != 0)
                {
                    if (idProveedores.Count != 0)
                    {

                        string res = new PNegocio.Administrador.ValidacionXML().guardarNuevoGrupoValidacion(idGrupoValidacion, this.txtNombreGrupo.Text, idValidaciones, idProveedores, int.Parse(Session["idPants"].ToString()));
                        if (res == "correcto")
                        {
                            this.GridView1.DataBind();
                            this.GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                            this.pnlNuevo.Visible = false;
                            Session.Remove("idGrupoValidacion");

                            mensajeJS = "Proceso realizado satisfactoriamente";
                        }
                        else
                        {
                            mensajeJS = "Error. Error al intentar guardar el grupo de validación.";
                        }
                    }
                    else
                    {
                        mensajeJS = "Advertencia. Debe de seleccionar proveedores para agregar al grupo de validación";
                    }
                }
                else
                {
                    mensajeJS = "Advertencia. Debe de escribir el nombre para el grupo de validación";
                }

            }
            else
            {
                mensajeJS = "Elija alguna validación para este grupo";
            }
            if (mensajeJS.Length != 0)
            {
                this.lblDialog.Text = mensajeJS;
                //Session["textoDialogo"] = mensajeJS;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
                //string script = @"<script type='text/javascript'>alert('" + mensajeJS + "');</script>";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
            if (this.GridView1.Rows.Count != 0)
            {
                this.lblSinRegistros.Visible = false;
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.pnlNuevo.Visible = true;

            int idGrupoVal = int.Parse(this.GridView1.Rows[e.NewEditIndex].Cells[0].Text);
            Session["idGrupoValidacion"] = idGrupoVal;
            this.lstBoxSelectedProv.DataBind();

            int[] idValidaciones = new PNegocio.Administrador.ValidacionXML().getValFactByIdGrpVal(idGrupoVal);

            string nombre = HttpUtility.HtmlDecode(this.GridView1.Rows[e.NewEditIndex].Cells[1].Text);
            this.txtNombreGrupo.Text = nombre.Trim();

            this.chbListValidaciones.DataBind();
            foreach (ListItem item in this.chbListValidaciones.Items)
            {
                for (int i = 0; i < idValidaciones.Length; i++)
                {
                    if (int.Parse(item.Value) == idValidaciones[i])
                    {
                        item.Selected = true;
                    }
                }
            }
            e.Cancel = true;
            this.btnEliminar.Visible = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lstBoxNoSelectedProv.SelectedItem != null)
            {
                for (int i = this.lstBoxNoSelectedProv.Items.Count - 1; i >= 0; i--)
                {
                    if (this.lstBoxNoSelectedProv.Items[i].Selected)
                    {
                        this.lstBoxSelectedProv.Items.Add(this.lstBoxNoSelectedProv.Items[i]);
                        this.lstBoxNoSelectedProv.Items.RemoveAt(i);
                    }
                }
            }

            //Para de-seleccionar todos los items recien pasados
            foreach (ListItem item in this.lstBoxSelectedProv.Items)
            {
                item.Selected = false;
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lstBoxSelectedProv.SelectedItem != null)
            {
                for (int i = this.lstBoxSelectedProv.Items.Count - 1; i >= 0; i--)
                {
                    if (this.lstBoxSelectedProv.Items[i].Selected)
                    {
                        this.lstBoxNoSelectedProv.Items.Add(this.lstBoxSelectedProv.Items[i]);
                        this.lstBoxSelectedProv.Items.RemoveAt(i);

                    }
                }
            }

            //Para de-seleccionar los items recien pasados
            foreach (ListItem item in this.lstBoxNoSelectedProv.Items)
            {
                item.Selected = false;
            }
        }

        protected void btnAddTodo_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in this.lstBoxNoSelectedProv.Items)
            {
                this.lstBoxSelectedProv.Items.Add(item);
            }
            this.lstBoxNoSelectedProv.Items.Clear();
        }

        protected void btnRemoveTodo_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in this.lstBoxSelectedProv.Items)
            {
                this.lstBoxNoSelectedProv.Items.Add(item);
            }
            this.lstBoxSelectedProv.Items.Clear();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.pnlNuevo.Visible = false;
            Session.Remove("idGrupoValidacion");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idGrupoValidacion = int.Parse(Session["idGrupoValidacion"].ToString());
            if (new PNegocio.Administrador.ValidacionXML().eliminarGrupoValidacion(idGrupoValidacion))
            {
                this.GridView1.DataBind();
                this.GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                Session.Remove("idGrupoValidacion");
                this.pnlNuevo.Visible = false;
                if (this.GridView1.Rows.Count == 0)
                {
                    this.lblSinRegistros.Visible = true;
                }
                this.lblDialog.Text = "Eliminado correctamente";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

            }
        }

        protected void rdbPantalla_SelectedIndexChanged(object sender, EventArgs e)  //mgv: escoge el tipo de validacion a presentar, en base a que pantalla
        {
            if (this.rdbPantalla.Items.FindByValue("1").Selected)
            {
                Session["idPants"] = 1;
            }
            else if (this.rdbPantalla.Items.FindByValue("32").Selected)
            {
                Session["idPants"] = 32;
            }
            this.lstBoxNoSelectedProv.DataBind();   //mgv:actualizar la lista de proveedores NO seleccionados
        }
    }
}