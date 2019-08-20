using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Seguridad
    {
        public string insertarCredInacep(string credencial)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdInsertCredInac(credencial);
        }

        public string cambiarContrasena(string contrasena,string user)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdCambiaContrasena(contrasena, user);
        }

        public string guardarCredencialFallida(string usuario,string pass)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdGuardarCredFall(usuario, pass);
        }

        public static string[] consultarUsuarioCOntrasenaInstancia(string instancia)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            string sqlString = "select usuario, pass, sociedad from instancia where idInstancia = " + instancia;
            List<string[]> res = ejec.ejcPsdConsulta(sqlString);
            string[] array = new string[3];
            array[0] = res[1][0].Trim();
            array[1] = res[1][1].Trim();
            array[2] = res[1][2].Trim();
            return array;  
        }

        public string consultarCredenciales()
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaCredenciales();
            PNegocio.Encript encript = new PNegocio.Encript();
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                for(int i = 1; i < resultado.Count; i++){
                    resultado[i][1] = encript.Desencriptar(encript.Desencriptar(resultado[i][1]));   
                }
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:90%;", listaEvitar, false, false, true, false, 0, 1);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }

        }




    }
}
