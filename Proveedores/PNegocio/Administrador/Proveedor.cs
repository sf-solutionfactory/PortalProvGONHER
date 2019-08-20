using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Proveedor
    {

        public Proveedor() { }

        public List<string[]> consultarproveedoresArray()
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdConsultaProveedores();
        }

        public string consultarProveedorPorGrProv(string idGGrNoticia, string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaProvedorPorGrupoprov(idGGrNoticia);
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, false, false, true, false, 0, 0);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        public string consultarProveedores(string ancho, bool modificable, bool activable, bool desechable, int evitar, ref List<string[]> resultado, ref List<PEntidades.ArrTablas> tablas)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            
            resultado = ejec.ejcPsdConsultaProveedores();
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();                
                tablas = PEntidades.Utiles.getTablasbyLetras(resultado, "tab");
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(tablas[0].tabla, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, modificable, activable, desechable, false, evitar, 1);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        public string consultarDetProveedor(string ancho, ref List<PEntidades.ArrTablas> tablas)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaDetProveedor();
            if (resultado.Count > 1)
            {
                tablas = PEntidades.Utiles.getTablasbyLetras(resultado, "tab");
                return Gen.Util.CS.Gen.convertToHtmlTableSort(tablas[0].tabla, "sortable1", "droptrue' style='width:" + ancho + ";");
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        public string consultarProveedoresSinNoticia(string ancho, ref List<PEntidades.ArrTablas> tablas)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaProveedoresSinNoticia();
            if (resultado.Count > 1)
            {
                tablas = PEntidades.Utiles.getTablasbyLetras(resultado, "div");
                return Gen.Util.CS.Gen.convertToHtmlTableSort(tablas[0].tabla, "sortable1", "droptrue' style='width:" + ancho + ";");
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        public string consultarProveedoresSinNoticiaPorId(string idGrupoToEdit,string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConProvConNotPorId(idGrupoToEdit);
            if (resultado.Count > 1)
            {

                return Gen.Util.CS.Gen.convertToHtmlTableSort(resultado, "sortable2", "droptrue' style='width:" + ancho + ";");
            }
            else
            {
                //return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
                return "<ul id='sortable2' class='droptrue'>" + "</ul>";
            }
        }

        public string consultarProveedoresConNoticia(string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaProveedoresConNoticia();
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

        
    }
}
