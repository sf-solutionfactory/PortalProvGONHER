using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEntidades
{
    public class FacturasXVerificar
    {

        public FacturasXVerificar()
        {
            
        }

        string urlXML = "";

        public string UrlXML
        {
            get { return urlXML; }
            set { urlXML = value; }
        }

        string descripcionErrorSAT = "N/A";

        public string DescripcionErrorSAT
        {
            get { return descripcionErrorSAT; }
            set { descripcionErrorSAT = value; }
        }
        string descripcionErrorSAP = "N/A";

        public string DescripcionErrorSAP
        {
            get { return descripcionErrorSAP; }
            set { descripcionErrorSAP = value; }
        }

        string insidenciaPersonal = "";

        public string InsidenciaPersonal
        {
            get { return insidenciaPersonal; }
            set { insidenciaPersonal = value; }
        }

        string errorMostrar = "";

        public string ErrorMostrar
        {
            get { return errorMostrar; }
            set { errorMostrar = value; }
        }

        string errorCompleto = "";

        public string ErrorCompleto
        {
            get { return errorCompleto; }
            set { errorCompleto = value; }
        }

        public string XBLNR2 { get; set; }
        public string BUDAT { get; set; }
        public string BLDAT { get; set; }
        public string WRBTR { get; set; }
        public string IVA { get; set; }
        public string MWSKZ { get; set; }
        public string RETENCION { get; set; }
        public string WAERS { get; set; }
        public string ELECT { get; set; }
        public string SALDO { get; set; }
        public string IMP_TOTAL { get; set; }
        public string LIGHTS { get; set; }
        public string MATNR { get; set; }
        public string FACT_SAP { get; set; }
        public string FACT_SAT { get; set; }
        public int IDINSTANCIA { get; set; }
        public string TIPOINSTANCIA { get; set; }
        // nuevos datos
        public string TIPO { get; set; }
        public string LIFNR { get; set; }
        public string WERKS { get; set; }
        public string BUKRS { get; set; }
        public string EBELN { get; set; }

        public string descMaterial { get; set; }
        public string RFCCorrespon { get; set; }
        public string RFCSociedad { get; set; }
        public string posicion { get; set; }
        public string tipoLinea { get; set; }
        public string uuid { get; set; }
        public decimal total { get; set; }
        public int cantidadXML { get; set; }
        public string consola { get; set; }

        public string msgVarios { get; set; }

        public bool esPrimerCarga { get; set; }

        public string BWTAR { get; set; }
        public string KSCHL { get; set; }
        public string BELNR { get; set; }
        public string GJAHR { get; set; }



    }
}
