using SendEmailCSI.Context;
using SendEmailCSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SendEmailCSI.Common
{
    #region DataAccess class work with entity framework
    public class DataAccess
    {

        #region Set Variable
        List<SendMail> sendmails = new List<SendMail>();
        static SendMailContext dbcontext = new SendMailContext();
        #endregion

        #region return list sendmail object from sql server : where by month
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
        #endregion

        #region Save list sendmail object to sql server 
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
        #endregion
    }
    #endregion
}
