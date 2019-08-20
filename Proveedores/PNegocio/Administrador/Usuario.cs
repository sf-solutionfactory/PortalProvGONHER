using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Usuario
    {
       
        public Usuario()
        {

        }

        public string insertarUsuAdmin(string usuari, string nombre, string apellidos, string pass1)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdInsertUsuAdmin(usuari, nombre, apellidos, pass1);
        }

        public string insertarUsuario(string usuari, string nombre, string apellidos, string pass1, string inicioVigencia, string FinVigencia, string proveedor, string esCambiarPassNext, string creadoPor, string email, string rol, List<string[]> listaSociedades)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            string sqlStringSocieddes = crearStringSQLSociedades(usuari,listaSociedades);

            return ejec.ejcPsdInsertUsuario(usuari, nombre, apellidos, pass1, inicioVigencia, FinVigencia, proveedor, esCambiarPassNext, creadoPor, email, rol, sqlStringSocieddes);
        }

        public string ActualizaUsuario(string usuari, string nombre, string apellidos, string pass1, string inicioVigencia, string FinVigencia, string esCambiarPassNext, string creadoPor, string email, string usuarioACambiar, string rol, List<string[]> listaSociedades)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            string sqlStringSocieddes = crearStringSQLSociedadesUpdate(usuari, listaSociedades);
            return ejec.ejcPsdUpdateUsuario(usuari, nombre, apellidos, pass1, inicioVigencia, FinVigencia, esCambiarPassNext, creadoPor, email, usuarioACambiar, rol, sqlStringSocieddes);
                                       
        }

        public string ActualizaAdmin(string usuari, string nombre, string apellidos, string pass1, string creadoPor, string usuarioACambiar)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdUpdateAdmin(usuari, nombre, apellidos, pass1, creadoPor, usuarioACambiar);

        }

        public List<string[]> consultarProveedoresSQL()
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdConsultaProveedores();
        }

        public List<String[]> cosultarUsuariosPorFiltro(string rfc)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdConsultaUsuariosPorFiltro(rfc);
        }

        public List<String[]> cosultarUsuarioSociedad(string usuario)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdConsultaUsuariosSOciedad(usuario);
        }

        public List<string[]> cosultarSociedadesPorprov(string rfc, string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConSocPorProv(rfc);
            return resultado;
         }

        public string cosultarUsuariosPorFiltroEnString(string rfc,string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaUsuariosPorFiltro(rfc);
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
               return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, true, true, false, false, 0, 0);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
            
        }

        public string cosultaUsuariosAdministrador(string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaUsuariosAdministrador();
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, true, true, false, false, 0, 0);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        private string crearStringSQLSociedades(string usuario,List<string[]> listaSociedades)
        {
            string sqlString = "";
            for(int i = 0; i < listaSociedades.Count; i++ ){
                sqlString += "insert into UsuarioSociedad(usuario_idUsuario, sociedad_bukrs, RFC, lifnr, instancia) values('"+usuario+"',"+listaSociedades[i][1]+",'"+listaSociedades[i][2]+"',"+listaSociedades[i][3]+","+listaSociedades[i][0]+");";
            }
            return sqlString;
        }

        private string crearStringSQLSociedadesUpdate(string usuario, List<string[]> listaSociedades)
        {
            string sqlString = "";
            sqlString = "delete from UsuarioSociedad where usuario_idUsuario = '" + usuario + "'; ";
            //if (listaSociedades.Count > 0)
            //{
                for (int i = 0; i < listaSociedades.Count; i++)
                {
                    //sqlString += "insert into UsuarioSociedad values('" + usuario + "'," + listaSociedades[i][2] + ",'" + listaSociedades[i][1] + "'," + listaSociedades[i][0] + "," + listaSociedades[i][3] + "); ";
                    sqlString += "insert into UsuarioSociedad(usuario_idUsuario, sociedad_bukrs, RFC, lifnr, instancia) values('" + usuario + "'," + listaSociedades[i][1] + ",'" + listaSociedades[i][2] + "'," + listaSociedades[i][3] + "," + listaSociedades[i][0] + ");";

                }   
            //}
            
            return sqlString;
        }
    }
}
