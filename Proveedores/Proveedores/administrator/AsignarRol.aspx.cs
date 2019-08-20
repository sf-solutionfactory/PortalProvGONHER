using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class AsignarRol : System.Web.UI.Page
    {
       

        List<String[]> listaUsuarios;
        List<String[]> listRoles;
        String prov = "";
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
            this.lblLeyenda.Text = "";
            
            String nombre = "";
            String descripcion = "";
            try
            {
                prov = Request.QueryString["rfc"];
                nombre = Request.QueryString["nom"];
                descripcion = Request.QueryString["desc"];

                this.hidProv.Value = prov;
                this.hidComplementoUr.Value = "?rfc="+prov+"&nom="+nombre+"&desc="+descripcion;
                this.lblProveedorSelected.Text = nombre + " " + descripcion;
                if (prov != "" && prov != null && !IsPostBack)
                {
                    //cargar usuarios
                    cmbUsuarios.Items.Clear();
                    listaUsuarios= new PNegocio.Administrador.Usuario().cosultarUsuariosPorFiltro(prov);
                    llenarDropDownUser(listaUsuarios);
                    //Cargar roles
                    listRoles = new PNegocio.Administrador.Roles().consultarRolesArray();
                    llenarDropDownRol(listRoles);
                }
                cagarTablaGrupoRoles();
            }
            catch (Exception)
            {

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

        public void llenarDropDownUser(List<string[]> lista)
        {
            for (int i = 1; i < lista.Count() ; i++)
            {
                cmbUsuarios.Items.Add(lista[i][0].ToString());
            }
        }

        public void llenarDropDownRol(List<string[]> lista)
        {
            for (int i = 1; i < lista.Count(); i++)
            {
                cmbRol.Items.Add(lista[i][1].ToString());
            }
        }


        protected void cargarRoles(object sender, EventArgs e)
        {
        }

        public void tomarRol(object sender, EventArgs e)
        {
        }

        protected void btnAsignarRol_Click(object sender, EventArgs e)
        {
            if(this.hidVerificar.Value == "si"){
                asignarRol();
            }
        }

        public void asignarRol() {
            PNegocio.Administrador.Roles objInstancia = new PNegocio.Administrador.Roles();
            try
            {
                int i = this.cmbUsuarios.SelectedIndex;
                switch (objInstancia.asignarRol(this.cmbUsuarios.Text.Trim(), this.cmbRol.Text.Trim(), "1"))
                {
                    case "0":
                        this.lblUsuario.Text = "No se inserto";
                        break;
                    case "1":
                        this.lblUsuario.Text = "Se inserto correctamente";
                        cagarTablaGrupoRoles();
                        break;
                    case "el usuario ya esta asignado":
                        this.lblUsuario.Text = "El usuario ya esta asignado";
                        break;
                    case "el usuario no existe":
                        this.lblUsuario.Text = "El usuario no existe";
                        break;
                    case "envio un modo desconocido":
                        this.lblUsuario.Text = "Envió un modo desconocido";
                        break;
                    default:
                        this.lblUsuario.Text = "Ocurrió algún error desconocido, posiblemente dejo de funcionar su servicio de BD";
                        break;
                }
            }
            catch (Exception)
            {
                this.lblUsuario.Text = "No se pudo conectar a la BD";
            } 
        }

        public void seleccionarDatosDeList() {
            
        }

        protected void btnElegir_Click(object sender, EventArgs e)
        {
            seleccionarDatosDeList();
        }

        private void cagarTablaGrupoRoles() {
            this.lblTablaGrupoRoles.Text = new PNegocio.Administrador.Roles().ejcPsdConsultaRolPorProvedor(prov);
            if (this.lblTablaGrupoRoles.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblLeyenda.Text = "<strong>Estos son los usuarios pertenecientes a este proveedor que ya estan asignados:</strong>";
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltro();
            }
            else
            {
            }
            
        }

    }

        
    

}