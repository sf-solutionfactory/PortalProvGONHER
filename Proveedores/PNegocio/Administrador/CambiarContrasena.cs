using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class CambiarContrasena
    {
        public CambiarContrasena()
        {

        }

        public string cambiarContrasena(string contrasena, string user)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdCambiaContrasena(contrasena, user);
        }

        public string cosultarUsuariosPorFiltroEnString(string rfc, string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaUsuariosPorFiltro(rfc);
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, true, true, false, false, 0, 0);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
            }
         
        }
}
