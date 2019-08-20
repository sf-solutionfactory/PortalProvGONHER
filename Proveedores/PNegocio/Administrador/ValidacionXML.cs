using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class ValidacionXML
    {
        public ValidacionXML()
        {

        }

        public List<string[]> getGrupoValidacionXML()
        {
            return new PPersistencia.ejecutaProcedures().ejcPsdconsultarGrupoValidacionesXML();
        }

        public int[] getValFactByIdGrpVal(int idGrupoValidacion)
        {
            return new PPersistencia.ejecutaProcedures().ejcPsdConsultaValFactByIdGrpVal(idGrupoValidacion);
        }

        public string guardarNuevoGrupoValidacion(int idGrupoValidación,string nombreGrupoVal,List<int> idValidaciones, List<int> idProveedores, int idPant)
        {
            if (idGrupoValidación == 0)
            {
                return this.insertarNuevoGrupoValidacion(nombreGrupoVal, idValidaciones, idProveedores, idPant);
            }
            else
            {
                return this.actualizarNuevoGrupoValidacion(idGrupoValidación, nombreGrupoVal, idValidaciones, idProveedores, idPant);
            }
        }

        private string insertarNuevoGrupoValidacion(string nombreGrupoVal, List<int> idValidaciones, List<int> idProveedores, int idPant)
        {
            string sql = "INSERT INTO GrupoValidacion (descripcion,fecha_creacion,esBloq) VALUES ('" + nombreGrupoVal + "',GETDATE(),1);";
            sql += "declare @IdGrupoValidacion int;set @IdGrupoValidacion = @@Identity;";
            foreach (int idValidacion in idValidaciones)
            {
                sql += "INSERT INTO Grupo_Validacion_Factura(idGrupoValidacion,idValidacionFactura,idPantalla) VALUES (@IdGrupoValidacion," + idValidacion + "," + idPant + ");";
            }
            foreach (int idProveedore in idProveedores)
            {
                sql += "INSERT INTO Proveedor_GrupoValidacion(idProveedor,idGrupoValidacion) VALUES (" + idProveedore + ",@IdGrupoValidacion); ";
                sql += "select 'correcto'";
            }
            try
            {
                List<string[]> res = new PPersistencia.ejecutaProcedures().ejcPsdConsulta(sql);
                return res[1][0];
            }
            catch
            {
                return "incorrecto";
            }
        }

        private string actualizarNuevoGrupoValidacion(int idGrupoValidación, string nombreGrupoVal, List<int> idValidaciones, List<int> idProveedores, int idPant)
        {
            string sql = "UPDATE GrupoValidacion SET descripcion='" + nombreGrupoVal + "' WHERE idGrupoValidacion=" + idGrupoValidación + ";";
            sql += "DELETE Proveedor_GrupoValidacion WHERE idGrupoValidacion=" + idGrupoValidación + ";";
            sql += "DELETE grupo_validacion_factura WHERE idGrupoValidacion = " + idGrupoValidación + ";";

            foreach (int idValidacion in idValidaciones)
            {
                sql += "INSERT INTO Grupo_Validacion_Factura(idGrupoValidacion,idValidacionFactura,idPantalla) VALUES (" + idGrupoValidación+"," + idValidacion + "," + idPant + ");";
            }
            foreach (int idProveedore in idProveedores)
            {
                sql += "INSERT INTO Proveedor_GrupoValidacion(idProveedor,idGrupoValidacion) VALUES (" + idProveedore + "," + idGrupoValidación + "); ";
                sql += "select 'correcto' ";
            
                
            }
            try
            {
                List<string[]> res = new PPersistencia.ejecutaProcedures().ejcPsdConsulta(sql);
                return res[1][0];
            }
            catch
            {
                return "incorrecto";
            }
        }

        public bool eliminarGrupoValidacion(int idGrupoValidacion)
        {
            return new PPersistencia.ejecutaProcedures().ejcPsdEliminarGrupoValidacion(idGrupoValidacion);
        }
    }
}
