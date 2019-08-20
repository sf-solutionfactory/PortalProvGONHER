using PEntidades;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNegocio
{
    public class FactFV60
    {
        public string msg = "";

        public FactFV60()
        {
        }

        public static List<string[]> obtenerListaValidacionesXML(string idproveedor)
        {
            PPersistencia.ejecutaProcedures instancia = new PPersistencia.ejecutaProcedures();
            return instancia.ejcPsdConsultaListaValidacionesXML(idproveedor, 32);   //mgv, clavado 32 que es la pantalla de FV60
        }

        public List<PEntidades.FV60XVerificar> exec_connSAP(string xprov, string xxblrn, string xbladti, string xbladtf)
        //public List<PEntidades.FV60> exec_connSAP(List<string> listaProveed, string xxblrn, string xbladti, string xbladtf)
        {
            PPersistencia.SAPConn psc = new PPersistencia.SAPConn();
            List<ParamsCallSAP> list = new List<ParamsCallSAP>();  //tipo de parametro  S-tring  B-oolean Y-byte  D-ecimal
            ParamsCallSAP pr = new ParamsCallSAP();
            pr = new ParamsCallSAP();
            pr.NameVar = "XBLNR";
            pr.ValVar = xxblrn;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "BLDATI";
            pr.ValVar = xbladti;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "BLDATF";
            pr.ValVar = xbladtf;                      
            pr.TipVar = "S";
            list.Add(pr);

            List<TablasCallSAP> listT = new List<TablasCallSAP>();
            TablasCallSAP tr = new TablasCallSAP();
            
            List<ParamsCallSAP> listTb = new List<ParamsCallSAP>();
            pr = new ParamsCallSAP();
            pr.NameVar = "LIFNR";
            pr.ValVar =  xprov;          //"1000082";  "1000072" 1000037;  algunos acreedores para FV60 - gonher
            pr.TipVar = "S";
            listTb.Add(pr);

            tr.TablaVar = "PROVEEDOR_TB";
            tr.CamposVar = listTb;
            listT.Add(tr);

            IRfcFunction resCon = psc.conSAP("Z_DFAC_FV60_LIST", list, listT);
            List<PEntidades.FV60XVerificar> listF = new List<PEntidades.FV60XVerificar>();

            PEntidades.FV60XVerificar objFV60;
            IRfcTable tb = resCon.GetTable(0);
            if (tb.Count == 0)
            {
                msg = "No se encontraron resultados";
            }

            for (int o = 0; o < tb.Count; o++)
            {
                tb.CurrentIndex = o;
                objFV60 = new PEntidades.FV60XVerificar();
                objFV60.BUKRS = tb.CurrentRow.GetString("BUKRS");
                objFV60.BELNR = tb.CurrentRow.GetString("BELNR");
                objFV60.GJAHR = tb.CurrentRow.GetString("GJAHR");
                objFV60.BLDAT = tb.CurrentRow.GetString("BLDAT");
                objFV60.BUDAT = tb.CurrentRow.GetString("BUDAT");
                objFV60.XBLNR = tb.CurrentRow.GetString("XBLNR");
                objFV60.WAERS = tb.CurrentRow.GetString("WAERS");
                objFV60.KURSF = decimal.Parse(tb.CurrentRow.GetString("KURSF"));
                objFV60.LIFNR = tb.CurrentRow.GetString("LIFNR");
                objFV60.DMBTR = decimal.Parse(tb.CurrentRow.GetString("DMBTR"));
                objFV60.WRBTR = decimal.Parse(tb.CurrentRow.GetString("WRBTR"));
                objFV60.WMWST = decimal.Parse(tb.CurrentRow.GetString("WMWST"));
                objFV60.TIPOLINEA = tb.CurrentRow.GetString("TIPOLINEA");
                objFV60.ZCOUNT = tb.CurrentRow.GetInt("ZCOUNT"); 
                objFV60.INCIDENCIA = tb.CurrentRow.GetString("INCIDENCIA");
                //objFV60.InsidenciaPersonal = tb.CurrentRow.GetString("InsidenciaPersonal");
                //objFV60.DescripcionErrorSAP = tb.CurrentRow.GetString("DescripcionErrorSAP");
                //objFV60.DescripcionErrorSAT = tb.CurrentRow.GetString("DescripcionErrorSAT");

                if (tb.CurrentRow.GetString("MSG_VARIOS") == null)
                {
                    objFV60.msgVarios = "";
                }
                else
                {
                    objFV60.msgVarios = tb.CurrentRow.GetString("MSG_VARIOS");
                }
                objFV60.esPrimerCarga = false;

                listF.Add(objFV60);
            }
            return listF;
        }
    }
}
