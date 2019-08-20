using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class MostrarNoticias : System.Web.UI.Page
    {
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

            if(!IsPostBack){
                
                string idEdit = "";
                try
                {
                    
                    idEdit = Session["GrupoNoticiaEdit"].ToString(); // grupo de noticia a editar
                }
                catch (Exception)
                {
                
                }
                string numeros = "";
                try
                {
                    numeros = Session["idsNiticias"].ToString().Trim(); // ids de numeros de noticias seleccionadas
                }
                catch (Exception)
                {
                }
                if (numeros != "" && numeros != null)
                {
                   idEdit = "permitir";  
                }
                

                if (idEdit != "" && idEdit != null)
                {
                    bool NoCargado = true;
                    try
                    {

                        this.lblTablaDos.Text = crearTablaNoticiasSeleccionadas(numeros);
                        if (this.lblTablaDos.Text == "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
                        {
                            this.lblTablaDos.Text = "<table><tr><td><ul id='sortable2' class='droptrue'>" + "</ul></ul></td></tr></table>";
                                                
                        }

                        this.lblTablaNoticias.Text = crearTablaNoticiasNoSeleccionadas(numeros);
                        if (this.lblTablaNoticias.Text == "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
                        {
                            this.lblTablaNoticias.Text = "<table><tr><td><ul id='sortable1' class='droptrue'>" + "</ul></ul></td></tr></table>";

                        }

                        this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltroSortMostrarNoticias();
                        
                        NoCargado = false;
                    }
                    catch (Exception)
                    {
                    }
                    if (NoCargado)
                    {
                        mostrarTablarNoticiasUl();
                        
                    }
                    
                }
                else {
                    mostrarTablarNoticiasUl();  
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

            private void mostrarTablarNoticias()
            {
                
        }

        public void mostrarTablarNoticiasUl()
        {
            this.lblTablaNoticias.Text = new PNegocio.Administrador.Noticia().consultarNoticiasUl("");
            if (this.lblTablaNoticias.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltroSortMostrarNoticias();

                string html = "<ul id='sortable2' class='droptrue'>" + "</ul>";
                html += "</ul>";
                this.lblTablaDos.Text = html;
            }
        }

        public void cargarNoticiasSeleccionadas(string idGrupo)
        {
            this.lblTablaDos.Text = new PNegocio.Administrador.Noticia().consultarNoticiasUlSeleccionadas(idGrupo, "");
            if (this.lblTablaDos.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                
            }
        }

        public void cargarNoticiasNoseleccionadas(string idGrupo)
        {
            PNegocio.Administrador.Noticia instancia = new PNegocio.Administrador.Noticia();
            this.lblTablaNoticias.Text = instancia.consultarNoticiasUlNoSeleccionados(idGrupo, "");
            if (this.lblTablaNoticias.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltroSortMostrarNoticias();
                
            }
        }

        
                

        protected void btnSelectNoticia_Click(object sender, EventArgs e)
        {
            guardarIds();
        }

        private void guardarIds() {
           
            Session["permitir"] = "linkGrupoNoticia";
            Session["idsNiticias"] = this.hidIdSelected.Value.Trim();
            Response.Redirect("GrupoNoticia.aspx"); 
            
        }

        private string crearTablaNoticiasSeleccionadas(string numeros)
        {
            string[] split = numeros.Split(new Char[] { ',' });
            PNegocio.Administrador.Noticia instancia = new PNegocio.Administrador.Noticia();
            return instancia.consultarNoticiasSeleccionadasMotrarEnSortTable(split, "dentro", "sort");
        }
        private string crearTablaNoticiasNoSeleccionadas(string numeros)
        {
            string[] split = numeros.Split(new Char[] { ',' });
            PNegocio.Administrador.Noticia instancia = new PNegocio.Administrador.Noticia();
            return instancia.consultarNoticiasSeleccionadasMotrarEnSortTable(split, "fuera", "sort");
        }

    }
}