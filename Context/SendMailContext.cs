using SendEmailCSI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SendEmailCSI.Context
{
    #region SendMailContext class use for connect with sql server by entity framework
    public class SendMailContext:DbContext
    {
        public SendMailContext():base("DBConnectionString")
        {
        }

        public DbSet<SendMail> SendMails { get; set; }
    }
    #endregion
}
