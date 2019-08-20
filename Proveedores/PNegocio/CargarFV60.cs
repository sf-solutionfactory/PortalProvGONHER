using PEntidades;
using SAP.Middleware.Connector;
using PPersistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio
{
    public class CargarFV60
    {

        public string msg = "";

        public int setFV60cargadasNew(string bukrs, string gjahr, string belnr, string uuid_cfdi, string rfc_emisor,
            string rfc_receptor, decimal wrbtr, string waers, decimal kursf, string bldat, byte[] rawxml, byte[] rawpdf, string fileName)
        {
            int res = 0;
            PPersistencia.SAPConn psc = new PPersistencia.SAPConn();
            List<ParamsCallSAP> list = new List<ParamsCallSAP>();
            ParamsCallSAP pr = new ParamsCallSAP();
            pr = new ParamsCallSAP();                  
            pr.NameVar = "BUKRS";
            pr.ValVar = bukrs;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "GJAHR";
            pr.ValVar = gjahr;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "BELNR";
            pr.ValVar = belnr;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "UUID_CFDI";
            pr.ValVar = uuid_cfdi;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "RFC_EMISOR";
            pr.ValVar = rfc_emisor;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "RFC_RECEPTOR";
            pr.ValVar = rfc_receptor;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "WRBTR";
            pr.ValVar = wrbtr.ToString();
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "WAERS";
            pr.ValVar = waers;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "KURSF";
            pr.ValVar = kursf.ToString();
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "BLDAT";
            pr.ValVar = bldat;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "FILE_NAME";
            pr.ValVar = fileName;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "RAWPDF";
            pr.ValVarb = rawpdf;
            pr.TipVar = "Y";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "RAWXML";
            pr.ValVarb = rawxml;
            pr.TipVar = "Y";
            list.Add(pr);

            List<TablasCallSAP> listT = new List<TablasCallSAP>();  // no lleva Tablas, solo para el llamado
            TablasCallSAP tr = new TablasCallSAP();
            List<ParamsCallSAP> listTb = new List<ParamsCallSAP>();

            string rescon = psc.conSAP2("Z_DFAC_CARGA_FV60", list, listT);

            if (rescon != "" && rescon != null)
            {
                try
                {
                    res = int.Parse(rescon.Trim());
                }
                catch (Exception)
                {
                    msg = rescon.Trim();
                }
            }
            return res;
        }

        public int desvincularFV60(string uuid_cfdi, string bukrs, string gjahr, string belnr)
        {
            int res = 0;
            PPersistencia.SAPConn psc = new PPersistencia.SAPConn();
            List<ParamsCallSAP> list = new List<ParamsCallSAP>();
            ParamsCallSAP pr = new ParamsCallSAP();

            pr = new ParamsCallSAP();
            pr.NameVar = "P_BELNR";
            pr.ValVar = belnr;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "P_GJAHR";
            pr.ValVar = gjahr;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "P_BUKRS";
            pr.ValVar = bukrs;
            pr.TipVar = "S";
            list.Add(pr);

            pr = new ParamsCallSAP();
            pr.NameVar = "P_UUID";
            pr.ValVar = uuid_cfdi;
            pr.TipVar = "S";
            list.Add(pr);

            List<TablasCallSAP> listT = new List<TablasCallSAP>();  // no lleva Tablas, solo para el llamado
            TablasCallSAP tr = new TablasCallSAP();
            List<ParamsCallSAP> listTb = new List<ParamsCallSAP>();

            string rescon = psc.conSAP2("Z_UFAC_FV60_DESADJUNTAR", list, listT);

            if (rescon != "" && rescon != null)
            {
                try
                {
                    int Start, End;     //buscar # de archivos desadjuntados
                    string xCuantos;
                    if (rescon.Contains("P_RETURN=") && rescon.Contains(", IMPORT"))
                    {
                        Start = rescon.IndexOf("P_RETURN=", 0) + "P_RETURN=".Length;
                        End = rescon.IndexOf(", IMPORT", Start);
                        xCuantos= rescon.Substring(Start, End - Start);
                        res = int.Parse(xCuantos.Trim());
                        return res;
                    }
                    else
                    {
                        return 0;
                    }


                }
                catch (Exception)
                {
                    msg = rescon.Trim();
                }
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
