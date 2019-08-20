using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEntidades
{
    public class Usuario
    {
        private string idUsuario;
        private string proveedor_idProveedor;
        private string email;
        private string rol_idRol;

        public Usuario()
        {
            this.idUsuario = "";
            this.proveedor_idProveedor = "";
            this.email = "";
            this.rol_idRol = "";
        }

        public string Proveedor_idProveedor
        {
            get { return proveedor_idProveedor; }
            set { proveedor_idProveedor = value; }
        }
        

        public string Rol_idRol
        {
            get { return rol_idRol; }
            set { rol_idRol = value; }
        }
        

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
    }
}
