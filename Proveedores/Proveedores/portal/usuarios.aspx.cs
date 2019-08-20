using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.portal
{
    public partial class Usuarios : System.Web.UI.Page
    {
        string usuari;
        string nombre;
        string apellidos;
        string pass1;
        string pass2;
        string inicioVigencia;
        string FinVigencia;
        string esCambiarPassNext;
        string proveedor;
        string email;
        string email2;
        string creadoPor;

        string rol;

        List<String[]> listRoles;
        List<string[]> listaProveedor;


        String prov = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.hidCerrarSesion.Value != "cerrar")
            {
                //INICIO Permiso de ver esta pantalla
                bool permiso = false;
                try
                {
                    int[] idPantallas = (int[])Session["Pantallas"];
                    for (int i = 0; i < idPantallas.Length; i++)
                    {
                        if (idPantallas[i] == 16)
                        {
                            permiso = true;
                            break;
                        }

                    }
                    if (permiso == false)
                    {
                        cerrarSesion();
                    }

                }
                catch (Exception)
                {
                    cerrarSesion();
                }
                //FIN Permiso de ver esta pantalla

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

                prov = "";
                String nombre = "";
                String descripcion = "";

                try
                {
                    prov = Session["ProveedorLoged"].ToString();

                    if (prov != "" && prov != null)
                    {
                        if (this.cmbRol.Text == "" || this.cmbRol.Text == null)
                        {
                            listRoles = new PNegocio.Administrador.Roles().consultarRolesArray();
                            cmbRol.Items.Clear();
                            llenarDropDownRol(listRoles);
                        }
                        cargarTablaUsuarios(prov);
                        List<int> listaProveedor = new List<int>();
                        mostrarSociedades(cargarSociedades(prov), listaProveedor);
                        this.hidComplementoUr.Value = "rfc=" + prov + "&nom=" + nombre + "&desc=" + descripcion + "&campo=Proveedor&primerproveedor=me";
                    }

                    this.btnGuardarCambios.Visible = false;
                    this.ltlbtnCancel.Visible = false;
                }
                catch (Exception)
                {
                    this.btnGuardarCambios.Visible = false;
                    this.ltlbtnCancel.Visible = false;
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
                        else
                        {
                            try
                            {

                                cargarTablaUsuarios(prov);

                                listRoles = new PNegocio.Administrador.Roles().consultarRolesArray();
                                cmbRol.Items.Clear();
                                llenarDropDownRol(listRoles);
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }

                    }
                    catch (Exception)
                    {

                    }
                }
                cargarConfigPass();
            }


        }

        private void cerrarSesion()
        {
            //se borra la cookie de autenticacion
            System.Web.Security.FormsAuthentication.SignOut();
            //se redirecciona al usuario a la pagina de login
            Response.Redirect("Inicio.aspx");
        }

        public void llenarDropDownRol(List<string[]> lista)
        {
            for (int i = 1; i < lista.Count(); i++)
            {
                cmbRol.Items.Add(lista[i][1].ToString());
            }
        }

        public void cargarEdit(string id)
        {
            try
            {
                PNegocio.Administrador.Proveedores objInstancia = new PNegocio.Administrador.Proveedores();
                string sqlString = "select u.usuariolog, u.nombre, u.apellidos, u.usuarioPass, u.fechaIni, u.fechaIFn, u.email, u.proveedor_idProveedor,p.nombre, r.nombre as rol from usuario as u, proveedor as p,  rol as r where u.proveedor_idProveedor= p.idProveedor and usuarioLog = '" + id + "' and r.idROl = u.rol_idRol;";
                listaProveedor = objInstancia.consultarProveedoresPorId(sqlString);
                if (listaProveedor.Count > 1)
                {
                    Session["listaProveedor"] = listaProveedor;
                    this.txtIdUsuario.Text = listaProveedor[1][0];
                    this.txtIdUsuario.Enabled = false;
                    this.txtIdNombre.Text = listaProveedor[1][1];
                    this.txtIdApellidos.Text = listaProveedor[1][2];
                    this.cmbRol.Text = listaProveedor[1][9];
                    this.datepicker.Text = Gen.Util.CS.Gen.convertirFechaBDaFormatoJq(listaProveedor[1][4].ToString());
                    this.datepicker2.Text = Gen.Util.CS.Gen.convertirFechaBDaFormatoJq(listaProveedor[1][5].ToString());
                    if (this.datepicker.Text.Equals("01/01/2010") && this.datepicker2.Text.Equals("12/31/2099"))
                    {
                        this.ckbVigenciaIlimitada.Checked = true;
                    }
                    this.txtIdemail.Text = listaProveedor[1][6];
                    this.txtIdemailRepetir.Text = listaProveedor[1][6];
                    this.lblProveedorSelected.Text = listaProveedor[1][8];
                    this.hidId.Value = listaProveedor[1][0];
                    PNegocio.Administrador.Usuario instancia = new PNegocio.Administrador.Usuario();

                    List<string[]> ListaUsuarioSoc = instancia.cosultarUsuarioSociedad(id); // lista de sociedades que tiene actulmente asignadas
                    List<string[]> socPorProv = cargarSociedades(prov);
                    List<int> lista = obtenerActivados(ListaUsuarioSoc, socPorProv);
                    mostrarSociedades(socPorProv, lista);


                }
                cargarTablaUsuarios(listaProveedor[1][7]);

                this.btnEnviar.Visible = false;
                this.btnGuardarCambios.Visible = true;
            }
            catch (Exception)
            {
                this.btnGuardarCambios.Visible = false;
                this.ltlbtnCancel.Visible = false;
            }
        }

        public void cargarTablaUsuarios(string prov)
        {

            this.lblTablaUsuarios.Text = new PNegocio.Administrador.Usuario().cosultarUsuariosPorFiltroEnString(prov, "90%");
            if (this.lblTablaUsuarios.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltro();
                this.lblExplicacionResultados.Text = "<strong>Estos son los usuarios que corresponden al proveedor que elegiste:</strong>";
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {

            if (this.hidVerificar.Value == "noEmail")
            {
                this.lblDialog.Text = "El email no cumple con las caracteristicas necesarias";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
            }
            else if (this.hidVerificar.Value == "no")
            {
                this.lblDialog.Text = "Exisaten campos si valor";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
            }
            else if (this.hidVerificar.Value == "si" && this.hidVerificarPass.Value == "si")
            {
                insertarUser();
            }
            else
            {
                this.lblDialog.Text = "La contraseña no cumple con las caracteristicas necesarias";
            }


        }
        public void insertarUser()
        {
            usuari = this.txtIdUsuario.Text; // int
            nombre = this.txtIdNombre.Text;
            apellidos = this.txtIdApellidos.Text;
            pass1 = this.txtPassword.Text;
            pass2 = this.txtPasswordRepetir.Text;
            inicioVigencia = this.datepicker.Text.ToString().Trim();// datetime    this.txtInicioVigencia.Text;
            FinVigencia = this.datepicker2.Text.ToString().Trim();// datetime   this.txtFinVigencia.Text;
            proveedor = this.lblProveedorSelected.Text; // int id
            creadoPor = "0"; // int
            try
            {
                creadoPor = Session["ProveedorLoged"].ToString();
            }
            catch (Exception)
            {
                creadoPor = "0"; // int
            }
            if (creadoPor == "")
            {
                creadoPor = "0";
            }
            email = this.txtIdemail.Text;
            email2 = this.txtIdemailRepetir.Text;
            rol = this.cmbRol.Text;
            string[] numeros = obtenerSociedades(); // numerador de las filas
            List<string[]> listaSociedades = obtenerDatsSocSel(numeros); // las sociedades que hacen referencia al numerador 

            if (pass1 == pass2 && email == email2)
            {
                if (this.ckbCambiarPassNext.Checked)
                {
                    esCambiarPassNext = "1";
                }
                else
                {
                    esCambiarPassNext = "0";
                }

                inicioVigencia = Gen.Util.CS.Gen.convertirFecha(inicioVigencia);
                FinVigencia = Gen.Util.CS.Gen.convertirFecha(FinVigencia);

                PNegocio.Administrador.Usuario us = new PNegocio.Administrador.Usuario();
                PNegocio.Encript encript = new PNegocio.Encript();

                switch (
                us.insertarUsuario(usuari, nombre, apellidos, encript.Encriptar(encript.Encriptar(pass1)), inicioVigencia.Trim(), FinVigencia.Trim(),
                prov, esCambiarPassNext, creadoPor, email, rol, listaSociedades)
                )
                {
                    case "insertado":
                        List<string> listaEmail = new List<string>();
                        listaEmail.Add(email);
                        bool enviadoEmail = PNegocio.EnviarEmail.SendMail(listaEmail, null, null, usuari, txtPassword.Text);
                        cargarTablaUsuarios(prov);
                        Session["textoDialogo"] = "Insertado";
                        Response.Redirect("usuarios.aspx?" + this.hidComplementoUr.Value);
                        break;
                    case "nombre existente":
                        this.lblDialog.Text = "El nombre de usuario ya existe";
                        break;
                    case "error":
                        this.lblDialog.Text = "Error al insertar";
                        break;
                    default:
                        this.lblDialog.Text = "Error desconocido";
                        break;
                    case "limite":
                        this.lblDialog.Text = "El numero maximo permitido de usuarios fue alcanzado";
                        break;
                }

            }
            else
            {
                this.lblDialog.Text = "El password o el email no coinciden";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (pass1 == null || pass1 == "" &&
             pass2 == null || pass2 == "")
            {
                this.hidVerificarPass.Value = "si";
            }
            if (this.hidVerificarPass.Value == "si")
            {
                guardarCambios();
            }
        }

        public void guardarCambios()
        {
            usuari = this.txtIdUsuario.Text.Trim(); // int
            nombre = this.txtIdNombre.Text.Trim();
            apellidos = this.txtIdApellidos.Text.Trim();
            pass1 = this.txtPassword.Text.Trim();
            pass2 = this.txtPasswordRepetir.Text.Trim();
            inicioVigencia = this.datepicker.Text.ToString().Trim();// datetime    this.txtInicioVigencia.Text;
            FinVigencia = this.datepicker2.Text.ToString().Trim();// datetime   this.txtFinVigencia.Text;
            email = this.txtIdemail.Text.Trim();
            email2 = this.txtIdemailRepetir.Text.Trim();
            rol = this.cmbRol.Text.Trim();
            string mensaje = "Buen dia " + usuari + ". \r Estos son los datos que fueron modificados en su usuario: \r";
            string asunto = "Modifcacion de cuenta de usuario.";
            creadoPor = "1"; // int

            bool verificarContrasena = true;
            bool verificarEmailb = true;
            bool entrarAModificar = false;

            if (pass1 == null || pass1 == "" &&
                pass2 == null || pass2 == ""
                )
            {
                verificarContrasena = false;
            }

            if (email == null || email == "" &&
                email2 == null || email2 == ""
                )
            {
                verificarEmailb = false;
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
                if (verificarEmailb)
                {
                    if (email == email2)
                    {
                        entrarAModificar = true;
                    }
                    else
                    {
                        entrarAModificar = false;

                        this.lblDialog.Text = "Email no coincide";
                    }
                }
            }


            if (entrarAModificar)
            {
                if (this.ckbCambiarPassNext.Checked)
                {
                    esCambiarPassNext = "1";
                }
                else
                {
                    esCambiarPassNext = "0";
                }

                PNegocio.Administrador.Usuario us = new PNegocio.Administrador.Usuario();
                PNegocio.Encript encript = new PNegocio.Encript();

                listaProveedor = (List<string[]>)Session["listaProveedor"];

                if (usuari == null || usuari == "")
                {

                    usuari = listaProveedor[1][0].Trim();
                }
                if (nombre == null || nombre == "")
                {
                    nombre = listaProveedor[1][1].Trim();
                }
                if (apellidos == null || apellidos == "")
                {

                    apellidos = listaProveedor[1][2].Trim();
                }
                if (ckbVigenciaIlimitada.Checked)
                {
                    mensaje += "<br>La vigencia es o sigue siendo permanente<br>";
                    inicioVigencia = Gen.Util.CS.Gen.convertirFecha(inicioVigencia);
                    FinVigencia = Gen.Util.CS.Gen.convertirFecha(FinVigencia);
                }
                else
                {

                    if (String.IsNullOrEmpty(inicioVigencia))
                    {
                        inicioVigencia = Gen.Util.CS.Gen.convertirFechaDesdeSesion(listaProveedor[1][4].Trim());
                    }
                    else
                    {
                        //inicioVigencia = Gen.Util.CS.Gen.convertirFechaDesdeSesion(inicioVigencia);
                        inicioVigencia = Gen.Util.CS.Gen.convertirFecha(inicioVigencia);
                        mensaje += "Vigencia de Inicio: " + inicioVigencia + "\n";
                    }
                    if (String.IsNullOrEmpty(FinVigencia))
                    {
                        //FinVigencia = listaProveedor[1][5].Trim();
                        FinVigencia = Gen.Util.CS.Gen.convertirFechaDesdeSesion(listaProveedor[1][5].Trim());
                    }
                    else
                    {
                        //FinVigencia = Gen.Util.CS.Gen.convertirFechaDesdeSesion(FinVigencia);
                        FinVigencia = Gen.Util.CS.Gen.convertirFecha(FinVigencia);
                        mensaje += "Vigencia de Fin: " + FinVigencia + " \n";
                    }

                }

                if (String.IsNullOrEmpty(email))
                {
                    email = listaProveedor[1][6].Trim();
                }
                else
                {
                    if (email != listaProveedor[1][6].Trim())
                    {
                        mensaje += "Email: " + email + " \r";
                    }
                }
                if (String.IsNullOrEmpty(pass1))
                {
                    pass1 = listaProveedor[1][3].Trim();
                }
                else
                {
                    pass1 = encript.Encriptar(encript.Encriptar(pass1));
                    mensaje += "Pass: " + txtPassword.Text + " \r";
                }

                if (listaProveedor[1][9] != rol)
                {
                    mensaje += "Rol nuevo: " + rol;
                }
                string[] numeros = obtenerSociedades(); // numerador de las filas
                List<string[]> listaSociedades = obtenerDatsSocSel(numeros); // las sociedades que hacen referencia al numerador 


                switch (
                us.ActualizaUsuario(usuari, nombre, apellidos, pass1, inicioVigencia.Trim(), FinVigencia.Trim(),
                esCambiarPassNext, creadoPor, email, this.hidId.Value.Trim(), rol, listaSociedades)
                )
                {
                    case "ok":
                        List<string> listaEmail = new List<string>();
                        listaEmail.Add(email);
                        bool enviadoEmail = PNegocio.EnviarEmail.SendMail(listaEmail, mensaje, asunto, null, null);
                        Session["textoDialogo"] = "Actualizado correctamente";
                        Response.Redirect("usuarios.aspx?" + this.hidComplementoUr.Value);
                        break;
                    case "error":
                        this.lblDialog.Text = "Error al insertar";
                        break;
                    case "no existe":
                        this.lblDialog.Text = "no se encontro el usuario, probablemente fue modificado por otro usuario";
                        break;
                    default:
                        this.lblDialog.Text = "Error desconocido";
                        break;
                }

                this.btnGuardarCambios.Visible = true;
                this.ltlbtnCancel.Visible = true;
            }
            else
            {
                this.lblDialog.Text = "El password o el email no coinciden";
                this.btnGuardarCambios.Visible = true;
                this.ltlbtnCancel.Visible = true;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarDialog()", true);
        }



        private List<string[]> cargarSociedades(string rfc)
        {
            PNegocio.Administrador.Usuario inst = new PNegocio.Administrador.Usuario();

            List<string[]> resultado = inst.cosultarSociedadesPorprov(rfc, "90%");
            return resultado;


        }

        private void mostrarSociedades(List<string[]> resultado, List<int> listaNumeros)
        {
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                this.ltlTablaSociedades.Text = Gen.Util.CS.Gen.convertToHtmlTableDelete2(resultado, "", "tblComun toCheck' style='width:" + "90%" + ";", listaEvitar, false, false, false, false, 0, 1, true, listaNumeros);
                Session["TablaSociedades"] = resultado;
            }
            else
            {
                this.ltlTablaSociedades.Text = "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        private string[] obtenerSociedades()
        {
            char[] delimiterChars = new char[1];
            string soc = this.hidValCheck.Value;
            delimiterChars[0] = ',';
            string[] nums;
            nums = soc.ToString().Split(delimiterChars);
            return nums;
        }

        private List<string[]> obtenerDatsSocSel(string[] numeros)
        {
            List<string[]> tablasoc = new List<string[]>();
            List<string[]> tablaRes = new List<string[]>();
            try
            {
                tablasoc = (List<string[]>)Session["TablaSociedades"];

                for (int i = 0; i < numeros.Length; i++)
                {
                    int J = int.Parse(numeros[i]);
                    string[] tablaRes1 = tablasoc[int.Parse(numeros[i])];
                    tablaRes.Add(tablaRes1);
                }
            }
            catch (Exception)
            {
            }
            return tablaRes;
        }



        private List<int> obtenerActivados(List<string[]> ListaUsuarioSoc, List<string[]> socPorProv)
        {
            List<int> activados = new List<int>();
            for (int i = 1; i < socPorProv.Count; i++)
            {
                for (int j = 1; j < ListaUsuarioSoc.Count; j++)
                {

                    if (
                        ListaUsuarioSoc[j][1].Trim() == socPorProv[i][1].Trim() &&
                        ListaUsuarioSoc[j][2].Trim() == socPorProv[i][2].Trim() &&
                        ListaUsuarioSoc[j][3].Trim() == socPorProv[i][3].Trim() &&
                        ListaUsuarioSoc[j][4].Trim() == socPorProv[i][0].Trim()
                        )
                    {
                        activados.Add(i);
                        break;
                    }

                }

            }

            return activados;
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
    }
}