//using PEntidades.SrvSAPProveedores;
using PEntidades.SrvSAPUProv;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class CargaAutomaticaProv : System.Web.UI.Page
    {
        List<PEntidades.ArrTabCorr> correos = new List<PEntidades.ArrTabCorr>();
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
            List<string[]> listEndpoints = new List<string[]>();
            string resultados = "", nombreRol = "";
            if (existeRolDefaul(ref nombreRol))
            {
                PNegocio.Administrador.WebServicesEndpoints instancia = new PNegocio.Administrador.WebServicesEndpoints();
                listEndpoints = instancia.consultarTodosEndponits();
                if (listEndpoints.Count > 1)
                {


                    for (int i = 1; i < listEndpoints.Count; i++)
                    {
                        try
                        {
                            string extranjero = "";
                            cargaAutomaticaProveedores(listEndpoints[i][0].ToString(), listEndpoints[i][1].ToString(), extranjero, ref correos);
                            cargaAutomaticaSociedades(listEndpoints[i][0].ToString(), listEndpoints[i][1].ToString(), extranjero);
                            extranjero = "X";
                            cargaAutomaticaProveedores(listEndpoints[i][0].ToString(), listEndpoints[i][1].ToString(), extranjero, ref correos);
                            cargaAutomaticaSociedades(listEndpoints[i][0].ToString(), listEndpoints[i][1].ToString(), extranjero);
                            crearUsuarioAutomatico(nombreRol);
                            resultados += "La carga en la instancia <strong>" + listEndpoints[i][2].ToString() + " </strong> fue exitosa. <br/>";
                        }
                        catch (Exception)
                        {
                            resultados += "La carga en la instancia  <strong>" + listEndpoints[i][2].ToString() + " </strong> fue erronea. <br/>";
                        }
                    }
                    Session["textoDialogo"] = resultados;
                }
                else
                {
                    Session["textoDialogo"] = "No existen endpoints o instancias disponibles";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);

                }
            }
            else
            {
                Session["textoDialogo"] = "No se encotro un Rol por defecto para crear usuarios. Cree un Rol";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
            }
        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("config.aspx");
        }

        public void cargaAutomaticaProveedores(string endpoint, string idInstancia, string extranjero, ref List<PEntidades.ArrTabCorr> correos)
        {
            PNegocio.Administrador.Proveedores prvd = new PNegocio.Administrador.Proveedores();
            ZERES_USUARIOS[] tablaProveedores = prvd.ResProvedores(endpoint,
                PNegocio.Administrador.Seguridad.consultarUsuarioCOntrasenaInstancia(idInstancia),
                extranjero
                );

            List<string> respuestas = new List<string>();

            int contadorBueno = 0;
            int contadorMalo = 0;
            int contadorProblemasX = 0;
            int contadorYaExistia = 0;

            for (int i = 0; i < tablaProveedores.Count(); i++)
            {
                try
                {
                    string[] resInsert = new string[1];
                    resInsert = prvd.insertarProveedores(
                    tablaProveedores[i].STCEG.ToString().Trim(),
                    tablaProveedores[i].NAME1.ToString().Trim(),
                    tablaProveedores[i].LIFNR.ToString().Trim(),
                    idInstancia
                    );

                    respuestas.Add(resInsert[0]);
                    if (!correos.Contains(new PEntidades.ArrTabCorr(tablaProveedores[i].SMTP_ADDR.ToString().Trim(), tablaProveedores[i].LIFNR.ToString().Trim())))
                    correos.Add(new PEntidades.ArrTabCorr(tablaProveedores[i].SMTP_ADDR.ToString().Trim(), tablaProveedores[i].LIFNR.ToString().Trim()));                    
                    if (resInsert[0] != "error1" && resInsert[0] != "error2")
                    {
                        contadorBueno++;
                    }
                    else
                    {
                        if (resInsert[0] == "error1")
                        {
                            contadorYaExistia++;
                        }
                        else
                        {
                            contadorProblemasX++;
                        }
                        contadorMalo++;
                    }
                }
                catch (Exception)
                {
                }
            }
            if (contadorBueno == tablaProveedores.Count())
            {
            }
            else
            {
            }


        }

        public void cargaAutomaticaSociedades(string endpoint, string idInstancia, string extranjero)
        {

            PNegocio.Administrador.Sociedades prvd = new PNegocio.Administrador.Sociedades();
            ZERES_SOCIEDADES[] tablaProveedores = prvd.ResSociedades(endpoint, 
                PNegocio.Administrador.Seguridad.consultarUsuarioCOntrasenaInstancia(idInstancia),
                extranjero
                );

            List<string> respuestas = new List<string>();

            int contadorBueno = 0;
            int contadorMalo = 0;
            int contadorProblemasX = 0;
           
            for (int i = 0; i < tablaProveedores.Count(); i++)
            {
                try
                {
                    string[] resInsert = new string[1];
                    resInsert = prvd.insertarSociedades(
                    tablaProveedores[i].STCEG.ToString(),
                    tablaProveedores[i].LIFNR.ToString(),
                    tablaProveedores[i].BUKRS.ToString(),
                    idInstancia
                    );

                    respuestas.Add(resInsert[0]);
                    if (resInsert[0] == "insertado sociedad")
                    {
                        contadorBueno++;
                    }
                    else
                    {
                        if (resInsert[0] == "sociedad fallida")
                        {
                            contadorMalo++;
                        }
                        else
                        {
                            contadorProblemasX++;
                        }
                        contadorMalo++;
                    }
                }
                catch (Exception)
                {
                }
            }
            if (contadorBueno == tablaProveedores.Count())
            {
            }
            else
            {
            }
        }
        private void crearUsuarioAutomatico(string idRol)
        {
            List<string[]> id = new List<string[]>();
            int inde;
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            string email = "example@grupogonher.com";
            PNegocio.Administrador.Proveedores idprovedor = new PNegocio.Administrador.Proveedores();
            string sqlString = "select d.proveedor_idProveedor, d.RFC, d.nombre, d.lifnr from detProveedor d where not exists(select 1 from usuario u where u.proveedor_idProveedor = d.proveedor_idProveedor ) order by nombre asc";
            //string sqlString = "select proveedor_idProveedor, RFC, nombre, lifnr from detProveedor where proveedor_idProveedor not in ( select proveedor_idProveedor from usuario ) order by nombre asc";
            //string sqlString = "select proveedor_idProveedor, RFC, nombre, lifnr from detProveedor order by nombre asc";
            id = idprovedor.consultarProveedoresPorId(sqlString);
            if (id.Count>1)
            {
                id.RemoveAt(0);
                for (int i = 0; i < id.Count; i++)
                {
                    List<string[]> socPorProv = cargarSociedades(id[i][0].Trim());
                    string[] name = id[i][2].Split(' ');
                    inde = correos.FindIndex(x => x.acreedor.Contains(id[i][3].Trim()));
                    if (inde > 0)
                    {
                        if (String.IsNullOrEmpty(correos[inde].correo))
                        {
                            email = "example@grupogonher.com";
                        }
                        else
                        {
                            email = correos[inde].correo;
                        }
                    }
                    else
                    {
                        email = "example@grupogonher.com";
                    }
                    if (name[0].Length < 5)
                    {
                        name[0] = id[i][2].Substring(0, 5).Replace(" ","");
                    }
                    string usuario = id[i][3].Trim() + ti.ToTitleCase(ti.ToLower(name[0]));
                    string nombre = id[i][2].Trim();
                    PNegocio.Administrador.Usuario us = new PNegocio.Administrador.Usuario();
                    PNegocio.Encript encript = new PNegocio.Encript();
                    socPorProv.RemoveAt(0);
                    string res = us.insertarUsuario(usuario, nombre, nombre, encript.Encriptar(encript.Encriptar(usuario)), "2010-01-01", "2099-12-31",
                        id[i][0].Trim(), Convert.ToString(1), Convert.ToString(1), email, "Rol Default", socPorProv);
                    if (res.Equals("insertado"))
                    {
                        res = "Se agrego correctamente";
                    }
                }
            }
        }
        private List<string[]> cargarSociedades(string id)
        {
            PNegocio.Administrador.Usuario sociedad = new PNegocio.Administrador.Usuario();
            
            List<string[]> resultado = sociedad.cosultarSociedadesPorprov(id, "90%");
            return resultado;
        }
        private bool existeRolDefaul(ref string nombreRol)
        {
            bool existe = false;
            PNegocio.Administrador.Roles rolDefault = new PNegocio.Administrador.Roles();
            string sql = "select nombre from rol where esCreacion = 1";
            List<string[]> resultado = rolDefault.roldeFaul(sql);
            if (resultado.Count>1)
            {
                resultado.RemoveAt(0);
                nombreRol = resultado[0][0].Trim();
                existe = true;
            }
            return existe;
        }
    }
}