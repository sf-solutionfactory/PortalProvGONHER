using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class WebServicesEndpoints
    {
        public WebServicesEndpoints() { }

        public string guardarEndpoints(string idPantalla, string Instancia, string endpoint)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdInsertEndpoints( idPantalla,  Instancia,  endpoint);
        }

        public string actualizaEndpoints(string idPantalla, string Instancia, string endpoint, string endpointAnterior)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdActualizarEndpoints(idPantalla, Instancia, endpoint, endpointAnterior);
        }

        public string consultarWebSPantallaIsntancia()
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdconsultarWebSPantallaIsntancia();
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:90%;", listaEvitar, true, false, false, false, 0, 2);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }

        }

        public List<string[]> consultarTodosEndponits()
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaTodosEndpoints();
            return resultado;
        }

        public List<string[]> consultar(string sqlString)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsulta(sqlString);
            return resultado;
        }

        
        

    }
}
