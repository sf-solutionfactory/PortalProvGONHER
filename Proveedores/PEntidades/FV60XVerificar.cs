using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEntidades
{
    public class FV60XVerificar
    {
    public FV60XVerificar()
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

        public string BUKRS { get; set; }
        public string GJAHR { get; set; }
        public string BELNR { get; set; }
        public string UUID_CFDI { get; set; }
        public string RFC_EMISOR { get; set; }
        public string RFC_RECEPTOR { get; set; }
        public decimal WRBTR { get; set; }
        public decimal WMWST { get; set; }
        public string WAERS { get; set; }
        public decimal KURSF { get; set; }
        public string BLDAT { get; set; }
        public string BUDAT { get; set; }
        public string XBLNR { get; set; }
        public decimal DMBTR { get; set; }

        public string RAWXML { get; set; }
        public string RAWPDF { get; set; }
        public string FILE_NAME { get; set; }

        public string LIFNR { get; set; }
        public string TIPOLINEA { get; set; }
        public int ZCOUNT { get; set; }
        public string INCIDENCIA { get; set; }

        public bool esPrimerCarga { get; set; }
        public string uuid { get; set; }
        public string consola { get; set; }
        public string msgVarios { get; set; }
        public int IDINSTANCIA { get; set; }
        public string TIPOINSTANCIA { get; set; }
    }
}
