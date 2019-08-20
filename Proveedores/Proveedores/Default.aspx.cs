using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PEntidades;
using System.IO;
using PEntidades.SrvSAPUProv;

namespace Proveedores
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["textoDialogo"] = "";
            this.txtrepitContra.Visible = false;
            cargarConfigPass();
        }

        protected void btnAcceder_Click(object sender, EventArgs e)
        {
            if (this.hidVerificarPass.Value == "si")
            {
                string resPass = "";
                if (this.btnAcceder.Text == "Acceder")
                {
                    PNegocio.Login login = new PNegocio.Login();
                    string[] resLog = null;
                    string usuario = this.txtUsuario.Text;
                    string pass = this.txtContrasena.Text;
                    
                    try
                    {
                        PNegocio.Encript encript = new PNegocio.Encript();
                        resLog = login.isUserBDDistinct(usuario, encript.Encriptar(encript.Encriptar(pass)));//Si es correcto devuelve un arreglo de dos posiciones, primera posicion el id de usuario y en la segunda el RFC, si el usuario es incorrecto devolvera un arreglo de dos: la posición uno es el error y la posicion dos estara vacia
                        resPass = resLog[0].ToString().Trim();
                        //resultado, idproveedor_rfc, idrol, cambiar_contraseña, lifnr, portal_activo, proveedor_activo, rol_acivo, fecha_vigencia
                        Session["resLog"] = resLog;
                        //resproc = detalle ,  RFCProv,  idRol, esCambiar, lifnr
                        if (resPass != "log incorrecto" && resPass != "log fallido" && resPass != "Admin desbloqueado" &&
                            resPass != "" && resPass != null && resPass != "Tiempo"
                            )
                        {

                            //Session["objUsuario"] = login.getUsuario(resPass); //usuarioLog as idUsuario, proveedor_idProveedor,rol_idRol,email FROM usuario WHERE usuarioLog=@idUsuario
                            Session["idUsuarioProveedor"] = resPass;
                            //HttpContext.Current.User.Identity.Name = resPass;
                            Session["ProveedorLoged"] = resLog[1].ToString().Trim();
                            Session["lifnr"] = resLog[4].ToString().Trim();
                            Session["rfc"] = resLog[9].ToString().Trim();
                            if (resLog[3] == "True")
                            {
                                this.lblError.Text = "";
                                this.lblPortalProveedores.Text = "Debe cambiar su contraseña<br/> escriba la nueva contraseña:";
                                this.lblDescripUsuario.Text = "Nueva contraseña";
                                this.txtUsuario.Visible = false;
                                //this.txtUsuario.TextMode = TextBoxMode.Password;
                                this.txtrepitContra.Visible = true;
                                this.txtContrasena.Visible = true;
                                this.lblDescContrasena.Text = "Repita la nueva contraseña";
                                this.btnAcceder.Text = "Cambiar";
                            }
                            else
                            {
                                if (resLog[5] == "1" && resLog[6] == "1" && resLog[7] == "1" && resLog[8] == "1") // portal activo, proveedorActivo, rolactivo , fechaActiva
                                {
                                    acceso(resLog);
                                }
                                else
                                {
                                    string why = "";
                                    //if (resPass == "Admin") // solo si es administrador de portal
                                    if (resLog[2].ToString() == "0") // solo si es administrador de portal
                                    {
                                        acceso(resLog);
                                    }
                                    if (resLog[7] == "0")
                                    {
                                        why = "rol";
                                    }
                                    if (resLog[8] == "0")
                                    {
                                        why = "fecha";
                                    }
                                    //if (resLog[0] == "Tiempo")
                                    //{
                                    //    why = "Tiempo";
                                    //}
                                    switch (why)
                                    {
                                        case "":
                                            this.lblError.Text = "El portal<br/>esta inactivo";
                                            break;
                                        case "rol":
                                            this.lblError.Text = "Su rol<br/>esta inactivo";
                                            break;
                                        case "fecha":
                                            this.lblError.Text = "Su usuario<br/>no esta vigente";
                                            break;
                                        //case "Tiempo":
                                        //    this.lblError.Text = "El tiempo de bloqueo<br/>no ha transcurrido";
                                        //    break;
                                        default:
                                            break;

                                    }
                                }


                            }
                        }
                        else
                        {
                            if (resPass == "Admin desbloqueado")
                            {
                                this.lblError.Text = "El usuario fue desbloqueado<br/>ingrese sus datos nuevamente<br/>para acceder";
                            }
                            else if (resPass == "Tiempo")
                            {
                                this.lblError.Text = "El lapso de tiempo para<br/>desbloquear el administrador<br/>aun no se cumple";
                            }
                            else 
                            {
                                string res;
                                res = guardarIntentoFallido(usuario, pass);
                                switch (res)
                                {
                                    case "bloqueado":
                                        this.lblError.Text = "El usuario fue bloqueado<br/>por superar intentos permitidos";
                                        break;
                                    case "error":
                                        this.lblError.Text = "Debe introducir<br/>datos correctos";
                                        break;
                                    default:
                                        this.lblError.Text = "Acceso<br/>denegado";
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception)         //catch (Exception ex)
                    {
                        //ex.Message.ToString();
                        this.lblError.Text = "Aplicación inaccesible en este momento";
                    }
                     resLog = new string[0];
                }
                else if (this.btnAcceder.Text == "Cambiar")
                {
                    //string pass1 = this.txtUsuario.Text;
                    this.txtrepitContra.Visible = true;
                    this.txtContrasena.Visible = true;
                    string pass1 = this.txtrepitContra.Text;
                    string pass2 = this.txtContrasena.Text;
                    if (pass1 == pass2)
                    {

                        string[] resLog = null;
                        resLog = (string[])Session["resLog"];
                        PNegocio.Administrador.Seguridad instancia = new PNegocio.Administrador.Seguridad();
                        PNegocio.Encript encript = new PNegocio.Encript();
                        string res;
                        res = instancia.cambiarContrasena(encript.Encriptar(encript.Encriptar(pass1)), resLog[0]);
                        if (res == "actualizado")
                        {
                            acceso(resLog);
                        }
                        else
                        {
                            this.lblError.Text = res;
                        }

                    }
                    else
                    {
                        this.lblError.Text = "Las contraseñas<br/> no coinciden";
                    }
                }
                
            }
            else {
                //this.txtrepitContra.Visible = true;
                this.lblError.Text = "El password no cumple<br/> con las características necesarias";
            }
        }



        private string guardarIntentoFallido(string usuario, string pass)
        {
            PNegocio.Encript encript = new PNegocio.Encript();
            PNegocio.Administrador.Seguridad instancia = new PNegocio.Administrador.Seguridad();
            return instancia.guardarCredencialFallida(usuario, encript.Encriptar(encript.Encriptar(pass)));
        }

        private void acceso(string[] resLog)
        {
            string urlDestino = "";
            if (resLog[2].ToString() == "0")//Cuando es el administrador
            {
                urlDestino = "~/administrator/config.aspx";
                PNegocio.Administrador.Proveedores prov = new PNegocio.Administrador.Proveedores();
                PNegocio.Administrador.Proveedores provInact = new PNegocio.Administrador.Proveedores();
                PNegocio.Administrador.WebServicesEndpoints instancia = new PNegocio.Administrador.WebServicesEndpoints();
                List<string[]> listEndpoints = new List<string[]>();
                listEndpoints = instancia.consultarTodosEndponits();
                for (int i = 1; i < listEndpoints.Count; i++)
                {
                    try
                    {
                          ZELISTA_PROVE[] result = provInact.ResProvedoresInactivos(listEndpoints[i][0].ToString(),
                             PNegocio.Administrador.Seguridad.consultarUsuarioCOntrasenaInstancia(listEndpoints[i][1].ToString())
                             );
                          string mensaje = prov.proveedoresInactivos(result);
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            else
            { // Cuando no es administrador
                urlDestino = "~/portal/Inicio.aspx";
            }
            FormsAuthentication.SetAuthCookie(this.txtUsuario.Text, false);
            Response.Redirect(urlDestino, false);
        }

        /// <summary>
        /// Carga la configuracion que tendra para ingresar la contraseña.
        /// </summary>
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
    }
}