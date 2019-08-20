using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Pantalla
    {
        public Pantalla() { }

        public string consultarPantalla(string ancho)
        {
            
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<int> listaEvitar = new List<int>();
            return Gen.Util.CS.Gen.convertToHtmlTableDelete(ejec.ejcPsdConsultaPantallas(), "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar,false,false,false,false,0,1);
        }
    }
}
