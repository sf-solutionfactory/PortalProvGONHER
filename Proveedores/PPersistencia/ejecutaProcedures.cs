using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PPersistencia;

namespace PPersistencia
{
    public class ejecutaProcedures : Conexion
    {
        public ejecutaProcedures() { 
        
        }

        public void ejcPsdBackConfig()
        {
            abrirConexion();
            string proc = "backConfiguracionGeneral";
            SqlCommand cmd = new SqlCommand(proc, Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public void ejcPsdUpdateConfig(string esActivPortal, string correoAsunto, string correoCuerpo, string email,
            string emailPass, string esCreacionUsuarios, string maxUsuarios, string esBloqSociedad, 
            string numPassRecordar, string intervalTiempo, string maxIntentosFail, string configPor, string caducidadPass,
            string identificador, string strignSQLComplemento, string tiempoBloqAdmin, string maxXML)
        {
            abrirConexion();
            string proc = "configuracionGeneral";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@esActivPortal", esActivPortal);
            SqlCommand.Parameters.AddWithValue("@correoAsunto", correoAsunto);
            SqlCommand.Parameters.AddWithValue("@correoCuerpo", correoCuerpo);
            SqlCommand.Parameters.AddWithValue("@email", email);
            SqlCommand.Parameters.AddWithValue("@emailPass", emailPass);
            SqlCommand.Parameters.AddWithValue("@esCreacionUsuarios", esCreacionUsuarios);
            SqlCommand.Parameters.AddWithValue("@maxUsuarios", maxUsuarios);
            SqlCommand.Parameters.AddWithValue("@esBloqSociedad", esBloqSociedad);
            SqlCommand.Parameters.AddWithValue("@numPassRecordar", numPassRecordar);
            SqlCommand.Parameters.AddWithValue("@intervalTiempo", intervalTiempo);
            SqlCommand.Parameters.AddWithValue("@maxIntentosFail", maxIntentosFail);
            SqlCommand.Parameters.AddWithValue("@configPor", configPor);
            SqlCommand.Parameters.AddWithValue("@caducidadPass", caducidadPass);
            SqlCommand.Parameters.AddWithValue("@identificadorMail", identificador);
            SqlCommand.Parameters.AddWithValue("@stringSQLComplement", strignSQLComplemento);
            SqlCommand.Parameters.AddWithValue("@tiempoBloqAdmin", tiempoBloqAdmin);
            SqlCommand.Parameters.AddWithValue("@maxXML", maxXML);
            
            SqlCommand.ExecuteNonQuery();
            Con.Close();
        }

        public string[] ejcPsdVerifcarUsuario(string usuario, string password)
        {
            abrirConexion();
            string proc = "verifUser";
            //string ret = "";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@usuario", usuario);
            SqlCommand.Parameters.AddWithValue("@pass", password);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string[] res2 = new string[10];
            res2[0] = datareader[0].ToString().Trim();
            res2[1] = datareader[1].ToString().Trim();
            res2[2] = datareader[2].ToString().Trim();
            res2[3] = datareader[3].ToString().Trim();
            res2[4] = datareader[4].ToString().Trim();
            res2[5] = datareader[5].ToString().Trim();
            res2[6] = datareader[6].ToString().Trim();
            res2[7] = datareader[7].ToString().Trim();
            res2[8] = datareader[8].ToString().Trim();
            res2[9] = datareader[9].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res2;

        }

        public string[] ejcPsdMaxXML()
        {
            abrirConexion();
            string proc = "getMaxXML";
            //string ret = "";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string[] res2 = new string[1];
            res2[0] = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res2;

        }

        public string[] ejecCon(string sqlString)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("ejecQuery", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@sqlString", sqlString);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            datRdr.Read();
            string[] res2 = new string[1];
            res2[0] = datRdr[0].ToString().Trim();
            datRdr.Dispose();
            Con.Close();
            return res2;
        }

        public string ejcPsdInsertInstancia(string descripcion,string usuario,string password, string endpoint)
        {
            abrirConexion();
            string proc = "insertInstancia";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@descripcion", descripcion);
            SqlCommand.Parameters.AddWithValue("@nombre", usuario);
            SqlCommand.Parameters.AddWithValue("@password", password);
            SqlCommand.Parameters.AddWithValue("@endpoint", endpoint);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res; 

        }

        public string ejcPsdInsertInstanciaCN(string xname, string appsh, string sapr, string sysn, string xuser, string pasw, string cliente)
        {
            abrirConexion();
            string proc = "insertRfcConfigParams";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@Name", xname);
            SqlCommand.Parameters.AddWithValue("@AppSH", appsh);
            SqlCommand.Parameters.AddWithValue("@SAPr", sapr);
            SqlCommand.Parameters.AddWithValue("@SysN", sysn);
            SqlCommand.Parameters.AddWithValue("@User", xuser);
            SqlCommand.Parameters.AddWithValue("@Passw", pasw);
            SqlCommand.Parameters.AddWithValue("@Client", cliente);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsdInsertCredInac(string credencial)
        {
            abrirConexion();
            string proc = "insertCredInacep";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@passw", credencial);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;

        }

        public string ejcPsdGuardarCredFall(string usuario, string pass)
        {
            abrirConexion();
            string proc = "psdPGuardaCredFall";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@usuario", usuario);
            SqlCommand.Parameters.AddWithValue("@passw", pass);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;

        }

        public string ejcPsdCambiaContrasena(string contrasena, string user )
        {
            abrirConexion();
            string proc = "psdPActualizaContrasena";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@passw", contrasena);
            SqlCommand.Parameters.AddWithValue("@user", user);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;

        }

        public string ejcPsdActualizaInstancia(string id,string descripcion, string usuario, string password, string endpoint)
        {
            abrirConexion();
            string proc = "actualizaInstancia";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@id", id);
            SqlCommand.Parameters.AddWithValue("@descripcion", descripcion);
            SqlCommand.Parameters.AddWithValue("@nombre", usuario);
            SqlCommand.Parameters.AddWithValue("@password", password);
            SqlCommand.Parameters.AddWithValue("@endpoint", endpoint);
            
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsdActualizaInstanciaCN(string id, string xname, string appsh, string sapr, string sysn, string xuser, string pasw, string cliente, string soc)
        {
            abrirConexion();
            string proc = "actualizaRfcConfigParams";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@id", id);
            SqlCommand.Parameters.AddWithValue("@Name", xname);
            SqlCommand.Parameters.AddWithValue("@AppSH", appsh);
            SqlCommand.Parameters.AddWithValue("@SAPr", sapr);
            SqlCommand.Parameters.AddWithValue("@SysN", sysn);
            SqlCommand.Parameters.AddWithValue("@User", xuser);
            SqlCommand.Parameters.AddWithValue("@Passw", pasw);
            SqlCommand.Parameters.AddWithValue("@Client", cliente);
            SqlCommand.Parameters.AddWithValue("@Sociedad", soc);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsdInsertEndpoints(string idPantalla, string Instancia, string endpoint)
        {
            abrirConexion();
            string proc = "insertEndpoints";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@idPantalla", idPantalla);
            SqlCommand.Parameters.AddWithValue("@Instancia", Instancia);
            SqlCommand.Parameters.AddWithValue("@endpoint", endpoint);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsdActualizarEndpoints(string idPantalla, string Instancia, string endpoint, string idInstanciaAnterior)
        {
            abrirConexion();
            string proc = "actualizaEndpoints";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@idPantalla", idPantalla);
            SqlCommand.Parameters.AddWithValue("@Instancia", Instancia);
            SqlCommand.Parameters.AddWithValue("@endpoint", endpoint);
            SqlCommand.Parameters.AddWithValue("@idInstanciaAnterior", idInstanciaAnterior);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcQuery(string sqlString)
        {
            abrirConexion();
            string proc = "ejecQuery";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@sqlString", sqlString);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res = "";
            try
            {
                res = datareader[0].ToString().Trim();
            }
            catch (Exception)
            {

            }
            
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcQueryWhitTran(string sqlString)
        {
            abrirConexion();
            string proc = "ejecQueryTryTran";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@sqlString", sqlString);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsdAsignarRol(string usuario, string rol, string mode)
        {
            abrirConexion();
            string proc = "AsignarRol";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@usuario", usuario);
            SqlCommand.Parameters.AddWithValue("@rol", rol);
            SqlCommand.Parameters.AddWithValue("@mode", mode);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;

        }

        public string ejcPsdInsertUsuAdmin(string usuario, string nombre, string apellidos, string pass1)
        {
            abrirConexion();
            string proc = "insertUAdmin";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@usuario", usuario);
            SqlCommand.Parameters.AddWithValue("@nombre", nombre);
            SqlCommand.Parameters.AddWithValue("@apellidos", apellidos);
            SqlCommand.Parameters.AddWithValue("@password", pass1);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsdInsertUsuario(string usuario, string nombre, string apellidos, string pass1, string inicioVigencia, string FinVigencia, string proveedor, string esCambiarPassNext, string creadoPor, string email, string rol, string sqlStringSocieddes)   
        {
            abrirConexion();
            string proc = "insertUsuario";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@usuario", usuario);
            SqlCommand.Parameters.AddWithValue("@nombre", nombre);
            SqlCommand.Parameters.AddWithValue("@apellidos", apellidos);
            SqlCommand.Parameters.AddWithValue("@password", pass1);
            SqlCommand.Parameters.AddWithValue("@inicioVigencia", inicioVigencia).DbType = DbType.Date;
            SqlCommand.Parameters.AddWithValue("@finVigencia", FinVigencia).DbType = DbType.Date;
            SqlCommand.Parameters.AddWithValue("@esCambiarNext", esCambiarPassNext);
            SqlCommand.Parameters.AddWithValue("@proveedor", proveedor);
            SqlCommand.Parameters.AddWithValue("@creadoPor", creadoPor);
            SqlCommand.Parameters.AddWithValue("@email", email);
            SqlCommand.Parameters.AddWithValue("@rol", rol);
            SqlCommand.Parameters.AddWithValue("@sqlStringSoc", sqlStringSocieddes);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;

        }

        public string ejcPsdUpdateUsuario(string usuario, string nombre, string apellidos, string pass1, string inicioVigencia, string FinVigencia, string esCambiarPassNext, string creadoPor, string email, string usuarioACambiar, string rol, string sqlSociedades)
        {
            abrirConexion();
            string proc = "psdPActualizaUsuario";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@usuario", usuario);
            SqlCommand.Parameters.AddWithValue("@nombre", nombre);
            SqlCommand.Parameters.AddWithValue("@apellidos", apellidos);
            SqlCommand.Parameters.AddWithValue("@password", pass1);
            SqlCommand.Parameters.AddWithValue("@inicioVigencia", inicioVigencia).DbType = DbType.DateTime;
            SqlCommand.Parameters.AddWithValue("@finVigencia", FinVigencia).DbType = DbType.DateTime;
            SqlCommand.Parameters.AddWithValue("@esCambiarNext", esCambiarPassNext);
            SqlCommand.Parameters.AddWithValue("@creadoPor", creadoPor);
            SqlCommand.Parameters.AddWithValue("@email", email);
            SqlCommand.Parameters.AddWithValue("@usuarioACambiar", usuarioACambiar);
            SqlCommand.Parameters.AddWithValue("@rol", rol);
            SqlCommand.Parameters.AddWithValue("@sqlUpdateSoc", sqlSociedades);

            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;

        }

        public string ejcPsdUpdateAdmin(string usuario, string nombre, string apellidos, string pass1, string creadoPor, string usuarioACambiar)
        {
            abrirConexion();
            string proc = "psdPActualizaAdmin";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@usuario", usuario);
            SqlCommand.Parameters.AddWithValue("@nombre", nombre);
            SqlCommand.Parameters.AddWithValue("@apellidos", apellidos);
            SqlCommand.Parameters.AddWithValue("@password", pass1);
            SqlCommand.Parameters.AddWithValue("@creadoPor", creadoPor);
            SqlCommand.Parameters.AddWithValue("@usuarioACambiar", usuarioACambiar);

            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsdInsertRoles(string nombreRol, string esActive, string facturas, string partidas, string pagos, string datosMaestros, string usuarios, string facturasfv60, string esCreacion)
        {
            abrirConexion();
            string proc = "insertRol";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@nombreRol", nombreRol);
            SqlCommand.Parameters.AddWithValue("@esActive", esActive);
            SqlCommand.Parameters.AddWithValue("@facturas", facturas);
            SqlCommand.Parameters.AddWithValue("@partidas", partidas);
            SqlCommand.Parameters.AddWithValue("@pagos", pagos);
            SqlCommand.Parameters.AddWithValue("@datosMaestros", datosMaestros);
            SqlCommand.Parameters.AddWithValue("@usuarios", usuarios);
            SqlCommand.Parameters.AddWithValue("@facturasfv60", facturasfv60);
            SqlCommand.Parameters.AddWithValue("@esCreacion", esCreacion);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res; 
        }

        public string ejcPsdActualizaRol(string nombreRol, string esActive, string facturas, string partidas, string pagos, string datosMaestros, string usuarios, string facturasfv60, string idAnterior, string esCreacion)
        {
            abrirConexion();
            string proc = "actualizaRol";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@nombreRol", nombreRol);
            SqlCommand.Parameters.AddWithValue("@esActive", esActive);
            SqlCommand.Parameters.AddWithValue("@facturas", facturas);
            SqlCommand.Parameters.AddWithValue("@partidas", partidas);
            SqlCommand.Parameters.AddWithValue("@pagos", pagos);
            SqlCommand.Parameters.AddWithValue("@datosMaestros", datosMaestros);
            SqlCommand.Parameters.AddWithValue("@usuarios", usuarios);
            SqlCommand.Parameters.AddWithValue("@facturasfv60", facturasfv60);
            SqlCommand.Parameters.AddWithValue("@idAnterior", idAnterior);
            SqlCommand.Parameters.AddWithValue("@esCreacion", esCreacion);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsdInsertNoticia(string titulo, string body, string fechaInicio, string fechaFin, string URLImagen , int tipoNoticia)
        {
            abrirConexion();
            string proc = "insertNoticia";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@titulo", titulo);
            SqlCommand.Parameters.AddWithValue("@body", body);
            SqlCommand.Parameters.AddWithValue("@fechaInicio", fechaInicio).DbType = DbType.DateTime;
            SqlCommand.Parameters.AddWithValue("@fechaFin", fechaFin).DbType = DbType.DateTime;
            SqlCommand.Parameters.AddWithValue("@URLImagen", URLImagen);
            SqlCommand.Parameters.AddWithValue("@tipoNoticia", tipoNoticia);
           
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsModificarNoticia(string id,string titulo, string body, string fechaInicio, string fechaFin, string URLImagen, int tipoNoticia)
        {
            abrirConexion();
            string proc = "ActualizaNoticia";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@id", id);
            SqlCommand.Parameters.AddWithValue("@titulo", titulo);
            SqlCommand.Parameters.AddWithValue("@body", body);
            SqlCommand.Parameters.AddWithValue("@fechaInicio", fechaInicio).DbType = DbType.DateTime;
            SqlCommand.Parameters.AddWithValue("@fechaFin", fechaFin).DbType = DbType.DateTime;
            SqlCommand.Parameters.AddWithValue("@URLImagen", URLImagen);
            SqlCommand.Parameters.AddWithValue("@tipoNoticia", tipoNoticia);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string ejcPsdInsertUnionProveedores(string RFC1, string RFC2)
        {
            abrirConexion();
            string proc = "insertUnionProveedores";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@RFC1", RFC1);
            SqlCommand.Parameters.AddWithValue("@RFC2", RFC2);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string res;
            res = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }
        
        public string[] ejcPsdProveedorDetProveedor(
            string identificador,
            string nombre,
            string proveedor,
            string idInstancia
            )
        {
            abrirConexion();
            string[] res = new string[1];
            string proc = "insertProveedorDetProveedor";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@RFC", identificador);
            SqlCommand.Parameters.AddWithValue("@nombre", nombre);
            SqlCommand.Parameters.AddWithValue("@lifnr", proveedor);
            SqlCommand.Parameters.AddWithValue("@idInstancia", int.Parse(idInstancia));
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            res[0] = datareader[0].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;

        }

        public string[] ejcPsdEjectSociedad(string proveedor_idProveedor, string detProveedor_lifnr, string bukrs, string idInstancia)
        {
            abrirConexion();
            string proc = "insertSociedad";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@detProveedor_RFC", proveedor_idProveedor);
            SqlCommand.Parameters.AddWithValue("@detProveedor_lifnr", detProveedor_lifnr);
            SqlCommand.Parameters.AddWithValue("@bukrs", bukrs);
            SqlCommand.Parameters.AddWithValue("@idInstancia", idInstancia);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string[] res = new string[1];
             res[0] = datareader[0].ToString();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public List<string[]> ejcPsdConsultaInstancias()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarInstancia", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaInstanciasCN()
        {  //todas las instancias que tenga el RFCConfigParamsSAP
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarInstanciaRfcConfigP", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdconsultarRfcConfigParams()
        {  //solo las conexiones activas = 1 y se trae TODOS los parámetros
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarRfcConfigParams", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaCredenciales()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdPConsultarCredInacep", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdconsultarWebSPantallaIsntancia()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarWebSPantallaIsntancia", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaRolPorProvedor(string proveedor)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarRolesPorProveedor", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@proveedor", proveedor);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaListaValidacionesXML(string proveedor, int pants)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdPConValidsFactByProv", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@idProveedor", proveedor);
            SqlCommand.Parameters.AddWithValue("@idPants", pants);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaConfiguracion(string status)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdProConsultarConfiguracion", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@estatus", status);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public string ejcPsdBackUpConfig()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("backConfiguracionGeneral", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            datRdr.Read();
            string lstDat = datRdr[0].ToString().Trim();
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaEndpoints()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarInstancia", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaNOticia()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdProConsultarNoticia", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaNOticiaNoSelect(string idGrupo)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdProConNotNoSelect", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@idGrupo", idGrupo);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaNOticiaEnGrupo(string idGrupo)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdProConNotiEnGrupo", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@idGrupo", idGrupo);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsulta(string sqlString)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("ejecQuery", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@sqlString", sqlString);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
                List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaTodosEndpoints()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdPConsultarTodosEndpoints", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaRolPorId(string id)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarRolPorId", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public string[] ejcPsdConsultaNOticiaPorIdProveedor(String idProveedor)
        {
            this.abrirConexion();
            string proc = "psdProConsultarNoticiaPorIdProveedor";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@idProveedor", idProveedor);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string[] res = new string[3];
            res[0] = datareader[0].ToString().Trim();
            res[1] = datareader[1].ToString().Trim();
            res[2] = datareader[2].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public string[] ejcPsdConsultaDatosEmail()
        {
            this.abrirConexion();
            string proc = "consultarDatosEmail";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            datareader.Read();
            string[] res = new string[7];
            res[0] = datareader[0].ToString().Trim();
            res[1] = datareader[1].ToString().Trim();
            res[2] = datareader[2].ToString().Trim();
            res[3] = datareader[3].ToString().Trim();
            res[4] = datareader[4].ToString().Trim();
            res[5] = datareader[5].ToString().Trim();
            res[6] = datareader[6].ToString().Trim();
            datareader.Dispose();
            Con.Close();
            return res;
        }

        public List<string[]> ejcPsdConsultaEndpointAddress(string lifnr,string idproveedor)
        {
            this.abrirConexion();
            string proc = "consultarEndpoints";
            SqlCommand = new SqlCommand(proc, Con);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@lifnr", lifnr);
            SqlCommand.Parameters.AddWithValue("@idProveedor", idproveedor);
            SqlDataReader datareader = SqlCommand.ExecuteReader();
            //datareader.Read();
            
            List<string[]> listaendpoints = new List<string[]>();
            int i = 0;
            while(datareader.Read()){
                string[] res = new string[4];
                res[0] = datareader[0].ToString().Trim();//endpoint
                res[1] = datareader[1].ToString().Trim();//descripcion
                res[2] = datareader[2].ToString().Trim();
                res[3] = datareader[3].ToString().Trim();
                listaendpoints.Add(res);
                i++;
            }

            datareader.Dispose();
            Con.Close();
            return listaendpoints;
        }

        public List<string[]> ejcPsdConsultaUsuariosAdministrador()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdProConUsersAdmin", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaUsuariosPorFiltro(string frc)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdProConUsersPorFiltro", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@id", frc);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaUsuariosSOciedad(string usuario)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdPConUsuarioSoc", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@usuario", usuario);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConSocPorProv(string frc)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("conSocPorProv", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.AddWithValue("@id", frc);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaRoles()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarRoles", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaProveedores()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdProConsultarProveedores", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaDetProveedor()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarDetProveedor", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaProveedoresSinNoticia()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarProveedoresSinNoticia", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaProveedoresConNoticia()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarProveedoresConNoticia", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConProvConNotPorId(string idGrupoNoticia)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdPConProvConNotPorId", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            this.SqlCommand.Parameters.AddWithValue("@idGrupoNoticia", idGrupoNoticia);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaGruposNoticia()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarGruposDeNoticia", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaGruposProveedores()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdPConsultarGrupoProv", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaProveedoresEnGrupo()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdPConsultarProvsEnGrupo", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaProvedorPorGrupoNoticia(string idGrupo)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("conProvPorGrNoticia", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            this.SqlCommand.Parameters.AddWithValue("@idGrupo", idGrupo);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaProvedorPorGrupoprov(string idGrupo)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("conProvPorGrProv", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            this.SqlCommand.Parameters.AddWithValue("@idGrupo", idGrupo);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaGrupoNoticiaPorId(string idGrupo)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarGrNoticiaPorId", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            this.SqlCommand.Parameters.AddWithValue("@idGrupo", idGrupo);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaPantallas()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdProConsultarPantalla", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public PEntidades.Usuario ejcPsdConsultaUsuario(string idUsuario)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarUsuarioLogeado", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            this.SqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            datRdr.Read();
            PEntidades.Usuario usr = new PEntidades.Usuario();
            usr.IdUsuario = datRdr["idUsuario"].ToString().Trim();
            usr.Proveedor_idProveedor = datRdr["proveedor_idProveedor"].ToString().Trim();
            usr.Rol_idRol = datRdr["rol_idRol"].ToString().Trim();
            usr.Email = datRdr["email"].ToString().Trim();
            Con.Close();
            return usr;
        }

        /**
         * Autor luis: 
         *  fecha : 09/04/2012
         * Descripción : Este se usa para sacar las sociedades a las que tiene acceso el usuarui.
         */
        public List<string[]> ejcPsdConsultaSociedadesByIdUsuario(string idUsuario)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarSociedadesByUsuario", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            this.SqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        public List<string[]> ejcPsdConsultaSociedadesByIdUsuarioCN(string idUsuario)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarSociedadesByUsrCN", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            this.SqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        /**
         * Autor : Luis Jesús Castro Guerrero
         * Fecha : Viernes 16 de mayo 2014, 04:19 PM.
         * Descripción: Método que ejecuta el procedimiento psdConsultarPantallaPorIdRol, para obtener las pantallas que corresponden al rol que esta asignado al usuario.
         * **/
        public int[] ejcPsdConsultaPantallaByIdRol(int idRol)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("psdConsultarPantallaPorIdRol", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            this.SqlCommand.Parameters.AddWithValue("@idRol", idRol);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            int[] idPantalla = null;
            if (datRdr.HasRows)
            {
                List<int> idPantallas = new List<int>();
                while (datRdr.Read())
                {
                    idPantallas.Add(int.Parse(datRdr["idPantalla"].ToString()));
                }
                idPantalla = new int[idPantallas.Count];
                int i = 0;
                foreach (int item in idPantallas)
                {
                    idPantalla[i] = item;
                    i++;
                }
            }
            Con.Close();
            return idPantalla ?? new int[] { };
        }

        /**
         * Autor : Luis Jesús Castro Guerrero
         * Fecha : Lunes 02 de junio 2014, 11:15 AM.
         * Descripción: Método que ejecuta el procedimiento consultarGrupoValidacionesXML, para obtener los grupos de validaciones con sus respectivas validaciones asignadas al grupo.
         * **/
        public List<string[]> ejcPsdconsultarGrupoValidacionesXML()
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarGrupoValidacionesXML", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            List<string[]> lstDat = Gen.Util.CS.Gen.SQLReaderToListArray(datRdr);
            Con.Close();
            return lstDat;
        }

        /**
         * Autor : Luis Jesús Castro Guerrero
         * Fecha : Lunes 02 de junio 2014, 4:45 PM
         * Descripción: Método que ejecuta el procedimiento consultarValFactByIdGrpVal,para obtener los id de la validaciones que contiene el id del grupo de validaciones
         * **/
        public int[] ejcPsdConsultaValFactByIdGrpVal(int idGrupoValidacion)
        {
            this.abrirConexion();
            this.SqlCommand = new SqlCommand("consultarValFactByIdGrpVal", this.Con);
            this.SqlCommand.CommandType = CommandType.StoredProcedure;
            this.SqlCommand.Parameters.AddWithValue("@idGrupoValidacion", idGrupoValidacion);
            SqlDataReader datRdr = this.SqlCommand.ExecuteReader();
            int[] idValidacion = null;
            if (datRdr.HasRows)
            {
                List<int> idValidaciones = new List<int>();
                while (datRdr.Read())
                {
                    idValidaciones.Add(int.Parse(datRdr["idValidacionFactura"].ToString()));
                }
                idValidacion = new int[idValidaciones.Count];
                int i = 0;
                foreach (int item in idValidaciones)
                {
                    idValidacion[i] = item;
                    i++;
                }
            }
            Con.Close();
            return idValidacion ?? new int[] { };
        }

        /**
         * Autor : Luis Jesús Castro Guerrero
         * Fecha : viernes 06 de junio 2014, 2:10 PM
         * Descripción :  Método que ejecuta el procedimiento almacenado eliminarGrupoValidacion, que se encarga de eliminar el grupo de validación por le id que se recibe como parametro en el método
         * */
        public bool ejcPsdEliminarGrupoValidacion(int idGrupoValidacion)
        {
            try
            {
                abrirConexion();
                SqlCommand = new SqlCommand("eliminarGrupoValidacion", Con);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.AddWithValue("@idGrupoValidacion", idGrupoValidacion);
                SqlCommand.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}