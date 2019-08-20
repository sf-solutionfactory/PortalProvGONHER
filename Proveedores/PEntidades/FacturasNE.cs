using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEntidades
{
    public class FacturasNE
    {
        public List<string> CENTRO { get; set; }
        public string FACTURA_HIGH { get; set; }
        public string FACTURA_LOW { get; set; }
        public string F_COMPRA_HIGH { get; set; }
        public string F_COMPRA_LOW { get; set; }
        public string REFERENCIA_HIGH { get; set; }
        public string REFERENCIA_LOW { get; set; }
        public string MONEDA_HIGH { get; set; }
        public string MONEDA_LOW { get; set; }
        public List<string> PROVEEDOR { get; set; }
        public string P_BLOCK { get; set; }
        public string P_ORDPED { get; set; }
        public string P_ORDREF { get; set; }
    }
}
