using PEntidades.SrvSAPUProv;
//using PEntidades.SrvSAPProveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class Proveedores
    {
        public Proveedores() { }

        public ZERES_USUARIOS[] ResProvedores(string endpoint, string[] userPass, string extranjero)
        {
            PEntidades.SrvSAPUProv.ZWS_UPROVEEDORESClient srv = new PPersistencia.WebServices().getZWS_UPROVEEDORESInstanceNew(
                    endpoint,
                    userPass
                    );
            srv.Open();
            Z_URES_PROVEEDORES proveedores = new Z_URES_PROVEEDORES();
            proveedores.BUKRS = userPass[2];
            proveedores.EXTRANJERO = extranjero;
            Z_URES_PROVEEDORESResponse resul = null;
            resul = srv.Z_URES_PROVEEDORES(proveedores);
            srv.Close();
            return resul.ZTRES_USUARIOS;
        }

        public ZELISTA_PROVE[] ResProvedoresInactivos(string endpoint, string[] userPass)
        {
            PEntidades.SrvSAPUProv.ZWS_UPROVEEDORESClient srv = new PPersistencia.WebServices().getZWS_UPROVEEDORESInstanceNew(
                    endpoint,
                    userPass
                    );
            srv.Open();
            Z_UFAC_LISTPAResponse resul = null;
            Z_UFAC_LISTPA listpa = new Z_UFAC_LISTPA();                        
            resul = srv.Z_UFAC_LISTPA(listpa);
            //resul = srv.Z_URES_PROVEEDORES(userPass[2], extranjero);
            srv.Close();
            return resul.IT_PROVACTIVOS;
        }

        public void proveedoresInactivos(string endpoint, string[] userPass) { 
                ZELISTA_PROVE[] lista = ResProvedoresInactivos(endpoint,userPass);
        }   

        public string[] insertarProveedores(
            string identificador,
            string nombre,
            string proveedor,
            string idInstancia
            )
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            return ejec.ejcPsdProveedorDetProveedor( identificador,nombre,proveedor,idInstancia);

        }

        public List<string[]> consultarProveedoresPorId(string sqlString)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsulta(sqlString);
            return resultado;
        }
        
        public string insertarUnionProveedores(string[] ids, string nombreGrupo)
        {
            //string res = "";
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            string sqlString =
                "DECLARE @idNuevo int; " +
                "DECLARE @idACambiar int; " +
                "DECLARE @informe char(50); " +
                "BEGIN TRY " +
                    "BEGIN TRAN " +
                        "select @idNuevo = proveedor_idProveedor from detProveedor where RFC = '" + ids[0] + "'; ";
            sqlString += " update proveedor set esCabeceraGrupo = 1, nombreGrupo = '"+nombreGrupo+"' where idProveedor = @idNuevo ";
                         string updates = "";
                         for (int i = 1; i < ids.Length; i++)
                         {
                             updates +=
                                 "select @idACambiar = proveedor_idProveedor from detProveedor where RFC = '" + ids[i] + "'; ";
                             updates +=
                             "update detProveedor set proveedor_idProveedor = @idNuevo where proveedor_idProveedor = @idACambiar; ";
                         }
                string final =  
                "set @informe = 'Actualizado correctamente' " +
                    "COMMIT TRAN " +
                "END TRY " +
                "BEGIN CATCH " +
                    "set @informe = 'ocurrio un error inesperado' " +
                 "ROLLBACK TRAN "+
                "END CATCH " +
                "select @informe as informe ";
                sqlString = sqlString + updates + final;
            return ejec.ejcQuery(sqlString);
            //return res;
        }

        public string proveedoresInactivos(ZELISTA_PROVE[] desactivados)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            string sqlString =

                "DECLARE @informe char(50); " +
                "BEGIN TRY " +
                    "BEGIN TRAN " +
                        "update proveedor set esBloq = 1;"+
                        "update usuario set esBloq = 1;";

            string updates = "";
            for (int i = 1; i < desactivados.Length; i++)
            {
                updates +=
                "update proveedor set esBloq = 0 where idProveedor = (select proveedor_idProveedor from detProveedor where lifnr = '" + desactivados[i].ACREDOR.ToString().Trim() + "');";
                updates +=
                "update usuario set esBloq = 0 where proveedor_idProveedor = (select proveedor_idProveedor from detProveedor where lifnr = '" + desactivados[i].ACREDOR.ToString().Trim() + "');";
            }
            string final =
            "set @informe = 'Actualizado correctamente' " +
                "COMMIT TRAN " +
            "END TRY " +
            "BEGIN CATCH " +
                "set @informe = 'ocurrio un error inesperado' " +
             "ROLLBACK TRAN " +
            "END CATCH " +
            "select @informe as informe ";
            sqlString = sqlString + updates + final;
            return ejec.ejcQuery(sqlString);
        }

        public string consultarGruposDeProveedores(string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaGruposProveedores();
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, false, false, true, true, 0, 1);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }

        public string consultarProveedoresEnGrupo(string ancho)
        {
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdConsultaProveedoresEnGrupo();
            if (resultado.Count > 1)
            {
                List<int> listaEvitar = new List<int>();
                return Gen.Util.CS.Gen.convertToHtmlTableDelete(resultado, "tableToOrder", "tblComun' style='width:" + ancho + ";", listaEvitar, false, false, true, false, 0, 1);
            }
            else
            {
                return "<strong>No se encontraron resultados para mostrar en la tabla</strong>";
            }
        }
    }
        
}

