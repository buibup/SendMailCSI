using SendEmailCSI.Context;
using SendEmailCSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendEmailCSI.Common
{
    public class DataAccess
    {
        List<SendMail> sendmails = new List<SendMail>();
        static SendMailContext dbcontext = new SendMailContext();
        public static List<SendMail> GetDataByMonth(string month)
        {
            using(var context = new SendMailContext())
            {
                var patients = (from p in context.SendMails
                             where p.SendDate.Month.ToString() == month
                             orderby p.PapmiNo
                             select p).ToList();

                return patients;
            }
        }

        public static void SendMailListToDb(List<SendMail> mailList)
        {
            using(var dbContext = new SendMailContext())
            {
                try
                {
                    foreach(var item in mailList)
                    {
                        dbcontext.SendMails.Add(item);
                        dbcontext.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
