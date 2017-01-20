using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SendEmailCSI.Common
{
    #region Constants class use for get data from app config 
    public class Constants
    {
        public static string Server108DataTest = ConfigurationManager.ConnectionStrings["Server108DataTest"].ToString();
        public static string cache112ConnectionString = ConfigurationManager.ConnectionStrings["Server112ConnectionString"].ToString();
        public static string mailAddress = ConfigurationManager.AppSettings["MailAddress"];
        public static string mailTo = ConfigurationManager.AppSettings["MailTo"];
        public static string mailCC = ConfigurationManager.AppSettings["MailCC"];
        public static string smtpClient = ConfigurationManager.AppSettings["SmtpClient"];
        public static string mailSubject = ConfigurationManager.AppSettings["subject"];
        public static string pathAttachment = ConfigurationManager.AppSettings["pathAttachment"];
    }
    #endregion
}
