using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEntidades
{
    public class DataConexion
    {
        public DataConexion() { 
            
        }

        public string conexion = "";

        public  string Conexion
        {
            get { return conexion; }
            set { conexion = value; }
        }

        public string usuariSap = "sapuser";

        public string UsuariSap
        {
            get { return usuariSap; }
            set { usuariSap = value; }
        }

        private string contrasenaSap = "password3";

        public string ContrasenaSap
        {
            get { return contrasenaSap; }
            set { contrasenaSap = value; }
        }
        //private string ipConn = "192.168.100.1";
        private string ipConn = "25.111.9.178";

        public string IpConn
        {
            get { return ipConn; }
            set { ipConn = value; }
        }


    }
}
