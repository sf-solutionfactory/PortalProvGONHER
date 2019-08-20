
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPersistencia;
using System.Data.SqlClient;
using System.Security;
using System.Web;


namespace PPersistencia
{
    public class Conexion
    {
        private string direccionBD;
        //private string puertoSrv;
        private string nombreBD;
        private string usuarioSrv;
        private string contrasenaSrv;

        private SqlConnection con;

        public SqlConnection Con
        {
            get { return con; }
            set { con = value; }
        }
        private SqlCommand sqlCommand;

        public SqlCommand SqlCommand
        {
            get { return sqlCommand; }
            set { sqlCommand = value; }
        }

        public Conexion()
        {
            //this.direccionBD = "SF-0007\\SQL12SF007BOND";
            ////this.puertoSrv = "3306";
            //this.nombreBD = "proveedores2";
            ////userSQL
            //this.usuarioSrv = "sa";
            //this.contrasenaSrv = "root";

            //this.direccionBD = "MARTYN-PC\\SQLS12";
            ////this.puertoSrv = "3306";
            //this.nombreBD = "proveedores2";
            ////userSQL
            //this.usuarioSrv = "sa";
            //this.contrasenaSrv = "root";

        }

        static string GetConnectionStrings()
        {
            System.Configuration.ConnectionStringSettingsCollection settings =
                System.Configuration.ConfigurationManager.ConnectionStrings;
            string con = "";
            if (settings != null)
            {
                con = System.Configuration.ConfigurationManager.ConnectionStrings["proveedores2ConnectionString"].ConnectionString;
                //con = settings[1].ConnectionString;
            }
            return con;
        }


        public void abrirConexion()
        {
            
            try
            {

                //string connectionString = "Server=" + this.direccionBD + "; Database=" + this.nombreBD + "; User Id=" + this.usuarioSrv + "; Password=" + this.contrasenaSrv;
                string connectionString = GetConnectionStrings();
                this.con = new SqlConnection();
                this.con.ConnectionString = connectionString;
                this.sqlCommand = con.CreateCommand();
                this.con.Open();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public Boolean testConnection()
        {
            try 
            {
                abrirConexion();
                cerrarConexion();
                return true;
            }
            catch(SqlException)
            {
                return false;
            }
        }

        public void cerrarConexion()
        {
            con.Close();
        }

        public SqlDataReader executeQuery(string sql)
        {
            //con.Close();
            con.Open();
            sqlCommand.CommandText = sql;
            SqlDataReader resul = sqlCommand.ExecuteReader();
            con.Close();
            return resul;
        }

        public void executeUpdate(string sql)
        {
            //con.Close();
            con.Open();
            SqlCommand sqlCommand = con.CreateCommand();
            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }
    }
}