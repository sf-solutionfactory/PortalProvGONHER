using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Roles
    {
        public string consultarRoles(ref int cont)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<int> listaEvitar = new List<int>();
            listaEvitar.Add(1);
            listaEvitar.Add(2);
            listaEvitar.Add(4);
            listaEvitar.Add(8);
            listaEvitar.Add(16);
            listaEvitar.Add(32);
            List<string[]> lista = ejec.ejcPsdConsultaRoles();
            cont = lista.Count;
            return Gen.Util.CS.Gen.convertToHtmlTableDelete(lista, "tableToOrder", "tblComun' style='width:90%;", listaEvitar, true, true, false, false, 0, 1);
        }

        public string ejcPsdConsultaRolPorProvedor(string proveedor)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaRolPorProvedor(proveedor);
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:90%;", listaEvitar, false, false, true, false, 0, 1);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }

        }

        public List<string[]> consultarRolesArray()
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdConsultaRoles();
        }

        public string insertarRol(string nombreRol, string esActive, string facturas, string partidas, string pagos, string datosMaestros, string usuarios, string facturasFV60, string esCreacion)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdInsertRoles(nombreRol, esActive, facturas, partidas, pagos, datosMaestros, usuarios, facturasFV60, esCreacion);
            
        }

        public string actualizaRol(string nombreRol, string esActive, string facturas, string partidas, string pagos, string datosMaestros, string usuarios, string facturasFV60, string idAnterior, string esCreacion)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdActualizaRol(nombreRol, esActive, facturas, partidas, pagos, datosMaestros, usuarios, facturasFV60, idAnterior, esCreacion);
        }

        public List<string[]> consultarRolPorId(string id)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaRolPorId(id);
            return resultado;
        }

        public string asignarRol(string usuario, string rol, string mode)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdAsignarRol(usuario, rol, mode);

        }
        public List<string[]> roldeFaul(string sqlString)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsulta(sqlString);
            return resultado;
        }
    }
}
