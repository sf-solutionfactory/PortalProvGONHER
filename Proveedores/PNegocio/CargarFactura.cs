using PEntidades;
using SAP.Middleware.Connector;
using PPersistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio
{
    public class CargarFactura
    {

        public int setFacturascargadasNewConn(string bukrs, string correo, string ebeln, string lifnr, string msjsap, string msgsat, string estatus, string tipo,
            string werks, string xblnr, string fecha_xml, string xmlfile, string endpoint, string[] userPass, byte[] raw, string uuid, decimal total,
            string numeroItem, string BELNR, string BWTAR, string KSCHL, string tipoarchivo, byte[] rawpdf, string pdffile, decimal retencion)
        {
            //PEntidades.SrvSAPUProv.Z_UFAC_CARGADASResponse result;
            PEntidades.SrvSAPUProv.Z_UFAC_CARGADAS cargadas = new PEntidades.SrvSAPUProv.Z_UFAC_CARGADAS();
            int res = 0;
            //PEntidades.SrvSAPUProv.ZWS_UPROVEEDORESClient srv = new PPersistencia.WebServices().getZWS_UPROVEEDORESInstanceNew(endpoint, userPass);

            PPersistencia.SAPConn psc = new PPersistencia.SAPConn();    //cambio del llamado hacia SAP
            List<ParamsCallSAP> listPA = new List<ParamsCallSAP>();
            ParamsCallSAP pr = new ParamsCallSAP();

            pr = new ParamsCallSAP();       //cargadas.BELNR = BELNR;
            pr.NameVar = "BELNR";
            pr.ValVar = BELNR;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.BUKRS = bukrs;
            pr.NameVar = "BUKRS";
            pr.ValVar = bukrs;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.BWTAR = BWTAR;
            pr.NameVar = "BWTAR";
            pr.ValVar = BWTAR;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.CORREO = correo;
            pr.NameVar = "CORREO";
            pr.ValVar = correo;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.DESADJUNTAR = "";
            pr.NameVar = "DESADJUNTAR";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.EBELN = ebeln;
            pr.NameVar = "EBELN";
            pr.ValVar = ebeln;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.EBELP = numeroItem;
            pr.NameVar = "EBELP";
            pr.ValVar = numeroItem;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.FECHA_XML = fecha_xml;
            pr.NameVar = "FECHA_XML";
            pr.ValVar = fecha_xml;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.IMP_RETEN = retencion;
            pr.NameVar = "IMP_RETEN";
            pr.ValVard = retencion;
            pr.TipVar = "D";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.KSCHL = KSCHL;
            pr.NameVar = "KSCHL";
            pr.ValVar = KSCHL;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.LIFNR = lifnr;
            pr.NameVar = "LIFNR";
            pr.ValVar = lifnr;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.MSJ_SAP = msjsap;
            pr.NameVar = "MSJ_SAP";
            pr.ValVar = msjsap;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.MSJ_SAT = msgsat;
            pr.NameVar = "MSJ_SAT";
            pr.ValVar = msgsat;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.PDFFILE = pdffile;
            pr.NameVar = "PDFFILE";
            pr.ValVar = pdffile;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.RAW = raw;
            pr.NameVar = "RAW";
            pr.ValVarb = raw;
            pr.TipVar = "Y";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.RAWPDF = rawpdf;
            pr.NameVar = "RAWPDF";
            pr.ValVarb = rawpdf;
            pr.TipVar = "Y";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.STATUS = estatus;
            pr.NameVar = "STATUS";
            pr.ValVar = estatus;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.TIPO = tipo;
            pr.NameVar = "TIPO";
            pr.ValVar = tipo;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.TIPOARCHIVO = tipoarchivo;
            pr.NameVar = "TIPOARCHIVO";
            pr.ValVar = tipoarchivo;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.UUID_XML = null;
            pr.NameVar = "UUID_XML";
            pr.ValVar = null;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.WERKS = werks;
            pr.NameVar = "WERKS";
            pr.ValVar = werks;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.XBLNR = xblnr;
            pr.NameVar = "XBLNR";
            pr.ValVar = xblnr;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.XMLFILE = xmlfile;
            pr.NameVar = "XMLFILE";
            pr.ValVar = xmlfile;
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.ZCFDI_UUID = uuid;
            pr.NameVar = "ZCFDI_UUID";
            pr.ValVar = uuid;
            pr.TipVar = "S";
            listPA.Add(pr);

            List<TablasCallSAP> listT = new List<TablasCallSAP>();  // no lleva tablas, solo para la llamada
            TablasCallSAP tr = new TablasCallSAP();
            List<ParamsCallSAP> listTb = new List<ParamsCallSAP>();

            string msg;
            msg = psc.conSAP2("Z_DFAC_CARGADAS", listPA, listT);                //result = srv.Z_UFAC_CARGADAS(cargadas);

            if (msg != "" && msg != null)
            {
                try
                {
                    res = int.Parse(msg.Trim());
                }
                catch (Exception)
                {
                    res = 1;
                }
            }
            return res;
        }

            public int setFacturascargadasNew(string bukrs, string correo, string ebeln, string lifnr, string msjsap, string msgsat, string estatus, string tipo,
                        string werks, string xblnr, string fecha_xml, string xmlfile, string endpoint, string[] userPass, byte[] raw, string uuid, decimal total,
                        string numeroItem, string BELNR, string BWTAR, string KSCHL, string tipoarchivo, byte[] rawpdf, string pdffile, decimal retencion)
            {
                PEntidades.SrvSAPUProv.Z_UFAC_CARGADASResponse result;
                PEntidades.SrvSAPUProv.Z_UFAC_CARGADAS cargadas = new PEntidades.SrvSAPUProv.Z_UFAC_CARGADAS();
                int res = 0;
                PEntidades.SrvSAPUProv.ZWS_UPROVEEDORESClient srv = new PPersistencia.WebServices().getZWS_UPROVEEDORESInstanceNew(endpoint, userPass);
                cargadas.BELNR = BELNR;
                cargadas.BUKRS = bukrs;
                cargadas.BWTAR = BWTAR;
                cargadas.CORREO = correo;
                cargadas.DESADJUNTAR = "";
                cargadas.EBELN = ebeln;
                cargadas.EBELP = numeroItem;
                cargadas.FECHA_XML = fecha_xml;
                cargadas.IMP_RETEN = retencion;
                cargadas.KSCHL = KSCHL;
                cargadas.LIFNR = lifnr;
                cargadas.MSJ_SAP = msjsap;
                cargadas.MSJ_SAT = msgsat;
                cargadas.PDFFILE = pdffile;
                cargadas.RAW = raw;
                cargadas.RAWPDF = rawpdf;
                cargadas.STATUS = estatus;
                cargadas.TIPO = tipo;
                cargadas.TIPOARCHIVO = tipoarchivo;
                cargadas.UUID_XML = null;
                cargadas.WERKS = werks;
                cargadas.XBLNR = xblnr;
                cargadas.XMLFILE = xmlfile;
                cargadas.ZCFDI_UUID = uuid;
                srv.Open();
                srv.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                result = srv.Z_UFAC_CARGADAS(cargadas);
                srv.Close();
                if (result.RESULT != "" && result != null)
                {
                    try
                    {
                        res = int.Parse(result.RESULT.Trim());
                    }
                    catch (Exception)
                    {
                        res = 1;
                    }

                }
                return res;
            }

        public int desvincularConn(List<string[]> listaDiferentesInstancias, string[] uuid)
        {
            PEntidades.SrvSAPUProv.ZEDATA_UUID[] objetoUui = PEntidades.Utiles.objetoUuid(uuid);
            PEntidades.SrvSAPUProv.Z_UFAC_CARGADAS cargadas = new PEntidades.SrvSAPUProv.Z_UFAC_CARGADAS();
            //PEntidades.SrvSAPUProv.Z_UFAC_CARGADASResponse result;
            int res = 0;
            PEntidades.SrvSAPUProv.ZWS_UPROVEEDORESClient srv = new PPersistencia.WebServices().getZWS_UPROVEEDORESInstanceNew(
                    listaDiferentesInstancias[0][1].ToString().Trim(),
                    listaDiferentesInstancias[0][4].Split(new Char[] { ',' })
                    );

            PPersistencia.SAPConn psc = new PPersistencia.SAPConn();    //cambio del llamado hacia SAP
            List<ParamsCallSAP> listPA = new List<ParamsCallSAP>();
            ParamsCallSAP pr = new ParamsCallSAP();

            pr = new ParamsCallSAP();       //cargadas.BELNR = "";
            pr.NameVar = "BELNR";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.BUKRS = "";
            pr.NameVar = "BUKRS";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.BWTAR = "";
            pr.NameVar = "BWTAR";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.CORREO = "";
            pr.NameVar = "CORREO";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.DESADJUNTAR = "X";
            pr.NameVar = "DESADJUNTAR";
            pr.ValVar = "X";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       // cargadas.EBELN = "";
            pr.NameVar = "EBELN";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       // cargadas.EBELP = "";
            pr.NameVar = "EBELP";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       // cargadas.FECHA_XML = "";
            pr.NameVar = "FECHA_XML";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.IMP_RETEN = 0;
            pr.NameVar = "IMP_RETEN";
            pr.ValVard = 0;
            pr.TipVar = "D";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.KSCHL = "";
            pr.NameVar = "KSCHL";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.LIFNR = "";
            pr.NameVar = "LIFNR";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);
            
            pr = new ParamsCallSAP();       //cargadas.MSJ_SAP = "";
            pr.NameVar = "MSJ_SAP";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.MSJ_SAT = "";
            pr.NameVar = "MSJ_SAT";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.PDFFILE = "";
            pr.NameVar = "PDFFILE";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.RAW = null;
            pr.NameVar = "RAW";
            pr.ValVarb = null;
            pr.TipVar = "Y";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.RAWPDF = null;
            pr.NameVar = "RAWPDF";
            pr.ValVarb = null;
            pr.TipVar = "Y";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.STATUS = "";
            pr.NameVar = "STATUS";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.TIPO = "";
            pr.NameVar = "TIPO";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.TIPOARCHIVO = "";
            pr.NameVar = "TIPOARCHIVO";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.WERKS = "";
            pr.NameVar = "WERKS";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.XBLNR = "";
            pr.NameVar = "XBLNR";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.XMLFILE = "";
            pr.NameVar = "XMLFILE";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            pr = new ParamsCallSAP();       //cargadas.ZCFDI_UUID = "";
            pr.NameVar = "ZCFDI_UUID";
            pr.ValVar = "";
            pr.TipVar = "S";
            listPA.Add(pr);

            List<TablasCallSAP> listT = new List<TablasCallSAP>();
            TablasCallSAP tr = new TablasCallSAP();
            List<ParamsCallSAP> listTb = new List<ParamsCallSAP>();
            int cont = 0;
            foreach (string uui in uuid)
            {
                pr = new ParamsCallSAP();
                pr.NameVar = "UUID_XML";
                pr.ValVar = uui;        
                pr.TipVar = "S";
                listTb.Add(pr);
                cont++;
            }
            tr.TablaVar = "PROVEEDOR_TB";
            tr.CamposVar = listTb;
            listT.Add(tr);

            srv.Open();
            srv.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);

            string msg;
            msg = psc.conSAP2("Z_DFAC_CARGADAS", listPA,listT);      //result = srv.Z_UFAC_CARGADAS(cargadas);
            srv.Close();
            if (msg != "" && msg != null)
            {
                    res = int.Parse(msg.Trim());
            }
            return res;
        }

        public int desvincular(List<string[]> listaDiferentesInstancias, string[] uuid)
        {
            PEntidades.SrvSAPUProv.ZEDATA_UUID[] objetoUui = PEntidades.Utiles.objetoUuid(uuid);
            PEntidades.SrvSAPUProv.Z_UFAC_CARGADAS cargadas = new PEntidades.SrvSAPUProv.Z_UFAC_CARGADAS();
            PEntidades.SrvSAPUProv.Z_UFAC_CARGADASResponse result;
            int res = 0;
            PEntidades.SrvSAPUProv.ZWS_UPROVEEDORESClient srv = new PPersistencia.WebServices().getZWS_UPROVEEDORESInstanceNew(
                    listaDiferentesInstancias[0][1].ToString().Trim(),
                    listaDiferentesInstancias[0][4].Split(new Char[] { ',' })
                    );
            cargadas.BELNR = "";
            cargadas.BUKRS = "";
            cargadas.BWTAR = "";
            cargadas.CORREO = "";
            cargadas.DESADJUNTAR = "X";
            cargadas.EBELN = "";
            cargadas.EBELP = "";
            cargadas.FECHA_XML = "";
            cargadas.IMP_RETEN = 0;
            cargadas.KSCHL = "";
            cargadas.LIFNR = "";
            cargadas.MSJ_SAP = "";
            cargadas.MSJ_SAT = "";
            cargadas.PDFFILE = "";
            cargadas.RAW = null;
            cargadas.RAWPDF = null;
            cargadas.STATUS = "";
            cargadas.TIPO = "";
            cargadas.TIPOARCHIVO = "";
            cargadas.UUID_XML = objetoUui;
            cargadas.WERKS = "";
            cargadas.XBLNR = "";
            cargadas.XMLFILE = "";
            cargadas.ZCFDI_UUID = "";
            srv.Open();
            srv.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
            result = srv.Z_UFAC_CARGADAS(cargadas);
            srv.Close();
            if (result.RESULT != "" && result != null)
            {
                res = int.Parse(result.RESULT.Trim());
            }
            return res;
        }

        public string getMaxXML()
        {
            ejecutaProcedures ejecPd = new ejecutaProcedures();
            string[] res = ejecPd.ejcPsdMaxXML();
            return res[0];
        }

        public string otener_correo(string sqlstring)
        {
            ejecutaProcedures ejecPd = new ejecutaProcedures();
            string[] res = ejecPd.ejecCon(sqlstring);
            return res[0];
        }
    }
}
