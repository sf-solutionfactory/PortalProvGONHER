using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNegocio
{
    public class DatoMaestro
    {
        public DatoMaestro()
        {
        }

        public List<PEntidades.Proveedor> getDatoMaestro(PEntidades.SrvSAPUProv.ZELIFNR_PROV[] lifnrs, string endpointAddres, string[] userPass)
        {
            PEntidades.SrvSAPUProv.ZWS_UPROVEEDORESClient srv = new PPersistencia.WebServices().getZWS_UPROVEEDORESInstanceNew(
                    endpointAddres,
                    userPass
                    );
            
            srv.Open();
            srv.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
            PEntidades.SrvSAPUProv.Z_UDATA_MASTER master = new PEntidades.SrvSAPUProv.Z_UDATA_MASTER();
            master.PROVEEDOR = lifnrs;
            var result = srv.Z_UDATA_MASTER(master); 
            List<PEntidades.Proveedor> listaDatosMaestrosPorsoc= new List<PEntidades.Proveedor>();
            //var result = srv.Z_DATA_MASTER(lifnrs);
            for (int i = 0; i < result.DATOS_MAESTROS.Length ; i++ )
            {
                var resul = result.DATOS_MAESTROS[i];//Devuelve una tabla, entonces se accede a la fila 0.
                PEntidades.Proveedor tmpProveedor = new PEntidades.Proveedor();
                tmpProveedor.Liftnr = resul.LIFNR;
                tmpProveedor.TITLE_MEDI = resul.TITLE_MEDI;
                tmpProveedor.NAME1 = resul.NAME1;
                tmpProveedor.NAME2 = resul.NAME2;
                tmpProveedor.SORT1 = resul.SORT1;
                tmpProveedor.STREET = resul.STREET;
                tmpProveedor.HOUSE_NUM1 = resul.HOUSE_NUM1.ToString();// int
                tmpProveedor.POST_CODE1 = resul.POST_CODE1;
                tmpProveedor.ADDR1_DATA = resul.ADDR1_DATA;
                tmpProveedor.COUNTRY = resul.COUNTRY;
                tmpProveedor.NAMECOUNTRY = resul.NAMECOUNTRY;
                tmpProveedor.REGION = resul.REGION;
                tmpProveedor.NAMEREGION = resul.NAMEREGION;
                tmpProveedor.PO_BOX = resul.PO_BOX;
                tmpProveedor.POST_CODE2 = resul.POST_CODE2;
                tmpProveedor.LANGU = resul.LANGU;
                tmpProveedor.TEL_NUMBER = resul.TEL_NUMBER;
                tmpProveedor.FAX_NUMBER = resul.FAX_NUMBER;
                tmpProveedor.EXTENSION1 = resul.EXTENSION1;
                tmpProveedor.EXTENSION2 = resul.EXTENSION2;
                srv.Close();
                listaDatosMaestrosPorsoc.Add(tmpProveedor);
            }
            return listaDatosMaestrosPorsoc;
        }
    }
}
