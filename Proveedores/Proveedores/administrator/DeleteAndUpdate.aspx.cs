using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class DeleteAndUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            
            string identificador = Request.Form["identificador"].ToString();
            string pantalla = Request.Form["pantalla"].ToString();
            string desicion = Request.Form["desicion"].ToString();
            string complemento = Request.Form["complemento"].ToString();
            string valor2 = Request.Form["valor2"].ToString();
            string valor3 = Request.Form["valor3"].ToString();
            string valor4 = Request.Form["valor4"].ToString();

            string resultado = "";
            string pag = "";
            string sqlString = "";
            int numSet = 1;

            string resParaSession = "";

            
            if (desicion == "Eliminar")
            {
                string[] array = null;
                char[] delimiterChars = {','};
                switch (pantalla)
                {
                    case "AsignarRol":
                        sqlString = "update usuario set rol_idRol = null where usuarioLog = '" + identificador + "';";
                        pag = "AsignarRol.aspx" + complemento;
                        break;
                    case "Instancia":
                        sqlString = "update usuario set rol_idRol = null where usuarioLog = '" + identificador + "';";
                        pag = "AsignarRol.aspx" + complemento;
                        break;
                    case "Noticia":
                         sqlString += "delete from GrupoNoticia_Noticia where noticia_idNoticia = '" + identificador + "' ";
                         sqlString += "delete from noticia where idnoticia = '" + identificador + "' ";

                        pag = "Noticia.aspx";
                        break;
                    case "DesvincularGrupoNoticia":
                        
                        array = complemento.Split(delimiterChars);
                        if (array[0] == "Por proveedor")
                        {
                            sqlString = "delete GrupoNoticia_proveedor where proveedor_idProveedor = " + identificador + ";";     
                        }
                        if (array[0] == "Por grupo")
                        {
                            
                            sqlString += " delete GrupoNoticia_proveedor where grupoNoticia_idGrupoNoticia = " + identificador ;
                            sqlString += "delete from gruponoticia_noticia where grupoNoticia_idgruponoticia = " + identificador;
				            sqlString += " delete from gruponoticia where idGrupoNoticia = " + identificador ;
                            
                        }
                        if (array.Length > 1)
                        {
                            pag = "DesvincularGrupoNoticia.aspx?toSee=" + array[1];
                        }
                        else { 
                            pag = "DesvincularGrupoNoticia.aspx";
                        }
                        
                        break;
                    case "Seguridad":
                        sqlString = "delete from credencialesInaceptables where idCredencialInaceptable = " + identificador;
                        pag = "Seguridad.aspx" + complemento;
                        break;
                    case "DesvincularGrupoProv":
                         array = null;
                        array = complemento.Split(delimiterChars);
                        if (array[0] == "Por proveedor")
                        {
                            sqlString = "update detProveedor set proveedor_idProveedor = idOriginal where RFC = '" + identificador + "'";
                            pag = "DesvincularGrupoProv.aspx";

                        }
                        if (array[0] == "Por grupo")
                        {
                            
                            sqlString += "update detProveedor set proveedor_idProveedor = idOriginal where proveedor_idProveedor = " + identificador;
                            sqlString += " update proveedor set esCabeceraGrupo = null, nombreGrupo = null where idProveedor = " + identificador;
                            
                            pag = "DesvincularGrupoProv.aspx";
                        }
                        
                        break;
                    case "usuario":
                        
                        sqlString += " delete from usuariosociedad where usuario_idUsuario = '" + identificador + "' "; 
                        sqlString += " delete from credencialFallida where usuario_idUsuario= '"+identificador+"' "; 
                        sqlString += " delete from controlSeguridad where usuario_idUsuario = '"+identificador+"' "; 
                        sqlString += " delete from credencialesUsadas where usuario_idUsuario = '"+identificador+"' ";
                        sqlString += " delete from usuario where usuarioLog = '" + identificador + "'";

                        pag = "usuario.aspx?" + complemento;
                        break;

                    default:
                        break;
                }
                resParaSession = "Eliminado";
            }
            else {
                if (desicion == "Desactivar")
                {
                    numSet = 0;
                    resParaSession = "Desactivado";
                }
                if (desicion == "Activar")
                {
                    numSet = 1;
                    resParaSession = "Activado";
                }
                switch (pantalla)
                {
                    case "Instancia":
                        sqlString = "update instancia set esBloq = "+numSet+" where idInstancia = " + identificador + "  ;";
                        pag = "instancia.aspx";
                        break;
                    case "InstanciaCN":
                        sqlString = "update RfcConfigParams set esBloq = " + numSet + " where idRfc = " + identificador + "  ;";
                        pag = "instanciaCN.aspx";
                        break;
                    case "Proveedores":
                        sqlString = "update proveedor set esBloq = "+numSet+" where idPRoveedor = " + identificador + "  ;";
                        pag = "MostrarPertenencia.aspx?vinculador=Proveedores";
                        break;
                    case "Roles":
                        sqlString = "update rol set esActivo = "+numSet+" where idRol = " + identificador + ";";
                        pag = "Roles.aspx";
                        break;
                    case "MostrarPantalla":
                        sqlString = "update pantalla set esbloq = "+numSet+" where idPantalla = " + identificador + ";";
                        pag = "MostrarPertenencia.aspx?vinculador=Proveedores";
                        break;
                    case "Noticia":
                        sqlString = "update noticia set esBloq = "+numSet+" where idNoticia  = " + identificador + ";";
                        pag = "Noticia.aspx";
                        break;
                    case "usuario":
                        sqlString = "update usuario set esBloq = "+numSet+" where usuarioLog = '" + identificador + "';";
                        pag = "usuario.aspx?" + complemento;
                        break;
                    case "DevincularGrupoNoticia":
                        //sqlString = "update usuario set esBloq = " + numSet + " where usuarioLog = '" + identificador + "';";
                        //pag = "usuario.aspx?" + complemento;
                        break;
                    case "DesvincularGrupoProv":
                        sqlString = "update proveedor set esBloq = "+numSet+" where idPRoveedor = " + identificador + "  ;";                                       //pag = "DesvincularGrupoProv.aspx" + complemento;
                        pag = "DesvincularGrupoProv.aspx";
                        break;
                    case "AdministrarSoc":
                        sqlString = "update sociedad set esbloq = "+numSet+" where detproveedor_rfc = '"+valor3+"' and detproveedor_lifnr = '"+valor4+"' and bukrs = "+valor2+" and idinstancia = " + identificador + "  ;";                                       //pag = "DesvincularGrupoProv.aspx" + complemento;
                        pag = "AdministrarSociedades.aspx";
                        break;
                        
                    default:
                        break;
                }
            }

            resultado = new PNegocio.Administrador.Eliminar().ejecutarQueryWhitTran(sqlString);

            if (resultado != "0" && resultado != "")
            {
                Session["textoDialogo"] = resParaSession;
            }
            else
            {
                Session["textoDialogo"] = "La ultima operación no se concretó correctamente, posiblemente ocurrio un error o por el momento es una acción no permitida";
            }

            Response.Write(pag);

        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("config.aspx");
        }

    }
}