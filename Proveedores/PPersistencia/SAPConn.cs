using PEntidades;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PPersistencia
{
    public class SAPConn
    {
        public string mensaje = "";
        IRfcFunction IRfcFunction;

        public SAPConn()
        {
            this.mensaje = "";
        }

        public IRfcFunction conSAP(string funcionCall, List<ParamsCallSAP> listParam, List<TablasCallSAP> listTabls)
        {
            this.mensaje = "";

            try                                            //Establece conexion con SAP
            {
                RfcConfigParameters rfc = GetParameters();  //a la configuracion se le pasan los parametros de conexion
                RfcDestination rfcDest = null;
                rfcDest = RfcDestinationManager.GetDestination(rfc);
                RfcRepository repo = rfcDest.Repository;    //Crea repositorio para la función
                IRfcFunction conexion = repo.CreateFunction(funcionCall);

                for (int i = 0; i < listParam.Count; i++)   //save_settings.SetValue("BUKRS", "");
                {      
                    switch (listParam[i].TipVar)           //S-tring B-oolean Y-byte D-ecimal
                    {
                        case "S":   
                            conexion.SetValue(listParam[i].NameVar, listParam[i].ValVar);
                            break;
                        case "B":
                            conexion.SetValue(listParam[i].NameVar, listParam[i].ValBool);
                            break;
                        case "Y":
                            conexion.SetValue(listParam[i].NameVar, listParam[i].ValVarb);
                            break;
                        case "D":
                            conexion.SetValue(listParam[i].NameVar, listParam[i].ValVard);
                            break;
                        //default:
                        //    ;
                        //    break;
                    }
                }

                for (int u = 0; u < listTabls.Count; u++)
                {
                    IRfcTable Tabls = conexion.GetTable(listTabls[u].tablaVar);  //IRfcTable PROVEEDOR_TB = conexion.GetTable("PROVEEDOR_TB");
                    for (int z = 0; z < listTabls[u].CamposVar.Count; z++)
                    {
                        Tabls.Append();                                     //PROVEEDOR_TB.Append();
                        switch (listTabls[u].CamposVar[z].TipVar)           //S-tring B-oolean Y-byte D-ecimal
                        {
                            case "S":
                                Tabls.SetValue(listTabls[u].CamposVar[z].NameVar, listTabls[u].CamposVar[z].ValVar);      //PROVEEDOR_TB.SetValue("LIFNR", "1000037");
                                break;
                            case "B":
                                Tabls.SetValue(listTabls[u].CamposVar[z].NameVar, listTabls[u].CamposVar[z].ValBool);
                                break;
                            case "Y":
                                Tabls.SetValue(listTabls[u].CamposVar[z].NameVar, listTabls[u].CamposVar[z].ValVarb);
                                break;
                            case "D":
                                Tabls.SetValue(listTabls[u].CamposVar[z].NameVar, listTabls[u].CamposVar[z].ValVard);
                                break;
                       }
                    }
                }

                conexion.Invoke(rfcDest);               //Se ejecuta la consulta
                return conexion;
            }
            catch (RfcCommunicationException xe)
            {
                mensaje = xe.ToString();
                return IRfcFunction;
            }
            catch (RfcLogonException xe)         // user could not logon...
            {
                mensaje = xe.ToString();
                return IRfcFunction;
            }
            catch (RfcAbapRuntimeException xe)    // serious problem on ABAP system side..
            {
                mensaje = xe.ToString();
                return IRfcFunction;
            }
            catch (RfcAbapBaseException xe)      // The function module returned an ABAP exception, an ABAP message or an ABAP class-based exception...
            {
                mensaje = xe.ToString();
                return IRfcFunction;
            }
            catch (Exception xe)
            {
                mensaje = xe.ToString();
                return IRfcFunction;
            }
        }

        public string conSAP2(string funcionCall, List<ParamsCallSAP> listParam, List<TablasCallSAP> listTabls)
        {
            this.mensaje = "";

            try                                            //Establece conexion con SAP
            {
                RfcConfigParameters rfc = GetParameters();  //a la configuracion se le pasan los parametros de conexion
                RfcDestination rfcDest = null;
                rfcDest = RfcDestinationManager.GetDestination(rfc);
                RfcRepository repo = rfcDest.Repository;    //Crea repositorio para la función
                IRfcFunction conexion = repo.CreateFunction(funcionCall);

                for (int i = 0; i < listParam.Count; i++)   //save_settings.SetValue("BUKRS", "");
                {
                    switch (listParam[i].TipVar)           //S-tring B-oolean Y-byte D-ecimal
                    {
                        case "S":
                            conexion.SetValue(listParam[i].NameVar, listParam[i].ValVar);
                            break;
                        case "B":
                            conexion.SetValue(listParam[i].NameVar, listParam[i].ValBool);
                            break;
                        case "Y":
                            conexion.SetValue(listParam[i].NameVar, listParam[i].ValVarb);
                            break;
                        case "D":
                            conexion.SetValue(listParam[i].NameVar, listParam[i].ValVard);
                            break;
                            //default:
                            //    ;
                            //    break;
                    }
                }

                for (int u = 0; u < listTabls.Count; u++)
                {
                    IRfcTable Tabls = conexion.GetTable(listTabls[u].tablaVar);  //IRfcTable PROVEEDOR_TB = conexion.GetTable("PROVEEDOR_TB");
                    for (int z = 0; z < listTabls[u].CamposVar.Count; z++)
                    {
                        Tabls.Append();                                     //PROVEEDOR_TB.Append();
                        switch (listTabls[u].CamposVar[z].TipVar)           //S-tring B-oolean Y-byte D-ecimal
                        {
                            case "S":
                                Tabls.SetValue(listTabls[u].CamposVar[z].NameVar, listTabls[u].CamposVar[z].ValVar);      //PROVEEDOR_TB.SetValue("LIFNR", "1000037");
                                break;
                            case "B":
                                Tabls.SetValue(listTabls[u].CamposVar[z].NameVar, listTabls[u].CamposVar[z].ValBool);
                                break;
                            case "Y":
                                Tabls.SetValue(listTabls[u].CamposVar[z].NameVar, listTabls[u].CamposVar[z].ValVarb);
                                break;
                            case "D":
                                Tabls.SetValue(listTabls[u].CamposVar[z].NameVar, listTabls[u].CamposVar[z].ValVard);
                                break;
                        }
                    }
                }
                conexion.Invoke(rfcDest);                  //Se ejecuta la consulta
                return conexion.ToString();
            }
            catch (RfcCommunicationException xe)
            {
                mensaje = xe.ToString();
                //return IRfcFunction;
                return mensaje;
            }
            catch (RfcLogonException xe)         // user could not logon...
            {
                mensaje = xe.ToString();
                return mensaje;
            }
            catch (RfcAbapRuntimeException xe)    // serious problem on ABAP system side..
            {
                mensaje = xe.ToString();
                return mensaje;
            }
            catch (RfcAbapBaseException xe)      // The function module returned an ABAP exception, an ABAP message or an ABAP class-based exception...
            {
                mensaje = xe.ToString();
                return mensaje;
            }
            catch (Exception xe)
            {
                mensaje = xe.ToString();
                return mensaje;
            }
        }

        public static RfcConfigParameters GetParameters()
        {
            RfcConfigParameters parms = new RfcConfigParameters();
            PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
            List<string[]> resultado = ejec.ejcPsdconsultarRfcConfigParams();
            if (resultado.Count > 1)
            {
                parms.Add(RfcConfigParameters.Name, resultado[1][1].ToString().Trim());
                parms.Add(RfcConfigParameters.AppServerHost, resultado[1][2].ToString().Trim());
                parms.Add(RfcConfigParameters.SAPRouter, resultado[1][3].ToString().Trim());
                parms.Add(RfcConfigParameters.SystemNumber, resultado[1][4].ToString().Trim());
                parms.Add(RfcConfigParameters.User, resultado[1][5].ToString().Trim());
                parms.Add(RfcConfigParameters.Password, resultado[1][6].ToString().Trim());
                parms.Add(RfcConfigParameters.Client, resultado[1][7].ToString().Trim());
                parms.Add(RfcConfigParameters.Language, resultado[1][8].ToString().Trim());
                parms.Add(RfcConfigParameters.PoolSize, resultado[1][9].ToString().Trim());
                parms.Add(RfcConfigParameters.PeakConnectionsLimit, resultado[1][10].ToString().Trim());
                parms.Add(RfcConfigParameters.PoolIdleTimeout, resultado[1][11].ToString().Trim());
                return parms;
            }
            else
            {
                return parms;
            }
        }
    }
}
