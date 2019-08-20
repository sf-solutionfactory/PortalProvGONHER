using PPersistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNegocio
{
    public class Login
    {
        public Login()
        {
        }

        public string[] isUserBDDistinct(string usuario, string contrasena)
        {
            ejecutaProcedures ejecPd = new ejecutaProcedures();
            return ejecPd.ejcPsdVerifcarUsuario(usuario, contrasena);
        }

        public PEntidades.Usuario getUsuario(string idUsuario)
        {
            return new ejecutaProcedures().ejcPsdConsultaUsuario(idUsuario);
        }
    }
}
