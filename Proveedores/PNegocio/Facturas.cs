using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNegocio
{
    public class Facturas
    {
        public Facturas()
        {
        }

        public static List<string[]> obtenerListaValidacionesXML(string idproveedor) {
            PPersistencia.ejecutaProcedures instancia = new PPersistencia.ejecutaProcedures();
            return instancia.ejcPsdConsultaListaValidacionesXML(idproveedor, 1);   //mgv, clavado el 1 que es la pantalla de MIR7
        }
    }
}
