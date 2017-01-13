using SendEmailCSI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailCSI.Common
{
    public class MailSender
    {

        static string[] mailToList = { };
        static string[] mailCCList = { };
        static string mailBody = "";
        static string attachmentsFile = "";

        public static Tuple<string, DateTime> SendMailResult(string subject, string body,
                                     string[] mailTo, string[] mailCC,
                                     string attachmentFile, string mailAddress,
                                     string smtpClient)
        {
            string result = "";
            DateTime sendDate = new DateTime();

            // new mail
            MailMessage mailMsg = new MailMessage();
            // attachment file
            Attachment attachment = new Attachment(attachmentFile);
            // set maill address
            mailMsg.From = new MailAddress(mailAddress);

            // add mail to from array 
            foreach (var mail in mailTo)
            {
                mailMsg.To.Add(mail);
            }

            // add mail cc from array
            foreach (var mail in mailCC)
            {
                mailMsg.CC.Add(mail);
            }

            // set content
            mailMsg.Subject = subject;
            mailMsg.Body += body;
            mailMsg.Attachments.Add(attachment);
            mailMsg.IsBodyHtml = true;

            try
            {
                SmtpClient smtp = new SmtpClient(smtpClient);
                smtp.Port = 25;
                smtp.Send(mailMsg);
                result = "Completed";
                sendDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                result = ex.ToString();
                sendDate = DateTime.Now;
            }

            return Tuple.Create(result, sendDate);
        }

        public static bool SendMailSuccess(string subject, string body,
                                            string[] mailTo, string[] mailCC,
                                            string attachmentFile, string mailAddress,
                                            string smtp, int port)
        {
            var flag = false;

            
            
            var mail = new MailMessage()
            {
                From = new MailAddress(mailAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            };

            // add all mail to
            foreach (var mt in mailTo)
            {
                mail.To.Add(mt);
            }

            // add all mail cc
            foreach (var mcc in mailCC)
            {
                mail.CC.Add(mcc);
            }

            if (!string.IsNullOrEmpty(attachmentFile))
            {
                var attach = new Attachment(attachmentFile);
                // add attachment file
                mail.Attachments.Add(attach);
            }

            try
            {
                using(var smtpClient = new SmtpClient(smtp))
                {
                    smtpClient.Port = port;
                    smtpClient.Send(mail);
                    flag = true;
                };
            }
            catch (Exception ex)
            {
                throw;
            }


            return flag;
        }

        
        public static string[] splitMailToList(string mails)
        {
            string[] result = { };
            if (string.IsNullOrEmpty(mails))
            {
                return result;
            }
            result = mails.Split(new Char[] {
                Convert.ToChar(";"),
                Convert.ToChar(",")
            });

            return result;
        }

        public static string GetAttachmentsFile(string path)
        {

            string file = path + DateTime.Now.ToString("yyyyMMdd") + ".xls";

            return file;
        }
        public static string GetBody(string stringMessage)
        {

            StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("<html><head><title>Samitivej Hospitals - MSU Exchange Cart Report</title>");
            sb.Append("</head><body>");
            sb.Append("<table border=0 width=600 ");
            sb.Append("<tr><td>" + "<font face='tahoma'  color='#0000FF'>Dear </font><font face='tahoma'> All </font></td></tr>");
            sb.Append("<tr><td>" + "&nbsp;" + "</td></tr>");
            sb.Append("<tr><td>" + "<font face='tahoma'  color='#0000FF'> หากได้รับเมลล์นี้ รบกวนช่วยแจ้งกลับด้วยตอนนี้อยู่ในขั้นตอนทดสอบ E-mail . Please Notify New Report has been uploaded to Exchange Cart Report</A>  ,Please Dowload attachments to see Detail </font></td></tr>");
            sb.Append("<tr><td>" + stringMessage + " ");
            sb.Append("</td></tr></table>");
            sb.Append("</body></html>");

            return sb.ToString();
        }

        public static List<SendMail> SendMailListResult(List<SendMail> sendMails)
        {
            var resultList = new List<SendMail>();

            foreach (var sm in sendMails)
            {
                string link = Helper.GetLinkSurvey(sm.NationCode, sm.OldNew);
                Tuple<string, DateTime> send = SendMail(sm.EMail, link);

                var sendMail = new SendMail()
                {
                    Adm = sm.Adm,
                    AdmNo = sm.AdmNo,
                    PapmiNo = sm.PapmiNo,
                    NationCode = sm.NationCode,
                    NationDESC = sm.NationDESC,
                    EMail = sm.EMail,
                    PAADMDischgDate = sm.PAADMDischgDate,
                    SendDate = send.Item2,
                    SendFlag = send.Item1
                };

            }

            return resultList;
        }

        public static Tuple<string, DateTime> SendMail(string send, string link)
        {
            attachmentsFile = MailSender.GetAttachmentsFile(Constants.pathAttachment);
            mailBody = MailSender.GetBody("test");
            mailToList = MailSender.splitMailToList(send);
            //mailCCList = MailSender.splitMailToList(Constants.mailCC);

            Tuple<string, DateTime> result = MailSender.SendMailResult(Constants.mailSubject, mailBody,
                                            mailToList, mailCCList,
                                            attachmentsFile, Constants.mailAddress,
                                            Constants.smtpClient);

            return result;
        }


    }
}
