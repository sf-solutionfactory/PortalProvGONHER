//using PEntidades.SrvSAPResProveedores;
using PEntidades.SrvSAPUProv;
//using PEntidades.SrvSAPResSociedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class Proveedores : System.Web.UI.Page
    {
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
            if (String.IsNullOrEmpty(Request.Form["letra"]) && String.IsNullOrEmpty(Request.Form["procargado"]))
            {
                //this.btnCargaAutomaticaProv.Visible = false;
                cargarTablaProveedores();
                //this.txtNombreAsignar.Visible = false;
                cargarObjetos();
            }
            else
            {
                mostrarpor(Request.Form["letra"], Request.Form["procargado"]);
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

        
        public void cargarTablaProveedores() {

            this.lblTablaProveedores.Text = new PNegocio.Administrador.Proveedor().consultarDetProveedor("45%", ref tablas);
            Session["Tablas"] = tablas;
            if (this.lblTablaProveedores.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltroSort();
            }
        }

        public void cargarObjetos() {
            cargarObjeto1();
        }

        public void cargarObjeto1() {
            agregarTextoTablas(this.lblObjeto1, "sortable2");   
        }

        public void agregarTextoTablas(Label objeto,string id) {
            string html = "<ul id='"+id+"' class='droptrue'>" + "</ul>";
                   html+= "</ul>";
                   objeto.Text = html;
        }

        //public void cargaAutomaticaProveedores(string endpoint, string idInstancia)
        //{
        //    PNegocio.Administrador.Proveedores prvd = new PNegocio.Administrador.Proveedores();
        //    ZERES_USUARIOS[] tablaProveedores = prvd.ResProvedores(endpoint, PNegocio.Administrador.Seguridad.consultarUsuarioCOntrasenaInstancia(idInstancia));

        //    List<string> respuestas = new List<string>();

        //    int contadorBueno = 0;
        //    int contadorMalo = 0;
        //    int contadorProblemasX = 0;
        //    int contadorYaExistia = 0;

        //    for (int i = 0; i < tablaProveedores.Count(); i++)
        //    {
        //        try
        //        {
        //            string[] resInsert = new string[1];
        //            resInsert = prvd.insertarProveedores(
        //            tablaProveedores[i].STCEG.ToString(),
        //            tablaProveedores[i].NAME1.ToString(),
        //            tablaProveedores[i].LIFNR.ToString(),
        //            idInstancia
        //            );

        //            respuestas.Add(resInsert[0]);

        //            if (resInsert[0] != "error1" && resInsert[0] != "error2")
        //            {
        //                contadorBueno++;
        //            }
        //            else {
        //                    if (resInsert[0] == "error1")
        //                    {
        //                        contadorYaExistia++;
        //                    }
        //                    else {
        //                        contadorProblemasX++;
        //                    }
        //                contadorMalo++;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            this.lblDialog.Text = "Error al intentar conectar o encontrar el servicio";
        //        }
        //    }
        //    if (contadorBueno == tablaProveedores.Count())
        //    {
        //        Session["textoDialogo"] = "Todos los datos obtenidos fueron insertados correctamente, la catidad fue: " + contadorBueno;
        //        Response.Redirect("Proveedores.aspx");
        //    }
        //    else {
        //        this.lblDialog.Text = "Existieron algunos errores al insertar, al cantidad de errores obtenidos fue: " + contadorMalo + " de " + tablaProveedores.Count() + " existentes, " + contadorYaExistia + "fueron por que ya existia el registro y " + contadorProblemasX + " problemas internos de la BD";
                
        //    }


        //}

        
        protected void btnUnir_Click(object sender, EventArgs e)
        {
            if(this.hidVerificar.Value == "si"){
                realizarUnionDeProveedores();     
            }
           
        }

        public void realizarUnionDeProveedores() {
                string proveedores = this.hidIdSelected.Value;

                char[] delimiterChars = { ',' };
                String[] idProveedores = proveedores.Split(delimiterChars);

                if (proveedores == "C_E")
                {
                    this.lblDialog.Text = "No es posible crear grupos con proveedores extranjeros";    
                }
                else if (this.txtNombreGrupo.Text == "")
                {
                    this.lblDialog.Text = "Ingrese algun nombre de grupo";
                }
                else
                {
                    if (idProveedores.Length >= 2)
                    {
                        PNegocio.Administrador.Proveedores provedores = new PNegocio.Administrador.Proveedores();
                        this.lblResultado.Text = provedores.insertarUnionProveedores(idProveedores, this.txtNombreGrupo.Text);
                        Session["textoDialogo"] = "Se realizo la union correctamente";
                        Response.Redirect("Proveedores.aspx");
                    }
                    else
                    {
                        this.lblDialog.Text = "Debe selecionar al menos dos proveedores";
                    }
                }
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
            if (inde > -1)
            {
                this.lblTablaProveedores.Text = Gen.Util.CS.Gen.convertToHtmlTableSort(tablas[inde].tabla, "sortable1", "droptrue' style='width:" + "45%" + ";");
                this.lblObjeto1.Text = conttabl2.Replace('[', '<').Replace(']', '>');
            }
            else
            {
                this.lblTablaProveedores.Text = "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }

        }
    }
}