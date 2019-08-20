using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores.administrator
{
    public partial class MostrarPertenencia : System.Web.UI.Page
    {
        string vinculador = "" ;
        List<string[]> resultado = new List<string[]>();
        List<PEntidades.ArrTablas> tablas = new List<PEntidades.ArrTablas>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["esAdmin"] = "0";
            if (this.hidCerrarSesion.Value != "cerrar")
            {

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
                this.vinculador = Request.QueryString["vinculador"];
                this.hidVinculador.Value = Request.QueryString["vinculador"];
                this.hidCampo.Value = Request.QueryString["campo"];
                this.hidPrimerProveedor.Value = Request.QueryString["primerproveedor"];
                if (String.IsNullOrEmpty(Request.Form["letra"]))
                {
                    mostrarTablaroveedores();
                }
                else
                {
                    mostrarpor(Request.Form["letra"]);
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

        private void mostrarTablaroveedores()
        {
            int evitarCols = 0;
            bool editable = false;

            if (vinculador != "Proveedores")
            {
            }
            else {
                editable = true;
            }

            this.lblResultado.Text = new PNegocio.Administrador.Proveedor().consultarProveedores("90%",false, editable, false, evitarCols, ref resultado, ref tablas);
            Session["Tablas"] = tablas;
            if (this.lblResultado.Text != "<strong>No se encontraron resultados para mostrar en la tabla</strong>")
            {
                if (!editable)
                {
                    this.lblInfoProveedores.Text = "<strong>Seleccione alguno de los proveedores, para su elección clic sobre el registro</strong>";
                    this.btnDescargarE.Visible = true;
                }
                else {
                    this.lblInfoProveedores.Text = "<strong>Activar y desactivar</strong>";

                }
                this.lblTablaFiltro.Text = PNegocio.Administrador.TextoFiltro.textoTablaFiltro();
            }
            else {
                this.lblComplementoFail.Text = "Para realizar carga de proveedores elija en el menu :"+
                 "\n proveedores, <strong>''Carga automatica...''<strong>";

            }
        }

        private void mostrarpor(string letra)
        {
            bool editable = false;
            tablas = (List<PEntidades.ArrTablas>)Session["Tablas"];
            if (letra.Length > 1)
            {
                if (letra.Equals("0 - 9"))
                {
                    letra = letra.Substring(0, 1);
                }
            }
            if ((vinculador != "Proveedores") == false)
            {
                editable = true;
            }
            int inde = tablas.FindIndex(x => x.letra.Contains(letra));
            List<int> listaEvitar = new List<int>();
            if (inde > -1)
            {
                this.lblResultado.Text = Gen.Util.CS.Gen.convertToHtmlTableDelete(tablas[inde].tabla, "tableToOrder", "tblComun' style='width:" + "90%" + ";", listaEvitar, false, editable, false, false, 0, 1);
            }
            else
            {
                this.lblResultado.Text = "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
            
        }

        protected void btnDescargarE_Click(object sender, EventArgs e)
        {
            generarExcel();
        }
        private void generarExcel()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet 1");
            int contador = 1;
            string index;
            if (resultado.Count > 1)
            {
                foreach (string[] row in resultado)
                {
                    index = "A";
                    index = index + contador;
                    worksheet.Cell(index).Value = new[]
                 {
                  new { nombre=row[1], rfc=row[2], status = row[3], usu=row[4], grup=row[5]},
             };
                    contador++;
                }
                worksheet.Range("A1:E1").Style.Font.SetFontColor(XLColor.White).Fill.SetBackgroundColor(XLColor.FromHtml("#0B2161")).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold(true);
                worksheet.Range("A2:" + "A" + contador).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#CCCCCC"));
                worksheet.Range("B2:" + "B" + contador).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#B3B3B3"));
                worksheet.Range("C2:" + "C" + contador).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#CCCCCC"));
                worksheet.Range("D2:" + "D" + contador).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#B3B3B3"));
                worksheet.Range("E2:" + "E" + contador).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#CCCCCC"));
                worksheet.Column(1).AdjustToContents();
                worksheet.Column(2).AdjustToContents();
                worksheet.Column(3).AdjustToContents();
                worksheet.Column(4).AdjustToContents();
                worksheet.Column(5).AdjustToContents();
                workbook.SaveAs(@"c:\temp\Usuarios Proveedores.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=\"Usuarios Proveedores.xlsx\"");
                Response.WriteFile(@"C:\temp\Usuarios Proveedores.xlsx");
                Response.End();
            }
        }
    }
}