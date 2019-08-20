using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Eliminar
    {
        public string ejecutarQueryWhitTran(string query)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcQueryWhitTran(query);
        }
    }
}
