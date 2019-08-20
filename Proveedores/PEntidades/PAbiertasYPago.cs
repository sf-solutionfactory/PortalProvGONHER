using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEntidades
{
    public class PAbiertasYPago
    {
        string ZUONR;

        public string ZUONR1
        {
            get { return ZUONR; }
            set { ZUONR = value; }
        }
        string BELNR;

        public string BELNR1
        {
            get { return BELNR; }
            set { BELNR = value; }
        }
        string BLART;

        public string BLART1
        {
            get { return BLART; }
            set { BLART = value; }
        }
        string BLDAT;

        public string BLDAT1
        {
            get { return BLDAT; }
            set { BLDAT = value; }
        }

        float DMSHB;

        public float DMSHB1
        {
            get { return DMSHB; }
            set { DMSHB = value; }
        }
        string HWAER;

        public string HWAER1
        {
            get { return HWAER; }
            set { HWAER = value; }
        }

        private string AUGBL;

        public string AUGBL1
        {
            get { return AUGBL; }
            set { AUGBL = value; }
        }

        public int indice = -1;

        private string estadoDespliegue = "contraido";

        public string EstadoDespliegue
        {
            get { return estadoDespliegue; }
            set { estadoDespliegue = value; }
        }

        public string XBLNR { get; set; }
        public string KONTO { get; set; }
        public string NAME1 { get; set; }
        public string SGTXT { get; set; }
        public string EBELN { get; set; }

        public string F_BASE { get; set; }
        public string F_VENCIM { get; set; }

        public int IDINSTANCIA { get; set; }
        public string TIPOINSTANCIA { get; set; }
    }
}
