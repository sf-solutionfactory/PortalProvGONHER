using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;
using System.Configuration;

namespace PNegocio
{
    public class SAPConnect : IDestinationConfiguration
    {
        public bool ChangeEventsSupported()
        {
            bool variable;
            variable = false;
            // throw new NotImplementedException();
            return variable;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public RfcConfigParameters GetParameters(string cnx)
        {
            throw new NotImplementedException();
        }

        public static RfcConfigParameters GetParameters()
        {
            RfcConfigParameters parms = new RfcConfigParameters();
            // tala SQL               // consultar SQL
            //parms.Add(RfcConfigParameters.Name, "TEST");
            //parms.Add(RfcConfigParameters.AppServerHost, "172.18.81.13");
            //parms.Add(RfcConfigParameters.SAPRouter, "/H/187.188.143.200/H/");
            ////parms.Add(RfcConfigParameters.AppServerHost, "192.168.100.1");
            //parms.Add(RfcConfigParameters.SystemNumber, "00");
            //parms.Add(RfcConfigParameters.User, "ABAP01");
            //parms.Add(RfcConfigParameters.Password, "Gonher2018");
            //parms.Add(RfcConfigParameters.Client, "120");
            //parms.Add(RfcConfigParameters.Language, "ES");
            //parms.Add(RfcConfigParameters.PoolSize, "5");
            //parms.Add(RfcConfigParameters.PeakConnectionsLimit, "10");
            //parms.Add(RfcConfigParameters.PoolIdleTimeout, "600");
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdconsultarRfcConfigParams();
            if (resultado.Count > 1)
            {
                parms.Add(RfcConfigParameters.Name, resultado[0].ToString() );
                parms.Add(RfcConfigParameters.AppServerHost, resultado[1].ToString());
                parms.Add(RfcConfigParameters.SAPRouter, resultado[2].ToString());
                parms.Add(RfcConfigParameters.SystemNumber, resultado[3].ToString());
                parms.Add(RfcConfigParameters.User, resultado[4].ToString());
                parms.Add(RfcConfigParameters.Password, resultado[5].ToString());
                parms.Add(RfcConfigParameters.Client, resultado[6].ToString());
                parms.Add(RfcConfigParameters.Language, resultado[7].ToString());
                parms.Add(RfcConfigParameters.PoolSize, resultado[8].ToString());
                parms.Add(RfcConfigParameters.PeakConnectionsLimit, resultado[9].ToString());
                parms.Add(RfcConfigParameters.PoolIdleTimeout, resultado[10].ToString());
                return parms;
            }
            else
            {
                return parms;
            }
        }

    }
}
