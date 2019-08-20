using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using PEntidades;
using PEntidades.SrvSAPUProv;

namespace PPersistencia
{
    public class WebServices
    {
        //private string USUARIO_SAP="";
        //private string CONTRASENA_SAP="";
        //private string ipConn = "";

        ////Declaracion de constantes
        //private void getUsuario(){
        //    PEntidades.DataConexion dats = new DataConexion();
        //    USUARIO_SAP = dats.UsuariSap;
        //}

        //private void getPass()
        //{
        //    PEntidades.DataConexion dats = new DataConexion();
        //    CONTRASENA_SAP = dats.ContrasenaSap;
        //}

        //public void getConn()
        //{
        //    string ipcargada = "";
        //    IniFile ini = new IniFile("D:test\\configuracionH.ini");
        //    ipcargada = ini.IniReadValue("Info", "ip");
        //    if (ipcargada == "")
        //    {
        //        PEntidades.DataConexion dats = new DataConexion();
        //        ipConn = dats.IpConn;
        //    }
        //    else
        //    {
        //        ipConn = ipcargada;
        //    }

        //}
       
        //public WebServices()
        //{
        //    getUsuario();
        //    getPass();
        //    getConn();
        //}

        public ZWS_UPROVEEDORESClient getZWS_UPROVEEDORESInstanceNew(string endpoint, string[] userPass)
        {
            //Crea objeto de la clase proxy del servicio
            ZWS_UPROVEEDORESClient ws;

            BasicHttpBinding servicesRegistryBinding =
            new BasicHttpBinding("ZWS_UPROVEEDORES_soap12");
            //new BasicHttpBinding(BasicHttpSecurityMode );
            //ZWS_P_PROVEEDORESSoapBinding

            servicesRegistryBinding.MaxReceivedMessageSize = 65536 * 100;
            //servicesRegistryBinding.
            


            //Configuraciones de seguridad
            servicesRegistryBinding.Security.Mode =
              BasicHttpSecurityMode.TransportCredentialOnly;
            servicesRegistryBinding.Security.Transport.ClientCredentialType =
              HttpClientCredentialType.Basic;

            //Definicion de la dirección del EndPoint
            EndpointAddress servicesRegistryEndpointAddress =
                
            //new EndpointAddress("http://" + "25.111.9.178" + ":8004/sap/bc/srt/rfc/sap/ZWS_UPROVEEDORES?sap-client=800&wsdl=1.1");
            new EndpointAddress(endpoint);
            
            //Crea la instancia de la clase proxy para el servicio
            ws = new ZWS_UPROVEEDORESClient(servicesRegistryBinding, servicesRegistryEndpointAddress);

            PNegocio.Encript encriptar = new PNegocio.Encript();
            //Configuración de usuario y contraseña
            ws.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            ws.ClientCredentials.HttpDigest.ClientCredential.UserName = userPass[0];
            string desencrypt = encriptar.Desencriptar(encriptar.Desencriptar(userPass[1]));
            ws.ClientCredentials.HttpDigest.ClientCredential.Password = desencrypt;
            
            ws.ClientCredentials.UserName.UserName = userPass[0];
            ws.ClientCredentials.UserName.Password = desencrypt;

            //client.ClientCredentials.UserName.UserName = strUserName;
            //client.ClientCredentials.UserName.Password = strPassword;

            //ReportPortClient client = new ReportPortClient("Report");
            ws.Endpoint.Binding.OpenTimeout = new TimeSpan(0, 3, 0);
            ws.Endpoint.Binding.CloseTimeout = new TimeSpan(0, 3, 0);
            ws.Endpoint.Binding.SendTimeout = new TimeSpan(0, 3, 0);
            ws.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 15, 0);

            //ws.Endpoint.Binding.

            //Retorno de la instancia ZW_LOGClient
            return ws;
        }

        private string getUsuarioPorInstancia(string idInstancia) {
            PEntidades.DataConexion dats = new DataConexion();
            string USUARIO_SAP = dats.UsuariSap;
            return USUARIO_SAP;
        }
        
    }
}
