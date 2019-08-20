using PEntidades;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class InstanciaCN
    {
        public InstanciaCN()
        {
        }

        public string guardarInstanciaCN(string xname, string appsh, string xsapr, string sysn, string xuser, string pasw, string cliente)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdInsertInstanciaCN(xname,  appsh, xsapr, sysn,  xuser,  pasw,  cliente);
        }

        public string actualizarInstanciaCN(string xid, string xname, string appsh, string sapr, string sysn, string xuser, string pasw, string cliente, string soc)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdActualizaInstanciaCN(xid, xname, appsh, sapr, sysn, xuser, pasw, cliente, soc);
        }

        public string consultarInstanciaCN()
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaInstanciasCN();
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:90%;", listaEvitar, true, true, false, false, 0, 2);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        public List<string[]> consultarInstanciaCNPorId(string sqlString)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsulta(sqlString);
            return resultado;
        }

        public List<string[]> insertarRFCxCN(string sqlString)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsulta(sqlString);
            return resultado;
        }

        public List<string[]> consultarInstanciaArray()
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdConsultaInstanciasCN();
        }

        public static string RFCxSociedad(string xname, string appsh, string xsapr, string sysn, string xuser, string pasw, string cliente, string sociedad)
        {
            RfcConfigParameters parms = new RfcConfigParameters();
            parms.Add(RfcConfigParameters.Name, xname);
            parms.Add(RfcConfigParameters.AppServerHost, appsh);
            parms.Add(RfcConfigParameters.SAPRouter, xsapr.ToString().Trim());
            parms.Add(RfcConfigParameters.SystemNumber, sysn.ToString().Trim());
            parms.Add(RfcConfigParameters.User, xuser.ToString().Trim());
            parms.Add(RfcConfigParameters.Password, pasw.ToString().Trim());
            parms.Add(RfcConfigParameters.Client, cliente.ToString().Trim());
            parms.Add(RfcConfigParameters.Language, "ES" );
            parms.Add(RfcConfigParameters.PoolSize, "5");
            parms.Add(RfcConfigParameters.PeakConnectionsLimit, "10");
            parms.Add(RfcConfigParameters.PoolIdleTimeout, "600");

            RfcDestination rfcDest = null;
            rfcDest = RfcDestinationManager.GetDestination(parms);
            RfcRepository repo = rfcDest.Repository;    //Crea repositorio para la función
            IRfcFunction conexion = repo.CreateFunction("Z_URFC");

            conexion.SetValue("SOCIEDAD", sociedad);
            conexion.Invoke(rfcDest);                  //Se ejecuta la consulta
            //string xresul = conexion.GetString("RFC");
            return conexion.GetString("RFC");
        }

    }
}
