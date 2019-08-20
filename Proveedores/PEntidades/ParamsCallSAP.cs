using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEntidades
{
    public class ParamsCallSAP
    {
        public ParamsCallSAP() { }
        string nameVar = "";
        string tipVar = "S";   //tipo de parametro  S-tring  B-oolean Y-byte  D-ecimal
        string valVar = "";
        byte[] valVarb = null;
        Decimal valVard = 0;
        Boolean valBool = false;

        public string NameVar
        {
            get { return nameVar; }
            set { nameVar = value; }
        }

        public string TipVar
        {
            get { return tipVar; }
            set { tipVar = value; }
        }

        public string ValVar
        {
            get { return valVar; }
            set { valVar = value; }
        }

        public byte[] ValVarb
        {
            get { return valVarb; }
            set { valVarb = value; }
        }

        public Decimal ValVard
        {
            get { return valVard; }
            set { valVard = value; }
        }

        public Boolean ValBool
        {
            get { return valBool; }
            set { valBool = value; }
        }
        //public void SetValVar(string val)
        //{
        //    valVar = val;
        //    valVarb = null;
        //}
        //public string GetValVar()
        //{
        //    return valVar;
        //}  
    }
}
