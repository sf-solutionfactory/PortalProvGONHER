using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class CambiarContrasena : System.Web.UI.Page
    {
        string usuari;
        string pass1;
        string pass2;
        string creadoPor;
        List<string[]> listaProveedor;
        String prov = "";

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

                prov = "";
                try
                {
                    prov = Session["idUsuarioProveedor"].ToString().Trim();
                    cargarEdit(prov);
                    this.hidProveedor.Value = prov;
                    if (prov != "" && prov != null)
                    {
                      this.hidComplementoUr.Value = "rfc=" + prov + "&campo=Proveedor&primerproveedor=me";
                    }

                    this.btnGuardarCambios.Visible = true;
                    this.ltlbtnCancel.Visible = true;
                }
                catch (Exception)
                {
                    this.btnGuardarCambios.Visible = true;
                    this.ltlbtnCancel.Visible = true;
                }
                if (!IsPostBack)
                {
                    try
                    {
                        string id = Request.QueryString["toEdit"];
                        if (id != "" && id != null)
                        {
                            cargarEdit(id);
                            this.btnGuardarCambios.Visible = true;
                            this.ltlbtnCancel.Visible = true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            cargarConfigPass();
        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("config.aspx");
        }

            public void cargarEdit(string id)
        {
            try
            {
                PNegocio.Administrador.Proveedores objInstancia = new PNegocio.Administrador.Proveedores();
                string sqlString = "select u.usuariolog, u.nombre, u.apellidos, u.usuarioPass, u.fechaIni, u.fechaIFn, u.email, u.proveedor_idProveedor,p.nombre, r.nombre as rol from usuario as u, proveedor as p, rol as r where u.proveedor_idProveedor= p.idProveedor and usuarioLog = '" + id + "' and r.idROl = u.rol_idRol;";
                listaProveedor = objInstancia.consultarProveedoresPorId(sqlString);
                if (listaProveedor.Count > 1)
                {
                    Session["listaProveedor"] = listaProveedor;
                    this.txtIdUsuario.Text = listaProveedor[1][0];
                    this.txtIdUsuario.Enabled = false;
                    this.hidId.Value = listaProveedor[1][0];
                    PNegocio.Administrador.CambiarContrasena instancia = new PNegocio.Administrador.CambiarContrasena();
                }
                this.btnGuardarCambios.Visible = true;
            }
            catch (Exception)
            {
                this.btnGuardarCambios.Visible = true;
                this.ltlbtnCancel.Visible = true;
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
                pass1 = this.txtPassword.Text.Trim();
                pass2 = this.txtPasswordRepetir.Text.Trim();
                if (pass1 == pass2)
                {
                    guardarCambios(); 
                }
               else
               {
                    this.lblDialog.Text = "Contraseña no coincide";
                }
        }

        public void guardarCambios()
        {
            usuari = this.txtIdUsuario.Text.Trim(); 
            pass1 = this.txtPassword.Text.Trim();
            pass2 = this.txtPasswordRepetir.Text.Trim();
            string mensaje = "Estimado proveedor su cuenta en nuestro portal de proveedores ha sido modificada. <br>";
            creadoPor =  "0"; 
            bool verificarContrasena = true;
            bool entrarAModificar = false;

            if (pass1 == null || pass1 == "" &&
                pass2 == null || pass2 == ""  )
            {
                verificarContrasena = false;
            }

            if (verificarContrasena)
            {
                if (pass1 == pass2)
                {
                    entrarAModificar = true;
                }
                else
                {
                    entrarAModificar = false;
                    this.lblDialog.Text = "Contraseña no coincide";
                }
            }
            else
            {
                entrarAModificar = true;
            }

              if (entrarAModificar)
            {
                PNegocio.Administrador.CambiarContrasena us = new PNegocio.Administrador.CambiarContrasena();
                PNegocio.Encript encript = new PNegocio.Encript();
                listaProveedor = (List<string[]>)Session["listaProveedor"];

                if (String.IsNullOrEmpty(usuari))
                {
                    usuari = prov;   //listaProveedor[1][0].Trim();
                }
                if (String.IsNullOrEmpty(pass1))
                {
                    pass1 = listaProveedor[1][3].Trim();
                }
                else
                {
                    pass1 = encript.Encriptar(encript.Encriptar(pass1));
                    mensaje += "Contraseña: " + txtPassword.Text + "<br>";
                }

                creadoPor = us.cambiarContrasena(pass1, usuari);
                switch (   creadoPor   )
                {
                    case "actualizado":
                        this.lblDialog.Text = "Actualizado correctamente";
                        Session["textoDialogo"] = "Actualizado correctamente";
                        break;
                    default:
                        this.lblDialog.Text = creadoPor;
                        break;
                }
                this.btnGuardarCambios.Visible = true;
                this.ltlbtnCancel.Visible = true;
            }
            else
            {
                this.lblDialog.Text = "El password no coincide";
                this.btnGuardarCambios.Visible = false;
                this.ltlbtnCancel.Visible = true;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
        }

         private void cargarConfigPass()
        {
            if (File.Exists(@"C:\temp\config.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\temp\config.txt");
                hidNumeroLetras.Value = lines[0];
                hidNumeroLetrasM.Value = lines[1];
                hidCantidadNumeros.Value = lines[2];
                hidNumeroCaracteres.Value = lines[3];
            }
            else
            {
                hidNumeroLetras.Value = "1";
                hidNumeroLetrasM.Value = "1";
                hidCantidadNumeros.Value = "1";
                hidNumeroCaracteres.Value = "8";
            }
        }

        protected void txtIdUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
