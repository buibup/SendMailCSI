using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SendEmailCSI.Common
{
    public class Constants
    {
        public static string commandCache112 = ConfigurationManager.ConnectionStrings["Server112ConnectionString"].ToString();
        public static string mailAddress = ConfigurationManager.AppSettings["MailAddress"];
        public static string mailTo = ConfigurationManager.AppSettings["MailTo"];
        public static string mailCC = ConfigurationManager.AppSettings["MailCC"];
        public static string smtpClient = ConfigurationManager.AppSettings["SmtpClient"];
        public static string mailSubject = ConfigurationManager.AppSettings["subject"];
        public static string pathAttachment = ConfigurationManager.AppSettings["pathAttachment"];
    }
}
