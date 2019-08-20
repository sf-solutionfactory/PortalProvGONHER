using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio {
    public class Usuario {
        
        public Usuario() {

        }

        public List<string[]> getSociedadesByUsuario(string idUsuario) {
            return new PPersistencia.ejecutaProcedures().ejcPsdConsultaSociedadesByIdUsuario(idUsuario);
        }

        public List<string[]> getSociedadesByUsuarioCN(string idUsuario)
        {
            return new PPersistencia.ejecutaProcedures().ejcPsdConsultaSociedadesByIdUsuarioCN(idUsuario);
        }

        public int[] getIdPantallasByIdRol(int idRol)
        {
            return new PPersistencia.ejecutaProcedures().ejcPsdConsultaPantallaByIdRol(idRol);
        }
    }
}
