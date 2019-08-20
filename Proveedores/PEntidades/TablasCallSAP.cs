using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEntidades
{
    public class TablasCallSAP
    {
        public TablasCallSAP() { 
        }

        public string tablaVar = "";

        public List<ParamsCallSAP> camposVar;

    public string TablaVar
        {
        get { return tablaVar; }
        set { tablaVar = value; }
         }

        public List<ParamsCallSAP> CamposVar
        {
            get { return camposVar; }
            set { camposVar = value; }
        }


    }
}
