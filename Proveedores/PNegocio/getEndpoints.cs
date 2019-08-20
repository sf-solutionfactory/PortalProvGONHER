using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio
{
    public class getEndpoints
    {
        public getEndpoints(){}

        public List<string[]> consultarEndpoints(string lifnr, string idproveedor)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdConsultaEndpointAddress( lifnr, idproveedor);
        }

    }
}
