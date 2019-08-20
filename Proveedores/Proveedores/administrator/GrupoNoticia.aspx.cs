using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class GrupoNoticia : System.Web.UI.Page
    {

        String proveedoresSeleccionados;
        String[] idProveedores;
        ////Variables de la noticia seleccionada
        String nameGroup;
        String n_noticia;
        String titulo;
        List<PEntidades.ArrTablas> tablas = new List<PEntidades.ArrTablas>();

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
                //INICIO cargar noticias seleccionadas
                string link = "";
                try
                {
                    link = Session["permitir"].ToString().Trim();
                    Session["permitir"] = "";

                }
                catch (Exception)
                {

                }
                if (link == "linkGrupoNoticia") // si se recibio el link desde ...
                {
                    try
                    {
                        this.txtNombreGrupoN.Text = Session["nombreGrupo"].ToString().Trim();
                        Session["nombreGrupo"] = "";
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        string numeros = Session["idsNiticias"].ToString().Trim(); // viene de mostrar noticias
                        this.ltlTablaNoticiasSeleccionadas.Text = crearTablaNoticiasSeleccionadas(numeros);
                        this.hidIdsNoticiasSeleciondas.Value = numeros;
                        Session["idsNiticias"] = "";
                    }
                    catch (Exception)
                    {

                    }


                }



                //FIN cargar noticias seleccionadas

                if (!IsPostBack)
                {
                    string idEdit = "";
                    idEdit = Request.QueryString["toEdit"]; // usado cuando es por url desde la seleccion por grupo
                    if (idEdit == "" || idEdit == null)
                    {
                        try
                        {
                            idEdit = Session["GrupoNoticiaEdit"].ToString().Trim(); // usado cuando se redirecciona al seleccionar noticias
                            Session["GrupoNoticiaEdit"] = "";
                        }
                        catch (Exception)
                        {
                        }

                    }
                    if (idEdit != "" && idEdit != null)
                    {
                        try
                        {
                            cargarProveedoresSinNoticiaEdit();
                            cargarProveedoresEdit(idEdit);
                            this.hidIDX.Value = idEdit;
                            if (this.txtNombreGrupoN.Text == "" && this.txtNombreGrupoN.Text == "")
                            {
                                string nombre = selectNombreGrupo(idEdit);
                                this.txtNombreGrupoN.Text = nombre;
                            }
                            if (link == "linkGrupoNoticia") // si existen noticias de la selccion
                            {

                            }
                            else
                            {
                                this.ltlTablaNoticiasSeleccionadas.Text = getTablaNoticiasGruposSelect(idEdit);

                            }

                            this.btnSubmit.Visible = false;
                            this.btnGuardarCambios.Visible = true;
                            this.btnCancel.Visible = true;
                            this.HidGrupoEdit.Value = idEdit;

                        }
                        catch (Exception)
                        {
                            cargarProveedores();
                            this.btnCancel.Visible = false;
                            this.btnGuardarCambios.Visible = false;
                        }

                    }
                    else
                    {
                        cargarProveedores();
                        this.btnCancel.Visible = false;
                        this.btnGuardarCambios.Visible = false;
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(Request.Form["letra"]) && String.IsNullOrEmpty(Request.Form["procargado"]))
                    {
                        cargarProveedores();
                    }
                    else
                    {
                        mostrarpor(Request.Form["letra"], Request.Form["procargado"]);
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

        private void cargarProveedores(){

            this.lblTablaProveedores.Text = new PNegocio.Administrador.Proveedor().consultarProveedoresSinNoticia("", ref tablas);
            Session["Tablas"] = tablas;
            if (this.lblTablaProveedores.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltroSort();

                //string html = "<table><tr><td><ul id='sortable2' class='droptrue'>" + "</ul>";
                //html += "</ul></td></tr></table>";
                string html = "<ul id='sortable2' class='droptrue'>" + "</ul>";
                //html += "</ul>";

                this.lblTablaDos.Text = html;
            }
            
        }

        private void cargarProveedoresEdit(string edit)
        {

            this.lblTablaDos.Text = new PNegocio.Administrador.Proveedor().consultarProveedoresSinNoticiaPorId(edit, "");
            if (this.lblTablaProveedores.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
            }
        }

        private void cargarProveedoresSinNoticiaEdit() {
            this.lblTablaProveedores.Text = new PNegocio.Administrador.Proveedor().consultarProveedoresSinNoticia("", ref tablas);
            Session["Tablas"] = tablas;
            if (this.lblTablaProveedores.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltroSort();
            }
        }

        protected void crearGrupo(object sender, EventArgs e)
        {
            crearGrupo("insertar");
        }

        private void crearGrupo(string mode)
        {
            proveedoresSeleccionados = this.hidIdSelected.Value;
            nameGroup = this.txtNombreGrupoN.Text;
            string idGrupoNoticiaModificar = this.hidIDX.Value.ToString().Trim();
            string[] tablaNumeros = this.hidIdsNoticiasSeleciondas.Value.ToString().Trim().Split(new Char[] { ',' });
            char[] delimiterChars = { ',' };
            if (proveedoresSeleccionados != "" && nameGroup != "" && tablaNumeros.Length > 0 &&
                proveedoresSeleccionados != null && nameGroup != null)
            {
                idProveedores = proveedoresSeleccionados.Split(delimiterChars);
                idProveedores = identificar(idProveedores);
                this.lblIdNoticia.Text = "";
                this.lblNoticiaSeleccionada.Text = "";

                if (mode == "insertar")
                {
                    new PNegocio.Administrador.Noticia().insertarGrupoNoticia(nameGroup, n_noticia, idProveedores, tablaNumeros);
                    nameGroup = n_noticia = "";
                    Session["textoDialogo"] = "El grupo de noticia se insertó correctamente";
                    Response.Redirect("GrupoNoticia.aspx");
                }
                if (mode == "modificar")
                {
                    List<string> listaString= new List<string>();
                    if (idProveedores.Length > 0)
                    {
                        listaString.Add(idProveedores[0]);
                    }
                    for (int i = 0; i < idProveedores.Length; i++ )
                    {
                        bool existe = false;
                        for (int j = 0; j < listaString.Count; j++)
                        {
                            if (idProveedores[i] == listaString[j])
                            {
                                existe = true;
                                break;
                        
                            }
                        }
                        if (!existe)
                        {
                            listaString.Add(idProveedores[i]);
                        }
                    }

                    string res = new PNegocio.Administrador.Noticia().modificarGrupoNoticia(idGrupoNoticiaModificar, nameGroup, n_noticia, listaString, tablaNumeros);
                    nameGroup = n_noticia = "";
                    Session["textoDialogo"] = res;
                    Response.Redirect("GrupoNoticia.aspx");
                }

                
            }
            else
            {
                this.lblDialog.Text = "debe de llenar todos los campos necesarios";
                if (n_noticia != "" && titulo != "" && n_noticia != null && titulo != null)
                {
                    this.lblIdNoticia.Text = n_noticia;
                    this.lblNoticiaSeleccionada.Text = " Titulo: " + titulo;

                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);



        }

        private string crearTablaNoticiasSeleccionadas(string numeros) {
            string[] split = numeros.Split(new Char[] { ',' });
            PNegocio.Administrador.Noticia instancia = new PNegocio.Administrador.Noticia();
            return instancia.consultarNoticiasSeleccionadas(split, "dentro", "tabla"); 
        }

        protected void GuardarCambios(object sender, EventArgs e)
        {
            crearGrupo("modificar");
        }

        protected void Cancelar(object sender, EventArgs e)
        {
            cancelar();
        }

        private void cancelar() {
            Response.Redirect("GrupoNoticia.aspx");
        }

        private string getTablaNoticiasGruposSelect(string idGrupo) {

           
            PNegocio.Administrador.Noticia instancia = new PNegocio.Administrador.Noticia();
            string sqlString = "select * from  noticia as n, GrupoNoticia_Noticia as gn " +
           "where n.idNoticia = gn.Noticia_idNoticia and GrupoNoticia_idGrupoNoticia = " + idGrupo;
            List<string[]> resultado = instancia.consultarNoticiaPorId(sqlString);
            string noticias = "";
            if (resultado.Count > 1)
            {
                noticias += resultado[1][0];
                for (int i = 2; i < resultado.Count; i++ )
                {
                    if (i != resultado.Count)
                    {
                        noticias += ",";
                    }
                    noticias += resultado[i][0];
                    
                }
            }
            this.hidIdsNoticiasSeleciondas.Value = noticias;
            return instancia.consultarNoticiasGrupoEditar(resultado);

        }

        private string selectNombreGrupo(string idGrupo)
        {
            PNegocio.Administrador.Noticia instancia = new PNegocio.Administrador.Noticia();
            string sqlString = "select nombre from gruponoticia where idgrupoNoticia  = " + idGrupo;
            return instancia.consultarNoticiaPorId(sqlString)[1][0].ToString().Trim();

        }

        protected void btnBUscarNoticia_Click(object sender, EventArgs e)
        {
            Session["nombreGrupo"] = this.txtNombreGrupoN.Text.Trim(); // se envia el nombre oara no borralo cuando se regrese
            Session["GrupoNoticiaEdit"] = this.HidGrupoEdit.Value; // grupo de noticia editando
            Session["idsNiticias"] = this.hidIdsNoticiasSeleciondas.Value;
            Response.Redirect("MostrarNoticias.aspx"); 
        }

        private void mostrarpor(string letra, string conttabl2)
        {            
            tablas = (List<PEntidades.ArrTablas>)Session["Tablas"];
            if (letra.Length > 1)
            {
                if (letra.Equals("0 - 9"))
                {
                    letra = letra.Substring(0, 1);
                }
            }
            
            int inde = tablas.FindIndex(x => x.letra.Contains(letra));
            List<int> listaEvitar = new List<int>();
            if (inde> - 1)
            {
                this.lblTablaProveedores.Text = Gen.Util.CS.Gen.convertToHtmlTableSort(tablas[inde].tabla, "sortable1", "droptrue' style='width:" + "" + ";");
                this.lblTablaDos.Text = conttabl2.Replace('[', '<').Replace(']', '>');
            }
            else
            {
                this.lblTablaProveedores.Text = "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
            
        }

        private string[] identificar(string[] idproveedores)
        {
            PNegocio.Administrador.Noticia instancia = new PNegocio.Administrador.Noticia();
            List<string[]> idpro = new List<string[]>();
            if (drlbTipoGrupo.SelectedValue.Equals("grupo"))
            {
                foreach (string idprove in idproveedores)
                {
                    string sqlString = "select idOriginal from detProveedor where proveedor_idProveedor  = " + idprove;
                    idpro = instancia.consultarNoticiaPorId(sqlString);
                    idpro.RemoveAt(0);
                    int indice = idproveedores.Length;
                    Array.Resize(ref idproveedores, idproveedores.Length + idpro.Count);
                    foreach (string[] item in idpro)
                    {
                        idproveedores[indice] = item[0];
                        indice++;
                    }
                }
                HashSet<string> set = new HashSet<string>(idproveedores);
                string[] result = new string[set.Count];
                set.CopyTo(result);
                idproveedores = result;
            }
            return idproveedores;
        }
    }
}