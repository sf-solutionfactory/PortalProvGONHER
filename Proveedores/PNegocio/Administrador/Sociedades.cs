//using PEntidades.SrvSAPResSociedades;
using PEntidades.SrvSAPUProv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Sociedades
    {
        public ZERES_SOCIEDADES[] ResSociedades(string endpoint, string[] userPass, string extranjero)
        {
            PEntidades.SrvSAPUProv.ZWS_UPROVEEDORESClient srv = new PPersistencia.WebServices().getZWS_UPROVEEDORESInstanceNew(
                    endpoint,
                    userPass
                    );
            srv.Open();
            Z_URES_SOCIEDADESResponse resul = null;
            Z_URES_SOCIEDADES sociedad = new Z_URES_SOCIEDADES();
            sociedad.EXTRANJERO = extranjero;
            sociedad.BUKRS = userPass[2];
            resul = srv.Z_URES_SOCIEDADES(sociedad);
            srv.Close();
            return resul.ZTRES_SOCIEDADES;
        }

        public string[] insertarSociedades(
            string proveedor_idProveedor, string detProveedor_lifnr, string bukrs, string idInstancia
            )
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdEjectSociedad(proveedor_idProveedor, detProveedor_lifnr, bukrs, idInstancia);

        }
    }
}
