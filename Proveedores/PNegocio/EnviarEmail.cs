using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace PNegocio
{
    public class EnviarEmail
    {

        public static Boolean SendMail(List<string> emailTo, string conMessage, string asunto, string usuario, string contra)
        {
            try
            {
                string[] datosEmail = null;
                PPersistencia.ejecutaProcedures ejec = new PPersistencia.ejecutaProcedures();
                datosEmail = ejec.ejcPsdConsultaDatosEmail(); 
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                string codehtmlini = "<html><head><title>Bienvenido</title>" +
                "<meta http-equiv=\"Content-Type\" content=\"text/html;charset=iso-8859-1\"></head><body>" +
                "<div align=\"center\">" +
                "<img src=\"http://imageshack.com/a/img924/1765/dQ7ew7.jpg\" />" +
                //"<img src=\"http://sf-solutionfactory.com/images/logo.png\" />" +
                "<p style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; color: #000000; font-size: 16px;" +
                "'line-height: 18px; text-align: center;\">";
                string codehtmlfin = "<p style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; color: #8F8F8F; font-size: 13px;" +
                "padding-top: 30px; line-height: 18px; text-align: center;\">" +
                "Este es un mensaje enviado automaticamente por el sistema SAP <br> Favor de no responder a esta dirección." +
                "</p></div></body></html>";
                string smtp = datosEmail[4].ToString();

                //string[] split = datosEmail[2].Split(new Char[] { '@' });
                //switch(split[1]){
                //    case "hotmail.com":
                //            smtp = "smtp.live.com";
                //        break;
                //        case "yahoo.com":
                //            smtp = "smtp.mail.yahoo.com";
                //        break;
                //        case "gmail.com":
                //        smtp = "smtp.gmail.com";
                //        break;
                //    default:
                //        smtp = datosEmail[4].ToString();
                //        break;
                //}

                //c.correoAsunto, c.correoCuerpo, c.email, c.emailPass, e.SMTPAdd, e.puerto, e.SSLOpt from configuracion as c inner join email as e on c.emailDatos = e.sufijo where idConfig = 'Activo';

                
                SmtpClient SmtpServer = new SmtpClient(smtp);
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress(datosEmail[2], "Portal de proveedores", Encoding.UTF8);
                //Aquí ponemos el asunto del correo
                if (String.IsNullOrEmpty(asunto))
                {
                    mail.Subject = datosEmail[0];                    
                }
                else
                {
                    mail.Subject = asunto;
                }
                //Aquí ponemos el mensaje que incluirá el correo
                if (String.IsNullOrEmpty(conMessage))
                {
                    mail.Body = codehtmlini + datosEmail[1] + "<br> Usuario: " + usuario + "<br> Contraseña: " + contra + " <br> Ingrese a:  " + codehtmlfin;
                }
                else
                {
                    mail.Body = codehtmlini + conMessage + codehtmlfin;
                }
                mail.IsBodyHtml = true;
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                //mail.To.Add("eric.iori_zcr@hotmail.com");
                //mail.To.Add("ljesuscastrog@outlook.com, eric.iori_zcr@hotmail.com");
                string mailToComplete = "";
                for (int i = 0; i < emailTo.Count; i++ )
                {
                    mailToComplete += emailTo[i];
                    if ((emailTo.Count-1) != i)
                    {
                        mailToComplete += ",";
                    }
                }
                mail.To.Add(mailToComplete);
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                //mail.Attachments.Add(new Attachment(@"C:\Documentos\carta.docx"));
                //Configuracion del SMTP
                //SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                SmtpServer.Port = int.Parse(datosEmail[5].ToString()); //Puerto que utiliza Gmail para sus servicios
                //Especificamos las credenciales con las que enviaremos el mail
                PNegocio.Encript encript = new Encript();
                string pass  = encript.Desencriptar(encript.Desencriptar(datosEmail[3].ToString()));
                SmtpServer.Credentials = new System.Net.NetworkCredential(datosEmail[2], pass);
                //int ssl;
                //ssl = int.Parse(datosEmail[6].ToString());
                //if (ssl == 1)
                //{
                //    SmtpServer.EnableSsl = true;
                //}
                //else
                //{
                //    SmtpServer.EnableSsl = false;
                //}
                SmtpServer.EnableSsl = bool.Parse(datosEmail[6]);
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static void sentEmailHot()
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("norberto.rojas@live.com");
            mail.To.Add("north_18rojas@hotmail.com");
            mail.From = new MailAddress("norberto.rojas@live.com");
            mail.Subject = "Mail usando Live";

            string Body = "Hola" + Environment.NewLine +
                          "Este es un mail de prueba..." + Environment.NewLine +
                          "Utilizando live in ASP.NET";
            mail.Body = Body;

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.live.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("tucorreo@live.com", "tucontraseña");
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }
    }

        
}
