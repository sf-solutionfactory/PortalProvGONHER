using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Configuracion
    {
        public Configuracion()
        {

        }

        public void guardarConfiguracion(string estatus, string asunto, string contenido, string email, string contrasena, 
            string formaRegistro, string umbral, string bloqSociedad, string numPassRec, string intervaloTiempoBloq, 
            string maxIntentos, string confPor, string caducidadPass, string identificador, string stringSQLComplemento, 
            string tiempoBloqAdmin, string maxXML)
        {
            PPersistencia.ejecutaProcedures ejecPd = new PPersistencia.ejecutaProcedures();
            ejecPd.ejcPsdUpdateConfig(estatus, asunto, contenido, email, contrasena, formaRegistro, umbral, bloqSociedad, 
                numPassRec, intervaloTiempoBloq, maxIntentos, confPor, caducidadPass, identificador, stringSQLComplemento,
                tiempoBloqAdmin, maxXML);
        }

        public static List<string[]> consultarConfiguracion(string status)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdConsultaConfiguracion(status);
        }

        public static List<string[]> consultarConfiguracionEmail(string tipoConfig)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            string sqlString = "select SMTPAdd,puerto,SSLOpt from email where sufijo = '" + tipoConfig + "';";
            return ejec.ejcPsdConsulta(sqlString);
        }

        public static string backupCOnfig()
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdBackUpConfig();
        }

        

    }
}
