using PNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proveedores
{
    public partial class datosMaestros : System.Web.UI.Page
    {
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
                        if (idPantallas[i] == 8)
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

                DatoMaestro dm = new DatoMaestro();
                string userName = HttpContext.Current.User.Identity.Name;

                try
                {
                    List<string[]> listaDiferentesInstancias = (List<string[]>)Session["listaDiferentesInstancias"];

                    if (listaDiferentesInstancias.Count > 0)
                    {
                        for (int i = 0; i < listaDiferentesInstancias.Count; i++)
                        {
                            try
                            {
                                PEntidades.SrvSAPUProv.ZELIFNR_PROV[] objLifnr;

                                string[] splitLifnr = listaDiferentesInstancias[i][3].Split(new Char[] { ',' });

                                objLifnr = PEntidades.Utiles.objetoLifnr(splitLifnr);

                                List<PEntidades.Proveedor> proveedoresPorIsntancia;

                                proveedoresPorIsntancia = dm.getDatoMaestro(objLifnr,
                                    listaDiferentesInstancias[i][1].ToString().Trim(),  // endpoint
                                listaDiferentesInstancias[i][4].Split(new Char[] { ',' }) // pass
                                    );

                                Panel panelTotal = new Panel();

                                Literal imagenMaestros = new Literal();
                                imagenMaestros.Text = "<img src='../images/ico-datos_maestros.png'/>";
                                panelTotal.Controls.Add(imagenMaestros);

                                Label etiquetaInstancia = new Label();
                                etiquetaInstancia.Text = listaDiferentesInstancias[i][6].ToString().Trim();
                                etiquetaInstancia.CssClass = "lblDM_H";
                                panelTotal.Controls.Add(etiquetaInstancia);

                                for (int j = 0; j < proveedoresPorIsntancia.Count; j++)
                                {

                                    panelTotal.Controls.Add(pintarDatosMestros(proveedoresPorIsntancia[j]));
                                }
                                pnlDatosMaestros.Controls.Add(panelTotal);

                            }
                            catch (Exception)
                            {
                                //throw;
                            }
                        }
                    }
                    else
                    {
                        this.lblTabla.Text = "<br/><br/><br/><h3>" + "Este usuario no tiene sociedades activas, por lo que no puede obtener datos" + "</h3>";
                    }




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
            Response.Redirect("Inicio.aspx");
        }

        

        

        private Panel pintarDatosMestros(PEntidades.Proveedor objProveedor)
        {
            //------
                Panel panelInterno = new Panel();
                panelInterno.CssClass = "datMtrWrap";
                //------
                Table tabla1_1 = new Table();
                tabla1_1.CssClass = "tblDatMtr";
                    //------
                    TableRow tr_1 = new TableRow();

                    TableCell celda1_1 = new TableCell();
                    celda1_1.Text = "N° Proveedor";

                    TableCell celda1_2 = new TableCell();
                    Label lbltexto1_2 = new Label();
                    lbltexto1_2.Text = "";
                    lbltexto1_2.CssClass = "lblDM";
                    lbltexto1_2.Text = objProveedor.Liftnr;
                    celda1_2.Controls.Add(lbltexto1_2);

                    tr_1.Cells.Add(celda1_1);
                    tr_1.Cells.Add(celda1_2);
                    //------
                    TableRow tr_2 = new TableRow();

                    TableCell celda2_1 = new TableCell();
                    celda2_1.Text = "País";

                    TableCell celda2_2 = new TableCell();
                    Label lbltexto2_2 = new Label();
                    lbltexto2_2.Text = "";
                    lbltexto2_2.CssClass = "lblDM";
                    lbltexto2_2.Text = objProveedor.NAMECOUNTRY.ToUpper();
            celda2_2.Controls.Add(lbltexto2_2);

                    tr_2.Cells.Add(celda2_1);
                    tr_2.Cells.Add(celda2_2);
                    //------
                    TableRow tr_3 = new TableRow();

                    TableCell celda3_1 = new TableCell();
                    celda3_1.Text = "Nombre";

                    TableCell celda3_2 = new TableCell();
                    Label lbltexto3_2 = new Label();
                    lbltexto3_2.Text = "";
                    lbltexto3_2.CssClass = "lblDM";
                    lbltexto3_2.Text = objProveedor.NAME1.ToUpper();
            celda3_2.Controls.Add(lbltexto3_2);

                    tr_3.Cells.Add(celda3_1);
                    tr_3.Cells.Add(celda3_2);
                    //------
                    TableRow tr_4 = new TableRow();

                    TableCell celda4_1 = new TableCell();
                    celda4_1.Text = "Ciudad";

                    TableCell celda4_2 = new TableCell();
                    Label lbltexto4_2 = new Label();
                    lbltexto4_2.Text = "";
                    lbltexto4_2.CssClass = "lblDM";
                    lbltexto4_2.Text = objProveedor.ADDR1_DATA.ToUpper();
            celda4_2.Controls.Add(lbltexto4_2);


                    tr_4.Cells.Add(celda4_1);
                    tr_4.Cells.Add(celda4_2);
                    //------
                    tabla1_1.Rows.Add(tr_1); tabla1_1.Rows.Add(tr_2); tabla1_1.Rows.Add(tr_3); tabla1_1.Rows.Add(tr_4);
                    //this.pnlDatosMaestros.Controls.Add(espacio);
                //------
                Table tabla2_1 = new Table();
                tabla2_1.CssClass = "tblDatMtr";
                    //------
                TableRow tr_1_2 = new TableRow();

                TableCell celda1_1_2 = new TableCell();
                    celda1_1_2.Text = "Estado";

                    TableCell celda1_2_2 = new TableCell();
                     Label lbltexto5_2 = new Label();
                     lbltexto5_2.Text = "";
                     lbltexto5_2.CssClass = "lblDM";
                     lbltexto5_2.Text = objProveedor.NAMEREGION.ToUpper();
                     celda1_2_2.Controls.Add(lbltexto5_2);


                     tr_1_2.Cells.Add(celda1_1_2);
                     tr_1_2.Cells.Add(celda1_2_2);
                    //------
                     TableRow tr_2_2 = new TableRow();

                     TableCell celda2_1_2 = new TableCell();
                     celda2_1_2.Text = "Calle";

                     TableCell celda2_2_2 = new TableCell();
                     Label lbltexto6_2 = new Label();
                     lbltexto6_2.Text = "";
                     lbltexto6_2.CssClass = "lblDM";
                     lbltexto6_2.Text = objProveedor.STREET.ToUpper();
            celda2_2_2.Controls.Add(lbltexto6_2);


                     tr_2_2.Cells.Add(celda2_1_2);
                     tr_2_2.Cells.Add(celda2_2_2);
                    //------
                     TableRow tr_3_2 = new TableRow();

                     //TableCell celda3_1_2 = new TableCell();
                     //celda3_1_2.Text = "Colonia";

                     //TableCell celda3_2_2 = new TableCell();
                     //Label lbltexto7_2 = new Label();
                     //lbltexto7_2.Text = "";
                     //lbltexto7_2.CssClass = "lblDM";
                     //lbltexto7_2.Text = "";
                     //celda3_2_2.Controls.Add(lbltexto7_2);
                     //tr_3_2.Cells.Add(celda3_1_2);
                     //tr_3_2.Cells.Add(celda3_2_2);
                    //------
                    TableRow tr_4_2 = new TableRow();

                    TableCell celda4_1_2 = new TableCell();
                    celda4_1_2.Text = "CP";

                    TableCell celda4_2_2 = new TableCell();
                     Label lbltexto8_2 = new Label();
                     lbltexto8_2.Text = "";
                     lbltexto8_2.CssClass = "lblDM";
                     lbltexto8_2.Text = objProveedor.POST_CODE1;
                     celda4_2_2.Controls.Add(lbltexto8_2);

                     tr_4_2.Cells.Add(celda4_1_2);
                     tr_4_2.Cells.Add(celda4_2_2);
                    //------
                    tabla2_1.Rows.Add(tr_1_2); tabla2_1.Rows.Add(tr_2_2); tabla2_1.Rows.Add(tr_3_2); tabla2_1.Rows.Add(tr_4_2);
                //------

            //------
                    panelInterno.Controls.Add(tabla1_1);
                    panelInterno.Controls.Add(tabla2_1);
                    return panelInterno;
        }
    }
}