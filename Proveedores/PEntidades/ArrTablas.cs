using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEntidades
{
    public class ArrTablas
    {
        public List<string[]> tabla;
        public string letra;
        
        public ArrTablas(List<string []> _tabla, string _letra)
        {
            this.tabla = _tabla;
            this.letra = _letra;
        }
    }
    public class ArrTabCorr
    {
        public string correo;
        public string acreedor;

        public ArrTabCorr(string _correo, string _acreedor)
        {
            this.correo = _correo;
            this.acreedor = _acreedor;
        }
    }
}
