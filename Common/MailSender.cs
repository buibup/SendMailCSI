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
    #region MailSender class use about mail
    public class MailSender
    {
        #region Set variable
        static string[] mailToList = { };
        static string[] mailCCList = { };
        static string mailBody = "";
        static string attachmentsFile = "";
        static string subject = "";
        static string smtp = "";
        static string mailFrom = "";
        #endregion

        #region return tuble(status send mail, datetime send mail)
        public static Tuple<string, DateTime> SendMailResult(string subject, string body,
                                     string[] mailTo, string[] mailCC,
                                     string attachmentFile, string mailAddress,
                                     string smtpClient)
        {
            string result = "";
            DateTime sendDate = new DateTime();

            // new mail
            var mail = new MailMessage()
            {
                From = new MailAddress(mailAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            if (!string.IsNullOrEmpty(attachmentFile))
            {
                // attachment file
                Attachment attachment = new Attachment(attachmentFile);
                mail.Attachments.Add(attachment);
            }

            // add mail to from array 
            foreach (var m in mailTo)
            {
                mail.To.Add(m);
            }

            // add mail cc from array
            foreach (var m in mailCC)
            {
                mail.CC.Add(m);
            }

            try
            {
                SmtpClient smtp = new SmtpClient(smtpClient);
                smtp.Port = 25;
                smtp.Send(mail);
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
        #endregion

        #region return status send mail -> true , false
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
            catch (Exception)
            {
                return flag;
            }

            return flag;
        }
        #endregion

        #region Convert string mail to array mail 
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
        #endregion

        #region return attachments file name
        public static string GetAttachmentsFile(string path)
        {

            string file = path + DateTime.Now.ToString("yyyyMMdd") + ".xls";

            return file;
        }
        #endregion

        #region return result list affter send mail
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
                    SendFlag = send.Item1,
                    Link = link,
                    OldNew = sm.OldNew
                };

                resultList.Add(sendMail);
            }

            return resultList;
        }
        #endregion

        #region return tuple(status sendmail, datatime sendmail)
        public static Tuple<string, DateTime> SendMail(string send, string link)
        {
            Tuple<string, DateTime> result;
            try
            {
                subject = ConfigurationManager.AppSettings["subject"];
                attachmentsFile = (string.IsNullOrEmpty(Constants.pathAttachment)) ? attachmentsFile : Constants.pathAttachment;
                mailBody = ConfigurationManager.AppSettings["Message"].Replace(System.Environment.NewLine,"<br />");
                mailBody = mailBody.Replace("#[Link]", link);
                mailToList = MailSender.splitMailToList(send);
                mailFrom = Constants.mailAddress;
                smtp = Constants.smtpClient;

                //mailCCList = MailSender.splitMailToList(Constants.mailCC);

                result = MailSender.SendMailResult(subject, mailBody,
                                                mailToList, mailCCList,
                                                attachmentsFile, mailFrom,
                                                smtp);

                return result;
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.ToString(), DateTime.Now);
            }
        }
        #endregion

    }
    #endregion
}
