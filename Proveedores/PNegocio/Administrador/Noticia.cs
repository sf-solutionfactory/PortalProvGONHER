using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Noticia
    {
        public Noticia() { }

        public List<string> obtenerEMails()
        {
            List<string> list = new List<string>();
            //list
            return list;
        }

        public string insertarNoticia(string titulo, string body, string fechaInicio, string fechaFin, string URLImagen, int tipoNoticia)
        {
            //string res = "";
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdInsertNoticia(titulo, body, fechaInicio, fechaFin, URLImagen, tipoNoticia);
            //return res;
        }

        public string modificarNoticia(string id, string titulo, string body, string fechaInicio, string fechaFin, string URLImagen, int tipoNoticia)
        {
            //string res = "";
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsModificarNoticia(id, titulo, body, fechaInicio, fechaFin, URLImagen, tipoNoticia);
            //return res;
        }

        public string insertarGrupoNoticia(string nombreGrupo, String idNoticia, String[] grupoProveedores, string[] grupoNoticias)
        {
            //string res = "";
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            string sqlString = "";
            sqlString += "BEGIN TRY ";
            sqlString += "BEGIN TRAN ";
            sqlString += "insert into grupoNoticia(nombre) values('" + nombreGrupo + "');";
            String getId = "declare @idActual int;" +
            "select @idActual =  @@IDENTITY;";
            //String inserts = "insert into GrupoNoticia_proveedor values(@idActual,'" + grupoProveedores[0].ToString() + "');";
            String inserts = "";
            string idnoticia = "";
            string idprovedor = "";
            for (int i = 0; i < grupoProveedores.Length; i++)
            {
                idprovedor += grupoProveedores[i].ToString();
            }
            for (int i = 0; i < grupoNoticias.Length; i++)
            {
                inserts += "if not exists (select noticia_idNoticia from GrupoNoticia_Noticia as n " +
                            "inner join GrupoNoticia_proveedor as p on n.grupoNoticia_IdGruponoticia = p.grupoNoticia_idGruponoticia " +
                            "where p.proveedor_idProveedor in (" + idprovedor + ") and n.noticia_idNoticia = " + grupoNoticias[i].ToString().Trim() + ") " +
                            "begin ";
                inserts += "insert into GrupoNoticia_Noticia values(@idActual, " + grupoNoticias[i].ToString().Trim() + ");";
                inserts += "end ";
                idnoticia += grupoNoticias[i].ToString().Trim();
            }
            for (int i = 0; i < grupoProveedores.Length; i++)
            {
                inserts += "if not exists (select noticia_idNoticia from GrupoNoticia_Noticia as n " +
                            "inner join GrupoNoticia_proveedor as p on n.grupoNoticia_IdGruponoticia = p.grupoNoticia_idGruponoticia " +
                            "where p.proveedor_idProveedor = " + grupoProveedores[i].ToString() + " and n.noticia_idNoticia in (" + idnoticia + ")) " +
                            "begin ";
                inserts += "insert into GrupoNoticia_proveedor values(@idActual,'" + grupoProveedores[i].ToString() + "'); ";
                inserts += "end ";
            }
            sqlString = sqlString + getId + inserts;
            sqlString += "COMMIT TRAN ";
            sqlString += "END TRY ";
            sqlString += "BEGIN CATCH ";
            sqlString += "ROLLBACK TRAN ";
            sqlString += "END CATCH ";
            return ejec.ejcQuery(sqlString);
            //return res;
        }

        public string modificarGrupoNoticia(string idGrupoModificar, string nombreGrupo, String idNoticia, List<string> grupoProveedores, string[] grupoNoticias)
        {
            //string res = "";
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            string sqlString = "";
            sqlString += " DECLARE @informe char(50); ";
            sqlString += " BEGIN TRY ";
            sqlString += " BEGIN TRAN ";
            sqlString += " update grupoNoticia set nombre = '" + nombreGrupo + "'  where idGrupoNoticia = " + idGrupoModificar + "; ";

            sqlString += " delete from GrupoNoticia_proveedor where gruponoticia_idgruponoticia = " + idGrupoModificar;
            sqlString += " delete from  GrupoNoticia_Noticia where grupoNoticia_idGrupoNoticia = " + idGrupoModificar;
            String inserts = "";
            for (int i = 0; i < grupoProveedores.Count; i++)
            {
                inserts += " insert into GrupoNoticia_proveedor values(" + idGrupoModificar + ",'" + grupoProveedores[i].ToString() + "');";
            }

            for (int i = 0; i < grupoNoticias.Length; i++)
            {
                inserts += " insert into GrupoNoticia_Noticia values(" + idGrupoModificar + ", " + grupoNoticias[i].ToString().Trim() + ");";
            }

            sqlString = sqlString + inserts;
            sqlString += " set @informe = 'El grupo fue modificado correctamente' ";
            sqlString += " COMMIT TRAN ";
            sqlString += " END TRY ";
            sqlString += " BEGIN CATCH ";
            sqlString += " set @informe = 'Ocurrió un error inesperado' ";
            sqlString += " ROLLBACK TRAN ";
            sqlString += " END CATCH ";
            sqlString += " select @informe as informe ";
            return ejec.ejcQuery(sqlString);
            //return res;
        }

        public string consultarNoticias(string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            //return Gen.Util.CS.Gen.convertToHtmlTable(ejec.ejcPsdConsultaNOticia(), "tableToOrder", "tblComun' style='width:" + ancho + ";");
            List<string[]> resultado = ejec.ejcPsdConsultaNOticia();
            if (resultado.Count > 1)
            {
                for (int i = 1; i < resultado.Count; i++ )
                {
                    if (resultado[i][6].ToString().Trim() == "True")
                    {
                        resultado[i][6] = "General";  
                    }
                    else{
                        resultado[i][6] = "Asignable";  
                    }
                    


                }
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, true, true, true, false, 0, 1);
            }
            else
            {
                return "<div>No se encontraron resultados para mostrar en la tabla</div>";
            }
        }

        public string consultarNoticiasUl(string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
           string sqlString = "";
            sqlString += "select idNoticia as I,'<div>Titulo: </div>' + titulo as Titulo,'<div>Contenido: </div>' + (select SUBSTRING((select cuerpo),0,8000)) as Contenido, " +
            "'<div>Fecha de inicio: </div>' + cast(fechaInicio as CHAR(12)) as Fecha_Inicio,'<div>Fecha de fin: </div>' + cast(fechaFin as CHAR(12)) as Fecha_fin, " +
            "'<div>URL de la imagen: </div>' + (select SUBSTRING((select urlImagen),0,100)) as URL_de_imagen from noticia where tipoNoticia = 0 ";
            List<string[]> resultado = ejec.ejcPsdConsulta(sqlString);
            //List<string[]> resultado = ejec.ejcPsdConsultaNOticia();
            if (resultado.Count > 1)
            {
                return Gen.Util.CS.Gen.convertToHtmlTableSort(resultado, "sortable1", "droptrue' style='width:" + ancho + ";");
            }
            else
            {
                return "<div>No se encontraron resultados para mostrar en la tabla</div>";
            }
        }

        public string consultarNoticiasUlSeleccionadas(string idGrupo, string ancho) // esto es por grupo de noticia guardado
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaNOticiaEnGrupo(idGrupo);
            if (resultado.Count > 1)
            {
                //return Gen.Util.CS.Gen.convertToHtmlTableSortGrupoNoticia(resultado, "sortable2", "droptrue' style='width:" + ancho + ";", 1);
                return Gen.Util.CS.Gen.convertToHtmlTableSort(resultado, "sortable1", "droptrue' style='width:" + ancho + ";");
            }
            else
            {
                return "<div>No se encontraron resultados para mostrar en la tabla</div>";
            }
        }

        public string consultarNoticiasUlNoSeleccionados(string idGrupo, string ancho) // esto es por grupo de noticia guardado
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaNOticiaNoSelect(idGrupo);
            if (resultado.Count > 1)
            {
                //return Gen.Util.CS.Gen.convertToHtmlTableSortGrupoNoticia(resultado, "sortable1", "droptrue' style='width:" + ancho + ";", 1);
                return Gen.Util.CS.Gen.convertToHtmlTableSort(resultado, "sortable1", "droptrue' style='width:" + ancho + ";");
            }
            else
            {
                return "<div>No se encontraron resultados para mostrar en la tabla</div>";
            }
        }

        public List<string[]> consultarGrupoNoticiaPorId(string id)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaGrupoNoticiaPorId(id);
            return resultado;

        }

        public string consultarProveedorPorGrNoticia(string idGGrNoticia, string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaProvedorPorGrupoNoticia(idGGrNoticia);
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, false, false, true, false, 0, 1);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }



        public List<string[]> consultarNoticiaPorId(string sqlString)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsulta(sqlString);
            return resultado;
        }

        public string consultarNoticiasSeleccionadas(string[] numeros, string especificacion, string tipoTabla) // noticias seleccionadas, especificacion dentro(in), fuera(not in)
        {
            string sqlString = "";
            if (especificacion == "dentro")
            {
                sqlString += "select idNoticia as I, titulo as Titulo, (select SUBSTRING((select cuerpo),0,8000)) as Contenido, " +
                             "fechaInicio as Fecha_Inicio, fechafin as Fecha_fin, urlImagen as URL_de_imagen, esbloq as Estatus from noticia " +
                              "where idNoticia in (";
            }
            else
            {
                sqlString += "select idnoticia as I, titulo as Titulo, (select SUBSTRING((select cuerpo),0,8000)) as Contenido, " +
                             "fechainicio as Fecha_Inicio, fechafin as Fecha_fin, urlImagen as URL_de_imagen, esbloq as Estatus from noticia " +
                             "where idNoticia not in (";
            }


            for (int i = 0; i < numeros.Length; i++)
            {
                sqlString += numeros[i];
                if (i + 1 < numeros.Length)
                {
                    sqlString += ",";
                }

            }
            sqlString += ")";

            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsulta(sqlString);
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                if (especificacion == "dentro" && tipoTabla == "sort")
                {
                    return Gen.Util.CS.Gen.convertToHtmlTableSortGrupoNoticia(resultado, "sortable2", "droptrue' style='width:" + "auto" + ";", 1);
                }
                else if (especificacion == "fuera" && tipoTabla == "sort")
                {
                    return Gen.Util.CS.Gen.convertToHtmlTableSortGrupoNoticia(resultado, "sortable1", "droptrue' style='width:" + "auto" + ";", 1);

                }
                else
                {
                    return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + "90%" + ";", listaEvitar, false, false, false, false, 0, 1);
                }

            }
            else
            {
                return "<div>No se encontraron resultados para mostrar en la tabla</div>";
            }
        }

        public string consultarNoticiasSeleccionadasMotrarEnSortTable(string[] numeros, string especificacion, string tipoTabla) // noticias seleccionadas, especificacion dentro(in), fuera(not in)
        {
            string sqlString = "";
            if (especificacion == "dentro")
            {

                sqlString += "select idnoticia as I,'<div>Titulo: </div>' + titulo as Titulo,'<div>Contenido: </div>' + (select SUBSTRING((select cuerpo),0,100)) as Contenido, " +
                "'<div>Fecha de inicio: </div>' + cast(fechainicio as CHAR(12)) as Fecha_Inicio,'<div>Fecha de fin: </div>' + cast(fechafin as CHAR(12)) as Fecha_fin, " +
                "'<div>URL de la imagen: </div>' + (select SUBSTRING((select urlImagen),0,100)) as URL_de_imagen from noticia " +
                              "where idNoticia in (";
            }
            else
            {
                sqlString += "select idnoticia as I,'<div>Titulo: </div>' + titulo as Titulo,'<div>Contenido: </div>' + (select SUBSTRING((select cuerpo),0,100)) as Contenido, " +
                "'<div>Fecha de inicio: </div>' + cast(fechainicio as CHAR(12)) as Fecha_Inicio,'<div>Fecha de fin: </div>' + cast(fechafin as CHAR(12)) as Fecha_fin, " +
                "'<div>URL de la imagen: </div>' + (select SUBSTRING((select urlImagen),0,100)) as URL_de_imagen from noticia " +
                 "where idNoticia not in (";
            }


            for (int i = 0; i < numeros.Length; i++)
            {
                sqlString += numeros[i];
                if (i + 1 < numeros.Length)
                {
                    sqlString += ",";
                }

            }
            sqlString += ") and tipoNoticia = 0";

            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsulta(sqlString);
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                if (especificacion == "dentro" && tipoTabla == "sort")
                {
                    return Gen.Util.CS.Gen.convertToHtmlTableSort(resultado, "sortable2", "droptrue' style='width:" + "" + ";");

                }
                else if (especificacion == "fuera" && tipoTabla == "sort")
                {
                    return Gen.Util.CS.Gen.convertToHtmlTableSort(resultado, "sortable1", "droptrue' style='width:" + "" + ";");
                }
                else
                {
                    return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + "90%" + ";", listaEvitar, false, false, false, false, 0, 1);
                }

            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        public string consultarNoticiasGrupoEditar(List<string[]> resultado)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + "90%" + ";", listaEvitar, false, false, false, false, 0, 1);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        public string[] consultarNoticiaPorIdProveedor(string idProveedor)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdConsultaNOticiaPorIdProveedor(idProveedor);
        }

        public string consultarGruposDeNoticia(string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaGruposNoticia();
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, true, false, true, true, 0, 1);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

    }
}
